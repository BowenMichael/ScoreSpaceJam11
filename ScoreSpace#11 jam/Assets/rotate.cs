using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public int speed;
    public Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        vector = vector.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate( vector* speed * Time.deltaTime);
    }
}
