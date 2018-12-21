using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProcess : MonoBehaviour {
    public Sprite[] HpUI;
    public GameObject HpBar;
    public GameObject HpContain;
    public Image[] GameState;
    public Image Cover;
    public Image TitlePic;
    // Use this for initialization
    
    private int localLife = 0;
    private bool Showing = false;
    private bool DoneInit = false;
	void Start () {
        _createHpBar(GameMnger.MngerIns.PlayLife);
        localLife = GameMnger.MngerIns.PlayLife;
	}
	
	// Update is called once per frame
	void Update () {
        if (localLife != GameMnger.MngerIns.PlayLife)
        {
            if (HpContain.transform.childCount< GameMnger.MngerIns.PlayLife)
            {
                _updateHp(0);
            }
            else if(localLife < GameMnger.MngerIns.PlayLife)
            {
                _updateHp(2);
            }
            else
            {
                if (GameMnger.MngerIns.PlayLife == 0 && !Showing)
                {
                    Showing = true;
                    ShowGameOver();
                }
                _updateHp(1);
            }
            localLife = GameMnger.MngerIns.PlayLife;
        }
        if(GameMnger.MngerIns.Started == true && !DoneInit)
        {
            DoneInit = true;
            Init();
        }
    }
    void fixedUpdate()
    {
        
    }
    void _updateHp(int index)
    {
        switch (index)
        {
            case 0:
                if(GameMnger.MngerIns.PlayLife > HpContain.transform.childCount)
                {
                    Debug.Log("ran");
                    _createHpBar(GameMnger.MngerIns.PlayLife - HpContain.transform.childCount);
                }
                for (int i = 0; i < localLife; i++)
                {
                    HpContain.transform.GetChild(i).GetComponent<Image>().sprite = HpUI[index];
                }
                break;
            case 1:
                for(int i= GameMnger.MngerIns.PlayLife; i < HpContain.transform.childCount; i++)
                {
                    HpContain.transform.GetChild(i).GetComponent<Image>().sprite = HpUI[index];
                }
                
                break;
            case 2:
                for(int i = localLife; i < GameMnger.MngerIns.PlayLife; i++)
                {
                    HpContain.transform.GetChild(i).GetComponent<Image>().sprite = HpUI[0];
                }
                break;
         }
        
    }
    void _createHpBar(int lifeCount)
    {
        for (int i = 0; i < lifeCount; i++)
        {
            var TempUi = GameObject.Instantiate(HpBar, HpContain.transform);
            TempUi.GetComponent<RectTransform>().localPosition = new Vector3(-(Screen.width / 2) + ((HpContain.transform.childCount + 1) * 80), 420f);

        }
    }
    void ShowGameOver()
    {
        GameState[0].gameObject.SetActive(true);
        Cover.gameObject.SetActive(true);
        StartCoroutine(FadeInOut(Cover,Color.clear,Color.black));
        StartCoroutine(FadeInOut(GameState[0], Color.clear, Color.white));
        GameMnger.MngerIns.ShowGameOver = true;
    }
    IEnumerator FadeInOut(Image Target,Color Start,Color End)
    {
        float temp = 0;
        for (int i = 0; i < 20; i++)
        {
            if (i == 19) temp = 19;
            else temp = (float)i / 19;
            Target.color = Color.Lerp(Start, End, temp);
            yield return new WaitForSeconds(0.05f);
        }
    }
    void Init()
    {
        StartCoroutine(FadeInOut(TitlePic, Color.white, Color.clear));
        HpContain.SetActive(true);
    }
    
}
