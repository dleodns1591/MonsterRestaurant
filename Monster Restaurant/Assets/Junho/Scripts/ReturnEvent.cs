using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class EventCheck
{
    public List<bool> eventCheckList = new List<bool>();
}

public class ReturnEvent : MonoBehaviour
{
    const int maxDay = 20;

    [SerializeField] private List<EventCheck> eventChecks = new List<EventCheck>();
    public List<EeventCustomerType> returnEventCustomer = new List<EeventCustomerType>();

    public void Check()
    {
        int num = 0;

        returnEventCustomer.Clear();

        print(GameManager.Instance.Day - 1);

        foreach (bool item in eventChecks[GameManager.Instance.Day - 1].eventCheckList)
        {
            if (item == true)
            {
                switch (num)
                {
                    case 0:
                        if (Rand(45) == true)
                            returnEventCustomer.Add(EeventCustomerType.Human);
                        break;
                    case 1:
                        returnEventCustomer.Add(EeventCustomerType.Thief);
                        break;
                    case 2:
                        if (Rand(40) == true)
                            returnEventCustomer.Add(EeventCustomerType.Beggar);
                        break;
                    case 3:
                        returnEventCustomer.Add(EeventCustomerType.GroupOrder);
                        break;
                    case 4:
                        returnEventCustomer.Add(EeventCustomerType.SalesMan);
                        break;
                    case 5:
                        returnEventCustomer.Add(EeventCustomerType.FoodCleanTester);
                        break;
                }
            }
            num++;
        } 
    }

    public bool Rand(int percent)
    {
        if (percent <= Random.Range(0,100))
        {
            return true;
        }

        return false;
    }

}
