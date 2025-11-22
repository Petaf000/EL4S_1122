using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text coolKanValueText;

    [SerializeField]
    private int coolKanValue = 0;

    private int coolKanValueCurrent = 0;

    [SerializeField]
    private GameObject Kan;

    [SerializeField]
    private GameObject KanParent;

    [SerializeField]
    private float speed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetKanValue(coolKanValue);
    }

    // Update is called once per frame
    void Update()
    {
        coolKanValueText.text = "—â‚â‚µ‚½ŠÊ : " + coolKanValueCurrent + "–{";
        if(KanParent.transform.position.x > (6 - (coolKanValue - 3)) * 300)
            KanParent.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    public void SetKanValue(int value)
    {
        coolKanValue = value;

        for (int i = 0; i < coolKanValue; i++)
        {
            GameObject a = Instantiate(Kan, KanParent.transform);
            a.GetComponent<RectTransform>().position = new Vector3(200.0f + (300.0f * i), 200.0f, 0.0f);
            TiltAndReturn tar = a.GetComponent<TiltAndReturn>();
            tar.PlayTilt((tar.tiltTime + tar.returnTime) * i);
        }
    }

    public void AddKanValue()
    {
        coolKanValueCurrent++;
    }


}
