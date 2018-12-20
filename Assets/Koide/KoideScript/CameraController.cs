using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// カメラの制御関連
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("オブジェクト参照")]
    private GameObject baseBuild;   //拠点
    private GameObject player;      //プレイヤー

    [Header("移動関連")]
    private int distance = 10;      //プレイヤーとの距離
    private Vector3 moveVec;        //拠点とプレイヤーの方向ベクトル
    private Vector3 playerPos;      //プレイヤーのポジション記憶用
    private Vector3 homePos;        //拠点のポジション記憶用
    private float offsetY = 10.0f;

    //by Allen
    bool titleMode = true;

    void Start ()
    {
        ///---初期化---///
        baseBuild = GameObject.FindGameObjectWithTag("Tower");
        player = GameObject.Find("Player");
        
        playerPos = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
        homePos = new Vector3(baseBuild.transform.position.x, 0.0f, baseBuild.transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerPos = new Vector3(player.transform.position.x,0.0f, player.transform.position.z);   //プレイヤーの位置を更新

        moveVec = Vector3.Normalize(playerPos - homePos);                                         //ベクトルの計算
        transform.position = playerPos + moveVec * distance + new Vector3(0, offsetY, 0);         //位置調整

        transform.LookAt(baseBuild.transform.position);                                           //向き更新
	}
}
