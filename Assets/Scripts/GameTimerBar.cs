using UnityEngine;
using UnityEngine.UI; // Sliderを扱うために必要

public class GameTimerBar : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("操作するスライダー本体をセット")]
    public Slider timerSlider;

    [Tooltip("色を変えたい中の画像（Wave1, Wave2など）を全てここにセット")]
    public Image[] fillImages; // 複数の画像をまとめて色変えできるように配列にしました

    [Header("オプション")]
    [Tooltip("時間経過による色の変化（右側が残り時間たくさん、左側がピンチ）")]
    public Gradient barColor;

    void Update()
    {
        if (GameManager.Instance == null) return;

        // 0.0 〜 1.0 の比率を計算
        float ratio = GameManager.Instance.CurrentTime / GameManager.Instance.timeLimit;

        // 変更点1: Image.fillAmount ではなく Slider.value に入れる
        timerSlider.value = ratio;

        // 変更点2: 色の更新（登録された全ての画像に適用）
        Color currentColor = barColor.Evaluate(ratio);

        foreach (var img in fillImages)
        {
            if (img != null)
            {
                // グラデーションの色を画像に適用
                // ※もし波ごとに透明度を変えている場合、Gradient側のAlpha値が上書きされるので注意してください
                img.color = currentColor;
            }
        }
    }
}