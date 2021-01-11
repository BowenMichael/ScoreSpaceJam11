using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject tile;
    public float tileWidth;
    public float tileHeight;
    public Vector2Int gridSize;
    public Vector2Int gridOffset;
    public tileController[,] tiles;
    public GameObject player;
    public List<GameObject> rooms;

    private BoxCollider playSpace;
    private GameController gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameController>();
        setUpRooms();
    }

    void placePlayer()
    {
        int middleX = gridSize.x / 2;
        int middleY = gridSize.y / 2;
        player.transform.position = tiles[0, 0].GetComponent<BoxCollider>().center;
        //playSpace.center = new Vector3(((gridSize.x / 2.0f) * tileWidth) - tileWidth/2.0f, ((gridSize.y / 2.0f) * tileWidth) - tileHeight / 2.0f, 0.0f);
        //playSpace.size = new Vector3(tileWidth * gridSize.x, tileHeight * gridSize.y, 0.0f);
        //Instantiate(player, tiles[middleX, middleY].transform.position, new Quaternion());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }

    void genGrid()
    {
        int i, j, k = 1;
        for(i = 0; i < gridSize.x; i++)
        {
            for(j = 0; j < gridSize.y; j++)
            {
                
                Vector3 gridPos = new Vector3(tileWidth * i * gridOffset.x, tileHeight * j * gridOffset.y, 0.0f);
                tiles[i,j] = Instantiate(rooms[Random.Range(0, rooms.Count)], gridPos, new Quaternion()).GetComponent<tileController>();
                tiles[i, j].name = "Room " + (i + 1) + ", " + (j + 1);
                tiles[i, j].index = k;
                tiles[i, j].gameObject.GetComponent<EnemySpawner>().enemyScale = gm.getScaling();
                if (i != 0 && j == 0) 
                {
                    tiles[i - 1, gridSize.y - 1].setNextRoom(tiles[i, j].gameObject);
                }
                if (j > 0)
                    tiles[i, j-1].setNextRoom(tiles[i,j].gameObject);
                k++;
                
            }

        }
    }

    void assignNeighbors()
    {
        //int i, j;
        //for (i = 0; i < gridSize.x; i++)
        //{
        //    for (j = 0; j < gridSize.y; j++)
        //    {
        //        if(i < gridSize.x-1)
        //            tiles[i, j].setNeighbor(tiles[i++,j], tileController.neighbor.RIGHT);
        //        if (j < gridSize.y - 1)
        //            tiles[i, j].setNeighbor(tiles[i, j++], tileController.neighbor.ABOVE);
        //        if (j > 0)
        //            tiles[i, j].setNeighbor(tiles[i, j--], tileController.neighbor.BOTTOM);
        //        if (i > 0)
        //            tiles[i, j].setNeighbor(tiles[i--, j], tileController.neighbor.LEFT);

        //    }
        //}
    }

    public void resetRooms()
    {
        foreach(tileController tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        setUpRooms();
    }

    public void setUpRooms() {
        tiles = new tileController[gridSize.x, gridSize.y];
        //playSpace = gameObject.AddComponent<BoxCollider>();
        genGrid();
        assignNeighbors();
        placePlayer();
    }

}
