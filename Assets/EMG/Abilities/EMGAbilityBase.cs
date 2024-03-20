using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public abstract class EMGAbilityBase : MonoBehaviour, IEMGAbility
{
    public enum AbilityType {  TELEPORT, SLOWTIME, PUNCH }
    public enum State { IDLE, CHARGING, CHARGED, COOLDOWN }

    public State CurrentState = State.IDLE;
    public AbilityType Type;
    public float StartChargingThreshhold { get => startChargingThreshhold; set => throw new System.NotImplementedException(); }
    public float MinChargingThreshold { get => minChargingThreshold; set => throw new System.NotImplementedException(); }
    public float MaxChargingThreshold { get => maxChargingThreshold; set => throw new System.NotImplementedException(); }
    public float ReleaseChargingThreshold { get => interruptChargingThreshold; set => throw new System.NotImplementedException(); }
    public float AccumulatedStrength { get => accumulatedStrength; }
    public float CurrentCooldown { get => currentCooldown; }

    public float startChargingThreshhold;
    public float minChargingThreshold = 0;
    public float maxChargingThreshold = 10;
    public float interruptChargingThreshold;
    public float cooldown;
    public GameObject abilitymodel;

    private float accumulatedStrength, currentCooldown;

    public float signalMultiplier = 1f;

    private float cooldownPerFrameDecay = 1f / 60f;


    public UnityEvent<float> OnStartCharging, OnExecuting, OnCharging;

    public UnityEvent OnInterruptCharging;


    void Update()
    {
        if (CurrentState == State.COOLDOWN)
        {
            abilitymodel.SetActive(false);
            if (currentCooldown <= 0)
            {
                CurrentState = State.IDLE;
            }
            else
            {
                
                currentCooldown = Mathf.Max(0f, currentCooldown - cooldownPerFrameDecay);
            }
        }
        else
            abilitymodel.SetActive(true);
    }

    public void ProcessSignal(float strength)
    {
        strength *= signalMultiplier;
        if (CurrentState == EMGAbilityBase.State.IDLE && strength > StartChargingThreshhold)
        {
            StartCharging(strength);
        }
        else if (CurrentState == EMGAbilityBase.State.CHARGING)
        {
            if (strength < interruptChargingThreshold)
            {
                TryExecute();
            }
            else if(accumulatedStrength < MaxChargingThreshold)
            {
                Charge(strength);
            }
            // Staying on Max Charge
            else if (accumulatedStrength >= MaxChargingThreshold && strength > 0)
            {
                WhileMaxCharged();
            }
            else
            {
                InterruptCharging();
            }
        }
    }

    public virtual void Charge(float currentChargingStrength)
    {
        OnCharging.Invoke(currentChargingStrength);
        accumulatedStrength += currentChargingStrength;
    }

    public virtual void InterruptCharging()
    {
        currentCooldown = cooldown / 2f;
        CurrentState = State.COOLDOWN;
        OnInterruptCharging.Invoke();
        accumulatedStrength = 0;
    }

    public virtual bool TryExecute()
    {
        if (accumulatedStrength >= MinChargingThreshold)
        {
            currentCooldown = cooldown;
            CurrentState = State.COOLDOWN;
            OnExecuting.Invoke(accumulatedStrength);
            accumulatedStrength = 0;
            return true;
        }
        //InterruptCharging();
        return false;
    }

    public virtual void WhileMaxCharged()
    {

    }

    public virtual void StartCharging(float initChargingStrength)
    {
        if (currentCooldown <= 0)
        {
            CurrentState = State.CHARGING;
            OnStartCharging.Invoke(initChargingStrength);
            accumulatedStrength = initChargingStrength;
        }
    }
}
