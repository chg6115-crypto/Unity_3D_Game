using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // 시작 화면 전체
    public GameObject startPanel;

    // 게임 시작 여부
    private bool gameStarted = false;


    void Start()
    {
        // 시작 화면 보이기
        startPanel.SetActive(true);

        // 게임 일시정지
        Time.timeScale = 0f;
    }


    void Update()
    {
        // 이미 게임이 시작됐으면 아무것도 안 함
        if (gameStarted)
        {
            return;
        }

        // 아무 키나 눌렀을 때
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }


    void StartGame()
    {
        gameStarted = true;

        // 시작 화면 숨기기
        startPanel.SetActive(false);

        // 게임 다시 진행
        Time.timeScale = 1f;

        Debug.Log("GAME START");
    }
}