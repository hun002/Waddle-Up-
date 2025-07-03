using UnityEngine;

public class DuckController : MonoBehaviour
{
    [Header("오리 파츠")]
    public Transform leftLeg;
    public Transform rightLeg;
    
    [Header("이동 설정")]
    public float walkSpeed = 2f;
    public float walkBounceHeight = 0.1f;
    
    [Header("다리 애니메이션")]
    public float legSwingAngle = 20f;
    public float legAnimSpeed = 2f;
    
    private bool isWalking = false;
    private float legAnimationTime = 0f;
    private Vector3 initialPosition;
    
    void Start()
    {
        initialPosition = transform.position;
    }
    
    void Update()
    {
        if (isWalking)
        {
            HandleMovement();
            AnimateLegs();
        }
    }
    
    void HandleMovement()
    {
        // 앞으로 이동
        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
        
        float bounceOffset = Mathf.Sin(legAnimationTime * legAnimSpeed) * walkBounceHeight;
        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x, 
                                       currentPos.y + bounceOffset * Time.deltaTime, 
                                       currentPos.z);
    }
    
    void AnimateLegs()
    {
        legAnimationTime += Time.deltaTime;
        
        // 다리 움직임
        float leftLegAngle = Mathf.Sin(legAnimationTime * legAnimSpeed) * legSwingAngle;
        float rightLegAngle = Mathf.Sin(legAnimationTime * legAnimSpeed + Mathf.PI) * legSwingAngle;
        
        leftLeg.rotation = Quaternion.Euler(0, 0, leftLegAngle);
        rightLeg.rotation = Quaternion.Euler(0, 0, rightLegAngle);
    }
    
    public void StartWalking()
    {
        isWalking = true;
    }
    
    public void StopWalking()
    {
        isWalking = false;
    }
    
    public void ResetDuck()
    {
        isWalking = false;
        legAnimationTime = 0f;

        leftLeg.rotation = Quaternion.identity;
        rightLeg.rotation = Quaternion.identity;
    }
}