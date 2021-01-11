using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : MonoBehaviour
{
    public enum enemyType{
        UNKOWN = -1,
        CHASER,
        SHOOTER
    }
    public enemyType type;
    public ParticleSystem onDeath;
    public GameObject player;
    public float speed;
    public float dmg;
    public float forceOfKnockBack;
    public float range;
    public GameObject proj;
    private EnemySpawner spawner;
    private float dmgScale;
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //onDeath.Pause();
    }

    private void Update()
    {
        switch (type)
        {
            case enemyType.CHASER:
                moveTowardPlayer();
                rb.velocity = Vector3.zero;
                break;
            case enemyType.SHOOTER:
                if (!moveAwayFromPlayer())
                {

                }
                break;
        }
        
    }

    private void shootAtPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        
    }

    private bool moveAwayFromPlayer()
    {
        float distance = Mathf.Abs((player.transform.position - transform.position).magnitude);
        if (distance < range)
        {
            Vector3 dir = -lookAt(player.transform.position);
            moveTowardDir(dir);
            return true;
        }
        return false;
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

    void moveToward(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    void moveTowardDir(Vector3 dir)
    {
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        
        spawner.removeEnemy(this.gameObject);
        //tmp.Play();
    }
    public void collisionWithPlayer() {
        ParticleSystem tmp = Instantiate(onDeath, transform);
        tmp.transform.parent = null;
        Destroy(this.gameObject);

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
