using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyParticalSystem : MonoBehaviour
{
    public ParticleSystem p;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!p.IsAlive())
        {
            Destroy(gameObject);
        }   
    }
}
