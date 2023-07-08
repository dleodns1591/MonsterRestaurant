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
    int Day;

    [SerializeField] private List<EventCheck> eventChecks = new List<EventCheck>();
    public List<EeventCustomerType> returnEventCustomer = new List<EeventCustomerType>();

    public void Check()
    {
        int num = 0;

        foreach (var item in eventChecks[Day].eventCheckList)
        {
            if (item == true)
            {
                if (num == 0 && Rand(45) == true)
                {
                    returnEventCustomer.Add((EeventCustomerType)num);

                }
                else if(num == 2 && Rand(40) == true)
                {
                    returnEventCustomer.Add((EeventCustomerType)num);
                }
                else returnEventCustomer.Add((EeventCustomerType)num);
            }

            num++;
        } 


        Day++;
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