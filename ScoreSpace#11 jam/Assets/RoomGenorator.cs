using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenorator : MonoBehaviour
{
    public List<GameObject> enviorment;
    public List<GameObject> enemies;
    public List<GameObject> templates;
    public int variation;
    public List<GameObject> filledTemplates;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < templates.Count; i++)
        {
            for(int j = 0; j < variation; j++)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
