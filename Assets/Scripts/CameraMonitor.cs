using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CameraMonitor : MonoBehaviour
{
    public GameObject securityCamera;
    void Start()
    {
        Texture rt = securityCamera.GetComponent<CreateRenderTexture>().rt;
        Debug.Log(rt);
        Material material = GetComponent<Renderer>().material;
        material.mainTexture = rt;
        material.color = Color.white;
        // material.SetColor("_EmissionColor", Color.white);
        // material.SetTexture("_EmissionMap", rt);
        // material.EnableKeyword("_EMISSION");
    }
}
