using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPursuitState : EnemyState
{
    private Transform player;
    private SkeletonEnemy enemy;
    public SkeletonPursuitState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.pursuitTime;
        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();
        // Countdown the pursuit timer
        stateTimer -= Time.deltaTime;

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.position);

        // Check if the pursuit should continue or if it should transition to idle
        if (stateTimer > 0 && distanceToPlayer <= 10)
        {

            // Face player
            enemy.FacePlayer(player);

            // Check if the player is within attack range
            if (distanceToPlayer > enemy.attackDistance)
            {
                // Move towards the player
                int moveDir = (player.position.x > enemy.transform.position.x) ? 1 : -1;
                enemy.SetVelocity(enemy.moveSpeed * enemy.pursuitSpeedMultiplier * moveDir, rb.velocity.y);
            }
            else
            {
                // If close enough, transition to attack state
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            // Stop pursuing and transition to idle state
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        return Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown;
    }
}
