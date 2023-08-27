using UnityEngine;

public class CreateRenderTexture : MonoBehaviour
{
    public RenderTexture rt;
    void Awake()
    {
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();

        Camera cameraComponent = transform.GetComponentInChildren<Camera>();
        cameraComponent.targetTexture = rt;
    }

    private void OnDestroy()
    {
        rt.Release();
    }
}
