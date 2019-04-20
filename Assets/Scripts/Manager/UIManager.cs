using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region 引用
    
    GameObject InventoryContainer;
    GameObject PauseMenuContainer;
    
    #endregion

    #region 全局变量

    private bool _inventoryOpened;
    private bool _pauseMenuOpened;

    #endregion
    
    private void Awake()
    {
        InventoryContainer = GameObject.Find("InventoryCtnr");
        PauseMenuContainer = GameObject.Find("PauseMenuCtnr");
        
        // Hide things first
        InventoryContainer.SetActive(false);
        PauseMenuContainer.SetActive(false);
    }

    private void Update()
    {
        // ESC to bring up Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 如果先打开了Inventory，那么就仅是隐藏Inventory而已
            if (_inventoryOpened)
            {
                // Merely Hide Inventory Menu
                InventoryContainer.SetActive(false); 
            }
            else // 否则才是显示PauseMenu
            {
                // Show Pause Menu
                PauseMenuContainer.SetActive(true);
                _pauseMenuOpened = true;
                
                // Make it pause
                Time.timeScale = 0;
            }
        }
        
        // "Tab" to show Inventory Menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Hide Pause Menu
            if (_pauseMenuOpened == true)
            {
                PauseMenuContainer.SetActive(false);
            }
            
            // Show Inventory Menu
            InventoryContainer.SetActive(true);
            _inventoryOpened = true;

        }
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    
    public void ExitLevel()
    {
        SceneManager.LoadSceneAsync(0);

    }
    
}