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

        if (!knockedBack)
        {
            if (Time.timeScale == 1) {
                movementWSAD();
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
                    eSlider.setEnergy(energy / maxEnergy);
                }
                lookAtMouse();
            }
        }
        else
        {
            LeanTween.rotate(gameObject, new Vector3(0.0f, 0.0f, 360f), 1f).setOnComplete(onKnockbackComplete);
        }
        
    }

    private void onKnockbackComplete()
    {
        knockedBack = false;
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
        float widthPercent = 15 / 100 * Camera.main.pixelWidth;
        float heightPercent = 15 / 100 * Camera.main.pixelHeight;
        Vector3 offset = Vector3.zero;
        if (Camera.main.pixelWidth - playerPosScreenSpace.x < widthPercent || playerPosScreenSpace.x < widthPercent)
        {
            offset = Camera.main.transform.position - new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z) + offset;
        }
        if (Camera.main.pixelHeight - playerPosScreenSpace.y < heightPercent || playerPosScreenSpace.y < heightPercent)
        {
            offset = Camera.main.transform.position - new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z) + offset;
        }
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z) + offset;
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
