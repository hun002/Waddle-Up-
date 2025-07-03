using UnityEngine;

public class DuckHeadController : MonoBehaviour
{
    [Header("머리 파츠")]
    public Transform head;
    
    
    public void ResetHead()
    {
        if (head != null)
        {
            head.rotation = Quaternion.identity;
        }
    }
}