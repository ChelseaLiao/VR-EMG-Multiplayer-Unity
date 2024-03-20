using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LuminanceLight : MonoBehaviour
{
    public EMGAbilityController eMGAbilityController;

    public List<Lamp> Lamps = new List<Lamp>();

    public Light defaultlight;

    public Light lightBlue;

    public Light lightRed;

    public Light lightYellow;

    public Light winningLight;
    public Light winningLight2;
    private const float winningLightEndRange = 1.8f;

    public Light winningLightSmall;


    //is an ability fully charged the range should be at 40
    private const float verybright = 10f;
    public float multiplier;

    private bool gameWon = false;




    // Start is called before the first frame update
    void Start()
    {

        foreach (Lamp lamp in Lamps)
        {
            lamp.defaultlight.range = verybright;
            lamp.lightBlue.range = 0;
            lamp.lightRed.range = 0;
            lamp.lightYellow.range = 0;
        }
    }

        // Update is called once per frame
    void Update()
    {
        if (!gameWon)
        {
            var ability = eMGAbilityController.GetCurrentlyChargedAbility();
        
            if (ability != null)
            {
                foreach (Lamp lamp in Lamps)
                {
                    switch (ability.Type)
                    {
                        case EMGAbilityBase.AbilityType.TELEPORT:
                            lamp.defaultlight.range = 0;
                            lamp.lightBlue.range = ability.AccumulatedStrength / ability.signalMultiplier * multiplier;
                            break;
                        case EMGAbilityBase.AbilityType.SLOWTIME:
                            lamp.defaultlight.range = 0;
                            lamp.lightYellow.range = ability.AccumulatedStrength / ability.signalMultiplier * multiplier;
                            break;
                        case EMGAbilityBase.AbilityType.PUNCH:
                            lamp.defaultlight.range = 0;
                            lamp.lightRed.range = ability.AccumulatedStrength / ability.signalMultiplier * multiplier;
                            break;
                    }
                }
            }
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
}

   

