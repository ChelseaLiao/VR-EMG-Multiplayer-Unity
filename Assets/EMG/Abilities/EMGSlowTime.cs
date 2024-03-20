using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMGSlowTime : EMGAbilityBase
{
    public GameController gameController;

    public float slowAnimationTimeDivider = 3f;

    public override void Charge(float currentChargingStrength)
    {
        base.Charge(currentChargingStrength);
    }

    public override void StartCharging(float initChargingStrength)
    {
        base.StartCharging(initChargingStrength);
        Debug.Log("Start charging SlowTime: " + initChargingStrength);
    }

    public override bool TryExecute()
    {
        StartCoroutine(SlowTimeForDuration(this.AccumulatedStrength));
        Debug.Log("execute SlowTime for " + this.AccumulatedStrength + " Seconds");
        base.TryExecute();
        return true;
    }

    IEnumerator SlowTimeForDuration(float duration)
    {
        foreach (Zombie zombie in gameController.zombies)
        {
            zombie.animator.speed = zombie.animator.speed / slowAnimationTimeDivider;
        }
        yield return new WaitForSeconds(duration);
        foreach (Zombie zombie in gameController.zombies)
        {
            zombie.animator.speed = 1f;
        }
    }
}
