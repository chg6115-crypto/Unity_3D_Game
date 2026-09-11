using UnityEngine;

public class HealthItemSpawner : MonoBehaviour
{
    // 생성할 체력 아이템
    public GameObject healthItemPrefab;

    // 체력 아이템 생성 간격
    public float spawnInterval = 10f;

    // 아이템이 생성될 화면 위쪽 Z 위치
    public float spawnZ = 8f;

    // 생성 가능한 좌우 범위
    public float minX = -3f;
    public float maxX = 3f;

    // 다음 생성 시간
    private float nextSpawnTime;

    void Start()
    {
        // 게임 시작하자마자 생성되지 않고
        // spawnInterval 이후 처음 생성
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // 시작 화면 등 게임이 멈춰있으면 생성하지 않음
        if (Time.timeScale == 0f)
        {
            return;
        }

        // 생성 시간이 되면 체력 아이템 생성
        if (Time.time >= nextSpawnTime)
        {
            SpawnHealthItem();

            nextSpawnTime =
                Time.time + spawnInterval;
        }
    }

    void SpawnHealthItem()
    {
        // 좌우 위치 랜덤 결정
        float randomX =
            Random.Range(minX, maxX);

        // 생성 위치
        Vector3 spawnPosition =
            new Vector3(
                randomX,
                0f,
                spawnZ
            );

        // 체력 아이템 생성
        Instantiate(
            healthItemPrefab,
            spawnPosition,
            healthItemPrefab.transform.rotation
        );
    }
}