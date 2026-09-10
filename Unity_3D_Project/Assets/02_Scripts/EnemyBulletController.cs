using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    // 적 총알 이동 속도
    public float bulletSpeed = 5f;

    // 플레이어에게 줄 데미지
    public int damage = 1;

    // 총알 수명
    public float lifeTime = 5f;

    // 중복 충돌 방지
    private bool hasHit = false;


    void Start()
    {
        // 일정 시간이 지나면 자동 삭제
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        // 총알이 바라보는 방향으로 이동
        transform.position +=
            -transform.forward * bulletSpeed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        // 이미 충돌 처리한 총알이면 무시
        if (hasHit)
        {
            return;
        }


        // 플레이어 찾기
        PlayerController player =
            other.GetComponentInParent<PlayerController>();


        // 플레이어와 충돌했다면
        if (player != null)
        {
            hasHit = true;

            // 플레이어에게 데미지
            player.TakeDamage(damage);

            // 총알 삭제
            Destroy(gameObject);
        }
    }
}