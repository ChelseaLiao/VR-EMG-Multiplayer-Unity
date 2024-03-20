using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMGTeleport : EMGAbilityBase
{
    public Valve.VR.InteractionSystem.Teleport teleporter;

    public override void Charge(float currentChargingStrength)
    {
        base.Charge(currentChargingStrength);
        //Debug.Log("update charging teleport: " + currentChargingStrength);
        teleporter.UpdatePointer(this.AccumulatedStrength);
    }

    public override void StartCharging(float initChargingStrength)
    {
        base.StartCharging(initChargingStrength);
        Debug.Log("Start charging teleport: " + initChargingStrength);
        teleporter.ShowPointer();
    }

    public override void WhileMaxCharged()
    {
        base.WhileMaxCharged();
        teleporter.UpdatePointer(this.AccumulatedStrength);
    }

    public override bool TryExecute()
    {
        base.TryExecute();
        Debug.Log("execute teleport");
        bool success = teleporter.TryTeleportPlayer();
        teleporter.HideTeleportPointer();
        return success;
    }

    public override void InterruptCharging()
    {
        teleporter.HideTeleportPointer();
        base.InterruptCharging();
    }
}
