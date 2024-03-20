using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public void Awake()
    {
        instance = this;
    }
}
