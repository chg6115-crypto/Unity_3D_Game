using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // 최대 체력
    public int maxHP = 10;

    // 현재 체력
    private int currentHP;

    // 적 프리팹
    public GameObject enemyPrefab;

    // 일반 상태 스폰 간격
    public float normalSpawnInterval = 2f;

    // 체력이 낮을 때 스폰 간격
    public float dangerSpawnInterval = 0.5f;

    // 적이 생성될 위치 범위
    public float minX = -5f;
    public float maxX = 5f;

    public float spawnZ = 10f;

    // 다음 스폰 시간
    private float nextSpawnTime = 0f;


    void Start()
    {
        currentHP = maxHP;
    }


    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            // HP가 20 이하라면 빠르게 스폰
            if (currentHP <= 20)
            {
                nextSpawnTime = Time.time + dangerSpawnInterval;
            }
            else
            {
                nextSpawnTime = Time.time + normalSpawnInterval;
            }
        }
    }


    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPosition =
            new Vector3(randomX, 0f, spawnZ);

        Instantiate(
            enemyPrefab,
            spawnPosition,
            enemyPrefab.transform.rotation
        );
    }


    // 총알에 맞았을 때 호출
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log("Spawner HP : " + currentHP);

        if (currentHP <= 0)
        {
            Victory();
        }
    }


    void Victory()
    {
        Debug.Log("VICTORY");

        Destroy(gameObject);
    }
}