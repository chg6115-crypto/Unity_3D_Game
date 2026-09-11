using UnityEngine;

public class RotateToAxis : MonoBehaviour
{
    [Header("회전 대상, 회전 축 [회전 대상이 null이면 자신이 회전]")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 axis = Vector3.forward;

    [Header("특정 속도로 회전")]
    [SerializeField]
    private float rotateSpeed = 100f;

    [Header("임의의 속도로 회전")]
    [SerializeField]
    private bool isRandomSpeed = false;

    [SerializeField]
    private float minSpeed = 50;
    [SerializeField]
    private float maxSpeed = 200f;

    private void Awake()
    {
        if (target == null) target = transform;

        if (isRandomSpeed == true)
        {
            rotateSpeed = Random.Range(minSpeed, maxSpeed);
        }
    }

    private void Update()
    {
        target.Rotate(axis, rotateSpeed * Time.deltaTime);
    }
}