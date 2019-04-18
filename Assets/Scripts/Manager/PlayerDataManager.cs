using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace mygame
{
    [System.Serializable]
    public class PlayerDataManager
    {
        public static PlayerDataManager instance;
        public GameObject playerPrefab;
        public GameObject player;
        public PlayerManager playerManager;
        public Player playerData;
        
        public static void Load(string path)
        {
            instance = new PlayerDataManager();
        }
        public void PlayerCreate(string path)
        {
            TextAsset playerText = (TextAsset)Resources.Load("NewPlayer");
            playerData = JsonUtility.FromJson<Player>(playerText.text);
            player = Object.Instantiate(playerPrefab, new Vector3(0, 0, 0), new Quaternion());
            player.GetComponent<PlayerManager>().player = playerData;
            playerManager = player.GetComponent<PlayerManager>();
            playerData = playerManager.player;
        }
    }
}
