using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    public ZombieStateMachine stateMachine;

    public Animator animator;

    GameObject HP_Bar;
    Slider HP_Image;

    void Start()
    {
        HP_Bar = GameObject.FindGameObjectWithTag("HP_Zombie");
        HP_Image = HP_Bar.GetComponent<Slider>();
    }
    void update() { }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log("currenthealth: " + base.currentHealth + "hp: " + HP_Image.value);
        HP_Image.value = base.currentHealth / base.maxHealth;

    }

}
