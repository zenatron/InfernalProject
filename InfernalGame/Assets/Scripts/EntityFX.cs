using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("FX")]
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Material hitMaterial;
    private Material defaultMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;
        yield return new WaitForSeconds(flashDuration);
        sr.material = defaultMaterial;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelRedColorBlink()
    {
        CancelInvoke("RedColorBlink");
        sr.color = Color.white;
    }
}
