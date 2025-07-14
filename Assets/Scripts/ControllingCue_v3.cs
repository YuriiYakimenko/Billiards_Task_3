using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript2 : MonoBehaviour
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
        foreach (Rigidbody ball in balls)
        {
            float magnitude = ball.linearVelocity.magnitude;
            if (ball != null)
            {
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
                // Отримуємо точку на площині столу під мишкою
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane tablePlane = new Plane(Vector3.up, balls[0].transform.position); // площина столу (Y)
                float enter;
                if (tablePlane.Raycast(ray, out enter))
                {
                    Vector3 mouseWorldPos = ray.GetPoint(enter);

                    // Вектор від кулі до миші
                    Vector3 dir = (mouseWorldPos - balls[0].transform.position).normalized;

                    // Позиція кия на відстані 1.5f від кулі у напрямку миші
                    cueVisual.transform.position = balls[0].transform.position + dir * 1.5f;

                    // Повертаємо кий на кулю
                    cueVisual.transform.LookAt(balls[0].transform.position, Vector3.up);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 hitDirection = (balls[0].transform.position - cueVisual.transform.position).normalized;
                balls[0].AddForce(hitDirection * strikeForce, ForceMode.Impulse);
            }
            // if (Input.GetMouseButtonUp(0))
            // {
            //     balls[0].AddForce(Quaternion.AngleAxis(90f, Vector3.back) * cueVisual.transform.forward * strikeForce, ForceMode.Impulse);
            // }
        }
    }
}