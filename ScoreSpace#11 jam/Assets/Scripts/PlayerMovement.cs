using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 2f;
    public float maxEnergy = 100f;
    public float energyRegenPerSecond = 1f;
    public float teleportCost = 30f;
    public EnergySlider eSlider;
    public bool cameraMovesWithPlayer;
    public bool teleportGuideEnabled;
    public Transform guideImage;
    public bool knockedBack = false;

    private float horizontal;
    private float vertical;
    public float energy = 0;
    Vector3 mousePos;
    private GameController gm;
    private Rigidbody rb;
    float angle;
    public stickToPlayer stp;
    public float stunDurrationSeconds = 1f;
    private float stpf;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //MousePosition in world space
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (cameraMovesWithPlayer)
        {
            updateCameraPosition();
        }

        if (teleportGuideEnabled)
        {
            updateGuidePosition();
        }

        if (!knockedBack)
        {
            
            if (Time.timeScale == 1) {
                movementWSAD();
                lookAtMouse();
                if (energy >= teleportCost)
                {
                    teleportCheck();
                }
                if (energy < maxEnergy)
                {
                    energy += energyRegenPerSecond * Time.deltaTime;
                }
                if (eSlider != null)
                {
                    eSlider.setEnergy(energy, maxEnergy);
                }
                
            }
        }
        else
        {
            stp.gameObject.SetActive(true);
            //LeanTween.(gameObject, new Vector3(0.0f, 0.0f, 0.0f), stunDurrationSeconds).setOnComplete(onKnockbackComplete);
            stpf += Time.deltaTime;
            stp.setStun(stpf, stunDurrationSeconds);
            if (stpf > stunDurrationSeconds)
            {
                onKnockbackComplete();
            }
            
        }
        
    }

    private void onKnockbackComplete()
    {
        rb.velocity = Vector3.zero;
        knockedBack = false;
        stp.resetStun();
        stpf = 0;



    }

    private void updateGuidePosition()
    {
        Vector3 offset = (new Vector3(mousePos.x, mousePos.y, 0.0f) - transform.position).normalized * maxDistance;
        Vector2 normalizedOffset = new Vector2(offset.x, offset.y).normalized;
        guideImage.position = new Vector3(offset.x, offset.y, guideImage.position.z) ;
    }

    private void updateCameraPosition()
    {
        
        Vector2 playerPosScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log(playerPosScreenSpace + "; " + Screen.width + "," + Screen.height);
        if (playerPosScreenSpace.x > Screen.width ||
            playerPosScreenSpace.y > Screen.height ||
            playerPosScreenSpace.x < 0 ||
            playerPosScreenSpace.y < 0)
        {
            //Vector
            Debug.Log("Move Cam");
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }

    }

    private void teleportTowardMouse()
    {
        transform.position += (new Vector3(mousePos.x, mousePos.y, 0.0f) - transform.position).normalized * maxDistance;
        energy -= teleportCost;
       
    }

    private void hitEnemy(RaycastHit hit)
    {
        gm.addScore();
        hit.collider.gameObject.GetComponent<EnemyController>().collisionWithPlayer();
    }

    private void lookAtMouse()
    {
        //Look at
        Vector3 directionToMouse = transform.position - mousePos;
        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    private void teleportCheck()
    {
        Ray2D ray = new Ray2D(transform.position, (mousePos - transform.position).normalized);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        RaycastHit hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance))
            {
                //Target within range
                string hitTag = hit.collider.gameObject.tag;

                if (hitTag.Equals("Enemy"))
                {
                    hitEnemy(hit);
                }
                //else if (hitTag.Equals("Wall"))
                //{
                //    //teleportToNextRoom();
                //}
                if (!hitTag.Equals("Enviorment"))
                {
                    teleportTowardMouse();
                }

                Debug.Log("hit");
            }
            else
            {
                teleportTowardMouse();
            }

        }
    }

    private void teleportToNextRoom(Vector3 dir)
    {
        //if (angle > 45 && angle <= 145)
        //{
        //    //up
        //}
        //else if (angle > 145 && angle)
    }

    private void movementWSAD()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, vertical, 0.0f).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

}
