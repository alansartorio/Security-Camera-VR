using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VROrNot : MonoBehaviour
{
    [SerializeField] GameObject vrRig;
    [SerializeField] GameObject nonVrRig;

    void Start()
    {
        bool isVR = false;
#if UNITY_EDITOR
        isVR = false;
#else
        isVR = true;
#endif
        vrRig.SetActive(isVR);
        nonVrRig.SetActive(!isVR);
    }
}