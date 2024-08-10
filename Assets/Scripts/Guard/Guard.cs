using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Guard : MonoBehaviour
{
	private enum Behavior { 
		Idle,
		Moving,
		Alerted
	}
	[SerializeField] private SpriteAnimator spriteAnimator;
	[SerializeField] private Transform[] pathPoints;
	private Queue<Transform> pathQueue = new Queue<Transform>();
	private Transform nextPoint;
	[SerializeField] private Rigidbody2D rb2D;
	[Range(0.5f, 3)] [SerializeField] private float moveSpeed = 1;
	[Range(3, 5)] [SerializeField] private float idleWaitTime = 3;
	
	private Behavior behavior = Behavior.Idle;
	
	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();

		for(int i = 0; i < pathPoints.Length; i++)
			pathQueue.Enqueue(pathPoints[i]);

		nextPoint = pathQueue.Dequeue();
		MoveToNextPosition();
	}

	private void MoveToNextPosition() {
		Vector2 vector = (nextPoint.position - transform.position).normalized;
		rb2D.velocity = vector * moveSpeed;

	    string newAnimState = "";

        switch (vector)
        {
            case Vector2 behind when vector.y > 0:
                newAnimState = "Walking_Behind";
				behavior = Behavior.Moving;
            break;

            case Vector2 front when vector.y < 0:
                newAnimState = "Walking_Front";
				behavior = Behavior.Moving;
            break;

            case Vector2 left when vector.x < 0:
                newAnimState = "Walking_Left";
				behavior = Behavior.Moving;
            break;

            case Vector2 right when vector.x > 0:
                newAnimState = "Walking_Right";
				behavior = Behavior.Moving;
            break;
        }

        spriteAnimator.ChangeAnimationState(newAnimState);
        spriteAnimator.Animator.SetBool("isMoving", true);
	}
	
	private void FixedUpdate() {
		if(transform.position == nextPoint.position)
		{
			pathQueue.Enqueue(nextPoint);
			nextPoint = pathQueue.Dequeue();

			rb2D.velocity = Vector2.zero;
			behavior = Behavior.Idle;

			spriteAnimator.ChangeAnimationState("");
			spriteAnimator.Animator.SetBool("isMoving", false);

			Invoke(nameof(MoveToNextPosition), idleWaitTime);
		}	
	}
	// void Update()
	// {
	// 	Vector2 direction = (positions[i] - (Vector2)transform.position).normalized;
	// 	_rb.velocity = direction * speed;
	// 	if(Vector2.Distance(transform.position, positions[i]) < 0.1f)
	// 	{
	// 		if(turning)
	// 		{
	// 			i--;
	// 		} 
	// 		else 
	// 		{
	// 			i++;				
	// 		}
	// 	}
		
	// 	if(i == positions.Length)
	// 	{
	// 		turning = true;
	// 		i--;
	// 	}
	// 	if(i == -1)
	// 	{
	// 		turning = false;
	// 		i++;
	// 	}
	// }
}
