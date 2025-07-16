using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public UI_Script_EndGame endGame;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MainBall") && other.gameObject != null)
        {
            endGame.ShowTextLose();
            Time.timeScale = 0f;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Balls") && other.gameObject != null)
        {
            Destroy(other.gameObject);
        }
    }
}

// цей скрипт використовую для знищення шарів, які потрапляють у лузу
// та закінчення гри, якщо білий шар потрапив у лузу
// з затримкою не дуже гарно виходить, 
// я робив щоб шар завмирав і через секунду зникав, але виглядає не дуже