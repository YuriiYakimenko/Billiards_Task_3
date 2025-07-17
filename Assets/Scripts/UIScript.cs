using System;
using TMPro;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    public GameObject textObjectLose; // GameObject з компонентом TextMeshProUGUI
    public GameObject textObjectWin;
    public GameObject GameTime;
    public GameObject Score;
    public PocketsDestroyer[] pocketsDestroyer;

    public void ShowGameTime() // цей метод попросив зробити ШІ
    {
        float time = Time.timeSinceLevelLoad;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        string formattedTime = $"{minutes:D2}:{seconds:D2}";
        TextMeshProUGUI textComponent = GameTime.GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            textComponent.text = formattedTime;
        }

    }

    public void ShowTextWin() // цей метод зробив сам
    {
        int sum = 0;
        //логіка: якщо за 1 шар давати 100 балів, то за 1500 балів перемога
        foreach (var pocket in pocketsDestroyer)
        {
            sum += pocket.score;
            if (sum == 1600)
            {
                textObjectWin.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        TextMeshProUGUI textComponent = Score.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = sum.ToString();
        }
    }
    public void ShowTextLose()
    {
        textObjectLose.SetActive(true); // Увімкнути об'єкт
    }
    public void HideText()
    {
        textObjectLose.SetActive(false); // Вимкнути об'єкт
        textObjectWin.SetActive(false);
    }

    void Start()
    {
        HideText(); // Сховає текст при старті
    }
    void Update()
    {
        ShowGameTime();
        ShowTextWin();
    }

}

// цей скрипт додав для приховання та відображення тексту