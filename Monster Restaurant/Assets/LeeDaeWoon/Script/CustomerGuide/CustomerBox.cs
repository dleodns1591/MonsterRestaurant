using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerBox : MonoBehaviour
{
    [SerializeField] GameObject customerBoxParent;
    Button customerBoxBtn;

    bool isCustomerType;

    void Start()
    {
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
                var eCustomer = CustomerGuide.instance.generalList[num].eCustomer;

                if (!isCustomerType)
                {
                    switch (eCustomer)
                    {
                        case Guide.ECustomer.Spinach:
                            break;

                        case Guide.ECustomer.Sdh210224:
                            break;

                        case Guide.ECustomer.Zeto:
                            break;

                        case Guide.ECustomer.ChrisTheGhost:
                            break;

                        case Guide.ECustomer.Quasar:
                            break;

                        case Guide.ECustomer.Axolonaut:
                            break;

                        case Guide.ECustomer.Quantum:
                            break;

                        case Guide.ECustomer.Hellios:
                            break;

                        case Guide.ECustomer.FSM:
                            break;

                        case Guide.ECustomer.Garbage:
                            break;

                        case Guide.ECustomer.Joker:
                            break;
                    }
                }

                else
                {
                    switch (eCustomer)
                    {
                        case Guide.ECustomer.Stella:
                            break;

                        case Guide.ECustomer.Sock:
                            break;

                        case Guide.ECustomer.Florian:
                            break;

                        case Guide.ECustomer.Dopey:
                            break;

                        case Guide.ECustomer.Receid:
                            break;

                        case Guide.ECustomer.H30122:
                            break;
                    }
                }
            }
        }

    }
}
