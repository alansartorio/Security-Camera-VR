using UnityEngine;

public class ComputerCursor : MonoBehaviour
{
    private GameObject cursor;
    public float rotationSpeed = 5f;
    public float rayDistance = 100f;
    public LayerMask hitLayer; 

    void Start()
    {
        cursor = GameObject.Find("Ray"); 

        if (cursor == null)
        {
            Debug.LogError("Cursor wasn't found in the scene");
        }
    }

    void Update()
    {
        RotateCubeTowardsMouse();
        ShootRayFromCube();
        MoveCursorOnCanvas();
    }

    void RotateCubeTowardsMouse()
    {
        if (cursor != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer)) 
            {
                Vector3 direction = hit.point - cursor.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                cursor.transform.rotation = Quaternion.Slerp(cursor.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                Debug.DrawLine(cursor.transform.position, hit.point, Color.green);
            }
            else
            {
                Vector3 direction = ray.direction;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                cursor.transform.rotation = Quaternion.Slerp(cursor.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    void ShootRayFromCube()
    {
        if (cursor != null)
        {
            Ray ray = new Ray(cursor.transform.position, cursor.transform.forward);
            Debug.DrawRay(cursor.transform.position, cursor.transform.forward * rayDistance, Color.red, 0.1f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, hitLayer))
            {
                Debug.Log("Ray hit: " + hit.collider.name);

                Canvas canvas = hit.collider.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);

                    RectTransform cursorRectTransform = canvas.GetComponentInChildren<RectTransform>();
                    if (cursorRectTransform != null)
                    {
                        cursorRectTransform.position = screenPosition;
                    }
                    else
                    {
                        Debug.Log("Cursor wasn't found");
                    }
                }
                else
                {
                    Debug.Log("Canvas wasn't found");
                }
            }
        }
    }


    void MoveCursorOnCanvas()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer))
        {
            Canvas canvas = hit.collider.GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                GameObject cursorUI = canvas.GetComponentInChildren<Transform>().gameObject;
                if (cursorUI != null)
                {
                    Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
                    cursorUI.transform.position = screenPosition;
                }
            }
        }
    }
}
