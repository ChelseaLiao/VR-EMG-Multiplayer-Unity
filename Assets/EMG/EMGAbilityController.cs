using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMGAbilityController : MonoBehaviour
{

    public List<EMGAbilityBase> abilities = new List<EMGAbilityBase>();

    public static EMGAbilityController instance;

    public bool simulateEMGSignal = true;

    void Awake()
    {
        instance = this;
    }

    // TODO: Remove Update
    // TESTING
    void Update()
    {
        if (simulateEMGSignal)
        {
            // Simulate EMG Input
            float strength = Random.Range(1f, 10f);

            int classification = -1;

            if (Input.GetKey(KeyCode.T))
            {
                classification = 0;
            }
            else if (Input.GetKey(KeyCode.F))
            {
                classification = 1;
            }
            else if (Input.GetKey(KeyCode.R))
            {
                classification = 2;
            }

            if (Input.GetKeyUp(KeyCode.T))
            {
                // Triggers Execution
                strength = 0.01f;
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                strength = 0.01f;
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                strength = 0.01f;
            }
            ProcessEmgSignal("", classification, strength, 0.0d);
        }
    }

    public void ProcessEmgSignal(string type, int classification, float strength, double timeStamp)
    {
        if (_currentlyChargedAbility != abilities[classification])
        {
            foreach(EMGAbilityBase ability in abilities)
            {
                ability.TryExecute();
            }
        }

        _currentlyChargedAbility = abilities[classification];
        _currentlyChargedAbility.ProcessSignal(strength);
    }

    private EMGAbilityBase _currentlyChargedAbility;

    public EMGAbilityBase GetCurrentlyChargedAbility() => _currentlyChargedAbility;
}
