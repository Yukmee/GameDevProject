using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

namespace mygame
{
    [System.Serializable]
    public class Player:EntityBase
    {
        public int exp;
        public float critRate;
        public float critRateBonus;
        public float finalCritRate;
        public float critPower;
        public float critPowerBonus;
        public float finalCritPower;
        public int skillPoint;
    }
}