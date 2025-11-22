using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text coolKanValueText;

    [SerializeField]
    private Text coolKanValueTextBack;

    [SerializeField]
    private int coolKanValue = 0;

    private int coolKanValueCurrent = 0;

    [SerializeField]
    private GameObject Kan;

    [SerializeField]
    private GameObject KanParent;

    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private GameObject Mask;

    // [HideInInspector] 実行時にはこの文字列だけあれば良いのでインスペクタからは隠す
    [HideInInspector]
    [SerializeField] private string sceneToLoad;

    // #if UNITY_EDITOR ～ #endif で囲まれた部分はエディタ上でのみ有効になる
#if UNITY_EDITOR
    // インスペクタに表示するためのSceneAsset型変数
    [Header("遷移先シーン選択")] // インスペクタに見出しを表示
    [SerializeField] private SceneAsset sceneAsset; // ここにシーンファイルをD&Dする
#endif

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetKanValue(coolKanValue);
    }

    // Update is called once per frame
    void Update()
    {
        coolKanValueText.text = "冷やした缶 : " + coolKanValueCurrent + "本";
        coolKanValueTextBack.text = "冷やした缶 : " + coolKanValueCurrent + "本";
        if (KanParent.transform.position.x > (6 - (coolKanValue - 3)) * 300)
        {
            KanParent.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        }
        if(coolKanValue ==  coolKanValueCurrent)
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
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

    // OnValidateメソッドもエディタ専用
#if UNITY_EDITOR
    // インスペクタで値が変更された時などに自動で呼ばれるメソッド
    private void OnValidate()
    {
        // sceneAssetフィールドにシーンが設定されたら
        if (sceneAsset != null)
        {
            // そのシーンの名前（文字列）を sceneToLoad 変数にコピーする
            sceneToLoad = sceneAsset.name;
        }
        else
        {
            // SceneAssetが未設定なら文字列も空にする
            sceneToLoad = "";
        }
    }
#endif
}
