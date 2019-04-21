using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

namespace mygame
{
    public enum EnemyState
    {
        stand,
        move,
        attack,
        dead
    }
    public class EnemyManager: EntityManagerBase
    {
        public Enemy enemy;
        public int deathcount = 0;
        public EnemyState enemyState;
        public override void OnDamageTaken(Damage damage)
        {
            int dam = Random.Range(damage.min, damage.max);
            
            // For DEBUG Only
            Debug.Log(dam);
            
            if (damage.attacker != null)
            {
                damage.attacker.OnHit(this);
            }
            if (Random.Range(0, 100) <= damage.critRate)
            {
                dam = (int)(dam * damage.critPower);
            }
            dam = dam - enemy.finaldef;
            if (dam <= 0)
            {
                dam = 1;
            }
            
            enemy.nowHealth -= dam;
        }
        public override void OnDeath()
        {
            enemyState = EnemyState.dead;
            Destroy(gameObject, 0.8f);
            deathcount = 1;
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            if (enemy.nowHealth <= 0 && deathcount == 0)
            {
                OnDeath();
            }
        }
    }
}
