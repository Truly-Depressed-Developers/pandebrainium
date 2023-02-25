using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateCaptcha : MonoBehaviour
{
    [SerializeField] private int minCaptchaSize = 3;
    [SerializeField] private int maxCaptchaSize = 5;
    [SerializeField] private int maxTypes = 1;
    [SerializeField] private float multiplayer = 1.0f;

    [SerializeField] private Image mainCaptchaImg;
    [SerializeField] private GameObject gridPlane;
    [SerializeField] private Button clickBtnPrefab;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private ChallengeTrigger trigger;

    private List<List<List<int>>> imagesFieldData = new List<List<List<int>>>() { 
        new List<List<int>>() { 
            new List<int>() {
                0,0,0,
                1,1,0,
                1,1,0},
            new List<int>() {
                0,0,0,0,
                0,0,0,0,
                1,1,0,0,
                1,1,0,0},
            new List<int>() {
                0,0,0,0,0,
                0,0,0,0,0,
                2,1,2,0,0,
                1,1,2,0,0,
                2,1,2,0,0}
        } 
    };



    private int captchaSize;
    private int captchaType;
    private int spriteID;

    private int goodClick = 0;
    private int wrongClick = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitRandomizingValues();
        InitGenerateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkClickedBtn(int id, bool active)
    {
        if(imagesFieldData[spriteID][captchaSize - 3][id] == 1)
        {
            goodClick += active ? 1 : -1;
        } else if(imagesFieldData[spriteID][captchaSize - 3][id] == 0)
        {
            wrongClick += active ? 1 : -1;
        }
    }

    void InitRandomizingValues()
    {
        captchaSize = Mathf.Min(maxCaptchaSize, Mathf.FloorToInt(Random.Range(minCaptchaSize, minCaptchaSize*multiplayer + 1)));
        captchaType = Random.Range(0, maxTypes+1);
        spriteID = Random.Range(0, sprites.Length);
        Sprite sprite = sprites[spriteID];
        mainCaptchaImg.sprite = sprite;
    }

    void InitGenerateButtons()
    {
        GridLayoutGroup gridLay = gridPlane.GetComponent<GridLayoutGroup>();
        gridLay.cellSize = new Vector2(300.0f / captchaSize, 300.0f / captchaSize);
        for(int i = 0; i < Mathf.Pow(captchaSize, 2); i++)
        {
            Button btn = Instantiate(clickBtnPrefab);
            btn.GetComponent<ClickBtn>().setPosId(i);
            btn.GetComponent<ClickBtn>().OnClickToParent += checkClickedBtn;
            btn.transform.SetParent(gridPlane.transform);
        }
    }

    public void CheckCaptcha()
    {
        int good = 0;

        for(int i = 0; i < Mathf.Pow(captchaSize, 2); i++)
        {
            if (imagesFieldData[spriteID][captchaSize - 3][i] == 1) ++good;
        }

        if(good == goodClick && wrongClick == 0)
        {
            TaskCompleted();
        } else
        {
            TaskFailed();
        }
    }

    void TaskCompleted()
    {
        trigger.Fulfill();
    }

    void TaskFailed()
    {
        trigger.Fail();

    }
}
