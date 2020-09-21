using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    private List<Tween> activeTweens;
    private float timeFraction;

    void Update()
    {
        if (activeTween != null)
        {
            timeFraction = Mathf.Pow(((Time.time - activeTween.StartTime) / activeTween.Duration), 3.0f);
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
                activeTween.Target.GetComponent<Animator>().SetBool("isMoving", true);
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween.Target.GetComponent<Animator>().SetBool("isMoving", false);
                activeTween = null;
            }
        }
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (activeTween == null)
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }
    }
}
