using UnityEngine;

public class CameraMove : MonoBehaviour 
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }
}
