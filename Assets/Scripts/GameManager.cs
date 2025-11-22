using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要

public class GameManager : MonoBehaviour
{
    // どこからでも GameManager.Instance でアクセスできるようにする（シングルトン）
    public static GameManager Instance { get; private set; }

    [Header("ゲームバランス設定")]
    [Tooltip("1つの対象(缶)をクリアするのに必要なチャージ量")]
    public float targetValue = 100.0f;

    [Tooltip("制限時間（秒）")]
    public float timeLimit = 60.0f;

    [Tooltip("リザルトシーンの名前")]
    public string resultSceneName = "ResultScene";

    [Tooltip("リザルトに渡す時のPlayerPrefsのキー名")]
    public string scoreKey = "LastGameScore";

    public CountUpCanDisplay countUpCanDisplay;

    public float CurrentTime => _currentTime;
    public float CurrentValue => 1 - (_currentValue / targetValue);

    // 内部変数
    private float _currentValue = 0f;
    private float _currentTime = 0f;
    private int _completedCount = 0; // これをリザルトに渡す
    private bool _isPlaying = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _currentTime = timeLimit;
        _currentValue = 0f;
        _completedCount = 0;
        _isPlaying = true;
    }

    private void Update()
    {
        if (!_isPlaying) return;

        // --- タイマー減算 ---
        _currentTime -= Time.deltaTime;

        // 0秒になったら終了
        if (_currentTime <= 0f)
        {
            GameEnd();
        }
    }

    // PlayerInputから値を加算する関数
    public void AddProgress(float amount)
    {
        if (!_isPlaying) return;

        _currentValue += amount;

        // ② 一定値貯まったら NextCan を呼ぶ
        if (_currentValue >= targetValue)
        {
            NextCan();
        }
    }

    // ③ 次のものへ移行（値を初期化してカウントアップ）
    private void NextCan()
    {
        _completedCount++; // スコア加算
        _currentValue = 0f; // 蓄積値をリセット

        countUpCanDisplay?.CountUp();

        Debug.Log($"クリア！ 現在の個数: {_completedCount}個");

        // ★ここで「次の缶を出す処理」や「SE」を呼ぶと良いです
        // SpawnNextCan(); 
    }

    // タイムアップ時の処理
    private void GameEnd()
    {
        _isPlaying = false;
        _currentTime = 0f;

        Debug.Log($"タイムアップ！ 最終スコア: {_completedCount}");

        // ★リザルト遷移前に PlayerPrefs に保存
        PlayerPrefs.SetInt(scoreKey, _completedCount);
        PlayerPrefs.Save();

        // リザルトシーンへ
        AppSceneManager.Instance.LoadScene(resultSceneName);
    }
}