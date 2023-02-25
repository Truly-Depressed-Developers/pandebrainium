using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBtn : MonoBehaviour
{
    private Color activColor = new Color(0.5f, 0.5f, 1, 0.6f);
    private Color noActivColorNoImg = new Color(1, 1, 1, 0);
    private Color noActivColorWithImg = new Color(1, 1, 1, 1);

    public event Action<int, bool> OnClickToParent;

    private bool active = false;
    private bool withImg = false;

    private int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setId(int val)
    {
        id = val;
    }

    public void withImgTrue()
    {
        withImg = true;
    }

    public void onClick()
    {
        active = !active;

        GetComponent<Image>().color = active ? activColor : withImg ? noActivColorWithImg : noActivColorNoImg;
        OnClickToParent?.Invoke(id, active);
    }
}
