using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public GameObject gameobject;

	// Use this for initialization
	void Start () {

		gameobject.GetComponent<MapManagerControl>().CreateMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
