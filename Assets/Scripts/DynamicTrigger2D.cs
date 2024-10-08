using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DynamicTrigger2D : MonoBehaviour
{
    [SerializeField] protected UnityEvent EnterEvent;
    [SerializeField] protected UnityEvent ExitEvent;
    
    protected virtual void OnTriggerEnter2D (Collider2D other) 
    {
        EnterEvent?.Invoke();
    }

    protected virtual void OnTriggerExit2D (Collider2D other)
    {
        ExitEvent?.Invoke();
    }

    protected virtual bool HasListener(UnityEvent unityEvent, string methodName)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            if (unityEvent.GetPersistentMethodName(i).Equals(methodName))
                return true;

        return false;
    }
    
    public void LoadScene(int sceneIndex) => GameManager.Instance.LoadScene(sceneIndex);

    public void LoadScene(string sceneName) => GameManager.Instance.LoadScene(sceneName);

    //public void LoadScene(SceneAsset sceneAsset) => GameManager.Instance.LoadScene(sceneAsset.name);

    public void ReloadScene() => GameManager.Instance.ReloadScene();

    public void QuitGame() => GameManager.Instance.QuitGame();

    public void PlaySoundtrack(string newSoundtrack) => AudioManager.Instance.PlaySoundtrack(newSoundtrack);

    public void PlaySoundtrack(Soundtrack newSoundtrack) => AudioManager.Instance.PlaySoundtrack(newSoundtrack);

    public void PlaySoundEffect(string newSoundEffect) => AudioManager.Instance.PlaySoundEffect(newSoundEffect);
    
    public void PlaySoundEffect(SoundEffect newSoundEffect) => AudioManager.Instance.PlaySoundEffect(newSoundEffect);

}
