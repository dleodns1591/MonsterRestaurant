using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingBoard : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
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

    public ECookingStyle style;
    public EMainMatarials mainMaterial;
    public List<SubMaterialImages> subMaterials = new List<SubMaterialImages>();
    
    [SerializeField] private Image subM;

    public bool isFinish;
    private bool isMainMaterialDrop;
    [SerializeField] private bool isEnterImage;

    private Image myCook;
    private Vector2 machinePos = new Vector2(631,192);

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


        if (isMainMaterialDrop == true && GameManager.Instance.BuyCheck(mainPrice)) return;

        GameManager.Instance.Money -= mainPrice;

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
    }

    private IEnumerator BoardMove()
    {
        print("st");
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
            float price = Cooking.Instance.materialPrice;

            if ((isMainMaterialDrop == false || Cooking.Instance.myType == ESubMatarials.NULL)
                && GameManager.Instance.BuyCheck(price)) return;


            GameObject sub = Instantiate(subM, transform).gameObject;
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(eventData.position);
            sub.transform.position = inputPos;
            sub.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            sub.GetComponent<Image>().raycastTarget = false;

            subMaterials.Add(sub.GetComponent<SubMaterialImages>());
            GameManager.Instance.Money -= price;
        }
        else
        {
            transform.parent = Cooking.Instance.KitchenRoom.transform;
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
        myCook.rectTransform.localPosition = new Vector2(631, 192);


        if (isEnterTrash == true) Destroy(gameObject);

        ped.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        foreach (var item in results)
        {
            print(item.gameObject.tag);

            if (item.gameObject.tag == "Packaging")
            {
                OrderCheck();

                StartCoroutine(item.gameObject.GetComponent<Packaging>().CheckPack(gameObject));
            }
        }

    }

    private void OrderCheck()
    {
        int checkList = 0;

        OrderSet order = GameManager.Instance.orderSets[GameManager.Instance.randomCustomerNum];


        if (order.style != style) checkList++;
        if (order.main != mainMaterial) checkList++;
        if (order.count < subMaterials.Count) checkList++;


        int num = 0;
        bool isReturn = false; ;

        for (int i = 0; i < 3; i++)
        {
            isReturn = false;
            foreach (var subM in subMaterials)
            {
                if (order.sub[i] == subM.subM)
                {
                    if (isReturn == true) return;
                    num++;
                    isReturn = true;
                }
            }

        }

        checkList += 3 - num;

    }
}
