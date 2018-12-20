using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 現在のフラグ管理
/// </summary>
public enum FlgState
{
    FIRST_STATE = -1,
    ALPHA_ZERO = 0,
    ALPHA_ONE = 1,
    END = 2,
}

/// <summary>
/// 壁の倒れる関連
/// </summary>
public class WallController : MonoBehaviour
{
    [Header("参照")]
    private Rigidbody rig;
    private PlayerController playerCon;
    private Image icon;

    [Header("重力関連")]
    private float torqueEorce = 150.0f;
    Vector3 rightVec;
    Vector3 torque;

    [Header("アイコン関連")]
    private FlgState flgState = FlgState.FIRST_STATE;
    private float moveTimer;
    private float alphaTimer;
    private float timerSpeed = 3.0f;
    private float reachingSpeed = 1.0f;
    private float reachingHeight = 3.0f;

    public FlgState flg
    {
        set { flgState = value; }
        get { return flgState; }
    }

    public float AlphaTimer
    {
        set { alphaTimer = value; }
        get { return alphaTimer; }
    }

    void Start ()
    {
        ///---初期化---///
		rig = GetComponent<Rigidbody>();
        playerCon = GameObject.FindObjectOfType<PlayerController>();
        icon = transform.Find("Canvas/IconImage").gameObject.GetComponent<Image>();

        rightVec = transform.right;         //倒れる方向
        torque = rightVec * torqueEorce;    //加える力
    }

    // Update is called once per frame
    void Update ()
    {
        IconAction();
	}
    public void IconAction()
    {
        switch (flgState)
        {
            case FlgState.ALPHA_ZERO:
                AlphaIcon((int)flgState);
                break;

            case FlgState.ALPHA_ONE:
                MoveIcon();
                AlphaIcon((int)flgState);
                break;

            case FlgState.END:
                AlphaIcon(0);
                break;
            default:
                break;

        }
    }
    public void MoveIcon()
    {
        moveTimer += Time.deltaTime;
        icon.transform.localPosition = new Vector3(0, Mathf.Sin(moveTimer * Mathf.PI / reachingSpeed) * reachingHeight, 0);
    }

    public void AlphaIcon(int num)
    {
        alphaTimer += Time.deltaTime * timerSpeed;

        switch (num)
        {
            case 0:
                if(icon.color.a <= 0.0f)
                {
                    alphaTimer = 0.0f;
                    return;
                }
                icon.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(1.0f, 0.0f, alphaTimer / 1.0f));
                break;
            case 1:
                if (icon.color.a >= 1.0f)
                {
                    alphaTimer = 0.0f;
                    return;
                }
                icon.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, alphaTimer / 1.0f));
                break;
            default:
                break;
        }
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
