using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float dmg;
    private float dmgScale;
    public float forceOfKnockBack;

    private Vector3 dir;
    private float lifeTime = 0;

    public void setDirection(Vector3 dir)
    {
        this.dir = dir;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    public void setDmgScale(float scale)
    {
        dmgScale = scale;
    }

    public Vector3 getDir() { return dir; }

    public int getDmg() { return (int)(dmg * dmgScale); }

    public float getKnockBack() { return forceOfKnockBack; }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        if(lifeTime > 15f)
        {
            Destroy(gameObject);
        }
        lifeTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().onHitBullet(this);
            }
            Destroy(gameObject);
            Debug.Log("Dead Bullet");
        }



    }

    


}
