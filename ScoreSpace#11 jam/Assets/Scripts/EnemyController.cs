using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem onDeath;
    public GameObject player;
    public float speed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //onDeath.Pause();
    }

    private void Update()
    {
        moveTowardPlayer();
    }

    void moveTowardPlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.position += dir.normalized * speed * Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        ParticleSystem tmp = Instantiate(onDeath, transform);
        tmp.transform.parent = null;
        //tmp.Play();
    }
}
