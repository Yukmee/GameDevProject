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
        public EnemyState enemyState;
        public override void OnDamageTaken(Damage damage)
        {
            int dam = Random.Range(damage.min, damage.max);
            if (Random.Range(0, 100) > damage.critRate)
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
            base.OnDeath();
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            if (enemy.nowHealth <= 0)
            {
                OnDeath();
            }
        }
    }
}
