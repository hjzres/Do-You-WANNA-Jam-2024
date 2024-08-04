using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	float speedX, speedY;
	Rigidbody2D rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		
		speedX = Input.GetAxis("Horizontal") * moveSpeed;
		speedY = Input.GetAxis("Vertical") * moveSpeed;
		rb.velocity = new Vector2(speedX, speedY);
		
	}
}
