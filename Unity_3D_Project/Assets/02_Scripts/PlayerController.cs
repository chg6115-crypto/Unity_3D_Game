using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    public float moveSpeed = 5f;

    void Update()
    {
        // WASD 입력값을 저장할 변수
        Vector2 input = Vector2.zero;

        // W를 누르면 앞으로 이동
        if (Keyboard.current.wKey.isPressed)
        {
            input.y += 1;
        }

        // S를 누르면 뒤로 이동
        if (Keyboard.current.sKey.isPressed)
        {
            input.y -= 1;
        }

        // A를 누르면 왼쪽으로 이동
        if (Keyboard.current.aKey.isPressed)
        {
            input.x -= 1;
        }

        // D를 누르면 오른쪽으로 이동
        if (Keyboard.current.dKey.isPressed)
        {
            input.x += 1;
        }

        // 대각선 이동이 더 빨라지는 것을 방지
        input = input.normalized;

        // 실제 플레이어 이동
        Vector3 movement = new Vector3(input.x, 0f, input.y);

        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}