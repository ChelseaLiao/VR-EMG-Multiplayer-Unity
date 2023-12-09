using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEMGAbility
{
    public float StartChargingThreshhold { get; set; }
    public float MinChargingThreshold { get; set; }
    public float MaxChargingThreshold { get; set; }

    public float ReleaseChargingThreshold { get; set; }

    public float AccumulatedStrength { get; }
    public float CurrentCooldown { get; }

    public bool TryExecute();

    public void StartCharging(float initChargingStrength);

    public void Charge(float currentChargingStrength);

    public void ProcessSignal(float strength);

}
