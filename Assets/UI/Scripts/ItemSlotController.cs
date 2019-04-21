using mygame;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour
{
    private ItemBlock _itemBlock;
    private GameObject ItemNumber;

    private ItemManager _itemManager;

    private void Awake()
    {
        ItemNumber = GameObject.Find("ItemNumber");
        _itemManager = ItemManager.instance;
    }

    
    // Start is called before the first frame update
    public void Start()
    {

        // TODO: - Delete me
        ItemManager.instance.weaponList.Add(new ItemBlock(ItemManager.instance.itemDictionary[0]));

        // Get Index First
        var index = transform.GetSiblingIndex();

        // Debug.Log("Weapon List Count: " + ItemManager.instance.weaponList.Count);
        
        if (index < ItemManager.instance.weaponList.Count)
        {
            // TODO: - Delete me
            // Debug.Log(this + " / " + index);

            _itemBlock = ItemManager.instance.weaponList[index];
        }

        // 
    }

    // Update is called once per frame
    private void Update()
    {
        if (_itemBlock != null)
        {
            // Debug.Log(_itemBlock.item);
            // Debug.Log(_itemBlock.num);
            // Debug.Log(ItemNumber);
        
            // TODO: - 展示对应的图片
            
            // Display Item Number
            ItemNumber.GetComponent<Text>().text = _itemBlock.num.ToString();
        }
        
        // Test RayCast
        
        
    }

    private void OnMouseOver()
    {
        Debug.Log("✅MouseOver!");
    }
}