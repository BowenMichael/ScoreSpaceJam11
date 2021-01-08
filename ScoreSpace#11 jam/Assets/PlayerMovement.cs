using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 2f;
    private float horizontal;
    private float vertical;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, vertical, 0.0f).normalized;
        transform.position += dir * speed * Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        Ray2D ray = new Ray2D(transform.position, (mousePos -transform.position).normalized);
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
        



        


        //Look at
        Vector3 directionToMouse = transform.position - mousePos;
        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    private void teleportTowardMouse()
    {
        transform.position += (new Vector3(mousePos.x, mousePos.y, 0.0f) - transform.position).normalized * maxDistance;
    }

    private void hitEnemy(RaycastHit hit)
    {
        Destroy(hit.collider.gameObject);
    }

}
