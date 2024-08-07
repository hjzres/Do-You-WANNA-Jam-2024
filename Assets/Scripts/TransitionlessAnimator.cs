using UnityEngine;

public class TransitionlessAnimator : MonoBehaviour
{
    private Animator _animator;
    public string CurrentAnimState;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newAnimState)
    {
        //Stop the same animation from overriding itself
        if(CurrentAnimState == newAnimState)
            return;
        
        //Play the animation
        _animator.Play(newAnimState);

        //Reassign the current state to the new state
        CurrentAnimState = newAnimState;
    }
}
