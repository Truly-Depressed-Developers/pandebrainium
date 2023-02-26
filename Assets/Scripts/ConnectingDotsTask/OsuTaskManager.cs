using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;





public class OsuTaskManager : MonoBehaviour {


    [SerializeField] GameObject osuPanelContainer;

    [SerializeField] GameObject dotPrefab;

    [SerializeField] ChallengeTrigger challengeTrigger;


    int basePoints = 6;
    int currentPointNumber = 1;
    int totalPoints;

    // Start is called before the first frame update
    void Start() {
        Activate();
    }

    public void Activate() {

        var osuPanelTransform = osuPanelContainer.transform;

        var osuPanelRectTransform = osuPanelTransform.GetComponent<RectTransform>();

        Vector2 maxSpawnRange = osuPanelRectTransform.sizeDelta;

        Vector2 spawnRange = maxSpawnRange - dotPrefab.GetComponent<RectTransform>().sizeDelta;

        totalPoints = (int)Mathf.Ceil(basePoints * challengeTrigger.difficultyMod);

        for (int i = totalPoints; i >= 1; i--) {

            int randomX = (int)(Random.Range(0, spawnRange.x) - spawnRange.x / 2);
            int randomY = (int)(Random.Range(0, spawnRange.y) - spawnRange.y / 2);

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

    void buttonGetClickInfo(int number) {
        if (number != currentPointNumber) {
            challengeTrigger.Fail();
            currentPointNumber = 1;
        } else if (number == basePoints) {
            challengeTrigger.Fulfill();
            currentPointNumber = 1;
        } else {
            currentPointNumber++;
        }
    }
}
