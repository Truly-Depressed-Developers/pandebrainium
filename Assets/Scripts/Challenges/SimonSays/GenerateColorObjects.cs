using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateColorObjects : MonoBehaviour
{
    [SerializeField] private GameObject colorInfoPanel;
    [SerializeField] private GameObject colorBtnsPanel;
    [SerializeField] private Image colorInfoImgPrefab;
    [SerializeField] private UnityEngine.UI.Button colorBtnPrefab;
    [SerializeField] private List<Color> baseColors = new List<Color>() {
        new Color(1,0,0,1), 
        new Color(0,1,0,1), 
        new Color(0,0,1,1) 
    };
    [SerializeField] private float multip = 1.0f;
    [SerializeField] private int baseCount = 3;

    private List<Color> selectedColors = new List<Color>();
    private int colorIdToClick = 0;
    private int colorsCount;

    // Start is called before the first frame update
    void Start()
    {
        colorsCount = Mathf.Min(baseColors.Count, Mathf.FloorToInt(baseCount * multip));

        RandomColors();
        CreateInfoImgs();
        CreateColorBtns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomColors()
    {
        List<Color> tempArray = new List<Color>(baseColors);

        for (int i = 0; i < colorsCount; i++)
        {
            int rand = Random.Range(0, tempArray.Count);
            selectedColors.Add(tempArray[rand]);
            tempArray.RemoveAt(rand);

        }
    }

    void CreateInfoImgs()
    {
        for(int i = 0; i < selectedColors.Count; i++)
        {
            Image img = Instantiate(colorInfoImgPrefab);
            img.color = selectedColors[i];
            img.transform.SetParent(colorInfoPanel.transform);
        }
    }

    void TaskComplete()
    {
        Debug.Log("COMPLETE");
    }

    void FailTask()
    {
        Debug.Log("FAILED");
    }

    void checkClickedBtn(GameObject gObj, int colorId)
    {
        if(colorId == colorIdToClick)
        {
            ++colorIdToClick;
            gObj.GetComponent<ColorBtn>().disableClick();
            gObj.GetComponent<Image>().enabled = false;
        } else
        {
            FailTask();
            return;
        }

        if(colorIdToClick == colorsCount)
        {
            TaskComplete();
        }
    }

    void CreateColorBtns()
    {
        List<Color> tempArray = new List<Color>(selectedColors);

        for (int i = 0; i < selectedColors.Count; i++)
        {
            UnityEngine.UI.Button btn = Instantiate(colorBtnPrefab);
            btn.transform.SetParent(colorBtnsPanel.transform);

            int randId = Random.Range(0, tempArray.Count);
            Color color = tempArray[randId];

            ColorBlock btnColors = btn.colors;
            btnColors.normalColor = new Color(color.r, color.g, color.b, color.a * 0.7f);
            btnColors.selectedColor = new Color(color.r, color.g, color.b, color.a * 0.7f);
            btnColors.highlightedColor = new Color(color.r, color.g, color.b, color.a * 0.8f); ;
            btnColors.pressedColor = color;

            btn.colors = btnColors;

            ColorBtn btnScript = btn.GetComponent<ColorBtn>();
            btnScript.setBtnColorId(selectedColors.IndexOf(color));
            btnScript.OnClickToParent += checkClickedBtn;

            tempArray.RemoveAt(randId);
        }
    }
}
