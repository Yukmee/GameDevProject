using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mygame
{
    public enum BuffType
    {
        equipment,
        magic,
        system
    }
    public class BuffBase
    {
        public int id;
        public BuffType type;
        public EntityBase caster;
        public EntityManagerBase aim;
        public string describe;
        public float duration;
        public float timeAlive;
        public BuffBase(EntityBase _caster,BuffType _type)
        {
            
        }
        public BuffBase(BuffType _type)
        {

        }
        public BuffBase()
        {

        }
        public virtual void OnStart()
        {

        }
        public virtual void OnEnd()
        {

        }
        public virtual void OnExecute()
        {

        }
    }
}
