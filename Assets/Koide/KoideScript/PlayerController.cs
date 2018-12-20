using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのステータス
/// </summary>
public enum MoveState
{
    Stop       = 0,
    Move       = 1,
    WallAction = 2,
}
/// <summary>
/// プレイヤーの制御に関するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("アニメション関連")]
    private Animator animator;
    private AnimatorStateInfo stateInfo;

    [Header("プレイヤー移動関連")]
    [SerializeField] private float moveSpeed;   //移動スピード
    private int index = 0;                      //位相変更用インデックス
    //private float rayDistance;
    private Vector3 velocity;                   //Inputの入力値

    private Vector3[] move = new Vector3[5];    //カメラの方向ベクトルテーブル
    private Vector3[] vec =                     //プレイヤーのコントローラー入力値テーブル
    {
     new Vector3( 0, 0, 0),
     new Vector3( 0, 0, 1),         //上
     new Vector3( 1, 0, 0),         //右
     new Vector3( 0, 0,-1),         //下
     new Vector3(-1, 0, 0),         //左
    };

    private GameObject wall;        //壁
    private bool wallFlg = false;   //壁に触れているか

    [Header("参照物")]
    private CharacterController charaCon;
    private MoveState moveState;
    public Camera mainCamera;

///------プロパティ-------///
    /// <summary>
    /// ステート受け渡し用
    /// </summary>
    public MoveState State
    {
        set { moveState = value; }
        get { return moveState; }
    }

///---------------------///

    void Start ()
    {
        //---初期化---//
        charaCon = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        //rayDistance = (transform.localScale.z / 2) + 0.5f;
        MoveUpdate();
        moveState = MoveState.Move;
    }
	
	void Update ()
    {
        MoveUpdate();

        MoveInput();
    }

    /// <summary>
    /// moveの中身を更新(カメラの向きベクトル)
    /// </summary>
    public void MoveUpdate()
    {
        move[0] = new Vector3(0, 0, 0);
        move[1] = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
        move[2] = mainCamera.transform.right;
        move[3] = -new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
        move[4] = -mainCamera.transform.right;
    }
    /// <summary>
    /// プレイヤーの向き情報を入力
    /// </summary>
    public void MoveInput()
    {
        //Moveステータスの時だけ処理
        if(moveState == MoveState.Move)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); //コントローラーの入力情報
         
            //入力が最大の時
            if (velocity.magnitude >= 0.5f)
            {
                MoveDirection(velocity);                            //向きを決定する
                transform.LookAt(transform.position + move[index]); //向き変更
            }
            else
            {
                index = 0;
            }
            charaCon.Move(move[index] * moveSpeed * Time.deltaTime);    //移動
            velocity.y += Physics.gravity.y * Time.deltaTime;           //下に重力を掛ける

            //壁を倒す処理
            if (Input.GetKeyDown(KeyCode.J) && wallFlg)
            {
                moveState = MoveState.WallAction;          //ステータス変更
                
                transform.LookAt(wall.transform.position); //壁の方を向く

                wall.GetComponent<Rigidbody>().isKinematic = false; //重力オン
                wall.GetComponent<WallController>().Topple();       //倒す関数呼びだし

                //初期化//
                wall = null; 
                wallFlg = false;
            }
        }             
    }

    /// <summary>
    /// プレイヤーの方向決定
    /// </summary>
    public void MoveDirection(Vector3 dir)
    {
        for(int i = 0; i < vec.Length; i++)
        {
            if (dir == vec[i])
            {            
                index = i;
            }
        }
    }

    /// <summary>
    /// Rayを飛ばして壁があれば「true」無ければ「false」
    /// </summary>
    /// <returns></returns>
    //public bool RaycastHitCheck()
    //{
    //    Ray ray = new Ray(transform.position, transform.forward);

    //    RaycastHit hit;

    //    Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

    //    if(Physics.Raycast(ray,out hit, rayDistance))
    //    {
    //        if (hit.collider.gameObject.tag == "Player")
    //        {
    //            return true;
    //        }         
    //        return false;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            wall = col.gameObject.transform.root.gameObject;
            wallFlg = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            wall = null;
            wallFlg = false;
        }
    }
}
