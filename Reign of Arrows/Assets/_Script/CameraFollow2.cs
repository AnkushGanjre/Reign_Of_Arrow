using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] Vector3 offSet;
    private Vector3 velocity = Vector3.zero;


    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosiion = target.position + offSet;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosiion, ref velocity, smoothTime);
        }
    }
}
