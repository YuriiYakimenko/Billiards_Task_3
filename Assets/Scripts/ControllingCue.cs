using UnityEngine;

public class ControllingCue : MonoBehaviour
{
    public GameObject cueBall; // Основна куля (біла)
    public GameObject[] balls; // Всі кулі на столі
    public float cueOffset = 1.0f; // Відстань між києм і кулею
    public float minVelocity = 1.0f; // Мінімальна швидкість для зупинки кулі
    public Transform strikePoint; // Невидимий об'єкт для удару (перед києм)
    public float rotationSpeed = 100f; // Швидкість обертання кия мишкою
    public float minPower = 2f, maxPower = 20f; // Мін/макс сила удару

    private bool cueActive = true;
    private float currentAngle = 0f;
    private float shotPower = 5f;
    void Start()
    {
        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        ShowCue();
        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
    void Update()
    {
        // Перевіряємо, чи всі кулі зупинились
        bool allStopped = true;
        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log(ball.name + " velocity: " + rb.linearVelocity.magnitude);
                if (rb.linearVelocity.magnitude > 0 && rb.linearVelocity.magnitude < minVelocity)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                if (rb.linearVelocity.magnitude > minVelocity)
                {
                    allStopped = false;
                }
            }
            else
            {
                Debug.LogWarning(ball.name + " не має Rigidbody!");
            }
        }

        // Показуємо/ховаємо кий
        if (allStopped && !cueActive)
            ShowCue();
        else if (!allStopped && cueActive)
            HideCue();

        // Якщо кий активний — позиціонуємо і обертаємо його навколо кулі
        if (cueActive && cueBall != null)
        {
            // Обертання мишкою
            float mouseX = Input.GetAxis("Mouse X");
            if (Input.GetMouseButton(1)) // Права кнопка миші для обертання
                currentAngle += mouseX * rotationSpeed * Time.deltaTime;

            // Позиція кия позаду кулі по центру, обертається навколо кулі
            Vector3 offset = Quaternion.Euler(0, currentAngle, 0) * Vector3.back * cueOffset;
            transform.position = cueBall.transform.position + offset;
            transform.LookAt(cueBall.transform.position);

            // Позиціонуємо strikePoint перед києм (по напрямку удару)
            if (strikePoint != null)
                strikePoint.position = transform.position + transform.forward * (cueOffset * 0.8f);

            // Регулювання сили удару коліщатком миші
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                shotPower = Mathf.Clamp(shotPower + scroll * 10f, minPower, maxPower);
            }

            // Удар по кулі (ліва кнопка миші)
            if (Input.GetMouseButtonDown(0))
            {
                Strike();
            }
        }
        Debug.Log("allStopped: " + allStopped + ", cueActive: " + cueActive);
    }

    void Strike()
    {
        if (strikePoint != null && cueBall != null)
        {
            Rigidbody rb = cueBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 dir = (cueBall.transform.position - strikePoint.position).normalized;
                rb.AddForce(dir * shotPower, ForceMode.Impulse);
                OnCueStrike();
            }
        }
    }

    public void OnCueStrike()
    {
        HideCue();
    }

    void HideCue()
    {
        cueActive = false;
        gameObject.SetActive(false);
    }

    void ShowCue()
    {
        cueActive = true;
        gameObject.SetActive(true);
    }
}