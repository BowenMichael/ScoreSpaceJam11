using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITweener : MonoBehaviour
{
    public float distX;
    public float duration;
    public LeanTweenType easeType;
    public AnimationCurve moveCurve;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        onStartRoom();
    }

    public void onStartRoom()
    {
        if (easeType == LeanTweenType.animationCurve)
        {
            LeanTween.moveX(gameObject, distX, duration).setOnComplete(DestroyMe).setEase(moveCurve).setIgnoreTimeScale(true);
        }
        else
        {
            LeanTween.moveX(gameObject, distX, duration).setOnComplete(DestroyMe).setEase(easeType).setIgnoreTimeScale(true);
        }
    }

    private void DestroyMe()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
