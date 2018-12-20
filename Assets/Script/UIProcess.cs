using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProcess : MonoBehaviour {
    public Sprite[] HpUI;
    public GameObject HpBar;
    // Use this for initialization
    private int localLife = 0;
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
}
