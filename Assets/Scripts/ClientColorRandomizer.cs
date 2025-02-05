using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientColorRandomizer : MonoBehaviour
{
    [SerializeField] private Renderer personRenderer;

    private void Awake()
    {
        var materials = personRenderer.materials.ToDictionary(m => m.name);
        var skin = materials.GetValueOrDefault("Skin (Instance)")!;
        var pants = materials.GetValueOrDefault("Pants (Instance)")!;
        var shirt = materials.GetValueOrDefault("Shirt (Instance)")!;
        var shoes = materials.GetValueOrDefault("Shoes (Instance)")!;

        shirt.color = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);
        pants.color = Random.ColorHSV(0, 1, 0.5f, 1, 0, shirt.color.grayscale);
        shoes.color = Random.ColorHSV(0, 1, 0.5f, 1, 0, pants.color.grayscale);
    }
}