using UnityEngine;
using UnityEngine.UI;

public class SpeedGauge : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("FillTypeをFilledにしたImageをセット")]
    public Image gaugeImage;

    [Header("見た目設定")]
    [Tooltip("0～1の色変化")]
    public Gradient gaugeColor;

    // 外部からこの関数を呼ぶ
    public void UpdateGauge(float value)
    {
        if (gaugeImage == null) return;

        // 1.長さを更新
        gaugeImage.fillAmount = value;

        // 2.色を更新
        gaugeImage.color = gaugeColor.Evaluate(value);
    }
}