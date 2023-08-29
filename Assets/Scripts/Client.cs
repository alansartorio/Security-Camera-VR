using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Client : MonoBehaviour
{
    public int leftItemsToGrab;

    private void Awake()
    {
        leftItemsToGrab = Random.Range(0, 5);
    }
}