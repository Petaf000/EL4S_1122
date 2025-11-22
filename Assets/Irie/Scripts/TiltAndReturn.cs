using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TiltAndReturn : MonoBehaviour
{
    public float tiltAngle = 15f;     // ‚Ç‚ê‚­‚ç‚¢“|‚·‚©
    public float tiltTime = 0.1f;     // “|‚·‚Ü‚Å‚ÌŽžŠÔ
    public float returnTime = 0.2f;   // Œ³‚É–ß‚éŽžŠÔ

    public float yPos = 1.0f;

    public ResultManager resultManager;

    public Sprite awaImage;

    public AudioSource pushu;

    private void Start()
    {
        resultManager = GameObject.Find("ResultManager").GetComponent<ResultManager>();
    }

    private void Update()
    {

    }

    public void PlayTilt(float currentTime)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateTilt(currentTime));
    }

    private IEnumerator AnimateTilt(float currentTime)
    {
        Quaternion startRot = transform.localRotation;
        Quaternion tiltRot = Quaternion.Euler(0, 0, -tiltAngle);

        // 0) ‘Ò‚Â
        float t = 0;
        while (t < currentTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // 1) ­‚µ“|‚ê‚é
        t = 0;
        while (t < tiltTime)
        {
            t += Time.deltaTime;
            float lerp = t / tiltTime;
            transform.localRotation = Quaternion.Lerp(startRot, tiltRot, lerp);
            yield return null;
        }

        pushu.Play();
        resultManager.AddKanValue();
        GetComponent<Image>().sprite = awaImage;

        // 2) –ß‚é
        t = 0;
        while (t < returnTime)
        {
            t += Time.deltaTime;
            float lerp = t / returnTime;
            transform.localRotation = Quaternion.Lerp(tiltRot, startRot, lerp);
            yield return null;
        }

        transform.localRotation = startRot;
    }
}
