using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeTrigger : MonoBehaviour {
    public event Action OnTaskFulfilled;
    public event Action OnTaskFailure;


    [ContextMenu("Fulfill challenge")]
    public void Fulfill() {
        OnTaskFulfilled?.Invoke();
    }

    [ContextMenu("Fail challenge")]
    public void Fail() {
        OnTaskFailure?.Invoke();
    }
}
