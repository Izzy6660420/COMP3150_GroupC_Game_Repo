using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Text text;

    void Start()
    {
        Player.instance.GameOverEvent += DisplayText;
        Player.instance.RespawnEvent += HideText;
        text = GetComponent<Text>();
        text.enabled = false;
    }

    void DisplayText()
    {
        text.color = Color.white;
        text.enabled = true;
    }
    void HideText()
    {
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        var percent = 0f;
        var initColor = text.color;

        while (percent < 1)
        {
            percent += Time.deltaTime;
            text.color = Color.Lerp(initColor, Color.clear, percent);
            yield return null;
        }
        text.enabled = false;
    }
}
