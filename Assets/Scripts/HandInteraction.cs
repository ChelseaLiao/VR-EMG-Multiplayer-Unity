using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour
{
    public FighterBase fighter;

    private void OnTriggerEnter(Collider collision)
    {

        Hitbox hitbox = collision.GetComponent<Hitbox>();//get the hit box on the box collider object

        if (hitbox != null)
        {
            FighterBase otherFighter = hitbox.fighter;//= attacking object
            if (otherFighter != null && otherFighter != fighter)// avoid attack itself when hits the collider on the own body
            {
                if (fighter.hitableEnemyTypes.Contains(otherFighter.Type))
                {
                    fighter.Attack(otherFighter);
                }
            }
        }

        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null)
        {
            if(fighter is PlayerController player)
            {
                player.Collect(item);
                item.gameObject.SetActive(false);
            }
        }
    }
}
