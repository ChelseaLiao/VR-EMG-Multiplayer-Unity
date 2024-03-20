using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;

public class EMGAbilityController : MonoBehaviour
{

    public List<EMGAbilityBase> abilities = new List<EMGAbilityBase>();

    public static EMGAbilityController instance;

    private EMGAbilityBase _currentlyChargedAbility;

    public Teleport steamTeleport;

    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    // a reference to the action
    public SteamVR_Action_Boolean SlowTimeOnOff = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SlowTime"), ChargePunchOnOff = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Punch");

    // a reference to the hand
    public SteamVR_Input_Sources handTypeTeleport, handTypeSlowTime, handTypePunch;

    private bool _chargePunch, _chargeTeleport, _chargeSlow;

    public EMGAbilityBase GetCurrentlyChargedAbility() => _currentlyChargedAbility;

    public bool simulateEMGSignal = true;

    private int noneCount = 0, changeAbilityCount;
    public int maxEMGNoneCountToReset = 10, maxSimulateNoneCountToReset = 100, maxCountToChangeAbility = 40;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SlowTimeOnOff.AddOnStateDownListener((SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) =>
        {
            _chargeSlow = true;
        }, handTypeSlowTime);
        SlowTimeOnOff.AddOnStateUpListener((SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => _chargeSlow = false, handTypeSlowTime);


        teleportAction.AddOnStateDownListener((SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => _chargeTeleport = true, handTypeTeleport);
        teleportAction.AddOnStateUpListener((SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => _chargeTeleport = false, handTypeTeleport);

        ChargePunchOnOff.AddOnStateDownListener((SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => _chargePunch = true, handTypePunch);
        ChargePunchOnOff.AddOnStateUpListener((SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => _chargePunch = false, handTypePunch);
    }

    // TODO: Remove Update
    // TESTING
    void Update()
    {
        if (simulateEMGSignal)
        {
            // Simulate EMG Input
            float strength = 0.03f;

            int classification = -1;

            if (Input.GetKey(KeyCode.T) || _chargeTeleport)
            {
                classification = 1;
            }
            else if (Input.GetKey(KeyCode.F) || _chargeSlow)
            {
                classification = 0;
            }
            else if (Input.GetKey(KeyCode.R) || _chargePunch)
            {
                classification = 2;
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
                // Triggers Execution
                strength = 0f;
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                strength = 0f;
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                strength = 0f;
            }


            ProcessEmgSignal("", classification, strength, 0.0d);
        }
    }

    public void ProcessEmgSignal(string type, int classification, float strength, double timeStamp)
    {
        // Handle Charging Interrupt
        if (_currentlyChargedAbility != null)
        {
            if (classification == -1)
            {
                noneCount++;
            }

            if (noneCount >= (simulateEMGSignal ? maxSimulateNoneCountToReset : maxEMGNoneCountToReset) && _currentlyChargedAbility != null)
            {
                _currentlyChargedAbility.TryExecute();
                _currentlyChargedAbility = null;
                noneCount = 0;
            }
        }

        if (classification < 0 || classification > abilities.Count - 1)
            return;

        // Change Ability 
        if (_currentlyChargedAbility != abilities[classification])
        {
            changeAbilityCount++;
            if (changeAbilityCount >= maxCountToChangeAbility)
            {
                if (_currentlyChargedAbility != null)
                {
                    _currentlyChargedAbility.InterruptCharging();
                    changeAbilityCount = 0;
                }
                _currentlyChargedAbility = abilities[classification];
            }
        }
        else
        {
            changeAbilityCount = 0;
        }

        if (_currentlyChargedAbility == null)
            return;
       _currentlyChargedAbility.ProcessSignal(strength);
    }
}
