using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Header("ã¸‘¬“x")]
    public float riseSpeed = 1.0f;

    [Header("‰¡—h‚ê‚Ì‹­‚³")]
    public float swayAmount = 0.1f;

    [Header("‰¡—h‚ê‚Ì‘¬“x")]
    public float swaySpeed = 2.0f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.GetComponent<RectTransform>().position;
    }

    void Update()
    {
        // ã•ûŒü‚Éã¸
        transform.GetComponent<RectTransform>().position += Vector3.up * riseSpeed * Time.deltaTime;

        // ‰¡‚É‚Ó‚ç‚Ó‚ç—h‚ê‚é
        float sway = Mathf.Sin(Time.time * swaySpeed + startPos.x) * swayAmount;
        transform.GetComponent<RectTransform>().position += new Vector3(sway, 0f, 0f);

        // ˆê’èŠÔ‚Å”jŠüi©‘R‚ÉÁ‚¦‚éj
        if (transform.GetComponent<RectTransform>().position.y > 900.0f)
        {
            transform.GetComponent<RectTransform>().position = startPos;
        }
    }
}
