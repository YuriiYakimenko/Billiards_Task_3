using System.Collections;
using UnityEngine;

public class PocketsDestroyer : MonoBehaviour
{
    public UI_Script endGame;
    public int score;

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Миттєво зупинити кулю
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("MainBall") && other.gameObject != null)
        {
            endGame.ShowTextLose();
            Time.timeScale = 0f;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Balls") && other.gameObject != null)
        {
            StartCoroutine(DestroyWithDelay(other.gameObject)); // виклик затримки
            score += 100;
        }
    }
    IEnumerator DestroyWithDelay(GameObject obj)
    {
        // затримка на знищення
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
    }
}

// цей скрипт використовую для знищення шарів, які потрапляють у лузу
// та закінчення гри, якщо білий шар потрапив у лузу