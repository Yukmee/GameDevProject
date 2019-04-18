using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

namespace mygame
{
    public class Command
    {

    }
    public class MoveCommand : Command
    {
        public float h;
        public float v;
        public MoveCommand(float _h,float _v)
        {
            h = _h;
            v = _v;
        }
    }
    public class AttackCommand : Command
    {

    }
    public class DashCommand : Command
    {
        public float h;
        public float v;
        public DashCommand(float _h, float _v)
        {
            h = _h;
            v = _v;
        }
    }
    public class PlayerManager : EntityManagerBase
    {
        public Player player;
        public void GetCommand(Command c)
        {
            if (c.GetType() == typeof(DashCommand))
            {
                DashCommand dc = (DashCommand)c;
                Dash(dc);
            }
            else if (c.GetType() == typeof(MoveCommand))
            {
                MoveCommand mc = (MoveCommand)c;
                Move(mc);
            }
            else if (c.GetType() == typeof(AttackCommand))
            {
                AttackCommand ac = (AttackCommand)c;
                Attack(ac);
            }
            else
            {
                throw new System.Exception("Unknown Command");
            }
        }
        public void Attack(AttackCommand ac)
        {
            if (attackDelegate != null)
            {
                attackDelegate();
            }
        }
        public void EntityDataUpdate()
        {
            player.finalagi = player.agi + player.agibonus;
            player.finalend = player.end + player.endbonus;
            player.finalstr = player.str + player.strbonus;
            player.finalinte = player.inte + player.intebonus;
            player.finaldef = player.def + player.defbonus;
            player.baseHealth = player.level * 10 + player.finalend * 5;
            player.maxHealth = player.baseHealth + player.extraHealth;
            player.baseMana = player.level * 10 + player.finalinte * 5;
            player.maxMana = player.baseMana + player.extraMana;
        }
        public void Move(MoveCommand mc)
        {
            if (moveDelegate != null)
            {
                moveDelegate();
            }
        }
        public void Dash(DashCommand dc)
        {
            if (dashDelegate != null)
            {
                dashDelegate();
            }
        }
        public override void BuffIn(BuffBase buff)
        {
            buff.aim = this;
            buff.OnStart();
        }
        public override void Buffout(BuffBase buff)
        {
            buff.OnEnd();
            player.effectList.Remove(buff);
        }
        public override void OnDeath()
        {
            base.OnDeath();
        }
        public override void OnHit(EntityManagerBase victim)
        {
            base.OnHit(victim);
        }
        public override void OnDamageTaken(Damage damage)
        {
            int dam = Random.Range(damage.min, damage.max);
            if (Random.Range(0, 100) > damage.critRate)
            {
                dam = (int)(dam * damage.critPower);
            }
            dam = dam - player.finaldef;
            if (dam <= 0)
            {
                dam = 1;
            }
            player.nowHealth -= dam;
        }
        public override void OnHealTaken(Heal heal)
        {
            player.nowHealth += heal.heal;
            if (player.nowHealth > player.maxHealth)
            {
                player.nowHealth = player.maxHealth;
            }
        }
        public void Start()
        {

        }
        public void FixedUpdate()
        {
            if (updateDelegate!= null)
            {
                updateDelegate();
            }
            EntityDataUpdate();
        }
    }
}