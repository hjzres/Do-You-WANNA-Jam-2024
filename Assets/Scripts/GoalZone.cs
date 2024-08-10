using UnityEngine;

public class GoalZone : DynamicTrigger2D, IInteractable2D<string>
{
    protected bool canInteract = false;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("CameraVision"))
            canInteract = true;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("CameraVision"))
            canInteract = false;
    }

    public void Interact(string sceneName)
    {
        if (canInteract)
            LoadScene(sceneName);
        else
            return;
    }
}
