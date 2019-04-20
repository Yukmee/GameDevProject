using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity;

namespace mygame
{
    public static class BuffMethods
    {
        public static BuffBase GetBuff(int n)
        {
            switch (n)
            {
                case 0:
                    return new Explode();
                case 1:
                    return new LifeSteal();
                case 2:
                    return new PoisonHand();
                case 3:
                    return new Poison();
                case 4:
                    return new LifeRecover();
                default:
                    return new BuffBase();
            }
        }
        public static string GetBuffdescribe(int n)
        {
            switch (n)
            {
                case 0:
                    return (new Explode()).describe;
                case 1:
                    return (new LifeSteal()).describe;
                case 2:
                    return (new PoisonHand()).describe;
                case 3:
                    return (new Poison()).describe;
                case 4:
                    return (new LifeRecover()).describe;
                default:
                    return (new BuffBase()).describe;
            }
        }
    }
    
    public class Explode:BuffBase
    {
        OnHitDelegate onHitDelegate;
        public Explode()
        {
            describe = "击中时10%几率造成爆炸";
            id = 0;
        }
        //todo: 寻找粒子素材 修复物理效果
        public void OnExecute(EntityManagerBase victim)
        {
            if(UnityEngine.Random.Range(0, 100) <= 10)
            {
                PlayerManager p = (PlayerManager)aim;
                GameObject a = UnityEngine.Object.Instantiate(Resources.Load("Prefab/BuffEffects/Buff0Effect"), victim.gameObject.transform.position, new Quaternion()) as GameObject;
                a.GetComponent<Buff0Effect>().damage = new Damage(p.player.level * 10, p.player.level * 8, p.player.finalCritRate, p.player.finalCritPower,p);
            }
            //在victim的位置制造一个爆炸
        }
        public override void OnEnd()
        {
            if (aim.onHitDelegate != null)
            {
                Delegate.Remove(aim.onHitDelegate, onHitDelegate);
            }
        }
        public override void OnStart()
        {
            onHitDelegate = new OnHitDelegate(OnExecute);
            if (aim.onHitDelegate == null)
            {
                aim.onHitDelegate = onHitDelegate;
            }
            else
            {
                aim.onHitDelegate += onHitDelegate;
            }
        }
    }
    public class LifeSteal : BuffBase
    {
        OnHitDelegate onHitDelegate;
        public LifeSteal()
        {
            describe = "击中时恢复2%生命";
            id = 1;
        }
        public void OnExecute(EntityManagerBase victim)
        {
            if (aim.GetType() == typeof(PlayerManager))
            {
                PlayerManager pm = aim as PlayerManager;
                int h = (int)(pm.player.maxHealth * 2 / 100);
                if (h < 1)
                {
                    h = 1;
                }
                pm.OnHealTaken(new Heal(h));
            }
            else if(aim.GetType()==typeof(EnemyManager))
            {
                EnemyManager em = aim as EnemyManager;
                int h = (int)(em.enemy.maxHealth * 2 / 100);
                if (h < 1)
                {
                    h = 1;
                }
                em.OnHealTaken(new Heal(h));
            }
            //
        }
        public override void OnEnd()
        {
            if (aim.onHitDelegate != null)
            {
                Delegate.Remove(aim.onHitDelegate, onHitDelegate);
            }
        }
        public override void OnStart()
        {
            onHitDelegate = new OnHitDelegate(OnExecute);
            if (aim.onHitDelegate == null)
            {
                aim.onHitDelegate = onHitDelegate;
            }
            else
            {
                aim.onHitDelegate += onHitDelegate;
            }
        }
    }
    public class PoisonHand : BuffBase
    {
        public PoisonHand()
        {
            describe = "攻击时有几率使敌人中毒";
            id = 2;
        }
    }
    public class Poison : BuffBase
    {

    }
    public class LifeRecover : BuffBase
    {

    }
}
