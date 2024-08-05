using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
	[SerializeField] private Vector2[] positions;
	[SerializeField] private float speed;
	private bool turning;
	private Rigidbody2D _rb;
	private int i;
	
	void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		if(positions.Length > 0)
		{
			transform.position = new Vector3(positions[0].x, positions[0].y, transform.position.z);
		}
		
		i = 1;
		turning = false;
	}
	
	void Update()
	{
		Vector2 direction = (positions[i] - (Vector2)transform.position).normalized;
		_rb.velocity = direction * speed;
		transform.up = direction;
		if(Vector2.Distance(transform.position, positions[i]) < 0.1f)
		{
			if(turning)
			{
				i--;
			} 
			else 
			{
				i++;				
			}
		}
		
		if(i == positions.Length)
		{
			turning = true;
			i--;
		}
		if(i == -1)
		{
			turning = false;
			i++;
		}
	}
}
