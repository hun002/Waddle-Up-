using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("게임 설정")]
    public float gameOverAngle = 45f;

    public static GameManager Instance;

    private bool gameStarted = false;
    private bool gameOver = false;
    private float distance = 0f;

    private DuckController duckController;
    private DuckBodyController bodyController;

    void Awake()
    {
        Instance = this;
        duckController = FindObjectOfType<DuckController>();
        bodyController = FindObjectOfType<DuckBodyController>();
    }

    void Start()
    {
        Debug.Log("엔터를 눌러 게임 시작!");
    }

    void Update()
    {
        if (!gameStarted && !gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }
        else if (gameStarted && !gameOver)
        {
            distance += duckController.walkSpeed * Time.deltaTime;
        }

        if (gameOver && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        gameOver = false;
        distance = 0f;

        duckController.StartWalking();
        bodyController.StartBodyAngleChange();

        Debug.Log("게임 시작! 방향키로 몸통 각도를 조절하세요!");
    }

    public void TriggerGameOver(string reason)
    {
        if (gameOver) return;

        gameOver = true;
        gameStarted = false;

        duckController.StopWalking();
        bodyController.StopBodyAngleChange();

        Debug.Log($"게임 오버! 이유: {reason}");
        Debug.Log($"최종 거리: {distance:F1}m");
        Debug.Log("엔터를 눌러 다시 시작");
    }

    void RestartGame()
    {
        gameStarted = false;
        gameOver = false;
        distance = 0f;

        duckController.ResetDuck();
        bodyController.ResetBodyAngle();

        Debug.Log("게임 초기화 끝! 엔터를 눌러 시작!");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), $"거리: {distance:F1}m");
        GUI.Label(new Rect(10, 30, 200, 20), $"몸통 각도: {bodyController.GetTotalBodyAngle():F1}°");

        if (!gameStarted && !gameOver)
        {
            GUI.Label(new Rect(10, 60, 300, 20), "엔터: 게임 시작");
        }
        else if (gameStarted)
        {
            GUI.Label(new Rect(10, 60, 300, 20), "방향키: 몸통 각도 조절");
        }
        else if (gameOver)
        {
            GUI.Label(new Rect(10, 60, 300, 20), "게임 오버! 엔터: 다시 시작");
        }
    }

    public bool IsGameStarted() => gameStarted;
    public bool IsGameOver() => gameOver;
}
