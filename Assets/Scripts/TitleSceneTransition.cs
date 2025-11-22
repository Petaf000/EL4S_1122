using UnityEngine;

public class TitleSceneTransition : MonoBehaviour
{
	[SerializeField]
	private AppSceneManager _sceneManager;

	[SerializeField]
	private AudioManager _audioManager;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		_audioManager.PlayBGM("気分上々ジャズ");
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SceneTransition(string sceneName)
	{
		if(sceneName == "")
		{
			Debug.Log("Scene名がありません");
			return;
		}

		_sceneManager.LoadScene(sceneName);
		_audioManager.StopBGM();
	}
}
