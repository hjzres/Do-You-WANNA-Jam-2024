using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class DynamicTrigger2D : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D boxCollider2D;
    [SerializeField] protected UnityEvent EnterEvent;
    [SerializeField] protected UnityEvent ExitEvent;

    protected virtual void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
    
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

}
