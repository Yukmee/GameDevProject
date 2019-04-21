using mygame;
using TMPro;
using UnityEngine;

public class ItemSlotController : MonoBehaviour
{
    private ItemBlock _itemBlock;
    private GameObject ItemNumber;
    
    private GameObject WeaponTab;
    private ItemManager _itemManager;

    private void Awake()
    {
        WeaponTab = GameObject.Find("WeaponTab");
        ItemNumber = GameObject.Find("ItemNumber");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        // Get Index First
        var index = transform.GetSiblingIndex();

        Debug.Log(ItemManager.instance.weaponList.Count);
        
        if (index < ItemManager.instance.weaponList.Count)
        {

            // TODO: - Delete me
            Debug.Log(this + " / " + index);


            _itemBlock = ItemManager.instance.weaponList[index];
        }

        // 
    }

    // Update is called once per frame
    void Update()
    {
        if (_itemBlock != null)
        {
            ItemNumber.GetComponent<TextMeshPro>().text = _itemBlock.num.ToString();
        }
        
    }
}
