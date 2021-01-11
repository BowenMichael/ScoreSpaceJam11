using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int dmg;
    public float forceOfKnockBack;

    private Vector3 dir;

    public void setDirection(Vector3 dir)
    {
        this.dir = dir;
    }

    public void setDmgScale()
    {

    }

    public Vector3 getDir() { return dir; }

    public int getDmg() { return dmg; }

    public float getKnockBack() { return forceOfKnockBack; }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        
         Destroy(gameObject);

        
    }
}
