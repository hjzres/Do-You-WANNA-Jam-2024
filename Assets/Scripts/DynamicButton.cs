using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(Button))]
[DisallowMultipleComponent]
public class DynamicButton : MonoBehaviour
{
    [SerializeField] protected string namePrefix;
    [SerializeField] protected TextMeshProUGUI textMesh;
    [SerializeField] protected Button button;

    protected const string BTN_SUFFIX = "_Button";
    public string ButtonName => namePrefix + BTN_SUFFIX;
    protected const string TMP_SUFFIX = "_TMP";
    public string TextName => namePrefix + TMP_SUFFIX;

    protected virtual void Awake() {

        button = GetComponent<Button>();

        #if UNITY_WEBGL
            if (HasListener(button.onClick, nameof(QuitGame)))
            {
                this.gameObject.SetActive(false);
                return;
            }
        #endif

        if(textMesh == null)
            return;

        this.gameObject.name = ButtonName;
        textMesh.gameObject.name = TextName;

        if(textMesh.text == "")
            textMesh.text = namePrefix;
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

    public void OnValidate()  {
        this.gameObject.name = ButtonName;

        if(textMesh == null)
            return;

        textMesh.gameObject.name = TextName;
    }

    protected bool HasListener(UnityEvent unityEvent, string methodName)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            if (unityEvent.GetPersistentMethodName(i).Equals(methodName))
                return true;

        return false;
    }
}
