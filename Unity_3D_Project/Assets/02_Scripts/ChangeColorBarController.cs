using System.Collections;
using UnityEngine;

public class ChangeColorBarController : MonoBehaviour
{
    private SpriteRenderer[] renderers;
    private float fadeTime = 0.4f;

    private void Awake()
    {
        renderers = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(OnFadeLoop));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(OnFadeLoop));
    }

    IEnumerator OnFadeLoop()
    {
        var waitforSeconds = new WaitForSeconds(fadeTime);

        while (true)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                StartCoroutine(
                    FadeEffect.Fade(
                        renderers[i],
                        1f,
                        0f,
                        fadeTime
                    )
                );
            }

            yield return waitforSeconds;

            for (int i = 0; i < renderers.Length; i++)
            {
                StartCoroutine(
                    FadeEffect.Fade(
                        renderers[i],
                        0f,
                        1f,
                        fadeTime
                    )
                );
            }

            yield return waitforSeconds;
        }
    }
}