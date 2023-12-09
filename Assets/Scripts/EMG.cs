using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMG : MonoBehaviour
{
    public GameObject zombie;
    Animator zombie_Animator;
    bool slow_down = false;
    float slowndown_time;
    // Start is called before the first frame update
    void Start()
    {
        zombie_Animator = zombie.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")){
            Debug.Log("p press");
            slow_down = true;
            slowndown_time = 5.0f;
        }
        if(slow_down == true)
            SlownDown();
    }
    public void SlownDown()
    {
        Debug.Log("time: "+slowndown_time+"speed: "+zombie_Animator.speed);
        if(slowndown_time>0){
            zombie_Animator.speed = 0.3f;
            slowndown_time -= Time.deltaTime;
        }
        if(slowndown_time<=0) 
            zombie_Animator.speed = 0.6f;
    }
}
