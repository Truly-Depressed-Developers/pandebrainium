using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge: MonoBehaviour {
    public event Action OnTaskFulfilled;
    public event Action OnTaskFailure;

    // Add logic
    private void Start() {
        
    }

    [ContextMenu("Complete Task")]
    private void FulfillTask() {
        // Invoke the event
        Debug.Log("Invoking FulfullTask");
        Debug.Log(OnTaskFulfilled);
        OnTaskFulfilled?.Invoke();


        // Do other stuff


        // Close task
        Destroy(this.gameObject);
    }
}
