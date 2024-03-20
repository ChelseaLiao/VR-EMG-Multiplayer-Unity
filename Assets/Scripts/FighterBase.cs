using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FighterBase : MonoBehaviour, IFighter
{
    public enum FighterType { ZOMBIE, PLAYER }

    public FighterType Type;

    public float maxHealth;
    public float damage;
    protected float currentHealth, currentDamage;

    private bool dead = false;
    public bool Dead => dead;

    public List<FighterType> hitableEnemyTypes = new List<FighterType>();

    public UnityEvent OnAfterAttacking, OnAfterDying;

    public UnityEvent<float> OnAfterTakingDamage;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentDamage = damage;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void Attack(IFighter fighter)
    {
        fighter.TakeDamage(currentDamage);
        OnAfterAttacking.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0f);
        OnAfterTakingDamage.Invoke(damage);
        if (!dead && currentHealth <= 0)
        {
            dead = true;
            OnAfterDying.Invoke();
        }
    }
}
