using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private int currentHP;
    public int initialHP = 3;

    protected SpriteRenderer spriteRenderer;
    protected bool isInvincible;
    public float invinciblityDuration = 0.3f;
    public float knockbackStrength = 1f;

    public LayerMask attackableLayers;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHP = initialHP;
    }
    public void TakeDamage(Entity damageDealer)
    {
        currentHP--;

        Debug.Log($"{currentHP}/{initialHP}");

        Vector3 pushDirection = (transform.position - damageDealer.transform.position).normalized;

        transform.position += pushDirection * knockbackStrength;
        StartCoroutine(InvincibilityCoroutine(invinciblityDuration));

        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected void RestoreHP()
    {
        currentHP = initialHP;
    }
    protected abstract void Die();

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        // setup
        spriteRenderer.color = Color.red;
        isInvincible = true;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, t / duration);
            yield return null;
        }

        // reset
        spriteRenderer.color = Color.white;
        isInvincible = false;
    }
}