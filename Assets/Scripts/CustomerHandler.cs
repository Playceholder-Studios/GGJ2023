using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHandler : MonoBehaviour
{
    public List<Customer> customers;
    
    public void LoadCustomer(int characterId)
    {
        foreach (Customer c in customers)
        {
            if (c.customerId == characterId)
            {
                c.gameObject.SetActive(true);
            }
            else
            {
                c.gameObject.SetActive(false);
            }
        }
    }
}
