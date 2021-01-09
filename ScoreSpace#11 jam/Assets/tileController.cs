using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private tileController[] neighbors = new tileController[4]; //0 above, 1 right, 2 bot, 3 left
    private GameObject nextRoom;
    private tileController nextTile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            setRoomActive();
        }
    }

    public void setRoomActive()
    {
        setCameraToTile();
        setPlayerToTile();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
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
        nextTile.setRoomActive();
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




}
