using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class MapCreate : MonoBehaviour {

	public int hierarchy = 3 ; // 階層
	public int corner = 6; //　角の数

	public GameObject wall;

	enum MAP_STATE{ none,stand };

	struct MapData{
		Vector3 pos;
		Vector3 rot;
		MAP_STATE state;
	};
	private List<MapData> _mapData = new List<MapData> ();

	public TextAsset stageText;
	//private TextAsset stageText;

	// Use this for initialization
	void Start () {

		CreateMap ("stage_01");

	}
	void CreateMap(string stageName){
		
		string stageTextData = stageText.text;



		foreach (char c in stageTextData) {

			if (c == '\n') {
				continue;
			}

			float chipAngle = 360.0f / corner;
			float chipRad = chipAngle*Mathf.Deg2Rad;




			if (c == 'w') {
				//Instantiate (obj, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f)));
			} else if (c == ' ') {

			} 
		}
	}
}
