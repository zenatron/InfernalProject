using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public Stat strength;
    public Stat damage;
    public Stat maxHealth;

    public int currentHealth;
    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
    }

    public virtual void DoDamage(EntityStats _targetStats)
    {
        int totalDamage = strength.GetValue() + damage.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealth(_damage);
        Debug.Log(_damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void DecreaseHealth(int _damage)
    {
        currentHealth -= _damage;
        onHealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
        
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }
}
