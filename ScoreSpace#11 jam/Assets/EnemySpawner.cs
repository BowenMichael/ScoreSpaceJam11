using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyParent;
    public int numberOfEnemies;
    public float maxPos;
    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-maxPos, maxPos), Random.Range(-maxPos, maxPos), 0.0f);
            Instantiate(enemy, enemyParent.position + randomPos, new Quaternion(), enemyParent);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
