using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public PlayerController playerController;

    public Collider damageCollider; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


   private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit");

        if(collision.tag == "Hittable")
        {

            Zombie zombie = collision.GetComponent<Zombie>();
            playerController.Attack(zombie);
        }

       /* if (collision.tag == "PlayerVR")
        {
            if (collision.gameObject.name == "PlayerBody")
            {
                PlayerStats playerstats = collision.GetComponent<PlayerStats>();


                if (playerstats != null)
                {

                    playerstats.TakeDamage(zombieDamage);
                }
            }
            
        }*/
    }
}
