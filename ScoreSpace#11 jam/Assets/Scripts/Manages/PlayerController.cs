using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth;
    public List<Upgrade> upgrades;
    public PlayerMovement plrMvm;
    public HPSliderHandler hpUI;

    private Rigidbody rb;
    private GameController gm;
    private int health;

    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        hpUI.setHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
    }

    void checkDeath()
    {
        if(health <= 0)
        {
            onDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            onHitEnemy(collision.gameObject.GetComponent<EnemyController>());
            Debug.Log("Hit Enemy");
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            onHitBullet(collision.gameObject.GetComponent<BulletController>());
            Debug.Log("Hit Bullet");
         }
        //rb.velocity = Vector3.zero;
    }

    private void onHitEnemy(EnemyController enemy)
    {
        takeDamage(enemy.getDamage());
        plrMvm.knockedBack = true;
        rb.AddForce(enemy.getFacing() * -enemy.forceOfKnockBack);
        


    }
    public void onHitBullet(BulletController enemy)
    {
        Debug.Log(enemy.getDmg());
        takeDamage(enemy.getDmg());
        plrMvm.knockedBack = true;
        rb.AddForce(enemy.getDir() * enemy.forceOfKnockBack);


    }

    private void takeDamage(int dmg)
    {
        //Debug.Log(dmg);
        health -= dmg;
        hpUI.setHealth(health, maxHealth);
        
    }

    void onDeath()
    {
        gm.endGame();
    }

    public bool increaseHealth(int increment)
    {
        if(health + increment <= maxHealth && health + increment > 0) {
            health += increment;
            hpUI.setHealth(health, maxHealth);
            return true;
        }
        return false;
       
    }

    public void increaseEnergyRegen(int increment)
    {
        plrMvm.energyRegenPerSecond += increment;
    }
    public void increaseMaxEnergy(int increment)
    {
        plrMvm.maxEnergy += increment;
        plrMvm.eSlider.setEnergy(plrMvm.energy, plrMvm.maxEnergy);
    }

    public bool decreaseEnergyRegen(int increment)
    {
        if (plrMvm.energyRegenPerSecond - increment >= 5)
        {
            plrMvm.energyRegenPerSecond -= increment;
            return true;
        }
        return false;
    }
    public bool decreaseMaxEnergy(int increment)
    {
        if (plrMvm.maxEnergy - increment >= 5)
        {
            plrMvm.maxEnergy -= increment;
            plrMvm.eSlider.setEnergy(plrMvm.energy, plrMvm.maxEnergy);
            return true;
        }
        return false;
    }
}
