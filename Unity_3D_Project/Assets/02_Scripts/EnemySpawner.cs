using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 생성할 적
    public GameObject enemyPrefab;

    // 적 생성 간격
    public float spawnInterval = 2f;

    // 적이 생성될 Z 위치
    public float spawnZ = 8f;

    // 화면 좌우 끝에서 얼마나 안쪽에 생성할지
    public float sideMargin = 1f;

    // 다음 생성 시간
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // 화면 왼쪽 끝을 월드 좌표로 변환
        Vector3 leftPoint =
            Camera.main.ViewportToWorldPoint(
                new Vector3(0f, 0.5f, Camera.main.transform.position.y)
            );

        // 화면 오른쪽 끝을 월드 좌표로 변환
        Vector3 rightPoint =
            Camera.main.ViewportToWorldPoint(
                new Vector3(1f, 0.5f, Camera.main.transform.position.y)
            );

        // 화면 안쪽에서 랜덤 X 결정
        float randomX = Random.Range(
            leftPoint.x + sideMargin,
            rightPoint.x - sideMargin
        );

        // 적 생성
        Vector3 spawnPosition = new Vector3(randomX, 0f, spawnZ);

        Instantiate(
            enemyPrefab,
            spawnPosition,
            enemyPrefab.transform.rotation
        );
    }
}