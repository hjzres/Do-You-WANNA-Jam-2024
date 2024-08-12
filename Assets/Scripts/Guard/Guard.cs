using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	private enum Behavior { 
		Idle,
		Moving,
		Alerted
	}
	[SerializeField] private SpriteAnimator spriteAnimator;
	[SerializeField] private GuardVision vision;
	[SerializeField] private Transform[] pathPoints;
	private Queue<Transform> pathQueue = new Queue<Transform>();
	private Transform nextPoint;
	[SerializeField] private Rigidbody2D rb2D;
	[Range(0.5f, 3)][SerializeField] private float moveSpeed = 1;
	[Range(0, 5)][SerializeField] private float idleWaitTime = 3;
	
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
            case Vector2 behind when vector.y > 0 && Mathf.Abs(vector.y) > Mathf.Abs(vector.x):
                newAnimState = "Walking_Behind";
				behavior = Behavior.Moving;

				vision.transform.localRotation = Quaternion.Euler(0, 0, 90);
            break;

            case Vector2 front when vector.y < 0 && Mathf.Abs(vector.y) > Mathf.Abs(vector.x):
                newAnimState = "Walking_Front";
				behavior = Behavior.Moving;

				vision.transform.localRotation = Quaternion.Euler(0, 0, 270);
            break;

            case Vector2 left when vector.x < 0 && Mathf.Abs(vector.x) > Mathf.Abs(vector.y):
                newAnimState = "Walking_Left";
				behavior = Behavior.Moving;

				vision.transform.localRotation = Quaternion.Euler(0, 0, 180);
            break;

            case Vector2 right when vector.x > 0 && Mathf.Abs(vector.x) > Mathf.Abs(vector.y):
                newAnimState = "Walking_Right";
				behavior = Behavior.Moving;

				vision.transform.localRotation = Quaternion.Euler(0, 0, 0);
            break;
        }

        spriteAnimator.ChangeAnimationState(newAnimState);
        spriteAnimator.Animator.SetBool("isMoving", true);
	}
	
	private void FixedUpdate() {
		if(Vector2.Distance(transform.position, nextPoint.position) < 0.1f)
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

	public void Alert() {
		rb2D.velocity = Vector2.zero;
		behavior = Behavior.Alerted;
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
