using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : FighterBase
{
    public Camera viewpointVR, viewpointFallback;

    public Transform CurrentViewpoint => viewpointVR == null || !viewpointVR.isActiveAndEnabled ? viewpointFallback.transform : viewpointVR.transform;

    public Transform CurrentPosition => player.trackingOriginTransform;

    public GameObject HP_Player;
    public TMP_Text HP_Text;
    public SpriteRenderer Damage_Image;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public GameObject gameoverCube;
    public GameObject gameoverLight;

    private Valve.VR.InteractionSystem.Player player;


    protected override void Start()
    {
        base.Start();
        player = Valve.VR.InteractionSystem.Player.instance;

        if (player == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.", this);
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HP_Text.text = "Your Life:"+ base.currentHealth + "/"+ base.maxHealth;
    }

    public void ChargeAttack(float chargeAmount)
    {
        currentDamage = chargeAmount;
    }

    public void ReleaseAttack()
    {
        currentDamage = damage;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //hp_value.text = "HP:"+ base.currentHealth;
        
        if(base.currentHealth<0.8f*base.maxHealth && base.currentHealth>=0.5f*base.maxHealth){
           Damage_Image.sprite = sprite1; 
        }
            
        if(base.currentHealth<0.5f*base.maxHealth && base.currentHealth>=0.2f*base.maxHealth){
           Damage_Image.sprite = sprite2;
        }
        if(base.currentHealth<0.2f*base.maxHealth){
           Damage_Image.sprite = sprite3;
        }
        if(base.currentHealth==0){
            gameoverCube.SetActive(true);
            gameoverLight.SetActive(true);
        }

    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void ResetViewpointPosition()
    {
        viewpointFallback.transform.localPosition = new Vector3(0, 1.75f, 0);
        viewpointVR.transform.localPosition = new Vector3(0, 0f, 0);
    }

    public void Collect(Item item)
    {
        switch (item.Type)
        {
            case Item.ItemType.HealthBox:
                Heal(10);
                break;
            default:
                break;
        }
    }
}
