using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;





public class OsuTaskManager : MonoBehaviour
{
   [SerializeField] Transform challengeContainer;

   [SerializeField] GameObject osuPanelContainerPrefab;
   [SerializeField] GameObject dotPrefab;

   GameObject? osuGameObject;

   // [SerializeField] private ChallengeTrigger challengeScript;

   public UnityEvent onFailure;
   public UnityEvent onFulfilled;

   int pointCount = 5;
   int currentPointNumber = 1;

   // [SerializeField] int widthHeightParameter = 3;
   // [SerializeField] float dotGap = 50;
   // [SerializeField] StartPosition startPosition;
   // [SerializeField] List<DotPosition> dotsConnectionList;

   // Start is called before the first frame update
   void Start()
   {
      // List<DotPosition> connectionList = new List<DotPosition>();
      // connectionList.Add(new DotPosition(0, 0));
      // connectionList.Add(new DotPosition(0, 1));
      // connectionList.Add(new DotPosition(0, 2));
      // connectionList.Add(new DotPosition(1, 1));
      // connectionList.Add(new DotPosition(2, 2));

      // InitPath();
      //DrawPoints();
   }

   // Update is called once per frame
   void Update()
   {

   }


   // void InitPath(List<DotPosition> dotsConnectionList, int widthHeightParameter)

   public void Activate()
   {
      if (osuGameObject != null) return;

      osuGameObject = Instantiate(osuPanelContainerPrefab, challengeContainer);

      var osuPanelTransform = osuGameObject.transform.GetChild(0);

      var osuPanelRectTransform = osuPanelTransform.GetComponent<RectTransform>();

      Vector2 maxSpawnRange = new(1920, 1080);

      Vector2 spawnRange = maxSpawnRange - osuPanelRectTransform.sizeDelta;

      for (int i = pointCount; i >= 1; i--)
      {
         // var dotGameObject = Instantiate(dotPrefab, challengeContainer);

         // var osuGameObject = dotGameObject.transform.GetChild(0);
         // Debug.Log(osuGameObject.name);
         // var osuRectTransform = osuGameObject.GetComponent<RectTransform>(); ;
         // //Transform challengePanel = challenge.transform.GetChild(0);
         // Vector2 challengeBaseSize = ChallengeManager.CalculateTotalPanelSize(osuRectTransform.sizeDelta);

         // Vector2 maxSpawnRange = new(osuRectTransform.rect.width, osuRectTransform.rect.height);

         // Debug.Log(maxSpawnRange);
         // Vector2 spawnRange = maxSpawnRange - challengeBaseSize;

         // Debug.Log(osuRectTransform.sizeDelta);

         // //osuRectTransform.sizeDelta = challengeBaseSize;

         // int randomX = (int)(Random.Range(0, spawnRange.x) - spawnRange.x / 2);
         // int randomY = (int)(Random.Range(0, spawnRange.y) - spawnRange.y / 2);

         // var osuRectTransform2 = dotGameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>(); ;
         // osuRectTransform2.localPosition = new Vector3(randomX, randomY, osuRectTransform2.localPosition.z);



         int randomX = (int)(Random.Range(0, spawnRange.x) - spawnRange.x / 2);
         int randomY = (int)(Random.Range(0, spawnRange.y) - spawnRange.y / 2);

         // var osuDotGameObject = osuGameObject.transform.GetChild(0).GetChild(0);
         var osuDotGameObject = Instantiate(dotPrefab, osuPanelTransform);
         var osuDotRectTransform = osuDotGameObject.GetComponent<RectTransform>();
         osuDotRectTransform.localPosition = new Vector3(randomX, randomY, osuPanelRectTransform.localPosition.z);


         var textGameObject = osuDotGameObject.transform.GetChild(0);
         var textComp = textGameObject.GetComponent<TMP_Text>();

         textComp.SetText(i.ToString());

         var osuDotClickScript = osuDotGameObject.GetComponent<DotClick>();

         osuDotClickScript.onClickEvent.AddListener(buttonGetClickInfo);
      }


   }

   void buttonGetClickInfo(int number)
   {
      if (number != currentPointNumber)
      {
         // challengeScript.Fail();
         onFailure.Invoke();
         currentPointNumber = 1;

         Destroy(osuGameObject);

         // foreach (Transform g in challengeContainer.GetComponentsInChildren<Transform>())
         // {
         //    // Destroy(g.gameObject);
         //    if(g.name == )
         //    Debug.Log(g.name);
         // }

         // challengeContainer.get
      }
      else if (number == pointCount)
      {
         // challengeScript.Fulfill();
         onFulfilled.Invoke();
         currentPointNumber = 1;
         Destroy(osuGameObject);
      }
      else
      {
         currentPointNumber++;
      }
   }
}
