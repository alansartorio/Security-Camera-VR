using UnityEngine;

public class Gondola : MonoBehaviour
{
    [SerializeField] public GameObject ProductPrefab;
    [SerializeField] public int ProductCount;

    void Start()
    {
        var stands = GetComponentsInChildren<GondolaSpawnProducts>();
        foreach (var stand in stands)
        {
            stand.ProductPrefab = ProductPrefab;
            stand.ProductCount = ProductCount;
            stand.SpawnProducts();
        }
    }
}