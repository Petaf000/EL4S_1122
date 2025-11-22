using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioMixerGroup bgmGroup;
    [SerializeField] private AudioMixerGroup seGroup;
    
    [Header("Registered Clips (Inspectorで登録)")]
    [SerializeField] private List<SoundData> bgmList;
    [SerializeField] private List<SoundData> seList;

    // 検索用の辞書
    private Dictionary<string, SoundData> bgmDict;
    private Dictionary<string, SoundData> seDict;

    [System.Serializable]
    public class SoundData
    {
        public string key;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1.0f;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        // AudioSourceの設定
        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        if (seSource == null) seSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = bgmGroup;
        seSource.outputAudioMixerGroup = seGroup;

        // リストを辞書に変換（高速化のため）
        bgmDict = new Dictionary<string, SoundData>();
        seDict = new Dictionary<string, SoundData>();

        foreach (var data in bgmList) bgmDict[data.key] = data;
        foreach (var data in seList) seDict[data.key] = data;
    }

    // --- SE 再生 ---
    public void PlaySE(string key)
    {
        if (seDict.TryGetValue(key, out SoundData data))
        {
            // PlayOneShotの第2引数で、音量スケール(0～1)を指定できる！
            // これで「このSEだけ0.5倍」みたいなことが可能
            seSource.PlayOneShot(data.clip, data.volume);
        }
        else
        {
            Debug.LogWarning($"SE '{key}' not found.");
        }
    }

    // --- BGM 再生 ---
    public void PlayBGM(string key)
    {
        if (bgmDict.TryGetValue(key, out SoundData data))
        {
            if (bgmSource.clip == data.clip && bgmSource.isPlaying) return;

            bgmSource.Stop();
            bgmSource.clip = data.clip;

            // BGMの場合はSource自体のVolumeを書き換える
            bgmSource.volume = data.volume;

            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{key}' not found.");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // Unity 6のAwaitableを使った簡易フェードアウト停止
    public async void StopBGMFadeOut(float duration = 1.0f)
    {
        float startVolume = bgmSource.volume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            await Awaitable.NextFrameAsync();
        }

        bgmSource.Stop();
        bgmSource.volume = startVolume; // 音量を戻しておく
    }
}