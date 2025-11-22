using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : MonoBehaviour
{
    [SerializeField] Slider temprature;
    [SerializeField]TextMeshProUGUI scoretext;
    [SerializeField] Slider gauge;    // UIのスライダー
    [SerializeField] float duration = 30f;  // ゲージが0になるまでの秒数

    float timer = 0f;
    int score=0;
    bool timeout=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gauge.maxValue = duration;
        gauge.value = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        
    }
    void ChengeTemprature(int nowtemp)
    {
        temprature.value = nowtemp;
    }
    void ChengeScoreText(int addscore)
    {
        score += addscore;
        scoretext.text = addscore.ToString();
    }
    void Timer()
    {  timer += Time.deltaTime;
        gauge.value = Mathf.Clamp(duration - timer, 0, duration);
        if (gauge.value <= 0)
        {
            timeout= true;
        }
        else timeout= false;
    }
    bool GetTimeout()
        { return timeout; }
  
   


}
