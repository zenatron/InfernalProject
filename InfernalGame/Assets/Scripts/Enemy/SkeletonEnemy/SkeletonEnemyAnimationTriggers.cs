using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyAnimationTriggers : MonoBehaviour
{
    private SkeletonEnemy enemy => GetComponentInParent<SkeletonEnemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (Collider2D hit in hits)
        {
            var player = hit.GetComponent<Player>();
            if (hit.CompareTag("Player") && player != null)
            {
                enemy.stats.DoDamage(player.stats);
            }
        }
    }

    protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
