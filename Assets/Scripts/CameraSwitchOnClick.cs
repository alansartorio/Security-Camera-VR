using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwitchOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Transform mainCamera = Camera.main!.transform;
        mainCamera.parent.GetComponent<Collider>().enabled = true;
        mainCamera.SetParent(transform, false);
        GetComponent<Collider>().enabled = false;
    }
}
