using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<<<<<<< HEAD

//public class MapControl{


//=======
public class MapControl : MonoBehaviour{
    public static MapControl Mapmnger;

//>>>>>>> origin/t_nakano
	public float polygon=6; //角の数

    private float distance = 1.5f; //壁を立てる距離

    //壁を立てる距離



    public List<int> stageDate = new List<int>();

	public enum STAGE_KIND{
		none,brake
	};
    
    public void GetCreateMap(GameObject wall)
    {
       // CreateStage(wall);
    }

    void Start () {
        CreateStage();
    }

	// Update is called once per frame
	void Update () {

	}
    /*<<<<<<< HEAD

        void CreateStage(GameObject wall, int stageno = 1){

    =======*/

    void CreateStage()
    {

        string stageData = "StageData/stageNo_" + (1).ToString();

        TextAsset textAsset = Resources.Load(stageData) as TextAsset;

        string mapData = textAsset.text;

        GameObject wall = (GameObject)Resources.Load("Model/Wall");

        Debug.Log(wall);

        int i = 0;
        int j = 1;

        foreach (char c in mapData)
        {

            //GameObject obj = null;

            //Debug.Log (c);

            if (c == 'w')
            {
                stageDate.Add(1);
                int corner = i / j;

                float _angle = 360.0f / polygon;

                float _rad = _angle * Mathf.Deg2Rad;
                float wallSizeZ = wall.transform.localScale.z * distance;
                float wallSizeY = wall.transform.localScale.y * distance / 3 * 2;

                float x = Mathf.Cos(_rad * corner) * wallSizeY * j;
                float z = Mathf.Sin(_rad * corner) * wallSizeY * j;

                float corAngle = corner * _angle;

                GameObject obj = GameObject.Instantiate(wall, new Vector3(x, wall.transform.localScale.y/2, z), Quaternion.Euler(new Vector3(0.0f, -corAngle, 0.0f)));

				float side = (j - 1) * -wallSizeZ / 2 + (i % j) * (wallSizeZ );

                //loat side2 = i % j * 1.5f;

                obj.transform.localPosition += obj.transform.forward * side;

            }
            else if (c == '\n')
            {
                i = 0;
                j++;
                Debug.Log(j);
                continue;
            }
            else if (c == ' ')
            {
                stageDate.Add(0);
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
