using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Enemy : Entity
{

    [SerializeField] protected LayerMask playerLayerMask;
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;

    public float pursuitTime;
    public float pursuitSpeedMultiplier;

    [Header("Attack Info")]
    public float playerDetectionRange;
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;


    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual bool IsPlayerDetected()
    {
        // Perform a raycast towards the player and check for any hits on the player or wall layers
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerDetectionRange, playerLayerMask | groundLayerMask);

        // Return true if the hit object is the player; otherwise, return false
        return hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * facingDir * attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * facingDir * playerDetectionRange);
    }
}
