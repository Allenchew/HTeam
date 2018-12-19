using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour{
    public static MapControl Mapmnger;
	public TextAsset textAsset;

	public float polygon=6; //角の数

	public float distance = 0.5f; //壁を立てる距離

	public GameObject wall;

	public List<int> stageDate = new List<int>();

	public enum STAGE_KIND{
		none,brake
	};
    
    public void GetCreateMap()
    {
        CreateStage();
    }

  void Awake()
    {
        Mapmnger = this;
    }
    // Use this for initialization
    void Start () {
        //CreateStage();
    }

	// Update is called once per frame
	void Update () {

	}

	void CreateStage(){

		string stageTextData =textAsset.text;

		int i = 0;
		int j = 1;

		foreach(char c in stageTextData){

			//GameObject obj = null;

			//Debug.Log (c);

			if(c == 'w'){
				stageDate.Add (1);
				int corner = i / j;

				float _angle = 360.0f / polygon;

				float _rad = _angle*Mathf.Deg2Rad;
				float wallSizeZ = wall.transform.localScale.z * distance;

				float x = Mathf.Cos (_rad * corner) * wallSizeZ * j;
				float z = Mathf.Sin (_rad * corner) * wallSizeZ * j;

				float corAngle = corner * _angle;

				GameObject obj = GameObject.Instantiate (wall, new Vector3 (x, 0.0f, z),Quaternion.Euler(new Vector3(0.0f,-corAngle,0.0f))/*new Quaternion(0.0f,2.0f,0.0f,1.0f)*/);

				float side = (j - 1) * -wallSizeZ / 2 + (i % j) * (wallSizeZ /*+ wallZ / 10*/);

				//loat side2 = i % j * 1.5f;

				obj.transform.localPosition += obj.transform.forward * side;

			}else if(c == '\n'){
				i = 0;
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
	}

	List<int> GetMapChipData(){
		return stageDate;
	}
}
