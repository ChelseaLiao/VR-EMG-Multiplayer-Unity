using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMGPunch : EMGAbilityBase
{
    public PlayerController player;

    public override void Charge(float currentChargingStrength)
    {
        base.Charge(currentChargingStrength);
    }

    public override void StartCharging(float initChargingStrength)
    {
        base.StartCharging(initChargingStrength);
        Debug.Log("Start charging Punch: " + initChargingStrength);
    }

    public override bool TryExecute()
    {
        player.ChargeAttack(this.AccumulatedStrength);
        base.TryExecute();
        Debug.Log("execute Punch");
        return true;
    }
}
