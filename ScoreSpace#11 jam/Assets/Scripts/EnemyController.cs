using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem onDeath;
    public GameObject player;
    public float speed;
    private EnemySpawner spawner;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //onDeath.Pause();
    }

    private void Update()
    {
        moveTowardPlayer();
    }

    public void setSpawner(EnemySpawner spwn)
    {
        spawner = spwn;
    }

    void moveTowardPlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
        lookAt(player.transform.position);
    }

    private void OnDestroy()
    {
        ParticleSystem tmp = Instantiate(onDeath, transform);
        tmp.transform.parent = null;
        spawner.removeEnemy(this.gameObject);
        //tmp.Play();
    }

    private void lookAt(Vector3 pos)
    {
        //Look at
        Vector3 directionToMouse = transform.position - pos;
        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
