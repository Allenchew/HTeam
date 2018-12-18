using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    Stop  = 0,
    Move = 1,
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
    [SerializeField] private float moveTimerSpeed;
    [SerializeField] private float rotTimerSpeed;
    private float rayDistance;
    private float rotTimelimit;
    private CharacterController charaCon;
    private MoveState moveState;
    private Vector3 velocity;
    private Vector3 moveTemp;

    private Vector3[] vec = 
    {
     new Vector3( 0, 0, 0),
     new Vector3( 0, 0, 1),     //上
     new Vector3( 1, 0, 1),     //右上
     new Vector3( 1, 0, 0),     //右
     new Vector3( 1, 0,-1),     //右下
     new Vector3( 0, 0,-1),     //下
     new Vector3(-1, 0,-1),     //左下
     new Vector3(-1, 0, 0),     //左
     new Vector3(-1, 0, 1),     //左上
    };
    private Vector3[] rot =
    {
     new Vector3( 0,  0, 0),
     new Vector3( 0,  0, 0),     //上
     new Vector3( 0, 45, 0),     //右上
     new Vector3( 0, 90, 0),     //右
     new Vector3( 0,135, 0),     //右下
     new Vector3( 0,180, 0),     //下
     new Vector3( 0,225, 0),     //左下
     new Vector3( 0,270, 0),     //左
     new Vector3( 0,315, 0),     //左上
    };

	void Start ()
    {
        charaCon = GetComponent<CharacterController>();
        rayDistance = (transform.localScale.z / 2) + 0.5f;
        rotTimelimit = 1.0f;
        moveTemp = new Vector3(0, 0, 0);
        moveState = MoveState.Stop;
    }
	
	void Update ()
    {
        MoveInput();
    }

    /// <summary>
    /// プレイヤーの向き情報を入力
    /// </summary>
    public void MoveInput()
    {
        //停止状態の時に向きを選ぶ
        if(moveState == MoveState.Stop)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); //コントローラーの入力情報

            //入力が最大の時
            if (velocity.magnitude >= 1.0f)
            {
                MoveDirection(velocity);              //向きを決定する
            }

        }

        velocity.y += Physics.gravity.y * Time.deltaTime;   //下に重力を掛ける

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
                //if (dir == moveTemp) rotTimelimit = 0.0f;
                //else rotTimelimit = 1.0f;

                moveState = MoveState.Move;              //ステートを変える
                StartCoroutine(Move(vec[i], rot[i]));    //移動させる
                //moveTemp = dir;
            }
        }
    }

    /// <summary>
    /// Rayを飛ばして壁があれば「true」無ければ「false」
    /// </summary>
    /// <returns></returns>
    public bool RaycastHitCheck()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if(Physics.Raycast(ray,out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }         
            return false;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    /// <param name="moveVec">プレイヤーの方向ベクトル</param>
    /// <param name="rotVec">プレイヤーの回転角度</param>
    /// <returns></returns>
    IEnumerator Move(Vector3 moveVec,Vector3 rotVec)
    {
        Quaternion temp = transform.localRotation;  //プレイヤーの回転角度を記憶
        Vector3 tempPos = transform.localPosition;  //プレイヤーの座標を記憶

        //プレイヤーを回転
        float timer = 0.0f;
        do
        {
            timer += Time.deltaTime * rotTimerSpeed;
            transform.localRotation = Quaternion.Lerp(temp, Quaternion.Euler(rotVec), timer);
            yield return null;
        }
        while (timer < rotTimelimit);

        //レイを飛ばして目の前に壁があるかチェック
        if (RaycastHitCheck())
        {
            moveState = MoveState.Stop;                      //ステータスを「停止状態」へ
            velocity = new Vector3(0, 0, 0);
            yield break;
        }

        //プレイヤーの移動
        //for(int i = 0; i < 10; i++)
        //{
        //    transform.position = Vector3.Lerp(tempPos, tempPos + moveVec, (float)i / 10);
        //    yield return new WaitForSeconds(0.1f);
        //}
        timer = 0.0f;
        do
        {
            timer += Time.deltaTime * moveTimerSpeed;
            transform.position = Vector3.Lerp(tempPos, tempPos + moveVec, timer);
            yield return null;
        }
        while (timer < 1.0f);

        moveState = MoveState.Stop;                      //ステータスを「停止状態」へ
        velocity = new Vector3(0, 0, 0);
        yield break;
    }
}
