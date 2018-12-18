using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MapControl : MonoBehaviour {

	public TextAsset textAsset;

	public float polygon=6; //角の数

	public float distance = 0.5f; //壁を立てる距離

	public GameObject wall;

	public List<int> stageDate = new List<int>();

	public enum STAGE_KIND{
		none,brake
	};

	// Use this for initialization
	void Start () {

		CreateStage ();

	}

	// Update is called once per frame
	void Update () {

	}

	void CreateStage(){

		string stageTextData = textAsset.text;

		int i = 0;
		int j = 1;

		foreach(char c in stageTextData){

			//GameObject obj = null;

			//Debug.Log (c);

			if(c == 'w'){
				stageDate.Add (1);
				int n = i / j;

				float _angle = 360.0f / polygon;

				float _rad = _angle*Mathf.Deg2Rad;

				//Debug.Log (angle);

				float x = Mathf.Cos (_rad * n) * distance * j;
				float z = Mathf.Sin (_rad * n) * distance * j;

				float i_angle = n * _angle;

				GameObject obj = GameObject.Instantiate (wall, new Vector3 (x, 0.0f, z),Quaternion.Euler(new Vector3(0.0f,-i_angle,0.0f))/*new Quaternion(0.0f,2.0f,0.0f,1.0f)*/);

				float side = (j - 1) * 0.5f;

				float side2 = i % j * 1.5f;

				obj.transform.localPosition += new Vector3 (side2, 0.0f, 0.0f);

			}else if(c == '\n'){
				j++;
				Debug.Log (j);
				continue;
			}else if(c == ' '){
				stageDate.Add (0);
			}
			i++;
			//Debug.Log (i);
			//Debug.Log (j);
		}
		Debug.Log (stageDate);

	}
}
