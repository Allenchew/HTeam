using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの移動処理
/// </summary>
public class EnemyController : MonoBehaviour
{

    Rigidbody _rb;

    #region//タワーに向かって移動する処理
    //侵攻しているならtrue
    [SerializeField]
    bool _move = true;

    bool sidemove = false;

    [Tooltip("向かう先"), SerializeField]
    GameObject _tower;

    //タワーのポジション
    Vector3 _towerPos;

    [Tooltip("移動速度(整数)"), SerializeField]
    float _enemySpeed;
    #endregion


    #region//壁を避ける処理
    [Tooltip("壁"), SerializeField]
    GameObject _wall;

    //当たっている壁
    GameObject _hitWall;

    [SerializeField]
    float _rayLength;
    
    #endregion


    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _towerPos = _tower.transform.position;//Towerのポジション取得

        transform.LookAt(_towerPos);//Towerの方向に向く
        StartCoroutine(EnemyMove());//タワーに向かって移動
    }


    void Update()
    {
        //レイの準備
        Ray _rayForward;
        Ray _rayRight;
        Ray _rayLeft;
        RaycastHit _hitForward;
        RaycastHit _hitRight;
        RaycastHit _hitLeft;


        if (_move == false)
        {
            _move = true;
            StartCoroutine(EnemyMove());
        }

        //タワーの方向にレイを飛ばす
        _rayForward = new Ray(transform.position, transform.forward);//new Ray(始め,方向)
        _rayRight = new Ray(transform.position, transform.right);
        _rayLeft = new Ray(transform.position, -transform.right);
        Debug.DrawRay(_rayForward.origin, _rayForward.direction, Color.red, 3.0f);
        Debug.DrawRay(_rayRight.origin, _rayRight.direction, Color.red, 3.0f);
        Debug.DrawRay(_rayLeft.origin, _rayLeft.direction, Color.red, 3.0f);



        if (Physics.Raycast(_rayForward, out _hitForward, _rayLength))
        {//Physics.Raycast(判定するレイ,ヒットしたもの,長さ))
            WallCheck(_rayForward, _hitForward);
        }

        if (Physics.Raycast(_rayRight, out _hitRight, _rayLength+0.2f))
        {
            WallCheck(_rayRight, _hitRight);
        }

        if (Physics.Raycast(_rayLeft, out _hitLeft, _rayLength+0.2f))
        {
            WallCheck(_rayLeft, _hitLeft);
        }

    }


    void WallCheck(Ray _ray, RaycastHit _hit)
    {
        if (_hit.transform.tag == "Wall")
        {//レイがWallのタグに当たっている場合

            if (_hitWall == null)
            {
                //当たった壁の情報を保持
                _hitWall = _hit.collider.gameObject;

                //止まる
                _move = false;

                //向きを変える
                transform.Rotate(new Vector3(0, 90, 0));
                
            }

            if (_hit.collider.name != _hitWall.name)
            {
                //当たった壁の情報を保持
                _hitWall = _hit.collider.gameObject;

                //止まる
                _move = false;

                //向きを変える
                transform.Rotate(new Vector3(0, -60, 0));
            }
        }
    }
    ///// <summary>
    ///// 移動処理
    ///// </summary>
    ///// <param name="_startPos">移動開始時のポジション</param>
    ///// <param name="_endPos">最終的なポジション</param>
    //void EnemyMove(Vector3 _startPos,Vector3 _endPos)
    //{
    //    _timeCount += Time.deltaTime;

    //    _rb.MovePosition(
    //        new Vector3(
    //        Mathf.Lerp(_startPos.x, _endPos.x, _timeCount * _enemySpeed),//X軸
    //        _startPos.y,//Y軸
    //        Mathf.Lerp(_startPos.z, _endPos.z, _timeCount * _enemySpeed)));//Z軸
    //}

    /// <summary>
    /// 正面に移動
    /// </summary>
    IEnumerator EnemyMove()
    {
        while (_move)
        {
            _rb.MovePosition(Vector3.Lerp(transform.position,transform.position + transform.forward,Time.deltaTime*_enemySpeed));

            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {

            //Wallのタグ
            case "_wall":
                break;


            //Towerのタグ
            case "Tower":
                //止まる
                _move = false;

                //
                //攻撃のアニメーション
                //

                break;

            default:
                break;
        }
    }

}
