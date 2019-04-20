using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using mygame;

public class AI : MonoBehaviour
{
    public float viewRadius = 35;
    public Transform TargetObject = null;
    // Start is called before the first frame update
    public GameObject target;
    public EnemyView view;
    public EnemyManager enemyManager;
    public NavMeshAgent navMeshAgent;
    EnemyState StateChange(EnemyState aim)
    {
        if (aim == EnemyState.dead)
        {
            enemyManager.enemyState = aim;
        }
        else if (aim == EnemyState.attack)
        {
            if (enemyManager.enemyState != EnemyState.dead)
            {
                enemyManager.enemyState = aim;
            }
        }
        else if (aim == EnemyState.move)
        {
            if (enemyManager.enemyState == EnemyState.stand)
            {
                enemyManager.enemyState = aim;
            }
        }
        else if (aim == EnemyState.stand)
        {
            if (enemyManager.enemyState != EnemyState.dead)
            {
                enemyManager.enemyState = aim;
            }
        }
        return enemyManager.enemyState;
    }
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        GameObject View = Instantiate(Resources.Load("Prefab/View"), transform.position, new Quaternion()) as GameObject;
        view = View.AddComponent<EnemyView>();
        view.ai = this;
        View.transform.parent = transform;
    }
    void CheckPlayer()
    {
        if (target != null) {
            if (Vector3.Distance(transform.position, target.transform.position) >= viewRadius)
            {
                target = null;
                navMeshAgent.destination = this.transform.position;
            }
        }
    }
    void CheckAttack()
    {
        if (true)//todo:如果target在范围内则攻击
        {
            Attack();
        }
    }
    void Attack()
    {
        //todo:攻击方法
    }
    void Move()
    {
        if (target != null)
        {
            if (StateChange(EnemyState.move) == EnemyState.move)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPlayer();//目标在警戒范围外则丢失目标
        if (enemyManager.enemyState == EnemyState.attack || enemyManager.enemyState == EnemyState.dead)
        {
            navMeshAgent.isStopped = true;//当攻击或死亡时停止寻路
        }
        else
        {
            navMeshAgent.isStopped = false;//否则继续寻路
        }
        if(enemyManager.enemyState==EnemyState.move|| enemyManager.enemyState == EnemyState.stand)
        {
            CheckAttack();//目标在攻击范围内则攻击
        }
        Move();
    }
}
