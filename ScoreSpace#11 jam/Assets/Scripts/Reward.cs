using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private tileController parentTile;

    public void setTile(tileController tile)
    {
        parentTile = tile;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            parentTile.moveToNextRoom();
            Destroy(gameObject);
        }
    }
}
