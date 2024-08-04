using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField] private Transform player;
	
	void Update()
	{
		Vector3 newPosition = transform.position;
		newPosition.x = player.position.x;
		newPosition.y = player.position.y;
		transform.position = newPosition;
	}
}
