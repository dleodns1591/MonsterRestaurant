using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GraphicRaycaster gr;
    private PointerEventData ped;
    private GameObject canvas;


    [SerializeField] private Image mainMaterialImage;
    private Sprite[] styleImage;

    private Vector2[] mainMaterialImageSize =
    {
        new Vector2(300, 315), // 빵 사이즈
        new Vector2(340, 235), // 고기 사이즈
        new Vector2(324, 295) // 밥, 면 사이즈
    };

    public ECookingStyle style = ECookingStyle.None;
    public EMainMatarials mainMaterial;
    public List<SubMaterialImages> subMaterials = new List<SubMaterialImages>();

    [SerializeField] private Image subM;

    public bool isCooking;
    public bool isFinish;
    private bool isMainMaterialDrop;
    [SerializeField] private bool isEnterImage;

    private Image myCook;
    private Vector2 machinePos = new Vector2(631, 192);

    private bool isEnterTrash;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        myCook = this.GetComponent<Image>();
    }
    public void ImageProcessing()
    {
        mainMaterialImage.sprite = styleImage[((int)style)];

        foreach (var item in subMaterials)
        {
            item.ImageProcessing(style);
        }
    }



    public void DropMainMaterial(EMainMatarials main, Sprite sprite, Sprite[] styleSprites)
    {
        float mainPrice = Cooking.Instance.MainMaterialsPriece[((int)main)];


        if (isMainMaterialDrop == true) return;
        if (GameManager.Instance.BuyCheck(mainPrice) == false) return;

        isMainMaterialDrop = true;
        mainMaterial = main;

        mainMaterialImage.sprite = sprite;
        styleImage = styleSprites;

        Vector2 imageSize;
        switch (main)
        {
            case EMainMatarials.Bread:
                imageSize = mainMaterialImageSize[0];
                break;
            case EMainMatarials.Meat:
                imageSize = mainMaterialImageSize[1];
                break;
            default:
                imageSize = mainMaterialImageSize[2];
                break;
        }
        mainMaterialImage.rectTransform.sizeDelta = imageSize;
        mainMaterialImage.color = new Vector4(1, 1, 1, 1);

        // 소리 
        SoundManager.instance.PlaySoundClip("MainIngredient_SFX", SoundType.SFX, 10);
    }

    private IEnumerator BoardMove()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonUp(0))
            {
                break;
            }
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            myCook.rectTransform.position = new Vector3(mousePos.x, mousePos.y, 0);

            //쓰레기 위치 체크
            if ((mousePos.x > -2.5f && mousePos.x < 2.5f) && mousePos.y < -4.5f)
                isEnterTrash = true;
            else isEnterTrash = false;


            if (isEnterTrash == true) Cooking.Instance.trash.Enter();
            else Cooking.Instance.trash.Exit();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (isFinish == false)
        {
            if (isCooking == true) return;

            float price = Cooking.Instance.materialPrice;

            if (isMainMaterialDrop == false || Cooking.Instance.myType == ESubMatarials.NULL) return;
            if (GameManager.Instance.BuyCheck(price) == false) return;


            GameObject sub = Instantiate(subM, transform).gameObject;
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(eventData.position);
            sub.transform.position = inputPos;
            sub.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            sub.GetComponent<Image>().raycastTarget = false;

            // 소리
            SoundManager.instance.PlaySoundClip("Sub-Materials_SFX", SoundType.SFX);

            subMaterials.Add(sub.GetComponent<SubMaterialImages>());
        }
        else
        {
            transform.parent = Cooking.Instance.foodPool;
            StartCoroutine(BoardMove());
        }
    }

    public void CookingComplete()
    {
        isFinish = true;
        ImageProcessing();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (isFinish == false) return;

        myCook.transform.parent = Cooking.Instance.cookingMachine.transform;
        myCook.rectTransform.localPosition = new Vector2(605.9997f, -11.50008f);


        if (isEnterTrash == true)
        {
            Cooking.Instance.trash.Exit();
            Cooking.Instance.cookingMachine.isCooking = false;
            Destroy(gameObject);
        }
        ped.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        foreach (var item in results)
        {

            if (item.gameObject.tag == "Packaging")
            {
                OrderCheck();
                StartCoroutine(item.gameObject.GetComponent<Packaging>().CheckPack(gameObject));
            }
        }

    }


    private int SubMatarialsCount()
    {
        int num = 0;
        foreach (var item in subMaterials)
        {
            if (item.subM == Cooking.Instance.AnswerOrder.sub[0])
            {
                num++;
            }
        }

        return num;
    }

    public void OrderCheck()
    {

        print("OrderCheckOn");

        int checkList = 0;


        if (Cooking.Instance.AnswerOrder.style != style) checkList++;
        if (Cooking.Instance.AnswerOrder.main != mainMaterial) checkList++;
        if (Cooking.Instance.AnswerOrder.count > SubMatarialsCount()) checkList++;


        if (Cooking.Instance.AnswerOrder.sub[0] != ESubMatarials.NULL)
        {
            bool isReturn = false;

            foreach (var subM in subMaterials)
            {
                if (Cooking.Instance.AnswerOrder.sub[0] == subM.subM)
                {
                    isReturn = true;
                }
            }

            if(isReturn == false) checkList ++;
        }

        print("Satisfaction : "+(checkList * 20) / Cooking.Instance.AnswerOrder.dishCount);

        GameManager.Instance.Satisfaction -= (checkList * 20) / Cooking.Instance.AnswerOrder.dishCount;
    }
}
