using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSteamVRMouseFollower : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;
        if(Input.GetKey(KeyCode.Space))
        {
            mousePosition.z = 1.8f;
            timer = 0.5f;
            if(timer > 0)
                timer -= Time.deltaTime;
            else
                mousePosition.z = 1.3f;
        }
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = worldPosition;
        
    }
}
