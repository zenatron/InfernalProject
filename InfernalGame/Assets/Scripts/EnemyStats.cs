using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : EntityStats
{
    private Enemy enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.audioManager.PlaySFX(AudioManager.SFX.SKELETON_HURT);
        enemy.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
