using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 引用

    GameObject InventoryContainer;

    #endregion

    private void Awake()
    {
        InventoryContainer = GameObject.Find("InventoryCtnr");
        
        // Hide things first
        InventoryContainer.SetActive(false);
        
    }

    private void Update()
    {
        // Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Hide Inventory Menu
            InventoryContainer.SetActive(false);
            
            // TODO: - Show Pause Menu
        }
        
        // Inventory Menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // TODO: - Hide Pause Menu
            
            
            // Open Up Inventory Menu
            InventoryContainer.SetActive(true);
            
        }
    }
}
