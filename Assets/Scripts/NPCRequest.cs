using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class NPCRequest : MonoBehaviour
{
    public ItemData[] possibleItems;
    public ItemData currentRequest;

    public float requestTime = 10f;
    private float timer;
    private bool isRequestActive = false;

    public GameObject requestUI; 
    public Image requestIcon;

    public DemoData demoData;

    void Start()
    {
        StartRequest();
    }

    void Update()
    // starts request timer, fails if the request runs out
    {
        if (!isRequestActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            FailRequest();
        }
    }

    // game lifecycle event 1
    public void StartRequest()
    {
        currentRequest = possibleItems[Random.Range(0, possibleItems.Length)];
        timer = requestTime;
        isRequestActive = true;

        requestIcon.sprite = currentRequest.icon;
        requestUI.SetActive(true);
        GameData data = FindAnyObjectByType<GameData>();
        data.gameStatus.activeRequests+=1;
        demoData.activeRequests+=1;

        
    }

    public void CompleteRequest() //game lifecycle event 2 electric boogaloo
    {
        isRequestActive = false;
        requestUI.SetActive(false);
        
         GameData data = FindAnyObjectByType<GameData>();
        data.gameStatus.totalRequestsComplete++;
        demoData.totalRequestsComplete++;


        // add a cheeky delay to gather ur thoughts or move towards items
        Invoke(nameof(StartRequest), 2f); 
    }

    public void TryDeliver(ItemData item) // if the item is correct u get plus ten score yay
{
    if (!isRequestActive) return;

    if (item == currentRequest)
    {
        Debug.Log("Correct item");
        GameData data = FindAnyObjectByType<GameData>();
        data.gameStatus.score += 10;
        demoData.score += 10;
        demoData.totalRequestsFailed++;
        
        //if the score is greater than high score, updates high score again woawoaow

        if(data.gameStatus.score > data.gameStatus.highScore)
            {
                data.gameStatus.highScore = data.gameStatus.score;
            }
        

        CompleteRequest();
    }
    else
    {
        Debug.Log("Wrong item");
        
    }
}

    void FailRequest() //game lifecycle event 3
    {
        isRequestActive = false;
        requestUI.SetActive(false);

        Debug.Log("Request Failed");

        // add failed requests and start a new one
        GameData data = FindAnyObjectByType<GameData>();
        data.gameStatus.totalRequestsFailed++;
        Invoke(nameof(StartRequest), 3f);
    }
}
