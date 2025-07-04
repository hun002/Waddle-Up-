using System.Collections;
using UnityEngine;

public class DuckBodyController : MonoBehaviour
{   private float springVelocity = 0f;

    [Header("움직임 설정")]
    public float restoreForce = 2f;
    public float dampingForce = 0.8f;

    [Header("몸통 파츠")]
    public Transform body;

    [Header("몸통 각도 설정")]
    public float bodyAngleChangeInterval = 0.5f;
    public float maxBodyAngle = 30f;
    public float bodyControlSpeed = 50f;
    public float bodyRotationSmooth = 5f;

    private float targetBodyAngle = 0f;
    private float currentBodyAngle = 0f;
    private float displayBodyAngle = 0f;
    private Coroutine bodyAngleCoroutine;  

    void Update()
    {
        if (GameManager.Instance.IsGameStarted() && !GameManager.Instance.IsGameOver())
        {
            HandleBodyControl();

            springVelocity += -currentBodyAngle * restoreForce * Time.deltaTime;
            springVelocity -= springVelocity * dampingForce * Time.deltaTime;
            // 중심 복원
            currentBodyAngle += springVelocity * Time.deltaTime;

        }
    }

    void HandleBodyControl()
    {
        float input = 0f;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            input = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            input = -1f;
        }

        float impulseAmount = 15f; 
        currentBodyAngle += input * impulseAmount;

        // 각도 제한
        currentBodyAngle = Mathf.Clamp(currentBodyAngle, -maxBodyAngle, maxBodyAngle);

        float targetAngle = currentBodyAngle + targetBodyAngle;

        displayBodyAngle = Mathf.Lerp(displayBodyAngle, targetAngle, bodyRotationSmooth * Time.deltaTime);

        // 실제 회전 적용
        body.rotation = Quaternion.Euler(0, 0, displayBodyAngle);
        
    }


    public void StartBodyAngleChange()
    {
        bodyAngleCoroutine = StartCoroutine(ChangeBodyAngle());
    }

    public void StopBodyAngleChange()
    {
        if (bodyAngleCoroutine != null)
        {
            StopCoroutine(bodyAngleCoroutine);
        }
    }

    public void ResetBodyAngle()
    {
        currentBodyAngle = 0f;
        targetBodyAngle = 0f;
        displayBodyAngle = 0f;
        body.rotation = Quaternion.identity;

        if (bodyAngleCoroutine != null)
        {
            StopCoroutine(bodyAngleCoroutine);
        }
    }

    IEnumerator ChangeBodyAngle()
    {
        while (GameManager.Instance.IsGameStarted() && !GameManager.Instance.IsGameOver())
        {
            yield return new WaitForSeconds(bodyAngleChangeInterval);
            targetBodyAngle = Random.Range(-maxBodyAngle, maxBodyAngle);
            Debug.Log($"몸통이 {targetBodyAngle:F1}도 기울어집니다!");
        }
    }

    public float GetTotalBodyAngle()
    {
        return displayBodyAngle;
    }
}
