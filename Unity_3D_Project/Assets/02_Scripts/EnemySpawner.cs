using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // =========================
    // 승리 UI
    // =========================

    public GameObject victoryText;


    // =========================
    // 적 생성
    // =========================

    // 생성할 적 프리팹
    public GameObject enemyPrefab;

    // 기본 적 생성 간격
    public float spawnInterval = 2f;

    // 적이 생성될 Z 위치
    public float spawnZ = 8f;

    // 화면 좌우 끝에서 얼마나 안쪽에 생성할지
    public float sideMargin = 1f;

    // 다음 적 생성 시간
    private float nextSpawnTime = 0f;


    // =========================
    // 발악 패턴
    // =========================

    // 발악 패턴 때 적 생성 간격
    public float dangerSpawnInterval = 0.5f;

    // 발악 패턴이 이미 시작됐는지 확인
    private bool isDangerMode = false;


    // =========================
    // EnemySpawner 체력
    // =========================

    // 최대 체력
    public int maxHP = 10;

    // 현재 체력
    private int currentHP;


    // =========================
    // EnemySpawner 공격
    // =========================

    // 적 총알 프리팹
    public GameObject enemyBulletPrefab;

    // 적 총알이 생성될 위치
    public Transform enemyFirePoint;

    // 평소 공격 간격
    public float attackInterval = 2f;

    // 발악 상태 공격 간격
    public float dangerAttackInterval = 0.8f;

    // 다음 공격 시간
    private float nextAttackTime = 0f;


    // =========================
    // EnemySpawner 이동
    // =========================

    // 좌우 이동 속도
    public float moveSpeed = 2f;

    // 좌우 이동 범위
    private readonly float minX = -4f;
    private readonly float maxX = 3f;

    // 1 = 오른쪽
    // -1 = 왼쪽
    private int moveDirection = 1;


    void Start()
    {
        // 시작할 때 현재 체력을 최대 체력으로 설정
        currentHP = maxHP;
    }


    void Update()
    {
        // =========================
        // 적 생성
        // =========================

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime = Time.time + spawnInterval;
        }


        // =========================
        // EnemySpawner 공격
        // =========================

        if (Time.time >= nextAttackTime)
        {
            Attack();

            nextAttackTime = Time.time + attackInterval;
        }


        // =========================
        // EnemySpawner 좌우 이동
        // =========================

        MoveSpawner();
    }


    // =========================
    // 적 생성
    // =========================

    void SpawnEnemy()
    {
        // 화면 왼쪽 끝
        Vector3 leftPoint =
            Camera.main.ViewportToWorldPoint(
                new Vector3(
                    0f,
                    0.5f,
                    Camera.main.transform.position.y
                )
            );


        // 화면 오른쪽 끝
        Vector3 rightPoint =
            Camera.main.ViewportToWorldPoint(
                new Vector3(
                    1f,
                    0.5f,
                    Camera.main.transform.position.y
                )
            );


        // 화면 안에서 랜덤 X 위치 결정
        float randomX = Random.Range(
            leftPoint.x + sideMargin,
            rightPoint.x - sideMargin
        );


        // 적 생성 위치
        Vector3 spawnPosition =
            new Vector3(
                randomX,
                0f,
                spawnZ
            );


        // 적 생성
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
        // 일반 상태 : 가운데 1발
        if (!isDangerMode)
        {
            Instantiate(
                enemyBulletPrefab,
                enemyFirePoint.position,
                enemyFirePoint.rotation
            );
        }

        // 체력 50% 이하 : 부채꼴 3발
        else
        {
            // 가운데
            Quaternion centerRotation =
                enemyFirePoint.rotation;

            // 왼쪽으로 15도
            Quaternion leftRotation =
                enemyFirePoint.rotation
                * Quaternion.Euler(0f, -10f, 0f);

            // 오른쪽으로 15도
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
    // EnemySpawner 좌우 이동
    // =========================

    void MoveSpawner()
    {
        // 현재 방향으로 이동
        transform.position +=
            Vector3.right
            * moveDirection
            * moveSpeed
            * Time.deltaTime;


        // 오른쪽 끝에 도착
        if (transform.position.x >= maxX)
        {
            moveDirection = -1;
        }


        // 왼쪽 끝에 도착
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

        Debug.Log("Spawner HP : " + currentHP);


        // 체력이 50% 이하가 되면 발악 패턴 시작
        if (!isDangerMode && currentHP <= maxHP / 2)
        {
            isDangerMode = true;

            // 적 생성 속도 증가
            spawnInterval = dangerSpawnInterval;

            // 총알 발사 속도 증가
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
    // 승리
    // =========================

    void Victory()
    {
        Debug.Log("VICTORY");

        // VICTORY UI 활성화
        victoryText.SetActive(true);

        // EnemySpawner 파괴
        Destroy(gameObject);
    }
}