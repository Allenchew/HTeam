using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD

public class MapControl{


=======
public class MapControl : MonoBehaviour{
    public static MapControl Mapmnger;

>>>>>>> origin/t_nakano
	public float polygon=6; //角の数

	 //壁を立てる距離



	public List<int> stageDate = new List<int>();

	public enum STAGE_KIND{
		none,brake
	};
    
    public void GetCreateMap(GameObject wall)
    {
        CreateStage(wall);
    }

    void Start () {
        CreateStage();
    }

	// Update is called once per frame
	void Update () {

	}
<<<<<<< HEAD
    
	void CreateStage(GameObject wall, int stageno = 1){
        
=======

	void CreateStage(){

		string stageData = "stageData/stageNo_" + (1).ToString ();

		TextAsset textAsset = Resources.Load (stageData) as TextAsset;

		string mapData = textAsset.text;
		Debug.Log (mapData);
		GameObject wall = (GameObject)Resources.Load ("Model/Wall");

		Debug.Log (wall);

>>>>>>> origin/t_nakano
		int i = 0;
		int j = 1;
        float distance = wall.transform.localScale.z;
        string stageTextData = "csv/stage_" + (stageno).ToString();
        TextAsset textAsset = Resources.Load(stageTextData) as TextAsset;
        foreach (char c in textAsset.text){
	

			//GameObject obj = null;

			//Debug.Log (c);

			if(c == 'w'){
				stageDate.Add (1);
				int corner = i / j;

				float _angle = 360.0f / polygon;

				float _rad = _angle*Mathf.Deg2Rad;
				float wallSizeZ = wall.transform.localScale.z * distance;
				float wallSizeY = wall.transform.localScale.y * distance / 3 * 2;

				float x = Mathf.Cos (_rad * corner) * wallSizeY * j;
				float z = Mathf.Sin (_rad * corner) * wallSizeY * j;

				float corAngle = corner * _angle;

<<<<<<< HEAD
				GameObject obj = GameObject.Instantiate (wall, new Vector3 (x,wall.transform.localScale.y/2, z),Quaternion.Euler(new Vector3(0.0f,-corAngle,0.0f))/*new Quaternion(0.0f,2.0f,0.0f,1.0f)*/);
=======
				GameObject obj = GameObject.Instantiate (wall, new Vector3 (x, wall.transform.localScale.y/2, z),Quaternion.Euler(new Vector3(0.0f,-corAngle,0.0f)));
>>>>>>> origin/t_nakano

				float side = (j - 1) * -wallSizeZ / 2 + (i % j) * (wallSizeZ + wallSizeZ / 15);

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
