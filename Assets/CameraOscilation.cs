using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOscilation : MonoBehaviour
{
    [SerializeField] private Transform fromTransform, toTransform;
    [SerializeField] private float movementDuration;
    [SerializeField] private float waitDuration;
    private float period;
    private float time;
    private Quaternion from, to;
    
    void Start()
    {
        period = (movementDuration + waitDuration) * 2;
        from = Matrix4x4.LookAt(transform.position, fromTransform.position, Vector3.up).rotation;
        to = Matrix4x4.LookAt(transform.position, toTransform.position, Vector3.up).rotation;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > period)
        {
            time -= period;
        }
        float value = GetValueForTime(time);
        transform.rotation = Quaternion.Lerp(from, to, value);
    }

    float GetValueForTime(float time)
    {
        if (time < waitDuration) return 0;
        if (time < waitDuration + movementDuration) return (time - waitDuration) / movementDuration;
        if (time < waitDuration * 2 + movementDuration) return 1;
        return 1 - (time - (waitDuration * 2 + movementDuration)) / movementDuration;
    }
}
