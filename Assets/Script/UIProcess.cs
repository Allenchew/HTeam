using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProcess : MonoBehaviour {
    public Sprite[] HpUI;
    public GameObject HpBar;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < GameMnger.MngerIns.PlayLife; i++)
        {
            var TempUi = GameObject.Instantiate(HpBar, transform);
            Debug.Log(TempUi.GetComponent<RectTransform>().position);
            TempUi.GetComponent<RectTransform>().localPosition = new Vector3(-(Screen.width / 2) + (i * 80), 280f);
            Debug.Log(Screen.width);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
