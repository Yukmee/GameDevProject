using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;
using System.IO;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Camera camera;
    public ItemManager test;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerDataManager.Load("");
        ItemManager.Load("");
        test = ItemManager.instance;
        PlayerDataManager.instance.playerPrefab = playerPrefab;
        TextAsset dictionarytext = (TextAsset)Resources.Load("Dictionary");
        ItemManager.instance.DictionaryInit(dictionarytext.text);
        PlayerDataManager.instance.PlayerCreate("NewPlayer");
        camera.GetComponent<CameraFollow>().target = PlayerDataManager.instance.player.transform;
    }
    void Start()
    {
        
    }
    void ClickManager()
    {
        if (Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                RaycastHit[] hits = Physics.RaycastAll(ray);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.tag == "item")
                    {//
                        hits[i].collider.gameObject.GetComponent<ItemOnGround>().onPick();
                        break;
                    }
                    else if (hits[i].collider.gameObject.tag == "chest")
                    {
                        hits[i].collider.gameObject.GetComponent<Chest>().onOpen();
                        break;
                    }
                }
            }
                
        }
    }
    // Update is called once per frame
    void Update()
    {
        ClickManager();
    }
}
