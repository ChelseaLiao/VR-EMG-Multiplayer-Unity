using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour
{
    public FighterBase fighter;

    private void OnTriggerEnter(Collider collision)
    {

        Hitbox hitbox = collision.GetComponent<Hitbox>();

        if (hitbox != null)
        {
            FighterBase otherFighter = hitbox.fighter;
            if (otherFighter != null && otherFighter != fighter)
            {
                if (fighter.hitableEnemyTypes.Contains(otherFighter.Type))
                {
                    fighter.Attack(otherFighter);
                }
            }
        }
    }
}
