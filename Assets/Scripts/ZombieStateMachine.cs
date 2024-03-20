using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieStateMachine : MonoBehaviour

{
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    private bool isAware = false;
    private bool isAttack = false;
    private bool isBreak = false;
    private bool isFallback = false;
    // float timer = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    //Transform playerTransform;

    // // Zombie's behaviors
    // public enum EnemyState
    // {
    //     Idle,
    //     Attack,
    //     FallingBack,
    //     FallingForward,
    //     Walk,
    //     Run
    // }
    // Start is called before the first frame update
    void Start()
    {
        //Get the Animator, which you attach to the GameObject you intend to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        //m_Animator = gameObject.GetComponentInChildren<Animator>();
        //Get player1's transform
        //GetPlayerTransform();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("isWalk", isAware);
        m_Animator.SetBool("isAttack", isAttack);
        m_Animator.SetBool("isBreak", isBreak);
        m_Animator.SetBool("isFallBack", isFallback);
    }

    public void OnAware()
    {
        isAware = true;
        isAttack = false;
        isBreak = false;
    }
    public void OnAttack()
    {
        isAttack = true;
        isBreak = false;
        isAware = true;
    }
    public void OnDead()
    {
        isFallback = true;
    }

    public void SetIdle()
    {
        isBreak = true;
        isAttack = false;
        isAware = false;
    }
}
