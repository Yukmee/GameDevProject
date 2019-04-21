using mygame;
using TMPro;
using UnityEngine;

public class ItemSlotController : MonoBehaviour
{
    private ItemBlock _itemBlock;
    private GameObject ItemNumber;
    
    private GameObject WeaponTab;
    private ItemManager _itemManager;
    private int index;

    private void Awake()
    {
        WeaponTab = GameObject.Find("WeaponTab");
        ItemNumber = GameObject.Find("ItemNumber");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        // Get Index First, it actually works!
        index = transform.GetSiblingIndex();
        
    }

    // Update is called once per frame
    void Update()
    {
        // ItemNumber.GetComponent<TextMeshPro>().text = _itemBlock.num.ToString();
        
    }

    private void OnMouseOver()
    {
        // TODO: - Display Item Info
        Debug.Log("🅾️Mouse Hovered over index: " + index + ".");
        
    }
    
    
}
