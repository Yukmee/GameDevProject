using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mygame
{
    [System.Serializable]
    public enum ItemType
    {
        shotgun,
        pistol,
        sniperRifle,
        assaultRifle,
        armor,
        shield,
        ring,
        expendable,
        other
    }

    /// <summary>
    /// 代表稀有程度。
    /// </summary>
    public enum Color
    {
        white,
        green,
        golden
    }

    /// <summary>
    /// 武器、防具和消耗品都是继承自此类。
    /// </summary>
    [System.Serializable]
    public class Item
    {
        public int itemId; //物品id
        public ItemType type; //物品类型
        public string name; //物品名称
        public string describe; //物品描述
        public Color color; //物品稀有程度
        public int ItemLevel; //物品等级,决定物品效果和价格

        public int requLevel; //需求等级

        //以下是装备用属性
        public int def; //防御
        public int str; //力量
        public int end; //耐力
        public int inte; //智力
        public int agi; //敏捷
        public int minatk; //最小攻击
        public int maxatk; //最大攻击
        public int atkCooldown; //攻击间隔
        public float critRate; //暴击几率
        public float critPower; //暴击倍率
        public int bulletNum; //每次攻击发射子弹数量,霰弹枪用
        public int bulletSpeed; //子弹飞行速度

        public int fireMultiply; //多重射击

        //以下是护盾用属性
        public float recoverSpeed; //恢复速度
        public float recoverCooldown; //护盾从被攻击到开始恢复的间隔

        public int shieldPower; //护盾容量

        //以下是消耗品用属性
        public int maxpile; //堆叠上限
        public int hpRecover; //消耗品用属性，hp回复量
        public int mpRecover; //消耗品用属性，mp回复量
        public List<int> effectid; //道具特殊效果的id

        private static TOut TransReflection<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            var tInType = tIn.GetType();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name);
                ;
                if (itemIn != null)
                {
                    itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                }
            }

            return tOut;
        }

        public static Item CreateWeapon(int itemLevel)
        {
            List<Item> wList = new List<Item>();
            for (int i = 0; i < ItemManager.instance.itemDictionary.Count; i++)
            {
                if (ItemManager.instance.itemDictionary[i].ItemLevel == itemLevel &&
                    (ItemManager.instance.itemDictionary[i].type == ItemType.pistol ||
                     ItemManager.instance.itemDictionary[i].type == ItemType.shotgun ||
                     ItemManager.instance.itemDictionary[i].type == ItemType.assaultRifle ||
                     ItemManager.instance.itemDictionary[i].type == ItemType.sniperRifle))
                {
                    //wList.Add(TransReflection<Item, Item>(ItemManager.instance.itemDictionary[i]));
                    wList.Add(ItemManager.instance.itemDictionary[i]);
                }
            }

            if (wList.Count == 0)
            {
                return null;
            }

            int r = UnityEngine.Random.Range(0, wList.Count);
            /*这里需要增加随机化功能
             
             */
            return wList[r];
        }

        public static Item CreateRing(int itemLevel)
        {
            List<Item> rList = new List<Item>();
            for (int i = 0; i < ItemManager.instance.itemDictionary.Count; i++)
            {
                if (ItemManager.instance.itemDictionary[i].ItemLevel == itemLevel &&
                    (ItemManager.instance.itemDictionary[i].type == ItemType.ring))
                {
                    rList.Add(TransReflection<Item, Item>(ItemManager.instance.itemDictionary[i]));
                }
            }

            if (rList.Count == 0)
            {
                return null;
            }

            int r = UnityEngine.Random.Range(0, rList.Count);
            /*这里需要增加随机化功能
             
             */
            return rList[r];
        }

        public static Item CreateArmor(int itemLevel)
        {
            List<Item> aList = new List<Item>();
            for (int i = 0; i < ItemManager.instance.itemDictionary.Count; i++)
            {
                if (ItemManager.instance.itemDictionary[i].ItemLevel == itemLevel &&
                    (ItemManager.instance.itemDictionary[i].type == ItemType.armor))
                {
                    aList.Add(TransReflection<Item, Item>(ItemManager.instance.itemDictionary[i]));
                }
            }

            if (aList.Count == 0)
            {
                return null;
            }

            int r = UnityEngine.Random.Range(0, aList.Count);
            /*这里需要增加随机化功能
             
             */
            return aList[r];
        }
    }
}