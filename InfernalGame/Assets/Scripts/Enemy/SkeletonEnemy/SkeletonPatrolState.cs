using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolState : SkeletonGroundedState
{
    public SkeletonPatrolState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        // if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        // {
        //     enemy.Flip();
        //     stateMachine.ChangeState(enemy.idleState);
        // }
        Patrol();
    }

    protected void Patrol()
    {
        if (enemy.IsWallDetected() && enemy.IsGroundDetected())
        {
            enemy.Jump(2f, 6f);
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (enemy.IsGroundDetected() && !enemy.IsWallDetected())
        {
            
        }
        else if (!enemy.IsGroundDetected() && enemy.IsWallDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (!(enemy.IsGroundDetected() && enemy.IsWallDetected()))
        {
            if (enemy.CanJump())
            {
                enemy.Jump(5f, 6f);
                stateMachine.ChangeState(enemy.idleState);
            }
            else
            {
                enemy.Flip();
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        else
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.IsPlayerDetected())
            stateMachine.ChangeState(enemy.pursuitState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
