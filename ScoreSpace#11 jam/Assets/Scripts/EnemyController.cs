using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem onDeath;

    private void Start()
    {
        //onDeath.Pause();
    }

    private void OnDestroy()
    {
        ParticleSystem tmp = Instantiate(onDeath, transform);
        tmp.transform.parent = null;
        //tmp.Play();
    }
}
