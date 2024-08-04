using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : Singleton<PlayerInputController>
{
    private PlayerControls playerControls = new PlayerControls();
    private PlayerControls.PatientActions patientActions;
    private PlayerControls.WatcherActions watcherActions;

    public Action<Vector2> OnMoveCharacter, OnMoveCamera;
    public Action OnInteract, OnChangeCamera;
    public Action OnSwitchRole, OnPauseGame;

    protected override void Awake() {
        base.Awake();

        patientActions = playerControls.Patient;
        watcherActions = playerControls.Watcher;
    }
    
    private void OnEnable() {
        patientActions.MoveCharacter.performed += x => OnMoveCharacter?.Invoke(x.ReadValue<Vector2>());
        patientActions.MoveCharacter.canceled += x => OnMoveCharacter?.Invoke(Vector2.zero);

        patientActions.SwitchRole.started += x => OnSwitchRole?.Invoke();

        patientActions.Interact.started += x => OnInteract?.Invoke();

        patientActions.PauseGame.started += x => OnPauseGame?.Invoke();

        // //

        watcherActions.MoveCamera.performed += x => OnMoveCamera?.Invoke(x.ReadValue<Vector2>());
        watcherActions.MoveCamera.canceled += x => OnMoveCamera?.Invoke(Vector2.zero);

        watcherActions.SwitchRole.started += x => OnSwitchRole?.Invoke();

        watcherActions.ChangeCamera.performed += x => OnChangeCamera?.Invoke();

        watcherActions.PauseGame.started += x => OnPauseGame?.Invoke();
    }

    private void OnDisable() {
        patientActions.MoveCharacter.performed -= x => OnMoveCharacter?.Invoke(x.ReadValue<Vector2>());
        patientActions.MoveCharacter.canceled -= x => OnMoveCharacter?.Invoke(Vector2.zero);

        patientActions.Interact.started -= x => OnInteract?.Invoke();

        patientActions.SwitchRole.started -= x => OnSwitchRole?.Invoke();

        patientActions.PauseGame.started -= x => OnPauseGame?.Invoke();

        // //

        watcherActions.MoveCamera.performed -= x => OnMoveCamera?.Invoke(x.ReadValue<Vector2>());
        watcherActions.MoveCamera.canceled -= x => OnMoveCamera?.Invoke(Vector2.zero);

        watcherActions.ChangeCamera.performed -= x => OnChangeCamera?.Invoke();

        watcherActions.SwitchRole.started -= x => OnSwitchRole?.Invoke();

        watcherActions.PauseGame.started -= x => OnPauseGame?.Invoke();
    }

    public void SwitchActionMap() 
    {
        if(patientActions.enabled && !watcherActions.enabled) {

            patientActions.Disable();
            watcherActions.Enable();

        } else if (!patientActions.enabled && watcherActions.enabled) {

            patientActions.Enable();
            watcherActions.Disable();

        }
    }

    public void EnableAction(InputAction action) => action.Enable();

    public void EnableAction(List<InputAction> actions) {
        foreach (InputAction action in actions)
            action.Enable();
    }

    public void DisableAction(InputAction action) => action.Disable();

    public void DisableAction(List<InputAction> actions) {
        foreach (InputAction action in actions)
            action.Disable();
    }
}
