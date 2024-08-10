using System;
using UnityEngine;

public class PatientController : MonoBehaviour
{
    [SerializeField] private SpriteAnimator spriteAnimator;
    [Range(1f, 10f)][SerializeField] private float moveSpeed = 1;
    private Rigidbody2D _rigidbody2D;
    private PlayerInputReader _playerInputReader;

    [SerializeField] private IInteractable interactable;
    [SerializeField] private bool nearInteractable = false;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _playerInputReader = PlayerInputReader.Instance;
        _playerInputReader.Initialize();

        _playerInputReader.OnMoveCharacter += ProcessMoveCharacter;
        _playerInputReader.OnInteract += ProcessInteract;
        _playerInputReader.OnPauseGame += ProcessPauseGame;
        _playerInputReader.OnSwitchRole += ProcessSwitchRole;
    }

    private void OnDisable() {
        _playerInputReader.OnMoveCharacter -= ProcessMoveCharacter;
        _playerInputReader.OnInteract -= ProcessInteract;
        _playerInputReader.OnPauseGame -= ProcessPauseGame;
        _playerInputReader.OnSwitchRole -= ProcessSwitchRole;
    }

    private void ProcessMoveCharacter(Vector2 vector) {

        _rigidbody2D.velocity = vector * moveSpeed;
        string newAnimState = "";
        bool isMoving = false;

        switch (vector)
        {
            case Vector2 behind when vector.y > 0:
                newAnimState = "Walking_Behind";
                isMoving = true;
            break;

            case Vector2 front when vector.y < 0:
                newAnimState = "Walking_Front";
                isMoving = true;
            break;

            case Vector2 left when vector.x < 0:
                newAnimState = "Walking_Left";
                isMoving = true;
            break;

            case Vector2 right when vector.x > 0:
                newAnimState = "Walking_Right";
                isMoving = true;
            break;

            case Vector2 idle when vector == Vector2.zero:
                newAnimState = "";
                isMoving = false;
            break;
        }

        spriteAnimator.ChangeAnimationState(newAnimState);
        spriteAnimator.Animator.SetBool("isMoving", isMoving);
    }

    private void ProcessInteract()
    {
        if (nearInteractable && interactable != null)
            interactable.Interact();
    }

    private void ProcessSwitchRole(string origin)
    {
        _playerInputReader.SwitchActionMap(origin);
    }

    private void ProcessPauseGame()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractable interactable))
        {
            this.interactable = interactable;
            nearInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractable interactable))
        {
            if(interactable == this.interactable)
            {
                this.interactable = null;
                nearInteractable = false;
            }
        }
    }
}
