using mygame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class UIManager : MonoBehaviour
{
    #region 引用

    private GameObject InventoryContainer;
    private GameObject PauseMenuContainer;

    // HP&MP Bar
    private Image hpNow;
    private Image mpNow;
    
    // 人物属性
    private GameObject HPValue;
    private GameObject MPValue;
    private GameObject LevelValue;
    private GameObject DefenseValue;
    
    private GameObject StrengthValue;
    private GameObject IntelliValue;
    private GameObject RapidValue;
    private GameObject CriticalValue;
    
    
    
    #endregion

    #region 全局变量

    private bool _inventoryOpened;
    private bool _isPaused;

    #endregion

    private void Awake()
    {
        InventoryContainer = GameObject.Find("InventoryCtnr");
        PauseMenuContainer = GameObject.Find("PauseMenuCtnr");
        hpNow = GameObject.Find("HPNow").GetComponent<Image>();
        mpNow = GameObject.Find("MPNow").GetComponent<Image>();
        HPValue = GameObject.Find("HPValue");
        MPValue = GameObject.Find("MPValue");
        LevelValue = GameObject.Find("LevelValue");
        DefenseValue = GameObject.Find("DefenseValue");
        
        StrengthValue = GameObject.Find("StrengthValue");
        IntelliValue = GameObject.Find("IntelliValue");
        RapidValue = GameObject.Find("RapidValue");
        CriticalValue = GameObject.Find("CriticalValue");
        
        
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

        // Get Player's Info First
        var hp = PlayerDataManager.instance.playerData.nowHealth;
        var maxHp = PlayerDataManager.instance.playerData.maxHealth;
        var mp = PlayerDataManager.instance.playerData.nowMana;
        var maxMp = PlayerDataManager.instance.playerData.maxMana;
        var playerLevel = PlayerDataManager.instance.playerData.level;
        var defenseValue = PlayerDataManager.instance.playerData.finaldef;
        var strengthValue = PlayerDataManager.instance.playerData.finalstr;
        var intelliValue = PlayerDataManager.instance.playerData.finalinte;
        var rapidValue = PlayerDataManager.instance.playerData.finalagi;
        var criPr = PlayerDataManager.instance.playerData.finalCritRate;
        var criPw = PlayerDataManager.instance.playerData.finalCritPower;


        // Update HP bar
        hpNow.fillAmount = (float)hp / maxHp;
        // Update Magic Bar
        mpNow.fillAmount = (float)mp / maxMp;

        // Update 人数属性Panel
        HPValue.GetComponent<Text>().text = hp + " / " + maxHp;
        MPValue.GetComponent<Text>().text = mp + " / " + maxMp;
        LevelValue.GetComponent<Text>().text = "Level " + playerLevel;
        DefenseValue.GetComponent<Text>().text = defenseValue.ToString();

        StrengthValue.GetComponent<Text>().text = defenseValue.ToString();
        IntelliValue.GetComponent<Text>().text = intelliValue.ToString();
        RapidValue.GetComponent<Text>().text = rapidValue.ToString();
        CriticalValue.GetComponent<Text>().text = criPr + "% / x" + criPw + "!";
        
        // Update 右边的装备栏
        var weapon = ItemManager.instance.nowWeapon;
        var armour = ItemManager.instance.nowArmor;
        var ring = ItemManager.instance.nowRing;
        var shield = ItemManager.instance.nowShield;

    }

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