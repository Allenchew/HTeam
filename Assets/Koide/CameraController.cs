using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject home;
    public GameObject player;
    private int dis = 15;
    private Vector3 moveVec;
    private Vector3 playerPos;
    private Vector3 homePos;
    // Use this for initialization
    void Start ()
    {
        playerPos = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
        homePos = new Vector3(home.transform.position.x, 0.0f, home.transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerPos = new Vector3(player.transform.position.x,0.0f, player.transform.position.z);
        homePos   = new Vector3(home.transform.position.x, 0.0f, home.transform.position.z);

        moveVec = Vector3.Normalize(playerPos - homePos);
        transform.position = playerPos + moveVec * dis + new Vector3(0,10,0);

        transform.LookAt(home.transform.position);
	}
}
