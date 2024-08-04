using UnityEngine;

[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    /// <summary>
    /// The singleton instance of this class.
    /// </summary>
    public static T Instance => instance;

    protected virtual void Awake() => HandleSingleton();

    private void HandleSingleton()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
