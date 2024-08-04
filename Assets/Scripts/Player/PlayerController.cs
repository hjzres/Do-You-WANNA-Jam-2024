using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float MoveSpeed;
	private float speedX, speedY;
	private Rigidbody2D rb;
	[SerializeField] private Player player;
	[SerializeField] private GameManager gameManager;
	
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		if((player == Player.Player1 && gameManager.Cam1.enabled) || (player == Player.Player2 && gameManager.Cam2.enabled))
		{
			Move();
		} else {
			Freeze();
		}
	}
	
	enum Player
	{
		Player1 = 1,
		Player2 = 2
	}
	
	void Move()
	{
		speedX = Input.GetAxis("Horizontal") * MoveSpeed;
		speedY = Input.GetAxis("Vertical") * MoveSpeed;
		rb.velocity = new Vector2(speedX, speedY);
	}
	
	void Freeze()
	{
		rb.velocity = Vector2.zero;
	}
}
