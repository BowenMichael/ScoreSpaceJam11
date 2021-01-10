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
        hpUI.setHealth(1f);
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
    }

    void checkDeath()
    {
        if(health < 0)
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
    }

    private void onHitEnemy(EnemyController enemy)
    {
        takeDamage(enemy.dmg);
        plrMvm.knockedBack = true;
        rb.AddForce(enemy.getFacing() * -enemy.forceOfKnockBack);


    }

    private void takeDamage(int dmg)
    {
        health -= dmg;
        hpUI.setHealth((health * 1.0f / maxHealth));
        
    }

    void onDeath()
    {
        gm.endGame();
    }

    public void upgrade(ItemController.itemType type)
    {
        switch (type)
        {
            case ItemController.itemType.UNKOWN:
                return;
            case ItemController.itemType.ITEM:
                break;
                //case

        }
    }
}
