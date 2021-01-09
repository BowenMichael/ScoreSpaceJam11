using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyParent;
    public int numberOfEnemies;
    public float maxPos;
    public bool waveSpawn;
    public float waveTimeInSeconds = 5;
    public int waveScaling;
    private float timeSinceWave = 0;

    public List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies();
    }

    void spawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-maxPos, maxPos), Random.Range(-maxPos, maxPos), 0.0f);
            GameObject tmp = Instantiate(enemy, enemyParent.position + randomPos, new Quaternion(), enemyParent);
            enemies.Add(tmp);
            tmp.GetComponent<EnemyController>().setSpawner(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveSpawn)
        {
            spawnWave();
        }
        checkIfRoomClear();
    }

    void checkIfRoomClear()
    {
       
        if(enemies.Count <= 0)
        {
            GetComponent<tileController>().setRoomToComplete();
            Destroy(this);
        }
    }

    public void removeEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    void spawnWave()
    {
        if(timeSinceWave > waveTimeInSeconds)
        {
            spawnEnemies();
            timeSinceWave = 0;
            numberOfEnemies *= waveScaling;
        }
        timeSinceWave += Time.deltaTime;
    }

   
}
