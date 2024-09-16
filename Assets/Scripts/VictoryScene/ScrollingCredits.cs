using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScrollingCredits : MonoBehaviour
{
    private float vspeed = 1f;
    private TextMeshProUGUI follow;
    private TextMeshProUGUI playAgain;
    private RectTransform wordsTransform;
    private SpriteRenderer sky;
    private CanvasGroup canvasGroup;

    private IEnumerator FadeInText(float duration, TextMeshProUGUI textToUse)
    {
        Color color = Color.white;
        float startAlpha = 0f;
        float endAlpha = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            textToUse.color = color;

            yield return null;
        }
    }

    private IEnumerator FadeInCanvasGroup(float duration, CanvasGroup canvasGroup)
    {
        float startAlpha = 0f;
        float endAlpha = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = alpha;

            yield return null;
        }
    }

    private IEnumerator FadeOutSprite(float duration, SpriteRenderer sprite)
    {
        Color color = Color.white;
        float startAlpha = 1f;
        float endAlpha = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            sprite.color = color;

            yield return null;
        }
        // GameObject.Find("Spotlight").GetComponent<Light2D>().enabled = false;
    }

    void Start()
    {
        wordsTransform = GetComponent<RectTransform>();
        follow = GameObject.Find("MainMenuText").GetComponent<TextMeshProUGUI>();
        playAgain = GameObject.Find("PlayAgainText").GetComponent<TextMeshProUGUI>();
        sky = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        canvasGroup = GameObject.Find("Score").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
    public void onSkip()
    {
        vspeed *= 25;
    }
    void Update()
    {
        // Debug.Log(wordsTransform.position.y);
        if (wordsTransform.position.y <= 25)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, vspeed);
        }
        else if (wordsTransform.position.y > 25 && wordsTransform.position.y <= 32)
        {
            StartCoroutine(FadeOutSprite(2f, sky));
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(FadeInCanvasGroup(1f, canvasGroup));
            canvasGroup.interactable = true;
        }

    }
}