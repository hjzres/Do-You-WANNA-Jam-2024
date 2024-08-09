using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : Singleton<PlayerInputReader>
{
    private PlayerControls _playerControls;
    private PlayerControls.PatientActions _patientActions;
    private PlayerControls.WatcherActions _watcherActions;

    public Action<Vector2> OnMoveCharacter, OnMoveCamera;
    public Action OnInteract, OnChangeCamera, OnPauseGame;
    public Action<string> OnSwitchRole;

    public const string PATIENT_ORIGIN = "Patient";
    public const string WATCHER_ORIGIN = "Watcher";

    protected override void Awake() {
        base.Awake();

        _playerControls = new PlayerControls();
        _patientActions = _playerControls.Patient;
        _watcherActions = _playerControls.Watcher;

        _patientActions.Enable();
        _watcherActions.Disable();
    }
    
    private void OnEnable() {
        _patientActions.MoveCharacter.performed += x => OnMoveCharacter?.Invoke(x.ReadValue<Vector2>());
        _patientActions.MoveCharacter.canceled += x => OnMoveCharacter?.Invoke(Vector2.zero);

        _patientActions.SwitchRole.started += x => OnSwitchRole?.Invoke(PATIENT_ORIGIN);

        _patientActions.Interact.started += x => OnInteract?.Invoke();

        _patientActions.PauseGame.started += x => OnPauseGame?.Invoke();

        // //

        _watcherActions.MoveCamera.performed += x => OnMoveCamera?.Invoke(x.ReadValue<Vector2>());
        _watcherActions.MoveCamera.canceled += x => OnMoveCamera?.Invoke(Vector2.zero);

        _watcherActions.SwitchRole.started += x => OnSwitchRole?.Invoke(WATCHER_ORIGIN);

        _watcherActions.ChangeCamera.performed += x => OnChangeCamera?.Invoke();

        _watcherActions.PauseGame.started += x => OnPauseGame?.Invoke();
    }

    private void OnDisable() {
        _patientActions.MoveCharacter.performed -= x => OnMoveCharacter?.Invoke(x.ReadValue<Vector2>());
        _patientActions.MoveCharacter.canceled -= x => OnMoveCharacter?.Invoke(Vector2.zero);

        _patientActions.Interact.started -= x => OnInteract?.Invoke();

        _patientActions.SwitchRole.started -= x => OnSwitchRole?.Invoke(PATIENT_ORIGIN);

        _patientActions.PauseGame.started -= x => OnPauseGame?.Invoke();

        // //

        _watcherActions.MoveCamera.performed -= x => OnMoveCamera?.Invoke(x.ReadValue<Vector2>());
        _watcherActions.MoveCamera.canceled -= x => OnMoveCamera?.Invoke(Vector2.zero);

        _watcherActions.ChangeCamera.performed -= x => OnChangeCamera?.Invoke();

        _watcherActions.SwitchRole.started -= x => OnSwitchRole?.Invoke(WATCHER_ORIGIN);

        _watcherActions.PauseGame.started -= x => OnPauseGame?.Invoke();
    }

    public void SwitchActionMap(string origin) 
    {
        if(origin == PATIENT_ORIGIN) {
            _patientActions.Disable();
            _watcherActions.Enable();

        } else {
            _patientActions.Enable();
            _watcherActions.Disable();
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
