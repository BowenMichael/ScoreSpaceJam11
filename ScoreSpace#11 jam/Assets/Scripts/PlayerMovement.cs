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

    private float horizontal;
    private float vertical;
    public float energy = 0;
    Vector3 mousePos;
    private GameController gm;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //MousePosition in world space
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        if (cameraMovesWithPlayer)
        {
            updateCameraPosition();
        }

        if (teleportGuideEnabled)
        {
            updateGuidePosition();
        }

        movementWSAD();
        if (energy >= teleportCost)
        {
            teleportCheck();
        }
        if (energy < maxEnergy)
        {
            energy += energyRegenPerSecond * Time.deltaTime;
        }
        if(eSlider != null)
        {
            eSlider.setEnergy(energy/ maxEnergy);
        }
        lookAtMouse();
        
    }

    private void updateGuidePosition()
    {
        guideImage.position = transform.position + (mousePos - transform.position).normalized * maxDistance;
    }

    private void updateCameraPosition()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void teleportTowardMouse()
    {
        transform.position += (new Vector3(mousePos.x, mousePos.y, 0.0f) - transform.position).normalized * maxDistance;
        energy -= teleportCost;
       
    }

    private void hitEnemy(RaycastHit hit)
    {
        gm.addScore();
        Destroy(hit.collider.gameObject);
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
