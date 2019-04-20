using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{

    /// <summary>
    /// 物品系统管理器，包含物品栏、装备栏&相关方法
    /// </summary>
    [System.Serializable]
    public class ItemManager
    {
        public static ItemManager instance;
        
        public static void Load(string path)
        {
            instance = new ItemManager();
        }
        
        public void DictionaryInit(string jsonString)
        {
            cover = (ItemCover)JsonUtility.FromJson(jsonString,typeof(ItemCover));
            itemDictionary = new Dictionary<int, Item>();
            foreach (var t in cover.itemList)
            {
                itemDictionary.Add(t.itemId, t);
            }
        }
        
        public ItemCover cover;
        public Dictionary<int, Item> itemDictionary;//物品字典
        public List<ItemBlock> weaponList = new List<ItemBlock>();//武器栏
        public List<ItemBlock> armorList = new List<ItemBlock>();//护甲栏
        public List<ItemBlock> ringList = new List<ItemBlock>();//饰品栏
        public List<ItemBlock> expendableList = new List<ItemBlock>();//消耗品栏
        public List<ItemBlock> otherList = new List<ItemBlock>();//其他道具栏
        public ItemBlock nowArmor = new ItemBlock();
        public ItemBlock nowWeapon = new ItemBlock();
        public ItemBlock nowRing = new ItemBlock();
        public ItemBlock nowShield = new ItemBlock();

        public bool equipIn(Item equipment)//将某件物品的附加属性增加至玩家
        {
            if (equipment != null)
            {
                Player player = PlayerDataManager.instance.playerData;
                player.defbonus += equipment.def;
                player.intebonus += equipment.inte;
                player.strbonus += equipment.str;
                player.agibonus += equipment.agi;
                player.endbonus += equipment.end;
                for (int i = 0; i < equipment.effectid.Count; i++)
                {
                    
                    BuffBase b = BuffMethods.GetBuff(equipment.effectid[i]);
                    Debug.Log(b.describe);
                    PlayerDataManager.instance.playerManager.BuffIn(b);
                }
            }
            return true;
        }

        public bool equipOut(Item equipment)//将某件物品的附加属性从玩家身上移除
        {
            if (equipment != null)
            {
                Player player = PlayerDataManager.instance.playerData;
                player.defbonus -= equipment.def;
                player.intebonus -= equipment.inte;
                player.strbonus -= equipment.str;
                player.agibonus -= equipment.agi;
                player.endbonus -= equipment.end;
                for (int i = 0; i < equipment.effectid.Count; i++)
                {
                    BuffBase b = BuffMethods.GetBuff(equipment.effectid[i]);
                    PlayerDataManager.instance.playerManager.Buffout(b);
                }
            }
            return false;
            
        }

        public string equip(ItemBlock aimItemBlock)//选择一件道具并装备，返回相关信息
        {
            Player player = PlayerDataManager.instance.playerData;
            ItemBlock nowItemBlock;
            Item aimEquipment=aimItemBlock.item;
            if (aimEquipment == null)
            {
                return "目标为空";
            }
            if(aimEquipment.type==ItemType.pistol|| aimEquipment.type == ItemType.shotgun|| aimEquipment.type == ItemType.sniperRifle|| aimEquipment.type == ItemType.assaultRifle)
            {
                nowItemBlock = nowWeapon;
            }
            else if (aimEquipment.type == ItemType.armor)
            {
                nowItemBlock = nowArmor;
            }
            else if (aimEquipment.type == ItemType.shield)
            {
                nowItemBlock = nowShield;
            }
            else if (aimEquipment.type == ItemType.ring)
            {
                nowItemBlock = nowRing;
            }
            else
            {
                return "物品类型错误";
            }
            if (aimEquipment.requLevel > player.level)
            {
                return "等级不足";
            }
            Item nowEquipment = nowItemBlock.item;
            equipOut(nowEquipment);
            equipIn(aimEquipment);
            nowItemBlock.item = aimEquipment;
            aimItemBlock.item = nowEquipment;
            return "装备成功";
        }

        public bool itemPick(GameObject aimItem)//捡取一件道具
        {
            ItemOnGround itemOnGround = aimItem.GetComponent<ItemOnGround>();
            Item item = itemOnGround.item; 
            if (item.type == ItemType.pistol || item.type == ItemType.shotgun || item.type == ItemType.sniperRifle || item.type == ItemType.assaultRifle)
            {
                weaponList.Add(new ItemBlock(item));
            }
            else if (item.type == ItemType.armor)
            {
                armorList.Add(new ItemBlock(item));
            }
            else if (item.type == ItemType.ring)
            {
                ringList.Add(new ItemBlock(item));
            }
            else if (item.type == ItemType.expendable)
            {
                expendableList.Add(new ItemBlock(item));
            }
            else if (item.type == ItemType.other)
            {
                otherList.Add(new ItemBlock(item));
            }
            return true;
        }
        public void itemDrop(ItemBlock aimItemBlock)//丢弃一件道具
        {
            
        }
        public void itemUse(ItemBlock aimItemBlock)
        {
            
        }
        public bool itemSell()//出售一件道具
        {
            return true;
        }
    }
}
