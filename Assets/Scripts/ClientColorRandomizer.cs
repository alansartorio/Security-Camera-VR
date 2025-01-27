using UnityEngine;

public class ClientColorRandomizer : MonoBehaviour
{
    [SerializeField] private Material skin;
    [SerializeField] private Material pants;
    [SerializeField] private Material shirt;
    [SerializeField] private Material shoes;

    private void Awake()
    {
        shirt.color = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);
        pants.color = Random.ColorHSV(0, 1, 0.5f, 1, 0, shirt.color.grayscale);
        shoes.color = Random.ColorHSV(0, 1, 0.5f, 1, 0, pants.color.grayscale);
    }
}