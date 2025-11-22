using TMPro;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    [SerializeField]TextMeshPro temprature;
    [SerializeField]TextMeshPro score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChengeTempratureText(int nowtemp)
    {
        temprature.text = nowtemp.ToString();
    }
    void ChengeScoreText(int nowscore)
    {
        score.text = nowscore.ToString();
    }
}
