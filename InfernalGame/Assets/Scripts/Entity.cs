using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components
	public Animator anim { get; private set; }
	public Rigidbody2D rb { get; private set; }
	public EntityFX fx { get; private set; }
	public EntityStats stats { get; private set; }
	public CapsuleCollider2D col { get; private set; }
	#endregion

    [Header("Collision Info")]
	public Transform attackCheck;
	public float attackCheckRadius;
	[SerializeField] protected Transform groundCheck;
	[SerializeField] protected float groundCheckDistance;
	[SerializeField] protected Transform wallCheck;
	[SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayerMask;

	[Header("Knockback Info")]
	[SerializeField] protected Vector2 knockbackDir;
	[SerializeField] protected float knockbackDuration = 0.1f;
	protected bool isKnockback;

    public int facingDir { get; private set; } = 1;
	protected bool facingRight = true;

	public System.Action onFlip;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
		fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody2D>();
		stats = GetComponent<EntityStats>();
		col = GetComponent<CapsuleCollider2D>();
    }
    
    protected virtual void Update()
    {

    }

	public virtual void DamageEffect()
	{
		fx.StartCoroutine("FlashFX");
		StartCoroutine("Knockback");
		Debug.Log(gameObject.name + " was damaged!");
	}

	protected virtual IEnumerator Knockback()
	{
		isKnockback = true;
		rb.velocity = new Vector2(knockbackDir.x * -facingDir, knockbackDir.y);
		yield return new WaitForSeconds(knockbackDuration);
		isKnockback = false;
	}

    #region Velocity
	public void SetZeroVelocity() => rb.velocity = Vector2.zero;

	public void SetVelocity(float _xVelocity, float _yVelocity)
	{
		if (isKnockback)
			return;

		rb.velocity = new Vector2(_xVelocity, _yVelocity);
		FlipController(_xVelocity);
	}
	#endregion

    #region Collision
	public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
	public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayerMask);

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
		onFlip?.Invoke();
	}

	public virtual void FlipController(float _x)
	{
		if (_x > 0 && !facingRight)
			Flip();
		else if (_x < 0 && facingRight)
			Flip();
	}

	public void FaceEntity(Transform _entityTrans)
	{
		if (_entityTrans.position.x > transform.position.x && facingDir < 0)
			Flip();
		else if (_entityTrans.position.x < transform.position.x && facingDir > 0)
			Flip();
	}

	#endregion

	public virtual void Die()
	{

	}
}