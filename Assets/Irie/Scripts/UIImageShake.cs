using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class UIImageShake : MonoBehaviour
{
    public float duration = 0.2f;   // k‚¦‚éŠÔ
    public float magnitude = 10f;   // —h‚ê• (UI‚Ípx‚È‚Ì‚Å‘å‚«‚ß)

    private RectTransform rect;
    private Vector3 originalPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    private void Update()
    {
        if(Keyboard.current.anyKey.wasPressedThisFrame)
        {
            PlayShake();
        }
    }

    public void PlayShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            rect.anchoredPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
    }
}
