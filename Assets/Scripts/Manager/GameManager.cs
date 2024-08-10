using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
	// public Camera Cam1, Cam2;
	// public Text State;
	// [SerializeField] private float rate;
	// private bool _inside = true;
	
	// protected override void Awake()
	// {
	// 	base.Awake();
		
	// 	Cam1.enabled = true;
	// 	Cam2.enabled = false;
	// 	InvokeRepeating("FlashText", rate, rate);
	// }
	
	// public void SwitchCamera()
	// {
	// 	Cam1.enabled = !Cam1.enabled;
	// 	Cam2.enabled = !Cam2.enabled;
	// 	_inside = !_inside;
	// }
	
	// void Update()
	// {
	// 	if (Input.GetKeyDown(KeyCode.C))
	// 	{
	// 		SwitchCamera();
	// 		if(_inside)
	// 		{
	// 			State.text = "Inside";
	// 		} else {
	// 			State.text = "Out";
	// 		}
	// 	}
	// }
	
	// private void FlashText()
	// {
	// 	State.gameObject.SetActive(!State.gameObject.activeSelf);
	// }

	public void LoadScene(int sceneIndex) => StartCoroutine(LoadSceneAsync(sceneIndex));
 
    public void LoadScene(string sceneName) => StartCoroutine(LoadSceneAsync(sceneName));
    
    public void ReloadScene() => LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void QuitGame() {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
            yield return null;
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
            yield return null;
    }	
}
