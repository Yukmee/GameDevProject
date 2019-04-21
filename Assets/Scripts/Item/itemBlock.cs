using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    /// <summary>
    /// 物品栏
    /// </summary>
    [System.Serializable]
    public class ItemBlock
    {
        public Item item;
        public int num;
        
        // 快捷Init函数，默认数量为1
        public ItemBlock(Item item)
        {
            this.item = item;
            num = 1;
        }
        
        public ItemBlock(Item item, int num)
        {
            this.item = item;
            this.num = num;
        }

        public ItemBlock()
        {
            
        }
    }
}
