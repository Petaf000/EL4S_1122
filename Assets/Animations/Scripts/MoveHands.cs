using UnityEngine;

public class MoveHands : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;

    [Header("下移動量")]
    public float downDistance = 0.5f;
    [Header("速度調整")]
    public float moveSpeed = 2f;
    [Header("高さ")]
    public float fallHeight = 0.5f;
    [Header("スケールアップ")]
    public float scaleBoost = 1.2f;
    [Header("切り替え時間")]
    public float swapInterval = 0.5f;   

    private bool isRightTurn = true;
    private float timer = 0f;
    private Vector3 rightStartPos;
    private Vector3 leftStartPos;
    private Vector3 originalScale;

    void Start()
    {
        rightStartPos = rightHand.localPosition;
        leftStartPos = leftHand.localPosition;
        originalScale = rightHand.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime * moveSpeed;

        Transform currentHand = isRightTurn ? rightHand : leftHand;
        Vector3 startPos = isRightTurn ? rightStartPos : leftStartPos;

        float t = timer / swapInterval; 

        if (t < 0.25f)
        {
            float fallT = t / 0.25f;
            currentHand.localPosition = startPos + new Vector3(0f, fallHeight * (1f - fallT), 0f);
            float scale = Mathf.Lerp(originalScale.x * scaleBoost, originalScale.x, fallT);
            currentHand.localScale = new Vector3(scale, scale, 1f);
        }
        else
        {
            float downT = (t - 0.25f) / 0.75f; 
            currentHand.localPosition = startPos + new Vector3(0f, -downDistance * downT, 0f);
            currentHand.localScale = originalScale;
        }

        if (timer >= swapInterval)
        {
            isRightTurn = !isRightTurn;
            timer = 0f;
        }
    }
}
