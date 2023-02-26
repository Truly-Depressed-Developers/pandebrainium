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
        },
        new List<List<int>>() {
            new List<int>() {
                0,0,0,
                2,1,1,
                1,1,2},
            new List<int>() {
                0,0,0,0,
                0,0,2,0,
                2,1,1,2,
                2,2,2,2},
            new List<int>() {
                0,0,0,0,0,
                0,0,0,0,0,
                2,1,1,1,0,
                2,1,1,1,0,
                0,2,2,2,0}
        },
        new List<List<int>>() {
            new List<int>() {
                0,1,1,
                1,1,1,
                2,2,0},
            new List<int>() {
                0,0,2,2,
                0,1,1,2,
                2,1,1,0,
                2,0,0,0},
            new List<int>() {
                0,0,0,2,0,
                0,2,1,1,2,
                0,1,1,1,0,
                2,2,1,2,0,
                2,0,0,0,0}
        }
    };

    private int captchaSize;
    private int captchaType;
    private int spriteID;

    private int goodClick = 0;
    private int wrongClick = 0;

    private int searchedSpriteId = -1;
    private int toFind = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitRandomizingValues();
        InitGenerateButtons();

        for(int i = 0; i < 9; i++)
        {

            Debug.Log("VAL(" + i + "): " + ((i % 3) * 3 + (3 - 1 - Mathf.FloorToInt(i / 3))));
        }
    }

    int getRandomRotation()
    {
        return 90 * Random.Range(0, 4);
    }

    void checkClickedBtn(int id, bool active)
    {
        if (captchaType == 0)
        {
            if (id == 1)
            {
                goodClick += active ? 1 : -1;
            }
            else if (id == 0)
            {
                wrongClick += active ? 1 : -1;
            }
        }
        else
        {
            if (id == searchedSpriteId)
            {
                goodClick += active ? 1 : -1;
            }
            else
            {
                wrongClick += active ? 1 : -1;
            }
        }
    }

    int getRandSpriteId()
    {
        return Random.Range(0, sprites.Length);
    }

    void InitRandomizingValues()
    {
        captchaSize = Mathf.Min(maxCaptchaSize, Mathf.FloorToInt(Random.Range(minCaptchaSize, minCaptchaSize * multiplayer + 1)));
        captchaType = 1;
        //captchaType = Random.Range(0, maxTypes + 1);
        if (captchaType == 0)
        {
            spriteID = getRandSpriteId();
            int rotation = getRandomRotation();
            Sprite sprite = sprites[spriteID];
            Debug.Log("ROTATION: " + rotation);
            mainCaptchaImg.transform.Rotate(new Vector3(0, 0, rotation));
            mainCaptchaImg.sprite = sprite;
        }
    }

    public string getSearchedAnimalName()
    {
        int id;
        if (captchaType == 0) id = spriteID;
        else id = searchedSpriteId;

        if (id == 0) return "PANDA";
        else if (id == 1) return "LION";
        else return "BIRD";
    }

    void InitGenerateButtons()
    {
        GridLayoutGroup gridLay = gridPlane.GetComponent<GridLayoutGroup>();
        gridLay.cellSize = new Vector2(450.0f / captchaSize, 450.0f / captchaSize);
        for (int i = 0; i < Mathf.Pow(captchaSize, 2); i++)
        {
            Button btn = Instantiate(clickBtnPrefab);
            if (captchaType == 0)
            {
                btn.GetComponent<ClickBtn>().setId(imagesFieldData[spriteID][captchaSize-3][i]);
            }
            else
            {
                int randSprite = getRandSpriteId();
                if (searchedSpriteId == -1)
                {
                    searchedSpriteId = randSprite;
                    ++toFind;
                }
                else if (randSprite == searchedSpriteId) ++toFind;

                btn.GetComponent<ClickBtn>().setId(randSprite);
                btn.GetComponent<ClickBtn>().withImgTrue();
                btn.image.color = new Color(1, 1, 1, 1);
                btn.image.sprite = sprites[randSprite];
                btn.image.transform.Rotate(new Vector3(0, 0, getRandomRotation()));

            }

            btn.GetComponent<ClickBtn>().OnClickToParent += checkClickedBtn;
            btn.transform.SetParent(gridPlane.transform);
        }
    }

    public void CheckCaptcha()
    {
        int good = 0;

        if (captchaType == 0)
        {
            for (int i = 0; i < Mathf.Pow(captchaSize, 2); i++)
            {
                if (imagesFieldData[spriteID][captchaSize - 3][i] == 1) ++good;
                Debug.Log(imagesFieldData[spriteID][captchaSize - 3][i]);
            }
        } else
        {
            good = toFind;
        }

        if (good == goodClick && wrongClick == 0)
        {
            TaskCompleted();
        }
        else
        {
            TaskFailed();
        }
    }

    void TaskCompleted()
    {
        Debug.Log("WIN");
        trigger.Fulfill();
    }

    void TaskFailed()
    {
        Debug.Log("LOSE");
        trigger.Fail();
    }
}
