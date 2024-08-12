using UnityEngine;
using UnityEngine.Events;

public class GoalZone : DynamicTrigger2D, IInteractable
{
    [SerializeField] private UnityEvent interactEvent;
    [SerializeField] private bool canInteract = false;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Camera Vision"))
            canInteract = true;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Camera Vision"))
            canInteract = false;
    }

    public void Interact()
    {
        if (canInteract)
            interactEvent?.Invoke();
    }
}
