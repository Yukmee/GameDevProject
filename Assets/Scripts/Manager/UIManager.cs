using mygame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 引用

    private GameObject InventoryContainer;
    private GameObject PauseMenuContainer;

    private Image hpNow;
    private Image mpNow;
    
    #endregion

    #region 全局变量

    private bool _inventoryOpened;
    private bool _isPaused;

    #endregion

    private void PauseGame()
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
        hpNow = GameObject.Find("HPNow").GetComponent<Image>();
        mpNow = GameObject.Find("MPNow").GetComponent<Image>();
        
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
       
        // Update HP bar
        var hp = PlayerDataManager.instance.playerData.nowHealth;
        var maxHp = PlayerDataManager.instance.playerData.maxHealth;
        hpNow.fillAmount = (float) hp / maxHp;

        // Update Magic Bar
        var mp = PlayerDataManager.instance.playerData.nowMana;
        var maxMp = PlayerDataManager.instance.playerData.maxMana;
        
        // TODO: - Delete me
        Debug.Log("👋MP: " + mp + ", " + maxMp);
        
        mpNow.fillAmount = (float) mp / maxMp;

    }

    private void ShowPauseMenu()
    {
        PauseMenuContainer.SetActive(true);
        
        // Advanced Show "SetActive"
        PauseMenuContainer.GetComponent<CanvasGroup>().alpha = 1;
        PauseMenuContainer.GetComponent<CanvasGroup>().interactable = true;
        PauseMenuContainer.GetComponent<CanvasGroup>().blocksRaycasts = true;
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