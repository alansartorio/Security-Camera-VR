using UnityEngine;

public class CreateRenderTexture : MonoBehaviour
{
    public RenderTexture rt;
    [SerializeField] private int size;
    void Awake()
    {
        rt = new RenderTexture(size, size, 16, RenderTextureFormat.ARGB32);
        rt.Create();


        Camera cameraComponent = transform.GetComponentInChildren<Camera>();
        cameraComponent.targetTexture = rt;
    }

    private void OnDestroy()
    {
        rt.Release();
    }
}
