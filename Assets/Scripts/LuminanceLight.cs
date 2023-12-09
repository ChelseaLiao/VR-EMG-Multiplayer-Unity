using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LuminanceLight : MonoBehaviour
{
    public EMGAbilityController eMGAbilityController;

    public List<Lamp> Lamps = new List<Lamp>();

    public Light defaultlight;
    private float defaultlightoriginalrange;

    public Light lightBlue;
    private float lightBlueOriginalRange;

    public Light lightRed;
    private float lightRedOriginalRange;

    public Light lightYellow;
    private float lightYellowOriginalRange;

    public Light winningLight;
    public Light winningLight2;
    private const float winningLightEndRange = 1.8f;

    public Light winningLightSmall;

    public Light gameoverLight;
    private const float gameoverLightEndRange = 3.0f;


    //is an ability fully charged the range should be at 40
    private const float verybright = 10f;
    public float multiplier;

    private bool gameWon = false;




    // Start is called before the first frame update
    void Start()
    {
      
        defaultlightoriginalrange = 39.8f;
        lightBlueOriginalRange = 0f;
        lightRedOriginalRange = 0f;
        lightYellowOriginalRange = 0f;

        

       
    }

    // Update is called once per frame
    void Update()
    {
        var ability = eMGAbilityController.GetCurrentlyChargedAbility();
        
        switch(ability.Type)
        {
            case EMGAbilityBase.AbilityType.TELEPORT:
                foreach (Lamp lamp in Lamps)
                {
                    lamp.defaultlight.range = 0;
                    lamp.lightBlue.range = ability.AccumulatedStrength * multiplier;

                }
                break;
            case EMGAbilityBase.AbilityType.SLOWTIME:
                foreach (Lamp lamp in Lamps)
                {
                    lamp.defaultlight.range = 0;
                    lamp.lightYellow.range = ability.AccumulatedStrength * multiplier;

                }
                break;
            case EMGAbilityBase.AbilityType.PUNCH:
                foreach (Lamp lamp in Lamps)
                {
                    lamp.defaultlight.range = 0;
                    lamp.lightRed.range = ability.AccumulatedStrength * multiplier;

                }
                break;
        }
       
    }

    public void ResetLight()
    {
        Debug.Log("Light gets reseted");
        foreach (Lamp lamp in Lamps)
        {
            lamp.defaultlight.range = verybright;
            lamp.lightBlue.range = 0;
            lamp.lightRed.range = 0;
            lamp.lightYellow.range = 0;
        }
    }

    public void StopLighting()
    {
        Debug.Log("stop lighting");
        foreach (Lamp lamp in Lamps)
        {
            lamp.defaultlight.range = verybright;
            lamp.lightBlue.range = 0;
            lamp.lightRed.range = 0;
            lamp.lightYellow.range = 0;
        }
    }

    public void WinningLight()
    {

        foreach (Lamp lamp in Lamps)
        {
            Debug.Log("lights on");
            gameWon = true;

            lamp.lightBlue.range = 0;
            lamp.lightRed.range = 0;
            lamp.lightYellow.range = 0;
            lamp.defaultlight.range = 0;
            lamp.winningLightSmall.range = 40f;
            winningLight.intensity = winningLightEndRange;
            // winningLight2.intensity = winningLightEndRange;

        }


    }

    public void OverLight()
    {

        foreach (Lamp lamp in Lamps)
        {
            Debug.Log("game over light");
            //gameWon = true;

            lamp.lightBlue.range = 0;
            lamp.lightRed.range = 0;
            lamp.lightYellow.range = 0;
            lamp.defaultlight.range = 0;
            lamp.gameoverLight.range = 40f;
            gameoverLight.intensity = gameoverLightEndRange;
            // winningLight2.intensity = winningLightEndRange;

        }


    }
}

   

