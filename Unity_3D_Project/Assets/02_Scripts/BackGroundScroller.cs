using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;

    // 배경 한 장의 길이
    public float backgroundLength = 20f;

    void Update()
    {
        transform.position +=
            Vector3.back * scrollSpeed * Time.deltaTime;

        // 화면 아래로 완전히 내려가면
        // 다시 위쪽으로 이동
        if (transform.position.z <= -backgroundLength)
        {
            transform.position +=
                Vector3.forward * backgroundLength * 2f;
        }
    }
}