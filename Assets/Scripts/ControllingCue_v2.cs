using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject cueVisual;
    public Rigidbody[] balls;
    public float minMagnitude;
    private float magnitude;
    private bool allStoped = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rigidbody ball in balls)
        {
            magnitude = ball.linearVelocity.magnitude;
            if (ball != null)
            {
                if (magnitude < minMagnitude)
                {
                    magnitude = 0;
                }
                else if (magnitude >= minMagnitude)
                {
                    allStoped = false;
                }
            }

        }
        cueVisual.SetActive(allStoped);
        balls[0].AddForce(Vector3.forward * 0.1f, ForceMode.Impulse);
    }
}
