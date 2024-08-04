using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	public Camera Cam1, Cam2;
	public Text State;
	[SerializeField] private float rate;
	private bool _inside = true;
	
	protected override void Awake()
	{
		base.Awake();
		
		Cam1.enabled = true;
		Cam2.enabled = false;
		InvokeRepeating("FlashText", rate, rate);
	}
	
	public void SwitchCamera()
	{
		Cam1.enabled = !Cam1.enabled;
		Cam2.enabled = !Cam2.enabled;
		_inside = !_inside;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			SwitchCamera();
			if(_inside)
			{
				State.text = "Inside";
			} else {
				State.text = "Out";
			}
		}
	}
	
	private void FlashText()
	{
		State.gameObject.SetActive(!State.gameObject.activeSelf);
	}
}
