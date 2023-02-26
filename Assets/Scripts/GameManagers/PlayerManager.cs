using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager instance;

   public DrawBrain.BrainStatistics selectedBrain = new DrawBrain.BrainStatistics();
   // public int budget = 500;
   public int sanity = 8;

   public UnityEvent changeEvent;

   // Start is called before the first frame update
   void Start()
   {
      instance = this;
   }

   public int dexterity
   {
      get { return selectedBrain.dexterity + 1; }
   }
   public int intelligence
   {
      get { return selectedBrain.intelligence + 1; }
   }
   public int strength
   {
      get { return selectedBrain.strength + 1; }
   }


   public void ReceiveBrain(DrawBrain.BrainStatistics brain)
   {
      selectedBrain = brain;
      // budget -= selectedBrain.cost;
      MoneyManager.instance.loseMoney(selectedBrain.cost);
      sanity += selectedBrain.sanity;

      changeEvent.Invoke();
   }
}
