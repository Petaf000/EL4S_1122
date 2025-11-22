using UnityEngine;
using UnityEngine.UI;

// ★これをつけると、再生していないとき(エディタ編集モード)でも動くようになります
[ExecuteAlways]
public class GaugeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image bgNormal;
    [SerializeField] private Image bgAlert;

    [Header("Settings")]
    [SerializeField] private Gradient fillColorGradient;
    [SerializeField, Range(0f, 1f)] private float switchThreshold = 0.7f;
    [SerializeField, Range(0.01f, 0.5f)] private float transitionSmoothness = 0.1f;

    [Header("Visual Settings")]
    [SerializeField, Range(0f, 1f)] private float minVisualFill = 0.2f;

    private void Start()
    {
        // ゲーム再生中だけイベント登録を行う
        if (Application.isPlaying && slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(slider.value);
        }
    }

    // ★追加: 毎フレームチェックする処理
    private void Update()
    {
        // 「再生モードじゃない時（エディタ編集中）」だけ、
        // 強制的に値を更新して見た目を同期させる
        if (!Application.isPlaying && slider != null)
        {
            OnSliderValueChanged(slider.value);
        }

        slider.value = (float)GameManager.Instance?.CurrentValue;
    }

    public void OnSliderValueChanged(float value)
    {
        // 1. ゲージの伸縮 (0.2 ~ 1.0 の範囲で動く)
        if (fillImage != null)
        {
            fillImage.fillAmount = Mathf.Lerp(minVisualFill, 1.0f, value);
            fillImage.color = fillColorGradient.Evaluate(value);
        }

        // 2. 背景の切り替え
        if (bgNormal != null && bgAlert != null)
        {
            float blendFactor = Mathf.InverseLerp(switchThreshold, switchThreshold + transitionSmoothness, value);
            SetImageAlpha(bgNormal, 1f - blendFactor);
            SetImageAlpha(bgAlert, blendFactor);
        }
    }

    private void SetImageAlpha(Image targetImage, float alpha)
    {
        if (targetImage == null) return;

        Color c = targetImage.color;
        c.a = alpha;
        targetImage.color = c;
    }
}