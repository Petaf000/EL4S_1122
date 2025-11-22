using UnityEngine;
using TMPro; // TextMeshProを使うために必須

public class CountUpCanDisplay : MonoBehaviour
{
    // Inspectorで表示用のTMPオブジェクトを割り当てる
    [SerializeField] private TextMeshProUGUI countText;

    // カウントする変数
    private int currentCount = 0;

    void Start()
    {
        // ゲーム開始時に初期値（0本）を表示
        UpdateDisplay();
    }

    // 外部（ボタンや他のスクリプト）から呼ぶための関数
    public void CountUp()
    {
        currentCount++; // 値を1増やす
        UpdateDisplay(); // 表示を更新
    }

    // 指定の数にセットしたい場合（リセットなど）
    public void SetCount(int value)
    {
        currentCount = value;
        UpdateDisplay();
    }

    // テキスト表示を更新する内部処理
    private void UpdateDisplay()
    {
        if (countText != null)
        {
            // 文字列補間を使って "n本" の形式にする
            countText.text = $"{currentCount}";
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUIが割り当てられていません");
        }
    }
}