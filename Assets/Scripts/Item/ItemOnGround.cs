using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    public class ItemOnGround:MonoBehaviour
    {
        public Item item;
        public int num;
        public void onPick()
        {
            if (item != null)
            {
                Destroy(gameObject);
            }
        }
    }
}