using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;

    // 게임 시작 여부
    public bool gameStarted = false;

    // 게임 시작 직후 입력 잠금 시간
    public float inputDelay = 0.3f;

    // 실제 입력이 가능해지는 시간
    private float inputEnableTime;

    void Start()
    {
        startPanel.SetActive(true);

        // 시작 화면에서는 게임 정지
        Time.timeScale = 0f;
    }

    void Update()
    {
        // 아직 게임이 시작되지 않았다면
        if (!gameStarted)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                StartGame();
            }

            return;
        }

        // ESC로 게임 종료
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            QuitGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;

        startPanel.SetActive(false);

        Time.timeScale = 1f;

        // 게임 시작 후 0.3초 뒤부터 입력 허용
        inputEnableTime =
            Time.unscaledTime + inputDelay;

        Debug.Log("GAME START");
    }

    public bool CanPlayerInput()
    {
        return
            gameStarted &&
            Time.unscaledTime >= inputEnableTime;
    }

    void QuitGame()
    {
        Debug.Log("GAME QUIT");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}