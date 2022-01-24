using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent ButtonEvent;
    [SerializeField] Vector3 PushDistance;
    [SerializeField] float pushTime = 0;
    private bool beingPushed = false;

    public void TriggerButton()
    {
        Debug.Log("trigger buttton");
        if (!beingPushed)
        {
            ButtonEvent.Invoke();
            beingPushed = true;
            StartCoroutine(HelperFunction.TranslateAnim(this.gameObject, PushDistance, pushTime));
        }
        
    }

    
}
