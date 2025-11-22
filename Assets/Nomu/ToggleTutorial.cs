using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
	[SerializeField]
	GameObject _toggleObject;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_toggleObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if(_toggleObject.activeSelf)
		{
			if (Input.GetMouseButtonDown(0))
			{
				_toggleObject.SetActive(false);
			}
		}
    }

	public void OnDisplay()
	{
		_toggleObject.SetActive(true);
	}
}
