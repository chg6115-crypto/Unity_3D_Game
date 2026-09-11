using UnityEngine;

public class HealthItemController : MonoBehaviour
{
    // 아래로 떨어지는 속도
    public float moveSpeed = 3f;

    // 회전 속도
    public float rotateSpeed = 180f;

    // 회복량
    public int healAmount = 1;

    // 일정 시간이 지나면 자동 삭제
    public float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 화면 위쪽에서 아래쪽으로 이동
        transform.position +=
            Vector3.back * moveSpeed * Time.deltaTime;

        // 계속 회전
        transform.Rotate(
            Vector3.up,
            rotateSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            player.Heal(healAmount);

            Destroy(gameObject);
        }
    }
}