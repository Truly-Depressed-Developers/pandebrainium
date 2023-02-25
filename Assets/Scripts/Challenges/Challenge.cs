using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
   public event Action OnTaskFulfilled;
   public event Action OnTaskFailure;

   [SerializeField] float taskTime = 3f;
   float startTime;

   // Add logic
   private void Start()
   {
      startTime = Time.time;
   }

   private void Update()
   {
      CheckTaskDeath();
   }

    public void ChallengeFulfilled() {
        OnTaskFulfilled?.Invoke();
        Destroy(this.gameObject);
    }

   [ContextMenu("Complete Task")]
   public void FulfillTask()
   {
      // Invoke the event
      Debug.Log("Invoking FulfullTask");
      Debug.Log(OnTaskFulfilled);
      OnTaskFulfilled?.Invoke();


      // Do other stuff


      // Close task
      Destroy(this.gameObject);
   }

   public void CheckTaskDeath()
   {
      if (Time.time - startTime < taskTime) return;

      OnTaskFailure?.Invoke();
      Destroy(this.gameObject);
   }
    public void ChallengeFailed() {
        OnTaskFailure?.Invoke();
        Destroy(this.gameObject);
    }
}
