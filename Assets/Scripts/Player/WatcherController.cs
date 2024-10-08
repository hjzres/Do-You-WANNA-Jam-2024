using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    [Range(1f, 3f)][SerializeField] private float moveSpeed = 1;
    [SerializeField] private SecurityCamera activeCamera;
    [SerializeField] private List<SecurityCamera> securityCameras;

    private PlayerInputReader _playerInputReader;

    private void Start() {
        _playerInputReader = PlayerInputReader.Instance;

        _playerInputReader.OnMoveCamera += ProcessMoveCamera;
        _playerInputReader.OnChangeCamera += ProcessChangeCamera;
        _playerInputReader.OnPauseGame += ProcessPauseGame;
        _playerInputReader.OnSwitchRole += ProcessSwitchRole;
        _playerInputReader.OnRoleSwitchCompleted += ChangeController;

        securityCameras = FindObjectsByType<SecurityCamera>(FindObjectsSortMode.None).ToList();
        activeCamera = securityCameras[0];
        activeCamera.CameraUI.gameObject.SetActive(false);
    }


    private void OnDisable() {
        _playerInputReader.OnMoveCamera -= ProcessMoveCamera;
        _playerInputReader.OnChangeCamera -= ProcessChangeCamera;
        _playerInputReader.OnPauseGame -= ProcessPauseGame;
        _playerInputReader.OnSwitchRole -= ProcessSwitchRole;
        _playerInputReader.OnRoleSwitchCompleted -= ChangeController;
    }

    private void ProcessMoveCamera(Vector2 vector)
    {
        activeCamera.Rb2D.velocity = vector * moveSpeed;
        activeCamera.transform.position = activeCamera.VirtualCamera.State.CorrectedPosition;
    }

    private void ProcessChangeCamera()
    {
        throw new NotImplementedException();
    }

    private void ProcessSwitchRole(string origin) 
    {
        _playerInputReader.SwitchActionMap(origin);
    }


    private void ChangeController(string origin) 
    {
        activeCamera.CameraUI.gameObject.SetActive(origin != "Watcher");
        //activeCamera.VisionBounds.gameObject.SetActive(origin != "Watcher");   
    }
    

    private void ProcessPauseGame()
    {
        throw new NotImplementedException();
    }


}
