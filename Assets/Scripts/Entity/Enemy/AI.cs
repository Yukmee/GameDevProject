using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using mygame;
public class AI : MonoBehaviour
{
    public float viewRadius = 35;
    public float attackRadius = 3;
    public Transform TargetObject = null;
    // Start is called before the first frame update
    public GameObject target;
    public GameObject attackTarget;
    public EnemyView view;
    public float attackTimeStamp = 0;
    public EnemyManager enemyManager;
    public NavMeshAgent navMeshAgent;
    public bool AttackSector(float distance)
    {
        //一条向前的射线
        if (AttackRay(gameObject, Quaternion.identity,distance,UnityEngine.Color.green))
            return true;
        //多一个精确度就多两条对称的射线,每条射线夹角是总角度除与精度
        float subAngle = 30 / 6;
        for (int i = 0; i < 6; i++)
        {
            if (AttackRay(gameObject, Quaternion.Euler(0, -1 * subAngle * (i + 1), 0), distance, UnityEngine.Color.green)
                || AttackRay(gameObject, Quaternion.Euler(0, subAngle * (i + 1), 0), distance, UnityEngine.Color.green))
                return true;
        }
        return false;
    }
    public static bool AttackRay(GameObject attacker, Quaternion eulerAnger,float distance, UnityEngine.Color DebugColor)
    {
        Debug.DrawRay(attacker.transform.position, eulerAnger * attacker.transform.forward.normalized* distance, DebugColor,60);
        RaycastHit hit;
        if (Physics.Raycast(attacker.transform.position, eulerAnger * attacker.transform.forward, out hit, distance) && hit.collider.CompareTag("Player"))
        {
            attacker.GetComponent<AI>().target = hit.transform.gameObject;
            return true;
        }
        return false;
    }
    IEnumerator AttackCoroutine(float beforeAtk,float afterAtk)
    {
        if (StateChange(EnemyState.attack) == EnemyState.attack)
        {
            yield return new WaitForSeconds(beforeAtk);
            AttackSector(attackRadius);
            //todo 具体的攻击代码
            yield return new WaitForSeconds(afterAtk);
            StateChange(EnemyState.stand);
        }
        else
        {
            yield return null;
        }
    }
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
        if (Vector3.Distance(transform.position,target.transform.position)<=attackRadius)
        {
            Attack();
        }
    }
    void Attack()
    {
        if (Time.time >= attackTimeStamp||enemyManager.enemyState==EnemyState.stand||enemyManager.enemyState==EnemyState.move)
        {
            StartCoroutine(AttackCoroutine(0.5f, 0.5f));
            attackTimeStamp += 2;
        }
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
        if(enemyManager.enemyState==EnemyState.move|| enemyManager.enemyState == EnemyState.stand&&target!=null)
        {
            CheckAttack();//目标在攻击范围内则攻击
        }
        Move();
    }
}
