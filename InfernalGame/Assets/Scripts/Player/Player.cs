using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : Entity
{
	[Header("Attack Info")]
	public Vector2[] attackMovement;
	public float counterAttackDuration = 0.2f;
	public bool isBusy {  get; private set; }

	[Header("Move Info")]
	public float moveSpeed;
	public float jumpForce;

	[Header("Dash Info")]
	[SerializeField] private float dashCooldown;
	private float dashUsageTimer;
	public float dashSpeed;
	public float dashDuration;
	public float dashDir {  get; private set; }

	#region States
	public PlayerStateMachine stateMachine { get; private set; }

	public PlayerIdleState idleState { get; private set; }
	public PlayerMoveState moveState { get; private set; }
	public PlayerJumpState jumpState { get; private set; }
	public PlayerAirState airState { get; private set; }
	public PlayerWallSlideState wallSlideState { get; private set; }
	public PlayerWallJumpState wallJumpState { get; private set; }
	public PlayerDashState dashState { get; private set; }

	public PlayerPrimaryAttackState primaryAttackState { get; private set; }
	public PlayerCounterAttackState counterAttackState { get; private set; }
	public PlayerDeadState deadState { get; private set; }
	#endregion

	[HideInInspector] public AudioManager audioManager;

	protected override void Awake()
	{
		base.Awake();

		stateMachine = new PlayerStateMachine();

		idleState = new PlayerIdleState(this, stateMachine, "Idle");
		moveState = new PlayerMoveState(this, stateMachine, "Move");
		jumpState = new PlayerJumpState(this, stateMachine, "Jump");
		airState  = new PlayerAirState(this, stateMachine, "Jump");
		wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
		dashState = new PlayerDashState(this, stateMachine, "Dash");
		wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");

		primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
		counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");


		deadState = new PlayerDeadState(this, stateMachine, "Die");

		audioManager = GetComponentInChildren<AudioManager>();
	}

	protected override void Start()
	{
		base.Start();

		stateMachine.Initialize(idleState);
	}

	protected override void Update()
	{
		base.Update();

		stateMachine.currentState.Update();
		CheckForDashInput();
	}

	public IEnumerator BusyFor(float _seconds)
	{
		isBusy = true;

		yield return new WaitForSeconds(_seconds);

		isBusy = false;
	}

	public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

	private void CheckForDashInput()
	{
		if (IsWallDetected())
			return;

		dashUsageTimer -= Time.deltaTime;


		if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
		{
			dashUsageTimer = dashCooldown;
			dashDir = Input.GetAxisRaw("Horizontal");

			if (dashDir == 0)
				dashDir = facingDir;

			stateMachine.ChangeState(dashState);
		}
	}

    public override void Die()
    {
        base.Die();
		stateMachine.ChangeState(deadState);
		SceneManager.LoadSceneAsync("Death Screen");
    }
}