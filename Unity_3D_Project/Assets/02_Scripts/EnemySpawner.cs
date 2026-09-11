using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    // =========================
    // UI
    // =========================

    public Slider bossHPSlider;
    public GameObject victoryText;


    // =========================
    // 적 생성
    // =========================

    public GameObject enemyPrefab;

    public float spawnInterval = 2f;
    public float spawnZ = 8f;
    public float sideMargin = 1f;

    private float nextSpawnTime = 0f;


    // =========================
    // 발악 패턴
    // =========================

    public float dangerSpawnInterval = 0.5f;
    private bool isDangerMode = false;


    // =========================
    // EnemySpawner 체력
    // =========================

    public int maxHP = 100;
    private int currentHP;


    // =========================
    // EnemySpawner 공격
    // =========================

    public GameObject enemyBulletPrefab;
    public Transform enemyFirePoint;

    public float attackInterval = 2f;
    public float dangerAttackInterval = 0.8f;

    private float nextAttackTime = 0f;


    // =========================
    // EnemySpawner 이동
    // =========================

    public float moveSpeed = 2f;

    private readonly float minX = -4f;
    private readonly float maxX = 3f;

    private int moveDirection = 1;


    void Start()
    {
        currentHP = maxHP;

        bossHPSlider.minValue = 0;
        bossHPSlider.maxValue = maxHP;
        bossHPSlider.wholeNumbers = true;
        bossHPSlider.value = currentHP;
    }


    void Update()
    {
        // 게임 시작 전에는 정지
        if (Time.timeScale == 0f)
        {
            return;
        }


        // 적 생성
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }


        // 공격
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackInterval;
        }


        // 좌우 이동
        MoveSpawner();
    }


    // =========================
    // 적 생성
    // =========================

    void SpawnEnemy()
    {
        Vector3 leftPoint =
            Camera.main.ViewportToWorldPoint(
                new Vector3(
                    0f,
                    0.5f,
                    Camera.main.transform.position.y
                )
            );

        Vector3 rightPoint =
            Camera.main.ViewportToWorldPoint(
                new Vector3(
                    1f,
                    0.5f,
                    Camera.main.transform.position.y
                )
            );

        float randomX = Random.Range(
            leftPoint.x + sideMargin,
            rightPoint.x - sideMargin
        );

        Vector3 spawnPosition =
            new Vector3(
                randomX,
                0f,
                spawnZ
            );

        Instantiate(
            enemyPrefab,
            spawnPosition,
            enemyPrefab.transform.rotation
        );
    }


    // =========================
    // EnemySpawner 공격
    // =========================

    void Attack()
    {
        if (!isDangerMode)
        {
            Instantiate(
                enemyBulletPrefab,
                enemyFirePoint.position,
                enemyFirePoint.rotation
            );
        }
        else
        {
            Quaternion centerRotation =
                enemyFirePoint.rotation;

            Quaternion leftRotation =
                enemyFirePoint.rotation
                * Quaternion.Euler(0f, -10f, 0f);

            Quaternion rightRotation =
                enemyFirePoint.rotation
                * Quaternion.Euler(0f, 10f, 0f);

            Instantiate(
                enemyBulletPrefab,
                enemyFirePoint.position,
                leftRotation
            );

            Instantiate(
                enemyBulletPrefab,
                enemyFirePoint.position,
                centerRotation
            );

            Instantiate(
                enemyBulletPrefab,
                enemyFirePoint.position,
                rightRotation
            );
        }
    }


    // =========================
    // EnemySpawner 이동
    // =========================

    void MoveSpawner()
    {
        transform.position +=
            Vector3.right
            * moveDirection
            * moveSpeed
            * Time.deltaTime;

        if (transform.position.x >= maxX)
        {
            moveDirection = -1;
        }

        if (transform.position.x <= minX)
        {
            moveDirection = 1;
        }
    }


    // =========================
    // EnemySpawner 데미지
    // =========================

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        currentHP = Mathf.Clamp(
            currentHP,
            0,
            maxHP
        );

        UpdateBossHPBar();

        Debug.Log("Spawner HP : " + currentHP);


        // 체력이 50% 이하가 되면 발악 패턴 시작
        if (!isDangerMode && currentHP <= maxHP / 2)
        {
            isDangerMode = true;

            spawnInterval = dangerSpawnInterval;
            attackInterval = dangerAttackInterval;

            Debug.Log("DANGER MODE");
        }


        // 체력이 0 이하가 되면 승리
        if (currentHP <= 0)
        {
            Victory();
        }
    }


    // =========================
    // 보스 체력바 갱신
    // =========================

    void UpdateBossHPBar()
    {
        bossHPSlider.value = currentHP;
    }



    // =========================
    // 승리
    // =========================

    void Victory()
    {
        Debug.Log("VICTORY");

        victoryText.SetActive(true);

        Destroy(gameObject);
    }
}