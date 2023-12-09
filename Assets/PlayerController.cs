using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : FighterBase
{
    private float chargedAttackDamage = 0f;

    public Transform viewpoint;

    GameObject HP_Player;
    GameObject HP_Text;
    Image Damage_Image;
    Text hp_value;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    GameObject gameoverCube;
    GameObject gameoverLight;

    void Start(){
        base.Start();
        HP_Player = GameObject.FindGameObjectWithTag("HP_Player");
        Damage_Image = HP_Player.GetComponent<Image>();
        //HP_Text = GameObject.FindGameObjectWithTag("HP_Player_Text");
        //hp_value = HP_Text.GetComponent<Text>();
        gameoverLight = GameObject.FindGameObjectWithTag("GameOverLight");
        gameoverCube = GameObject.FindGameObjectWithTag("GameoverCube");
        gameoverCube.SetActive(false);
        gameoverLight.SetActive(false);

    }

    private void Awake()
    {
        damage = 0;
    }

    public void OnDead()
    {
        //TODO
        Debug.Log("Player Dead");
    }

    public void ChargeAttack(float chargeAmount)
    {
        chargedAttackDamage = chargeAmount;
        damage += chargedAttackDamage;
    }

    public void ReleaseChargeAttackDamage()
    {
        damage -= chargedAttackDamage;
        chargedAttackDamage = 0;
    }


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //hp_value.text = "HP:"+ base.currentHealth;
        
        if(base.currentHealth<80 && base.currentHealth>=50){
            Damage_Image.sprite = sprite1; 
        }
            
        if(base.currentHealth<50 && base.currentHealth>=20){
            Damage_Image.sprite = sprite2;
        }
        if(base.currentHealth<20){
            Damage_Image.sprite = sprite3;
        }
        if(base.currentHealth==0){
            gameoverCube.SetActive(true);
            gameoverLight.SetActive(true);
        }

    }

}
