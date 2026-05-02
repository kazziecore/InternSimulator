using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Schema;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
// THIS CODE IS OBSOLETE IM KEEPING IT FOR REFERENCE
public class GameStatus : MonoBehaviour
{
    // comments and headers are to help with ur explanation divaaaa read em
   TMP_Text gameStatusUI;
   public static GameStatus Instance; //makes this script accessible globally meow, so it can update!

   [Header ("Gameplay Data")]
   string playerName;
   string playerPosition; 

   [Header("Envriomental data")]
   string npcPosition; 

   [Header("Statistic Data")]
   int score; 
   int totalRequestsComplete; 
   int totalRequestsFailed; 
   int activeRequests; 

   [Header("References")]
public Transform player;
public NPCRequest npc;

   void GetState()
    {
        gameStatusUI.text = "Now loading..";
        //check playerprefs isnt empty
        if(PlayerPrefs.HasKey("playerName"))
        {
            //get playerprefs data
            playerName = PlayerPrefs.GetString("playerName");
            playerPosition = PlayerPrefs.GetString("playerPosition");
            npcPosition = PlayerPrefs.GetString("npcPosition");
            score = PlayerPrefs.GetInt("score");
            totalRequestsComplete = PlayerPrefs.GetInt("totalRequestsComplete");
            totalRequestsFailed = PlayerPrefs.GetInt("totalRequestsFailed");
            activeRequests = PlayerPrefs.GetInt("activeRequests");
        }
    }

    // Update is called once per frame
    void Awake()
    {
        Instance = this;
        // retreieves text object in scene ui
        gameStatusUI = GetComponent<TMP_Text>();

        // awake / resume will be the getters
       // GetState();
        Debug.Log("Awake");
    }

    // void OnApplicationPause (bool pauseStatus)
    // {
    //     if (pauseStatus)
    //     {
    //         SetState();
    //         Debug.Log("Pause");
    //     }
    //     else
    //     {
    //         GetState();
    //         Debug.Log("Resume");
    //     }
    // }

    void SetState()
    {
       //get playerprefs data
            PlayerPrefs.SetString("playerName", playerName);
            PlayerPrefs.SetString("playerPosition", playerPosition);
            PlayerPrefs.SetString("npcPosition", npcPosition);
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetInt("totalRequestsComplete", totalRequestsComplete);
            PlayerPrefs.SetInt("totalRequestsFailed", totalRequestsFailed);
            PlayerPrefs.SetInt("activeRequests", activeRequests);

            // save the data to playerprefs on disk
            PlayerPrefs.Save();
    }

    //ui controls
    void ShowStatus()
    {
        // formatting that will be shown to the player
        string message = "";
        message += "Player Name: " + playerName + "\n";
        message += "Player Position: " + playerPosition + "\n";
        message += "NPC Position: " + npcPosition + "\n";
        message += "Score: " + score + "\n";
        message += "Total Requests Complete: " + totalRequestsComplete + "\n";
        message += "Total Requests Failed: " + totalRequestsFailed + "\n";
        message += "Active Requests: " + activeRequests + "\n";

        gameStatusUI.text = message;
    }

    void OnApplicationQuit()
    {
        SetState();
        Debug.Log("Quit");
    }

    void Start()
    {
        // declare variables
        if (!PlayerPrefs.HasKey("playerName"))
        {
            // load the save
             GetState(); 
        }
        else
        {
        //updates the data
        playerName = "Kazzie";
        playerPosition = player.position.ToString();

        if (npc != null)
        npcPosition = npc.transform.position.ToString();

        // stats
        score = 0;
        totalRequestsComplete = 0;
        totalRequestsFailed = 0;
        activeRequests = 1;

        SetState(); //saves
        }

        ShowStatus(); //and thennn shows it in ui
    }

    // counting times u complete request yayyy
    public void OnRequestCompleted() 
{
    score += 10;
    totalRequestsComplete++;
    activeRequests--;

    ShowStatus();
}

// counting times u fail request booo
public void OnRequestFailed()
{
    totalRequestsFailed++;
    activeRequests--;

    ShowStatus();
}

// adding active requests to ui county yoke
public void OnNewRequest()
{
    activeRequests++;
    ShowStatus();
}

    void Update()
    {

        //update player and npc positions for the ui :3
         if (player != null)
        playerPosition = player.position.ToString();

    if (npc != null)
        npcPosition = npc.transform.position.ToString();

        ShowStatus();
    }

}
    


