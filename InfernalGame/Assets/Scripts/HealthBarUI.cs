using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity;
    private EntityStats stats;
    private RectTransform healthBar;
    private Slider slider;

    private void Start()
    {
        healthBar = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        stats = entity.stats;

        entity.onFlip += FlipUI;
        stats.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = stats.currentHealth;
    }

    private void FlipUI() => healthBar.Rotate(0, 180, 0);
    private void OnDisable()
    {
        entity.onFlip -= FlipUI;
        stats.onHealthChanged -= UpdateHealthUI;
    }
        
}
