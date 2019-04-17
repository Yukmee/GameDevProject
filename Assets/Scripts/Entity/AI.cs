using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using mygame;
public class AI : MonoBehaviour
{
    public Transform TargetObject = null;
    // Start is called before the first frame update
    public GameObject target;
    public EnemyManager enemyManager;
    public NavMeshAgent navMeshAgent;
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void checkPlayer()
    {
        
    }
    void checkAttack()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (enemyManager.enemyState == EnemyState.attack || enemyManager.enemyState == EnemyState.dead)
        {
            navMeshAgent.isStopped = true;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
        checkPlayer();//敌对单位在视野范围内则设为目标
        checkAttack();//目标在攻击范围内则攻击
    }
}
