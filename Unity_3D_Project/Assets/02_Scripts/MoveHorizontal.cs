using System.Collections;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    [SerializeField]
    private float start;

    [SerializeField]
    private float end;

    [SerializeField]
    private float unitPerSecond = 1f;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    IEnumerator Process()
    {
        while (true)
        {
            // start → end
            yield return StartCoroutine(MoveTo(start, end));

            // end → start
            yield return StartCoroutine(MoveTo(end, start));
        }
    }

    IEnumerator MoveTo(float start, float end)
    {
        float percent = 0f;

        // 총 이동 거리
        float distance = Mathf.Abs(end - start);

        // 이동 시간 = 거리 / 속도
        float moveTime = distance / unitPerSecond;

        while (percent < 1f)
        {
            percent += Time.deltaTime / moveTime;

            Vector3 position = transform.position;

            // X 위치를 start에서 end까지 부드럽게 이동
            position.x = Mathf.Lerp(start, end, percent);

            // 실제 오브젝트 위치 변경
            transform.position = position;

            // 다음 프레임까지 대기
            yield return null;
        }
    }
}