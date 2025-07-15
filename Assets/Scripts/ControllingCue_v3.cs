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
                float deltaX = Input.mousePosition.x - mouseStartPos.x;
                transform.RotateAround(balls[0].transform.position, Vector3.up, deltaX * 0.2f);
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