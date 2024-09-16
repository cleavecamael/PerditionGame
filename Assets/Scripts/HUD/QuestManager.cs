using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public QuestObjectSystem questObjectSystem;
    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    [SerializeField] private XPSystem xpLevel;


    void Start()
    {
        text = transform.Find("QuestText").GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (SceneManager.GetActiveScene().name == "Tutorial-World")
        {
            OnResetQuest();
        }
        else if (SceneManager.GetActiveScene().name == "World-1")
        {
            OnResetQuest();
            OnNewQuest();
        }
        else if (SceneManager.GetActiveScene().name == "World-2")
        {
            OnNewQuest();
        }
    }

    public void OnNewQuest()
    {
        canvasGroup.alpha = 1f;
        text.text = questObjectSystem.GetNextQuest();
        StopCoroutine(FadeOutCanvas(questObjectSystem.timeToRead, canvasGroup));
        StartCoroutine(FadeOutCanvas(questObjectSystem.timeToRead, canvasGroup));
    }

    public void OnLevelUp()
    {
        // Debug.Log("leveltup");
        // Debug.Log("xpLevel.CurrentLevelCap " + xpLevel.CurrentLevelCap);
        // Debug.Log("xpLevel.CurrentLevel" + xpLevel.CurrentLevel);
        if (xpLevel.CurrentLevel + 1 == xpLevel.CurrentLevelCap)
            OnNewQuest();

    }

    private IEnumerator FadeOutCanvas(float duration, CanvasGroup canvasGroup)
    {
        float startAlpha = 1f;
        float endAlpha = 0f;
        yield return new WaitForSecondsRealtime(duration);

        float elapsedTime = 0f;

        while (elapsedTime < 5f)
        {
            // Debug.Log("Fade");
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / 5f);
            canvasGroup.alpha = alpha;

            yield return null;
        }
    }

    public void OnResetQuest()
    {
        questObjectSystem.ResetQuests();
    }
}