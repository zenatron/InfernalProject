using System;
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
    [SerializeField] protected Transform jumpCheck;
	[SerializeField] protected float jumpCheckDistance;

    [Header("Attack Info")]
    public float playerDetectionRange;
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("Stunned Info")]
    public float stunnedDuration;
    public Vector2 stunnedDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterAttackImage;

    // Experimental feature
    [Header("Experimental Feature")]
    private EnemyStatusEffectHandler statusEffectHandler;


    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        statusEffectHandler = GetComponent<EnemyStatusEffectHandler>();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        SetStatusEffectImage(EnemyStatusEffects.CAN_BE_STUNNED);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        SetStatusEffectImage(EnemyStatusEffects.NONE);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    public virtual bool CanJump() => Physics2D.Raycast(jumpCheck.position, Vector2.down, jumpCheckDistance, groundLayerMask);

    public virtual void Jump(float _xBoost, float _yBoost)
    {
        if (CanJump())
        {
            rb.velocity = new Vector2(rb.velocity.x * _xBoost, Vector2.Distance(jumpCheck.position, transform.position) * _yBoost);
        }
    }

    public virtual void SetLastAnimBoolName(string _animBoolName) => lastAnimBoolName = _animBoolName;

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

        Gizmos.color = Color.red;
        Gizmos.DrawLine(jumpCheck.position, jumpCheck.position + Vector3.down * jumpCheckDistance);
    }
    
    #region Status Effect Image Methods
    public virtual void SetStatusEffectImage(EnemyStatusEffects statusEffect)
    {
        if (statusEffectHandler != null)
        {
            statusEffectHandler.SetStatusEffectImage(statusEffect);
        }
        else
        {
            Debug.LogWarning("Status effect handler is null for this enemy");
            return;
        }
    }

    public virtual void FlashStatusEffectImage(EnemyStatusEffects statusEffect)
    {
        if (statusEffectHandler != null)
        {
            statusEffectHandler.FlashStatusEffectImage(statusEffect);
        }
        else
        {
            Debug.LogWarning("Status effect handler is null for this enemy");
            return;
        }
    }

    public virtual void ClearStatusEffectImage()
    {
        if (statusEffectHandler != null)
        {
            statusEffectHandler.ClearStatusEffectImage();
        }
        else
        {
            Debug.LogWarning("Status effect handler is null for this enemy");
            return;
        }
    }

    public virtual EnemyStatusEffects GetStatusEffectImage()
    {
        if (statusEffectHandler != null)
        {
            return statusEffectHandler.GetStatusEffectImage();
        }
        else
        {
            Debug.LogWarning("Status effect handler is null for this enemy");
            return EnemyStatusEffects.NONE;
        }
    }
    #endregion
}