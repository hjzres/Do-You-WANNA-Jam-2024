using UnityEngine;

public class GoalZone : DynamicTrigger2D, IInteractable
{
    [SerializeField] private string nextSceneName;
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
            LoadScene(nextSceneName);
        else
            return;
    }
}
