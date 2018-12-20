using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagerControl : MonoBehaviour {

	public int corner = 6;
	public float distance = 1.5f; //壁を立てる距離

	int childCount = 0;

	// Use this for initialization
	void Start () {
		//Debug.Log (childCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool MapSer(){
		int objectCount = this.transform.childCount;

		if (childCount != objectCount) {
			childCount = objectCount;
			return true;
		}
		return false;
	}

	public void CreateMap(int stageNo=1){
		Debug.Log (childCount);
		string stageData = "StageData/stageNo_" + (stageNo).ToString ();

		TextAsset textAsset = Resources.Load (stageData) as TextAsset;

		string mapData = textAsset.text;

		GameObject wall = (GameObject)Resources.Load ("Model/Wall");

		int i = 0;
		int j = 1;

		foreach (char c in mapData) {
			if (c == 'w') {
				int _corner = i / j;

				float _angle = 360.0f / corner;

				float _rad = _angle * Mathf.Deg2Rad;
				float wallSizeZ = wall.transform.lossyScale.z * distance;
				float wallSizeY = wall.transform.lossyScale.y * distance / 3 * 2;

				float x = Mathf.Cos (_rad * _corner) * wallSizeY * j;
				float z = Mathf.Sin (_rad * _corner) * wallSizeY * j;

				float corAngle = _corner * _angle;

				GameObject obj = GameObject.Instantiate (wall, new Vector3 (x, wall.transform.localScale.y / 2, z), Quaternion.Euler (new Vector3 (0.0f, -corAngle, 0.0f)));
				obj.transform.parent = transform;

				float side = (j - 1) * -wallSizeZ / 2 + (i % j) * (wallSizeZ);

				obj.transform.localPosition += obj.transform.forward * side;

			} else if (c == '\n') {
				i = 0;
				j++;
				continue;
			} else if (c == ' ') {
				
			}
			i++;
		}

		childCount = this.transform.childCount;
		Debug.Log (childCount);
	}


}
