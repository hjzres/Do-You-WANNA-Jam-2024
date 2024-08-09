using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class DynamicTrigger2D : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private UnityEvent EnterEvent;
    [SerializeField] private UnityEvent ExitEvent;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
    
    private void OnTriggerEnter2D (Collider2D other) 
    {
        EnterEvent?.Invoke();
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        ExitEvent?.Invoke();
    }
    public void LoadScene(int sceneIndex) => GameManager.Instance.LoadScene(sceneIndex);

    public void LoadScene(string sceneName) => GameManager.Instance.LoadScene(sceneName);

    private bool HasListener(UnityEvent unityEvent, string methodName)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            if (unityEvent.GetPersistentMethodName(i).Equals(methodName))
                return true;

        return false;
    }
}
