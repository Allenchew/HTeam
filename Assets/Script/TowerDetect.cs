using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnCollisionEnter(Collision collide)
    {
        if(collide.transform.tag == "Enemy" && GameMnger.MngerIns.PlayLife>0)
        {
            GameMnger.MngerIns.PlayLife -= 1;
            EnemyMnger.EnemyIns.EnemyCount--;
            Destroy(collide.gameObject);
        }
    }
    
}
