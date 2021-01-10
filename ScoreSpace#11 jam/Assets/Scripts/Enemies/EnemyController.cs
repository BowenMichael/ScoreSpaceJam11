using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem onDeath;
    public GameObject player;
    public float speed;
    public float dmg;
    public float forceOfKnockBack;
    private EnemySpawner spawner;
    private float dmgScale;
    

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

    public void setScale(float scale)
    {
        dmgScale = scale;
        dmg *= dmgScale;
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

    private Vector3 lookAt(Vector3 pos)
    {
        //Look at
        Vector3 directionTo = transform.position - pos;
        float angle = Mathf.Atan2(directionTo.x, directionTo.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        return directionTo;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    public Vector3 getFacing()
    {
        return lookAt(player.transform.position).normalized;
    }

    public int getDamage()
    {
        return (int)dmg;
    }
}