using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

namespace mygame
{
    [System.Serializable]
    public class EntityBase
    {
        public string name;
        public int level;// <----
        public int maxHealth;// <----
        public int maxMana;
        public int baseHealth;
        public int baseMana;
        public int extraHealth;
        public int extraMana;
        public int nowHealth; // <----
        public int nowMana;
        public int def;
        public int defbonus;
        
        public int finaldef;// <----
        
        public int str;//力量
        public int strbonus;
        public int finalstr;// <----
        public int end;//耐力
        public int endbonus;
        public int finalend; // <----
        public int inte;//智力
        public int intebonus;
        public int finalinte;// <----
        public int agi;//敏捷
        public int agibonus;
        public int finalagi;// <----
        [System.NonSerialized]
        public List<BuffBase> effectList=new List<BuffBase>();
    }
}
