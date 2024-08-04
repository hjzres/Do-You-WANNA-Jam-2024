using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
				if (_instance == null)
				{
					GameObject container = new GameObject();
					_instance = container.AddComponent<GameManager>();
					_instance.name = typeof(GameManager).ToString() + " (Singleton)";
				}
			}
			return _instance;
		}
	}
	
	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (this != _instance)
			{
				Destroy(gameObject);
			}
		}
	}
	
	public Camera Cam1, Cam2;
	public Text State;
	[SerializeField] private float rate;
	private bool _inside = true;
	
	
	void Start()
	{
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
		State.gameObject.SetActive(!State.gameObject.active);
	}
}
