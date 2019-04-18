using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

namespace mygame
{
    public delegate void UpdateDelegate();
    public delegate void MoveDelegate();
    public delegate void DashDelegate();
    public delegate void AttackDelegate();
    public delegate void OnDamageTakenDelegate();
    public delegate void OnKillDelegate();
    public delegate void OnHitDelegate(EntityManagerBase victim);
    [System.Serializable]
    public class Damage{
        public int max;
        public int min;
        public float critRate;
        public float critPower;
        public Damage(int _max,int _min,float _critRate,float _critPower) {
            max = _max;
            min = _min;
            critPower = _critPower;
            critRate = _critRate;
        }
    }
    public class Heal
    {
        public int heal;
        public Heal(int _heal)
        {
            heal = _heal;
        }
    }
    public class EntityManagerBase:MonoBehaviour
    {
        public UpdateDelegate updateDelegate;
        public MoveDelegate moveDelegate;
        public DashDelegate dashDelegate;
        public AttackDelegate attackDelegate;
        public OnDamageTakenDelegate onDamageTakenDelegate;
        public OnHitDelegate onHitDelegate;
        public OnKillDelegate onKillDelegate;
        
        public virtual void OnDamageTaken(Damage damage)
        {
            
            
        }
        public virtual void OnDeath()
        {

        }
        public virtual void OnHealTaken(Heal heal)
        {

        }
        public virtual void OnHit(EntityManagerBase victim)
        {
            if (onHitDelegate != null)
            {
                onHitDelegate(victim);
            }
        }
        public virtual void OnKill(EntityManagerBase victim)
        {

        }
        public virtual void BuffIn(BuffBase buff)
        {

        }
        public virtual void Buffout(BuffBase buff)
        {

        }
    }
}
