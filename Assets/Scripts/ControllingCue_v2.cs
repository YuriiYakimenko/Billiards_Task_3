using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject cueVisual;
    public Rigidbody[] balls;
    public float minMagnitude;
    public float strikeForce;
    private bool allStoped;
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
        if (cueVisual.activeSelf && Input.GetMouseButton(0))
        {
            balls[0].AddForce(Quaternion.AngleAxis(90f, Vector3.back) * cueVisual.transform.forward * strikeForce, ForceMode.Impulse);
        }

    }
}
