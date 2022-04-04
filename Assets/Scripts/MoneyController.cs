using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static event Action OnMoneySet;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            OnMoneySet?.Invoke();
        }
    }
}
