using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionReference dragRef;
    public SpeedGauge speedGauge;
    public MoveHands moveHands;
    [Header("設定")]
    public float maxSpeedThreshold = 25.0f;
    public float accelerationSpeed = 20.0f;
    public float brakeSpeed = 20.0f; // 上入力時の減速
    public float decaySpeed = 1.0f; // 持ち上げ中の減衰

    private float _currentSpeed = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Input Actionを「Delta/Y」に戻して、上下の値を取れるようにしておく
        float rawY = dragRef.action.ReadValue<float>();

        // ■ 判定ロジック
        if (rawY < -0.1f)
        {
            // 1. 下入力（ドラッグ）: 加速
            // 絶対値に変換して正規化
            float inputMagnitude = Mathf.Abs(rawY);
            float targetInput = Mathf.Clamp01(inputMagnitude / maxSpeedThreshold);

            _currentSpeed = Mathf.Lerp(_currentSpeed, targetInput, Time.deltaTime * accelerationSpeed);
        }
        else if (rawY > 0.1f)
        {
            // 2. 上入力（接地したまま戻そうとした）: ペナルティ！
            // ここを通るとスピードがガクッと落ちるため、こすり操作ができなくなる
            // "移動量その分マイナス" ではなく "勢いを殺す"
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, Time.deltaTime * brakeSpeed); // 急減速
            //_currentSpeed = 0.0f; // 0に固定
        }
        else
        {
            // 3. 停止/持ち上げ中 (rawY がほぼ 0)
            // ここを通るのが正解のルート。「持ち上げ」ていればここに来る。
            // 緩やかに減衰させる（コンボをつなぐ猶予）
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, Time.deltaTime * decaySpeed);
        }

        // 値の確認
        if (_currentSpeed > 0.01f)
        {
            Debug.Log($"現在のチャージ: {_currentSpeed:F2}");
        }

        if (speedGauge != null)
        {
            speedGauge.UpdateGauge(_currentSpeed);
            moveHands.moveHands(_currentSpeed);
        }
    }

    void OnEnable() => dragRef.action.Enable();
    void OnDisable() => dragRef.action.Disable();
}