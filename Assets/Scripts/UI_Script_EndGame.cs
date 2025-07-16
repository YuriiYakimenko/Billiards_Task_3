using System.Linq;
using UnityEngine;

public class UI_Script_EndGame : MonoBehaviour
{
    public GameObject textObjectLose; // GameObject з компонентом TextMeshProUGUI
    public GameObject textObjectWin;

    public void ShowTextWin()
    {
        // думаю зробити логіку: якщо за 1 шар давати 100 балів, то за 1500 балів перемога
        // або створити тут масив шарів і коли balls.Length == 0 тоді перемога
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
}

// цей скрипт додав для приховання та відображення тексту