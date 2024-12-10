using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.Scripting;

public enum EnemyStatusEffects
{
    NONE,
    CAN_BE_STUNNED,
    ALERT,
    ENRAGED,
    CRITICAL_HEALTH,
    HEALING,
}

public class EnemyStatusEffectHandler : MonoBehaviour
{
    public SpriteRenderer statusEffectImageRenderer;
    public Sprite[] statusEffectImages;

    private Dictionary<EnemyStatusEffects, Sprite> statusEffectSpriteMap;

    private void Awake()
    {
        statusEffectSpriteMap = new Dictionary<EnemyStatusEffects, Sprite>();
        foreach (EnemyStatusEffects statusEffect in System.Enum.GetValues(typeof(EnemyStatusEffects)))
        {
            statusEffectSpriteMap.Add(statusEffect, statusEffectImages[(int)statusEffect]);
        }
    }

    public void SetStatusEffectImage(EnemyStatusEffects statusEffect)
    {
        if (statusEffectSpriteMap.TryGetValue(statusEffect, out Sprite sprite))
        {
            statusEffectImageRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Status effect image not found for " + statusEffect);
        }
    }

    public void FlashStatusEffectImage(EnemyStatusEffects statusEffect)
    {
        StartCoroutine(FlashStatusEffectImageCoroutine(statusEffect));
    }

    public void ClearStatusEffectImage()
    {
        SetStatusEffectImage(EnemyStatusEffects.NONE);
    }

    private IEnumerator FlashStatusEffectImageCoroutine(EnemyStatusEffects statusEffect)
    {
        SetStatusEffectImage(statusEffect);
        yield return new WaitForSeconds(0.2f);
        SetStatusEffectImage(EnemyStatusEffects.NONE);
        yield return new WaitForSeconds(0.1f);
        SetStatusEffectImage(statusEffect);
        yield return new WaitForSeconds(0.2f);
        SetStatusEffectImage(EnemyStatusEffects.NONE);
        yield return new WaitForSeconds(0.1f);
        SetStatusEffectImage(statusEffect);
        yield return new WaitForSeconds(0.2f);
        SetStatusEffectImage(EnemyStatusEffects.NONE);
    }

    public EnemyStatusEffects GetStatusEffectImage()
    {
        foreach (KeyValuePair<EnemyStatusEffects, Sprite> pair in statusEffectSpriteMap)
        {
            if (pair.Value == statusEffectImageRenderer.sprite)
            {
                return pair.Key;
            }
        }

        return EnemyStatusEffects.NONE;
    }
}