using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Imageを使うために必要
using System.Threading.Tasks;

public class AppSceneManager : MonoBehaviour
{
    public static AppSceneManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private CanvasGroup loadingCanvasGroup;

    // 変更点1: SliderではなくImageにする
    [SerializeField] private Image loadingBarImage;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (loadingCanvasGroup != null)
            {
                loadingCanvasGroup.alpha = 0;
                loadingCanvasGroup.blocksRaycasts = false;
            }

            // 初期化：ゲージを0にしておく
            if (loadingBarImage != null)
            {
                loadingBarImage.fillAmount = 0f;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        await LoadSceneRoutine(sceneName);
    }

    private async Awaitable LoadSceneRoutine(string sceneName)
    {
        await Fade(1f);

        // ロード開始時にゲージをリセット
        if (loadingBarImage != null) loadingBarImage.fillAmount = 0f;

        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // 0.9で止まる仕様なので補正
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // 変更点2: valueではなくfillAmountに入れる (0.0 ～ 1.0)
            if (loadingBarImage != null)
            {
                loadingBarImage.fillAmount = progress;
            }

            if (operation.progress >= 0.9f)
            {
                // 演出用に少し待機（一瞬で100%になると見えないため）
                if (loadingBarImage != null) loadingBarImage.fillAmount = 1f;
                await Awaitable.WaitForSecondsAsync(0.5f);

                operation.allowSceneActivation = true;
            }

            await Awaitable.NextFrameAsync();
        }

        await Fade(0f);
    }

    private async Awaitable Fade(float targetAlpha)
    {
        if (loadingCanvasGroup == null) return;

        loadingCanvasGroup.blocksRaycasts = (targetAlpha > 0.1f);

        float startAlpha = loadingCanvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            loadingCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            await Awaitable.NextFrameAsync();
        }

        loadingCanvasGroup.alpha = targetAlpha;
    }
}