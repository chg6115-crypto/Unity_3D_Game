using System.Collections;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 end;
    [SerializeField]
    private float resizeTime = 1f;

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
            yield return StartCoroutine(ScaleTo(start, end));

            yield return StartCoroutine(ScaleTo(end, start));
        }
    }

    IEnumerator ScaleTo(Vector3 start, Vector3 end)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / resizeTime;
            transform.localScale = Vector3.Lerp(start, end, percent);
            yield return null;
        }
    }
}