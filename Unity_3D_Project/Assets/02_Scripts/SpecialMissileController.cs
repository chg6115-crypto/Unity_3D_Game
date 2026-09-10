using System.Collections.Generic;
using UnityEngine;

public class SpecialMissileController : MonoBehaviour
{
    // 미사일 속도
    public float missileSpeed = 12f;

    // 미사일 데미지
    public int damage = 3;

    // 일정 시간 후 자동 삭제
    public float lifeTime = 5f;

    // 이미 맞힌 일반 적 목록
    private HashSet<EnemyController> hitEnemies =
        new HashSet<EnemyController>();

    // EnemySpawner를 이미 맞혔는지
    private bool hitSpawner = false;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        transform.position +=
            Vector3.forward
            * missileSpeed
            * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        // =========================
        // 플레이어 충돌 무시
        // =========================

        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            return;
        }


        // =========================
        // 일반 Enemy
        // =========================

        EnemyController enemy =
            other.GetComponentInParent<EnemyController>();

        if (enemy != null)
        {
            // 이미 맞힌 적이면 데미지를 주지 않음
            if (hitEnemies.Contains(enemy))
            {
                return;
            }

            // 처음 맞힌 적이므로 목록에 추가
            hitEnemies.Add(enemy);

            enemy.TakeDamage(damage);

            // 미사일은 삭제하지 않음
            return;
        }


        // =========================
        // EnemySpawner
        // =========================

        EnemySpawner spawner =
            other.GetComponentInParent<EnemySpawner>();

        if (spawner != null)
        {
            // 이미 Spawner를 맞혔다면 무시
            if (hitSpawner)
            {
                return;
            }

            hitSpawner = true;

            spawner.TakeDamage(damage);

            // 미사일은 삭제하지 않음
            return;
        }
    }
}