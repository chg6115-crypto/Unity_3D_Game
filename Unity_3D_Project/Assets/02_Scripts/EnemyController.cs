using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // =========================
    // 적 체력
    // =========================

    // 적 최대 체력
    public int maxHP = 3;

    // 적 현재 체력
    private int currentHP;


    // =========================
    // 적 이동
    // =========================

    // 적 이동 속도
    public float moveSpeed = 2f;


    void Start()
    {
        // 적이 생성될 때 현재 체력을 최대 체력으로 설정
        currentHP = maxHP;
    }


    void Update()
    {
        // 적을 -Z 방향으로 계속 이동
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }


    // =========================
    // 적이 데미지를 받는 기능
    // =========================

    public void TakeDamage(int damage)
    {
        // 현재 체력에서 받은 데미지만큼 감소
        currentHP -= damage;

        Debug.Log("Enemy HP : " + currentHP);

        // 체력이 0 이하가 되면 적 삭제
        // 체력이 0 이하가 되면 적 삭제
        // 체력이 0 이하가 되면 적 삭제
        if (currentHP <= 0)
        {
            PlayerController player =
                FindAnyObjectByType<PlayerController>();

            if (player != null)
            {
                // 적 1마리 처치 → 점수 1점 획득
                player.AddScore(1);
            }

            Destroy(gameObject);
        }
    }


    // =========================
    // 플레이어와 충돌
    // =========================

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트에 PlayerController가 있는지 확인
        PlayerController player = other.GetComponent<PlayerController>();

        // PlayerController가 있다면 플레이어와 충돌한 것
        if (player != null)
        {
            // 플레이어에게 1 데미지
            player.TakeDamage(1);

            // 충돌한 적 삭제
            Destroy(gameObject);
        }
    }
}