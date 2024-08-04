using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float MoveSpeed;
	private float speedX, speedY;
	private Rigidbody2D rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		
		speedX = Input.GetAxis("Horizontal") * MoveSpeed;
		speedY = Input.GetAxis("Vertical") * MoveSpeed;
		rb.velocity = new Vector2(speedX, speedY);
		
	}
}
