using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Client : MonoBehaviour
{
    public int maxItemCapacity;
    public int leftItemsToGet;
    public int leftItemsToSteal;
    public int itemsInHand;
    public float thiefProbability = 0.1f;
    public int itemsHidden;
    public int itemsInCart;
    public int itemsInBag;

    private void Awake()
    {
        int totalItems = Random.Range(0, maxItemCapacity);
        bool steals = Random.Range(0f, 1) < thiefProbability;
        if (totalItems > 0 && steals)
        {
            leftItemsToSteal = Random.Range(1, totalItems + 1);
            leftItemsToGet = totalItems - leftItemsToSteal;
        }
        else
        {
            leftItemsToGet = totalItems;
            leftItemsToSteal = 0;
        }
        itemsInHand = 0;
    }

    public void Exited()
    {
        if (itemsHidden > 0)
        {
            GameObject.FindObjectOfType<PopupMonitor>().ShowPopup(PopupType.AThiefEscaped);
        }
        
        Destroy(gameObject);
    }
}