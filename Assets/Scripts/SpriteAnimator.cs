using UnityEngine;

public class SpriteAnimator : MonoBehaviour, ISoundPlayer
{
    [SerializeField] private Animator animator;
    public Animator Animator => animator;

    [SerializeField] private string currentAnimState;
    public string CurrentAnimState => currentAnimState;

    private void Awake()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newAnimState)
    {
        //Stop the same animation from overriding itself
        if(currentAnimState == newAnimState)
            return;
        
        //Reassign the current state to the new state
        currentAnimState = newAnimState;
        if(currentAnimState == "")
            return;

        //Play the animation
        animator.Play(newAnimState);
    }

    public void PlaySoundEffect(string soundEffectName) => AudioManager.Instance.PlaySoundEffect(soundEffectName);

    public void PlaySoundtrack(string soundtrackName)
    {
        throw new System.NotImplementedException();
    }
}
