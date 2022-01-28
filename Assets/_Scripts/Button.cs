using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent ButtonEvent;
    [SerializeField] Vector3 PushDistance;
    [SerializeField] float pushTime = 0.2f;
    private bool beingPushed = false;
    private bool correctSource;
    public GameObject triggerSource;

    public void TriggerButton(GameObject source)
    {
        correctSource = true;
        if (triggerSource != null && source != triggerSource)
        {
            correctSource = false;
        }

        if (!beingPushed && correctSource)
        {
            ButtonEvent.Invoke();
            beingPushed = true;
            StartCoroutine(HelperFunction.TranslateAnim(this.gameObject, PushDistance, pushTime));
        }
        
    }

}
