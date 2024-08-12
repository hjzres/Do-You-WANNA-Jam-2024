using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : Singleton<PlayerInputReader>
{
    public PlayerControls PlayerControls { get; private set; }
    public PlayerControls.PatientActions PatientActions { get; private set; }
    public PlayerControls.WatcherActions WatcherActions { get; private set; }

    public Action<Vector2> OnMoveCharacter, OnMoveCamera;
    public Action OnInteract, OnChangeCamera, OnPauseGame;
    public Action<string> OnSwitchRole, OnRoleSwitchCompleted;

    public const string PATIENT_ORIGIN = "Patient";
    public const string WATCHER_ORIGIN = "Watcher";

    protected override void Awake() {
        base.Awake();

        PlayerControls = new PlayerControls();
        PatientActions = PlayerControls.Patient;
        WatcherActions = PlayerControls.Watcher;

        PatientActions.Enable();
        WatcherActions.Disable();
    }
    
    private void OnEnable() {
        PatientActions.MoveCharacter.performed += x => OnMoveCharacter?.Invoke(x.ReadValue<Vector2>());
        PatientActions.MoveCharacter.canceled += x => OnMoveCharacter?.Invoke(Vector2.zero);

        PatientActions.SwitchRole.started += x => OnSwitchRole?.Invoke(PATIENT_ORIGIN);

        PatientActions.Interact.started += x => OnInteract?.Invoke();

        PatientActions.PauseGame.started += x => OnPauseGame?.Invoke();

        // //

        WatcherActions.MoveCamera.performed += x => OnMoveCamera?.Invoke(x.ReadValue<Vector2>());
        WatcherActions.MoveCamera.canceled += x => OnMoveCamera?.Invoke(Vector2.zero);

        WatcherActions.SwitchRole.started += x => OnSwitchRole?.Invoke(WATCHER_ORIGIN);

        WatcherActions.ChangeCamera.performed += x => OnChangeCamera?.Invoke();

        WatcherActions.PauseGame.started += x => OnPauseGame?.Invoke();
    }

    private void OnDisable() {
        PatientActions.MoveCharacter.performed -= x => OnMoveCharacter?.Invoke(x.ReadValue<Vector2>());
        PatientActions.MoveCharacter.canceled -= x => OnMoveCharacter?.Invoke(Vector2.zero);

        PatientActions.Interact.started -= x => OnInteract?.Invoke();

        PatientActions.SwitchRole.started -= x => OnSwitchRole?.Invoke(PATIENT_ORIGIN);

        PatientActions.PauseGame.started -= x => OnPauseGame?.Invoke();

        // //

        WatcherActions.MoveCamera.performed -= x => OnMoveCamera?.Invoke(x.ReadValue<Vector2>());
        WatcherActions.MoveCamera.canceled -= x => OnMoveCamera?.Invoke(Vector2.zero);

        WatcherActions.ChangeCamera.performed -= x => OnChangeCamera?.Invoke();

        WatcherActions.SwitchRole.started -= x => OnSwitchRole?.Invoke(WATCHER_ORIGIN);

        WatcherActions.PauseGame.started -= x => OnPauseGame?.Invoke();
    }

    public void Initialize() {
        PatientActions.Enable();
        WatcherActions.Disable();
    }

    public void Deinitialize() {
        PatientActions.Disable();
        WatcherActions.Disable();
    }

    public void SwitchActionMap(string origin) 
    {
        if(origin == PATIENT_ORIGIN) {
            PatientActions.Disable();
            WatcherActions.Enable();

        } else {
            PatientActions.Enable();
            WatcherActions.Disable();
        }
        
        OnRoleSwitchCompleted?.Invoke(origin);
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
