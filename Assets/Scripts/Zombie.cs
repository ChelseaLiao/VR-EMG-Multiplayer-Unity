using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    public ZombieStateMachine stateMachine;

    public Animator animator;

    public Transform healthbar;

    //GameObject HP_Bar;
    //Slider HP_Image;

    protected override void Start()
    {
        base.Start();
        //HP_Bar = GameObject.FindGameObjectWithTag("HP_Zombie");
        //HP_Image = HP_Bar.GetComponent<Slider>();
        if (OnAfterDying == null)
            OnAfterDying = new UnityEngine.Events.UnityEvent();

        OnAfterDying.AddListener(() =>
        {
            GameController.instance.ZombieDied(this);
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log(base.currentHealth);
        healthbar.localScale = new Vector3(healthbar.localScale.x * base.currentHealth / base.maxHealth, healthbar.localScale.y, healthbar.localScale.z);
        //Debug.Log("currenthealth: " + base.currentHealth + "hp: " + HP_Image.value);
        //HP_Image.value = base.currentHealth / base.maxHealth;

    }
}
