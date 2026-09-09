using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // =========================
    // 플레이어 이동
    // =========================

    public float moveSpeed = 5f;


    // =========================
    // 총알 발사
    // =========================

    // 발사할 총알 프리팹
    public GameObject BulletPrefab;

    // 총알 발사 간격
    public float fireRate = 0.2f;

    // 다음 발사 가능 시간
    private float nextFireTime = 0f;


    // =========================
    // 플레이어 체력
    // =========================

    // 플레이어 최대 체력
    public int maxHP = 5;

    // 현재 체력
    private int currentHP;


    // =========================
    // UI
    // =========================

    // GAME OVER 글자
    public GameObject gameOverText;


    void Start()
    {
        // 게임 시작 시 현재 체력을 최대 체력으로 설정
        currentHP = maxHP;
    }


    void Update()
    {
        // =========================
        // 이동 입력
        // =========================

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            input.y += 1;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            input.y -= 1;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            input.x -= 1;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            input.x += 1;
        }

        // 대각선 이동 속도 보정
        input = input.normalized;


        // =========================
        // 카메라 기준 이동
        // =========================

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Y축 이동 제거
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =
            right * input.x +
            forward * input.y;

        transform.position +=
            movement * moveSpeed * Time.deltaTime;


        // =========================
        // 총알 발사
        // =========================

        if (Keyboard.current.spaceKey.isPressed &&
            Time.time >= nextFireTime)
        {
            Instantiate(
                BulletPrefab,
                transform.position,
                transform.rotation
            );

            nextFireTime = Time.time + fireRate;
        }
    }


    // =========================
    // 플레이어 데미지
    // =========================

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log("Player HP : " + currentHP);

        // 체력이 0 이하라면 게임오버
        if (currentHP <= 0)
        {
            GameOver();
        }
    }


    // =========================
    // 게임 오버
    // =========================

    void GameOver()
    {
        Debug.Log("GAME OVER");

        // GAME OVER 글자 표시
        gameOverText.SetActive(true);

        // 플레이어 비활성화
        gameObject.SetActive(false);
    }
}