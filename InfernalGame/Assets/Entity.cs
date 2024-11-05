using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components
	public Animator anim { get; private set; }
	public Rigidbody2D rb { get; private set; }
	#endregion

    [Header("Collision Info")]
	public Transform attackCheck;
	public float attackCheckRadius;
	[SerializeField] protected Transform groundCheck;
	[SerializeField] protected float groundCheckDistance;
	[SerializeField] protected Transform wallCheck;
	[SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundMask;

    public int facingDir { get; private set; } = 1;
	protected bool facingRight = true;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update()
    {

    }

	public virtual void TakeDamage()
	{
		Debug.Log(gameObject.name + " was damaged!");
	}

    #region Velocity
	public void SetZeroVelocity() => rb.velocity = Vector2.zero;

	public void SetVelocity(float _xVelocity, float _yVelocity)
	{
		rb.velocity = new Vector2(_xVelocity, _yVelocity);
		FlipController(_xVelocity);
	}
	#endregion

    #region Collision
	public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
	public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundMask);

	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
		Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
		Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
	}

	#endregion

    #region Flip
	public virtual void Flip()
	{
		facingDir *= -1;
		facingRight = !facingRight;
		transform.Rotate(0, 180, 0);
	}

	public virtual void FlipController(float _x)
	{
		if (_x > 0 && !facingRight)
			Flip();
		else if (_x < 0 && facingRight)
			Flip();
	}
	#endregion
}