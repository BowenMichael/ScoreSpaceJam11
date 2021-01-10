using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipeTween : MonoBehaviour
{
    public LTBezierPath path;
    // Start is called before the first frame update
    void Start()
    {
        path = new LTBezierPath();
        LeanTween.moveLocal(gameObject, path, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
