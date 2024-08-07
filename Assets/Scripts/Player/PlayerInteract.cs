using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : Singleton<PlayerInteract>
{
	private PlayerInput _playerInput;
	public event System.Action<IInteractable> Interacted;
	private IInteractable _interactableInstance;

	protected override void Awake()
	{
		base.Awake();
		
		_playerInput = GetComponent<PlayerInput>();
	}
	
	private void OnEnable()
	{
		_playerInput.actions["Interact"].performed += TryToInteract;
	}
	
	private void OnDisable()
	{
		_playerInput.actions["Interact"].performed -= TryToInteract;
	}
	
	private void TryToInteract(InputAction.CallbackContext context)
	{
		if (_interactableInstance != null)
		{
			_interactableInstance.InteractLogic();
		}
	}
	
	public void SetInstance(IInteractable interactable)
	{
		_interactableInstance = interactable;
	}
	
	public void ClearInstance()
	{
		_interactableInstance = null;
	}
}
