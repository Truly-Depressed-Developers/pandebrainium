using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
   [SerializeField] private TMP_Text text;
   [SerializeField] private float multiplayer = 2.0f;
   [SerializeField] private int baseClickCount = 5;
   private int maxClickCount;

   private int clickCounter = 0;

   // Start is called before the first frame update
   void Start()
   {
      maxClickCount = Mathf.FloorToInt(baseClickCount * multiplayer);
      Debug.Log("MAX: " + maxClickCount.ToString());
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void ButtonActivation()
   {
      clickCounter++;

      if(clickCounter >= maxClickCount){
         clickCounter = maxClickCount;

         Debug.Log("Done");
      } ;
      
      text.SetText(clickCounter.ToString());
   }
}
