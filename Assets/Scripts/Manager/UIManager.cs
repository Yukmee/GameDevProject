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
    private bool _isPaused;

    #endregion
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        HideInventory();
        HidePauseMenu();
        
        Time.timeScale = 1;
        _isPaused = false;
    }
    
    public void ExitLevel()
    {
        SceneManager.LoadSceneAsync(0);

    }

    
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
            // 如果正在Pause, Resume game
            if (_isPaused)
            {
                ResumeGame();
                return;
            }
            
            // 如果打开了Inventory
            if (_inventoryOpened)
            {
                ResumeGame();
                return;
            }
            
            ShowPauseMenu();
            HideInventory();
            
        }
        
        // "Tab" to show Inventory Menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 只能是__时弹出⏏️
            if (_isPaused)
            {
                return;
            }
            
            // 再按一次就隐藏Inventory
            if (_inventoryOpened)
            {
                HideInventory();
                return;
            }
            
            ShowInventory();
            HidePauseMenu();
        }
       
    }

    private void ShowPauseMenu()
    {
        PauseMenuContainer.SetActive(true);
        PauseGame();

    }
    
    private void HidePauseMenu()
    {
        PauseMenuContainer.SetActive(false);
    }

    private void ShowInventory()
    {
        InventoryContainer.SetActive(true);
        _inventoryOpened = true;
    }

    private void HideInventory()
    {
        InventoryContainer.SetActive(false);
        _inventoryOpened = false;
    }
    
}