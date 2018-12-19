using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 壁の倒れる関連
/// </summary>
public class WallController : MonoBehaviour
{
    [Header("参照")]
    private Rigidbody rig;
    private PlayerController playerCon;

    [Header("重力関連")]
    private float torqueEorce = 150.0f;
    Vector3 rightVec;
    Vector3 torque;

    // Use this for initialization
    void Start ()
    {
        ///---初期化---///
		rig = GetComponent<Rigidbody>();
        playerCon = GameObject.FindObjectOfType<PlayerController>();

        rightVec = transform.right;         //倒れる方向
        torque = rightVec * torqueEorce;    //加える力
    }

    // Update is called once per frame
    void Update ()
    {

	}

    /// <summary>
    /// 壁を傾ける
    /// </summary>
    public void Topple()
    {
        rig.AddTorque(torque, ForceMode.Force); //押す

        StartCoroutine(DestroyWall());
    }

    /// <summary>
    /// 1.5秒待って消す
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);                //壁を消す
        playerCon.State = MoveState.Move;   //ステータスを戻す
    }
}
