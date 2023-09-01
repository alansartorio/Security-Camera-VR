using UnityEngine;

public class CameraMonitor : MonoBehaviour
{
    public GameObject securityCamera;
    void Start()
    {
        Texture rt = securityCamera.GetComponent<CreateRenderTexture>().rt;
        Material material = GetComponent<Renderer>().material;
        // material.mainTexture = rt;
        material.SetTexture("_EmissionMap", rt);
    }
}
