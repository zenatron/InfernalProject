using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessCounterAttack", false);
    }
    
    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (Collider2D hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (hit.CompareTag("Enemy") && enemy != null)
            {
                if (enemy.CanBeStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessCounterAttack", true);
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
