using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    // =========================
    // 플레이어 이동
    // =========================

    public float moveSpeed = 5f;

    // 플레이어 이동 범위
    private readonly float minX = -3f;
    private readonly float maxX = 3f;

    private readonly float minZ = -8f;
    private readonly float maxZ = 6f;


    // =========================
    // 일반 총알 발사
    // =========================

    // 일반 총알 발사 위치
    public Transform firePoint;

    // 발사할 총알 프리팹
    public GameObject BulletPrefab;

    // 총알 발사 간격
    public float fireRate = 0.2f;

    // 다음 발사 가능 시간
    private float nextFireTime = 0f;


    // =========================
    // 특수 공격
    // =========================

    // 특수공격에 사용할 미사일 프리팹
    public GameObject specialMissilePrefab;

    // 특수공격 발사 위치
    public Transform specialFirePoint;

    // 현재 특수공격 게이지
    private int specialGauge = 0;

    // 특수공격 최대 게이지
    private readonly int maxSpecialGauge = 3;

    // 특수공격 사용 가능 여부
    private bool specialReady = false;

    // 특수미사일 게이지 UI
    public Slider specialGaugeSlider;


    // =========================
    // 플레이어 체력
    // =========================

    // 플레이어 최대 체력
    public int maxHP = 5;

    // 현재 체력
    private int currentHP;

    // 플레이어 HP 아이콘 UI
    // HP01 ~ HP05를 연결
    public Image[] playerHPImages;


    // =========================
    // UI
    // =========================

    // GAME OVER 글자
    public GameObject gameOverText;


    void Start()

    {
        gameManager = FindAnyObjectByType<GameManager>();

        // 게임 시작 시 현재 체력을 최대 체력으로 설정
        currentHP = maxHP;

        // 플레이어 HP UI 갱신
        UpdatePlayerHPUI();

        // 특수 게이지 설정
        specialGaugeSlider.maxValue = maxSpecialGauge;
        specialGaugeSlider.value = specialGauge;
    }


    void Update()
    {
        if (gameManager != null &&
    !gameManager.CanPlayerInput())
        {
            return;
        }

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
        // 이동 범위 제한
        // =========================

        float clampedX =
            Mathf.Clamp(transform.position.x, minX, maxX);

        float clampedZ =
            Mathf.Clamp(transform.position.z, minZ, maxZ);

        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            clampedZ
        );


        // =========================
        // 일반 총알 발사
        // =========================

        if (Keyboard.current.jKey.isPressed &&
            Time.time >= nextFireTime)
        {
            Instantiate(
                BulletPrefab,
                firePoint.position,
                firePoint.rotation
            );

            nextFireTime = Time.time + fireRate;
        }


        // =========================
        // 특수 공격
        // =========================

        if (Keyboard.current.kKey.wasPressedThisFrame
            && specialReady)
        {
            FireSpecial();
        }
    }


    // =========================
    // 플레이어 데미지
    // =========================

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // HP가 0보다 작아지지 않도록 제한
        currentHP = Mathf.Clamp(
            currentHP,
            0,
            maxHP
        );

        // HP UI 갱신
        UpdatePlayerHPUI();

        Debug.Log("Player HP : " + currentHP);

        // 체력이 0 이하라면 게임오버
        if (currentHP <= 0)
        {
            GameOver();
        }
    }


    // =========================
    // 플레이어 회복
    // =========================

    public void Heal(int amount)
    {
        currentHP += amount;

        // 최대 체력을 넘지 않도록 제한
        currentHP = Mathf.Clamp(
            currentHP,
            0,
            maxHP
        );

        // HP UI 갱신
        UpdatePlayerHPUI();

        Debug.Log("Player HP : " + currentHP);
    }


    // =========================
    // 플레이어 HP UI 갱신
    // =========================

    void UpdatePlayerHPUI()
    {
        for (int i = 0; i < playerHPImages.Length; i++)
        {
            if (i < currentHP)
            {
                playerHPImages[i].enabled = true;
            }
            else
            {
                playerHPImages[i].enabled = false;
            }
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


    // =========================
    // 특수 게이지 획득
    // =========================

    public void AddScore(int amount)
    {
        specialGauge += amount;

        // 게이지는 최대 3까지만 올라감
        specialGauge =
            Mathf.Clamp(
                specialGauge,
                0,
                maxSpecialGauge
            );

        // UI 게이지 갱신
        specialGaugeSlider.value = specialGauge;

        Debug.Log(
            "Special Gauge : "
            + specialGauge
            + " / "
            + maxSpecialGauge
        );

        // 게이지가 최대치에 도달하면 특수공격 사용 가능
        if (specialGauge == maxSpecialGauge)
        {
            specialReady = true;

            Debug.Log("SPECIAL READY");
        }
    }


    // =========================
    // 특수 공격 발사
    // =========================

    void FireSpecial()
    {
        // 특수 미사일 생성
        Instantiate(
            specialMissilePrefab,
            specialFirePoint.position,
            specialFirePoint.rotation
        );

        // 특수공격 사용 후 게이지를 0으로 초기화
        specialGauge = 0;

        // 게이지 UI도 0으로 갱신
        specialGaugeSlider.value = specialGauge;

        // 다시 게이지를 채우기 전까지 특수공격 사용 불가
        specialReady = false;

        Debug.Log("SPECIAL FIRE");

        Debug.Log(
            "Special Gauge : "
            + specialGauge
            + " / "
            + maxSpecialGauge
        );
    }
}