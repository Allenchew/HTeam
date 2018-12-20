using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProcess : MonoBehaviour {
    public Sprite[] HpUI;
    public GameObject HpBar;
    public Image[] GameState;
    public Image Cover;
    // Use this for initialization
    private int localLife = 0;
    private bool Showing = false;
	void Start () {
        _createHpBar(GameMnger.MngerIns.PlayLife);
        localLife = GameMnger.MngerIns.PlayLife;
	}
	
	// Update is called once per frame
	void Update () {
        if (localLife != GameMnger.MngerIns.PlayLife)
        {
            if (transform.childCount< GameMnger.MngerIns.PlayLife)
            {
                _updateHp(0);
            }
            else if(localLife < GameMnger.MngerIns.PlayLife)
            {
                _updateHp(2);
            }
            else
            {
                Debug.Log(GameMnger.MngerIns.PlayLife == 0 && !Showing);
                if (GameMnger.MngerIns.PlayLife == 0 && !Showing)
                {
                    Showing = true;
                    StartCoroutine("ShowGameOver");
                }
                _updateHp(1);
            }
            localLife = GameMnger.MngerIns.PlayLife;
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
                if(GameMnger.MngerIns.PlayLife > transform.childCount)
                {
                    Debug.Log("ran");
                    _createHpBar(GameMnger.MngerIns.PlayLife - transform.childCount);
                }
                for (int i = 0; i < localLife; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = HpUI[index];
                }
                break;
            case 1:
                for(int i= GameMnger.MngerIns.PlayLife; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = HpUI[index];
                }
                
                break;
            case 2:
                for(int i = localLife; i < GameMnger.MngerIns.PlayLife; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = HpUI[0];
                }
                break;
         }
        
    }
    void _createHpBar(int lifeCount)
    {
        for (int i = 0; i < lifeCount; i++)
        {
            var TempUi = GameObject.Instantiate(HpBar, transform);
            TempUi.GetComponent<RectTransform>().localPosition = new Vector3(-(Screen.width / 2) + ((transform.childCount + 1) * 80), 420f);

        }
    }
    IEnumerator ShowGameOver()
    {
        Debug.Log("entered");
        GameState[0].gameObject.SetActive(true);
        Cover.gameObject.SetActive(true);
        float temp = 0;
        for (int i = 0; i < 20; i++)
        {
            if (i == 19) temp = 19;
            else temp = (float)i / 19;
            Cover.color = Color.Lerp(Color.clear, Color.black, temp);
            GameState[0].color = Color.Lerp(Color.clear, Color.white, temp);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
