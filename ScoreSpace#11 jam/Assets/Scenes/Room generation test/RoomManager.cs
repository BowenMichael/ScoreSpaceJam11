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


    // Start is called before the first frame update
    void Start()
    {
        tiles = new tileController[gridSize.x,gridSize.y];
        genGrid();
        assignNeighbors();
        placePlayer();
    }

    void placePlayer()
    {
        int middleX = gridSize.x / 2;
        int middleY = gridSize.y / 2;
        player.transform.position = tiles[middleX, middleY].transform.position;
        //Instantiate(player, tiles[middleX, middleY].transform.position, new Quaternion());
    }

    void genGrid()
    {
        int i, j;
        for(i = 0; i < gridSize.x; i++)
        {
            for(j = 0; j < gridSize.y; j++)
            {
                Vector3 gridPos = new Vector3(tileWidth * i * gridOffset.x, tileHeight * j * gridOffset.y, 0.0f);
                tiles[i,j] = Instantiate(tile, gridPos, new Quaternion()).GetComponent<tileController>();
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

   
}
