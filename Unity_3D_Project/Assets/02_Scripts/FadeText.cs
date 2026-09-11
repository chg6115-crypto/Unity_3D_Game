using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    // 페이드 속도
    public float fadeSpeed = 2f;

    private TextMeshProUGUI text;

    // 0 = 투명 쪽으로, 1 = 불투명 쪽으로
    private bool fadeIn = true;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Color color = text.color;

        if (fadeIn)
        {
            // 점점 보이게
            color.a += fadeSpeed * Time.unscaledDeltaTime;

            if (color.a >= 1f)
            {
                color.a = 1f;
                fadeIn = false;
            }
        }
        else
        {
            // 점점 안 보이게
            color.a -= fadeSpeed * Time.unscaledDeltaTime;

            if (color.a <= 0f)
            {
                color.a = 0f;
                fadeIn = true;
            }
        }

        text.color = color;
    }
}