using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileController : MonoBehaviour
{
    public enum neighbor
    {
        UNKOWN = -1,
        ABOVE = 0,
        RIGHT = 1,
        BOTTOM = 2,
        LEFT = 3,
    }

    public bool playerIn= false;
    public GameObject rewardObj;
    public UITweener startAnimation;
    public int index;
    public Text roundText;
    private Reward reward;
    private tileController[] neighbors = new tileController[4]; //0 above, 1 right, 2 bot, 3 left
    private GameObject nextRoom;
    private tileController nextTile;
    private bool completed=false;
    private GameController gm;
    private RoomManager rm;

    private void Start()
    {
        
        reward = rewardObj.AddComponent<Reward>();
        reward.setTile(this);
        rewardObj.SetActive(false);
        roundText.text = "Round " + index;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rm = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoomManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            setRoomActive();
            
            Debug.Log("Active");
        }
    }

    public void setRoomActive()
    {
        
        setCameraToTile();
        setPlayerToTile();

        startAnimation.onStartRoom();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

        }
        rewardObj.SetActive(false);

        



    }

    IEnumerator waitSeconds(float t)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(t);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void setNeighbor(tileController neighbor, neighbor t) 
    {
        neighbors[(int)t] = neighbor;
    }

    public void setNextRoom(GameObject next)
    {
        tileController tmp;
        if (next.TryGetComponent<tileController>(out tmp)) {
            nextRoom = next;
            nextTile = tmp;
        }
    }

    public void moveToNextRoom()
    {
        gm.roomComplete();
        if (nextTile != null)
            nextTile.setRoomActive();
        else
            gm.stageComplete();
    }

    private void setCameraToTile()
    { 
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
       
    }

    private void setPlayerToTile()
    {
        GameObject plr = GameObject.FindGameObjectWithTag("Player");
        plr.transform.position = new Vector3(transform.position.x, transform.position.y, plr.transform.position.z);
    }


    public void setRoomToComplete()
    {
        onComplete();
    }

    private void onComplete()
    {
        completed = true;
        rewardObj.SetActive(true);
        

    }



}
