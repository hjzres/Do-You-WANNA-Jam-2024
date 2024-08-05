using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardView : MonoBehaviour
{
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("Gotcha!");
		}
	}
}
