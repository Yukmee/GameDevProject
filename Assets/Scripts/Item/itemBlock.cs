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
        
        public ItemBlock(Item item)
        {
            this.item = item;
        }
        
        public ItemBlock()
        {
            
        }
    }
}
