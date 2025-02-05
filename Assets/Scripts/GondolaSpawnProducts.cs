using JetBrains.Annotations;
using UnityEngine;

public class GondolaSpawnProducts : MonoBehaviour
{
    [CanBeNull] public GameObject ProductPrefab;
    public int ProductCount = 1;
    [SerializeField] private float positionMultiplier = 0.8f;
    [SerializeField] private Transform spreadSize;
    
    public void SpawnProducts() 
    {
        for (int i = 0; i < ProductCount; i++)
        {
            float x, z;
            x = Random.Range(-0.5f, 0.5f) * spreadSize.localScale.y;
            z = Random.Range(-0.5f, 0.5f) * spreadSize.localScale.z;
            var product = Instantiate(ProductPrefab, transform);
            product.transform.localPosition = new Vector3(x, 0, z) * positionMultiplier;
            product.isStatic = true;
        }
    }
}
