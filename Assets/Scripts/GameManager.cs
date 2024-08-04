using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager _instance;
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
}
