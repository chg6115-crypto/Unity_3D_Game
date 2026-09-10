using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 총알 속도
    public float bulletSpeed = 10f;

    // 총알 데미지
    public int damage = 1;

    // 총알 수명
    public float lifeTime = 3f;

    // 이미 충돌했는지 확인
    private bool hasHit = false;


    void Start()
    {
        // 일정 시간이 지나면 총알 자동 삭제
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        // 총알을 앞쪽으로 이동
        transform.position +=
            transform.forward * bulletSpeed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("총알 충돌 : " + other.gameObject.name);

        // 플레이어와의 충돌은 무시
        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            return;
        }


        // 이미 다른 오브젝트를 맞았다면 무시
        if (hasHit)
        {
            return;
        }


        // 일반 적과 충돌
        EnemyController enemy =
            other.GetComponentInParent<EnemyController>();

        if (enemy != null)
        {
            hasHit = true;

            enemy.TakeDamage(damage);

            Destroy(gameObject);

            return;
        }


        // EnemySpawner와 충돌
        EnemySpawner spawner =
            other.GetComponentInParent<EnemySpawner>();

        if (spawner != null)
        {
            hasHit = true;

            spawner.TakeDamage(damage);

            Destroy(gameObject);

            return;
        }
    }
}