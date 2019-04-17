using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

namespace mygame
{
    
    public enum EnemyType
    {
        normal,
        boss
    }
    [System.Serializable]
    public class Enemy:EntityBase
    {
        public int enemyId;
        public EnemyType enemyType;
        public int exp;
    }
}