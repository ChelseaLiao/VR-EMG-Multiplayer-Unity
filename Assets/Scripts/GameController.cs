using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }

    public PlayerController player;

    public List<Zombie> zombies;


    public LuminanceLight lighthandler;
    private int enemiesDeadCounter;

    public GameObject winningText;



    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        foreach (Zombie zombie in zombies)
        {
            float playerZombieDistance = Vector3.Distance(player.viewpoint.position, zombie.transform.position);

            if (playerZombieDistance <= zombie.triggerRange && player.currentHealth>0)
            {
                //Gets close to player
                if (playerZombieDistance >= zombie.attackRange)
                {
                    zombie.stateMachine.OnAware();

                    //Turns to player
                    Vector3 RotateToPlayer = new Vector3(player.viewpoint.position.x, zombie.transform.position.y, player.viewpoint.position.z);
                    zombie.transform.LookAt(RotateToPlayer);
                    //Moves to player
                    zombie.transform.position += zombie.transform.forward * 0.3f * Time.deltaTime;
                }
                //Attacks player 
                else
                {
                    Vector3 RotateToPlayer = new Vector3(player.viewpoint.position.x, zombie.transform.position.y, player.viewpoint.position.z);
                    zombie.transform.LookAt(RotateToPlayer);
                    //Debug.Log(zombie.stateMachine.IsAttack);
                    zombie.stateMachine.OnAttack();
                }
            }
            else
            {
                zombie.stateMachine.SetIdle();
            }
            // if(player.currentHealth ==0)
            // {
            //     Debug.Log("stop");
            //     zombie.stateMachine.OnDead();
            // }

            //check how many are dead
            if (zombie.currentHealth == 0)
            {
                enemiesDeadCounter++;
            }

        }

        //if all of them dead
        if (enemiesDeadCounter.Equals(zombies.Count))
        {
            GameEnds();

        }
        //if player is dead
        if (player.currentHealth== 0)
        {
            GameOver();

        }
    
    }

    public void GameEnds()
    {
        lighthandler.WinningLight();
        winningText.active = true;
        Debug.Log("Game Ends");
        
    }
    public void GameOver()
    {
        // if(Time.deltaTime > 8.0f){
        //     Debug.Log(Time.deltaTime);
        //     SceneManager.LoadScene("Tutorial");
        // }
        
    }
}
    
