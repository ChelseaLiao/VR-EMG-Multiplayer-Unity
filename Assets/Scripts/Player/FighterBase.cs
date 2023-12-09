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
    public float currentHealth;

    public List<FighterType> hitableEnemyTypes = new List<FighterType>();

    public UnityEvent OnAfterAttacking, OnAfterTakingDamage, OnAfterDying;

    // Start is called before the first frame update
    protected void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Attack(IFighter fighter)
    {
        fighter.TakeDamage(damage);
        OnAfterAttacking.Invoke();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnAfterTakingDamage.Invoke();
        if (currentHealth <= 0)
        {
            OnAfterDying.Invoke();
        }
        if (currentHealth < 0)
            currentHealth = 0;
    }
}
