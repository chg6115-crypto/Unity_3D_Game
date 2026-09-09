using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 총알 속도
    public float bulletSpeed = 10f;

    // 총알 데미지
    public int damage = 1;

    // 총알 수명
    public float lifeTime = 3f;

    // 이미 적에게 맞았는지 확인
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // 이미 한 번 충돌 처리했다면 아무것도 하지 않음
        if (hasHit)
        {
            return;
        }

        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy != null)
        {
            // 이 총알은 이미 적을 맞았다고 기록
            hasHit = true;

            // 적에게 데미지
            enemy.TakeDamage(damage);

            // 총알 삭제
            Destroy(gameObject);
        }
        // Spawner 공격
        SpawnerController spawner =
            other.GetComponent<SpawnerController>();

        if (spawner != null)
        {
            hasHit = true;

            spawner.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}