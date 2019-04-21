using mygame;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour
{
    private ItemBlock _itemBlock;
    private GameObject ItemNumber;

    private ItemManager _itemManager;

    private void Awake()
    {
        ItemNumber = GameObject.Find("ItemNumber");
    }

    
    // Start is called before the first frame update
    void Start()
    {

        ItemManager.instance.weaponList.Add(new ItemBlock(ItemManager.instance.itemDictionary[0]));
        
        // Get Index First
        var index = transform.GetSiblingIndex();

        Debug.Log("Weapon List Count: " + ItemManager.instance.weaponList.Count);
        
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
            Debug.Log(_itemBlock.item);
            Debug.Log(_itemBlock.num);
            Debug.Log(ItemNumber);
            Debug.Log(ItemNumber.GetComponent<Text>().text);
            // ItemNumber.GetComponent<TextMeshPro>().text = _itemBlock.num.ToString();
        }
        
    }
}
