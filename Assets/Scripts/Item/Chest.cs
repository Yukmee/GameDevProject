using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    public class Chest : MonoBehaviour
    {
        public int itemLevel;
        public Item _item;
        public bool empty = false;
        // Start is called before the first frame update
        void Start()
        {
            int a = Random.Range(0, 100);
            if (a <= 33)
            {
                _item = Item.CreateWeapon(itemLevel);
            }
            else if (a > 33 && a <= 66)
            {
                _item = Item.CreateArmor(itemLevel);
            }
            else
            {
                _item = Item.CreateRing(itemLevel);
            }
        }
        public void onOpen()
        {
            if (!empty)
            {
                if (_item != null&&_item.name!="")
                {
                    GameObject item = Instantiate(Resources.Load("Prefab/ItemOnGround"), transform.position, new Quaternion()) as GameObject;
                    ItemOnGround iog = item.AddComponent<ItemOnGround>();
                    iog.item = _item;
                    iog.num = 1;
                }
                Destroy(gameObject, 2);
                empty = true;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
