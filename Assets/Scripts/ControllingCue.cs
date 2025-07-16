using Unity.VisualScripting;
using UnityEngine;

public class ControllingCue : MonoBehaviour
{
    public GameObject cueVisual;
    public Rigidbody[] balls;
    public float minMagnitude;
    public float strikeForce;
    private bool allStoped;

    private Vector3 mouseStartPos;

    void Update()
    {
        allStoped = true;
        if (balls[0] != null)
        {
            transform.position = balls[0].transform.position; // думаю, що краще сюди поставити
            // так кий після зупинки куль буде завжди біля білої кулі.
            Vector3 directionBack = transform.up; // через те, що я змінив вісь Z, треба рухати по Y
            float distanceBack = 1.0f; // відстань на яку відтягую
            transform.position += directionBack * distanceBack; // відтягую кий назад
        }
        foreach (Rigidbody ball in balls)
        {

            if (ball != null)
            {
                float magnitude = ball.linearVelocity.magnitude;
                if (magnitude < minMagnitude)
                {
                    ball.linearVelocity = Vector3.zero;
                    ball.angularVelocity = Vector3.zero;
                }
                else
                {
                    allStoped = false;
                }
            }
        }
        cueVisual.SetActive(allStoped);

        if (cueVisual.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseStartPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                float deltaX = Input.mousePosition.x - mouseStartPos.x;
                transform.RotateAround(balls[0].transform.position, Vector3.up, deltaX); // змінив оберти
                mouseStartPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 hitDirection = (balls[0].transform.position - cueVisual.transform.position).normalized;
                balls[0].AddForce(hitDirection * strikeForce, ForceMode.Impulse);
            }
        }
    }
}