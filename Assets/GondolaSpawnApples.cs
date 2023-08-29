using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GondolaSpawnApples : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private int appleCount;
    [SerializeField] private float positionMultiplier = 0.8f;
    [SerializeField] private Transform spreadSize;
    
    void Start()
    {
        for (int i = 0; i < appleCount; i++)
        {
            float x, z;
            x = Random.Range(-0.5f, 0.5f) * spreadSize.localScale.y;
            z = Random.Range(-0.5f, 0.5f) * spreadSize.localScale.z;
            var apple = Instantiate(applePrefab, transform);
            apple.transform.localPosition = new Vector3(x, 0, z) * positionMultiplier;
        }
    }
}
