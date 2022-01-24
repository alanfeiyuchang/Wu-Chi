using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
   public UnityEvent ButtonEvent;
   public void TriggerButton()
    {
        Debug.Log("trigger buttton");
        ButtonEvent.Invoke();
    }
}
