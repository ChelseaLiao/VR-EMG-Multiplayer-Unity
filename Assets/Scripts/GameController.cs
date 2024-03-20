using Assets.SteamVR.InteractionSystem.Teleport.Scripts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameArea
{
    public Zombie zombie;
    public List<PointableArea> accessableAfterZombieDead;
}

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }

    public PlayerController player;

    public List<Zombie> zombies;

    public GameArea[] gameAreas;

    public LuminanceLight lighthandler;
    private int enemiesDeadCounter;

    public GameObject winningText;

    public UnityEngine.Events.UnityEvent OnGameEnds;
    float timer = 0.0f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        foreach (Zombie zombie in zombies)
        {
            float playerZombieDistance = Vector3.Distance(player.CurrentViewpoint.position, zombie.transform.position);

            if (playerZombieDistance <= zombie.triggerRange && !player.Dead)
            { 
                //Gets close to player
                if (playerZombieDistance >= zombie.attackRange)
                {
                    zombie.stateMachine.OnAware();

                    //Turns to player
                    Vector3 RotateToPlayer = new Vector3(player.CurrentViewpoint.position.x, zombie.transform.position.y, player.CurrentViewpoint.position.z);
                    zombie.transform.LookAt(RotateToPlayer);
                    //Moves to player
                    zombie.transform.position += zombie.transform.forward * 0.3f * Time.deltaTime;
                }
                //Attacks player 
                else
                {
                    Vector3 RotateToPlayer = new Vector3(player.CurrentViewpoint.position.x, zombie.transform.position.y, player.CurrentViewpoint.position.z);
                    zombie.transform.LookAt(RotateToPlayer);
                    //Debug.Log(zombie.stateMachine.IsAttack);
                    zombie.stateMachine.OnAttack();
                }
            }
            else
            {
                zombie.stateMachine.SetIdle();
            }
        }

        //if all of them dead
        if (enemiesDeadCounter.Equals(zombies.Count))
        {
            GameEnds();

        }

        //if player is dead
        if (player.Dead)
        {
            timer += Time.deltaTime;
            if(timer > 5.0f){
                
                SceneManager.LoadScene("Tutorial");
                GameObject.FindGameObjectWithTag("PlayerUI").SetActive(false);

            }

        }
    }

    public void ZombieDied(Zombie zombie)
    {
        enemiesDeadCounter++;

        var firstArea = gameAreas.Where(i => i.zombie == zombie).FirstOrDefault();
        if (firstArea == null)
            return;

        var areas = firstArea.accessableAfterZombieDead;
        foreach (PointableArea pointable in areas)
        {
            pointable.locked = false;
        }
    }

    public void GameEnds()
    {
        OnGameEnds.Invoke();
        lighthandler.WinningLight();
        winningText.SetActive(true);
        Debug.Log("Game Ends");
        
    }

}
    
