using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    /// <summary>
    /// 在地上的物品
    /// </summary>
    public class ItemOnGround:MonoBehaviour
    {
        public Item item;
        public int num;
        
        public void onPick()
        {
            if (item != null)
            {
                ItemManager.instance.itemPick(gameObject);
                Destroy(gameObject);
            }
        }
    }
}