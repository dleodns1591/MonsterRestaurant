using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerBox : MonoBehaviour
{
    [SerializeField] GameObject customerBoxParent;
    Button customerBoxBtn;

    CustomerGuide customerGuide = null;
    bool isCustomerType = false;

    LanguageManager languageManager;

    void Start()
    {
        languageManager = LanguageManager.instance;
        customerGuide = CustomerGuide.instance;

        Btn();
    }

    void Update()
    {
        isCustomerType = CustomerGuide.instance.isCustomerCheck;
    }

    void Btn()
    {
        customerBoxBtn = GetComponent<Button>();

        customerBoxBtn.onClick.AddListener(() =>
        {
            switch (transform.GetSiblingIndex())
            {
                case 0:
                    CustomerBoxClick(0);
                    break;

                case 1:
                    CustomerBoxClick(1);
                    break;

                case 2:
                    CustomerBoxClick(2);
                    break;

                case 3:
                    CustomerBoxClick(3);
                    break;
            }
        });
    }

    void CustomerBoxClick(int num)
    {
        for (int i = 0; i < 4; i++)
        {
            if (num == i)
            {

                if (!isCustomerType)
                {
                    var eGeneraCustomer = customerGuide.generalList[num];
                    switch (languageManager.languageNum)
                    {
                        case 0:
                            customerGuide.story.sprite = eGeneraCustomer.storyEn;
                            break;

                        case 1:
                            customerGuide.story.sprite = eGeneraCustomer.storyKo;
                            break;
                    }

                }

                else
                {
                    var eEventCustomer = customerGuide.eventList[num];
                    switch (languageManager.languageNum)
                    {
                        case 0:
                            customerGuide.story.sprite = eEventCustomer.storyEn;
                            break;

                        case 1:
                            customerGuide.story.sprite = eEventCustomer.storyKo;
                            break;
                    }
                }

                CustomerStory();
            }
        }

    }

    void CustomerStory()
    {
        customerGuide.storyContant.position = Vector2.zero;

        customerGuide.customerBoxParent.SetActive(false);
        customerGuide.customerStoryParent.SetActive(true);

        customerGuide.storyContant.sizeDelta =
            new Vector2(customerGuide.storyContant.rect.width, customerGuide.story.rectTransform.rect.height);
    }
}
