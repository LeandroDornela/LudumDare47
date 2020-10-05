using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent onEndDelay;
    
    private IEnumerator Start()
    {
        Debug.Log("yielding");
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log("Playing event.");
        onEndDelay.Invoke();
    }
}
