using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager instance;

   public DrawBrain.BrainStatistics selectedBrain = new DrawBrain.BrainStatistics();
   // public int budget = 500;
   public int sanity = 20;

   public UnityEvent changeEvent;

   // Start is called before the first frame update
   void Start()
   {
      instance = this;

      selectedBrain.dexterity = 1;
      selectedBrain.intelligence = 1;
      selectedBrain.strength = 1;
   }

   public int dexterity
   {
      get { return selectedBrain.dexterity; }
   }
   public int intelligence
   {
      get { return selectedBrain.intelligence; }
   }
   public int strength
   {
      get { return selectedBrain.strength; }
   }


   public void ReceiveBrain(DrawBrain.BrainStatistics brain){
      selectedBrain = brain;
      // budget -= selectedBrain.cost;
      MoneyManager.instance.loseMoney(selectedBrain.cost);
      sanity += selectedBrain.sanity;

      changeEvent.Invoke();
   }
}
