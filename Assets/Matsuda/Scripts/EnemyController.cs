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
    //動いているならtrue
    [SerializeField]
    bool _move = false;

    [Tooltip("向かう先"), SerializeField]
    GameObject _tower;

    //タワーのポジション
    Vector3 _towerPos;

    [Tooltip("移動速度(整数)"), SerializeField]
    float _enemySpeed;
    #endregion


    #region//壁を避ける処理
    //当たっている壁
    GameObject _hitWall;

    [Tooltip("レイの長さ"), SerializeField]
    float _rayLength;

    [Tooltip("遅延時間"), SerializeField]
    float _delayTime;

    [Tooltip("壁に当たっているか(F)"), SerializeField]
    bool _wallF;
    [Tooltip("壁に当たっているか(R)"), SerializeField]
    bool _wallR;
    [Tooltip("壁に当たっているか(L)"), SerializeField]
    bool _wallL;
    #endregion


    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _towerPos = _tower.transform.position;//Towerのポジション取得

        //Towerの方向に向く
        transform.LookAt(_towerPos);

        //タワーに向かって移動
        StartCoroutine(EnemyMove());
    }


    void Update()
    {
        if(!_move)
        {
        StartCoroutine(EnemyMove());
        }
    }


    void WallCheck()
    {
        //レイの準備
        Ray _rayForward;
        Ray _rayRight;
        Ray _rayLeft;
        RaycastHit _hitForward;
        RaycastHit _hitRight;
        RaycastHit _hitLeft;

        //タワーの方向にレイを飛ばす
        _rayForward = new Ray(transform.position, transform.forward);//new Ray(始め,方向)
        _rayRight = new Ray(transform.position, transform.right);
        _rayLeft = new Ray(transform.position, -transform.right);
        Debug.DrawRay(_rayForward.origin, _rayForward.direction, Color.red, 3.0f);
        Debug.DrawRay(_rayRight.origin, _rayRight.direction, Color.red, 3.0f);
        Debug.DrawRay(_rayLeft.origin, _rayLeft.direction, Color.red, 3.0f);

        if (Physics.Raycast(_rayForward, out _hitForward, _rayLength))
        {//Physics.Raycast(判定するレイ,ヒットしたもの,長さ))
            if (_hitForward.transform.tag == "Wall")
            {//レイがWallのタグに当たっている場合
                _hitWall = _hitForward.collider.gameObject;
                _wallF = true;
            }
        }
        else
        {
            _wallF = false;
        }

        if (Physics.Raycast(_rayRight, out _hitRight, _rayLength))
        {//Physics.Raycast(判定するレイ,ヒットしたもの,長さ))
            if (_hitRight.transform.tag == "Wall")
            {//レイがWallのタグに当たっている場合
                _wallR = true;
            }
        }
        else
        {
            _wallR = false;
        }

        if (Physics.Raycast(_rayLeft, out _hitLeft, _rayLength))
        {//Physics.Raycast(判定するレイ,ヒットしたもの,長さ))
            if (_hitLeft.transform.tag == "Wall")
            {//レイがWallのタグに当たっている場合
                _wallL = true;
            }
        }
        else
        {
            _wallL = false;
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
        _move = true;

        while (!_wallF)
        {
            _rb.MovePosition(Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * _enemySpeed));
            WallCheck();

            yield return null;
        }

        //向きを変える
        transform.Rotate(new Vector3(0, _hitWall.gameObject.transform.rotation.y +90, 0));

        float a = (_hitWall.transform.localScale.x / 2 
            + (_rayLength * Mathf.Tan(Mathf.PI / 6)) + 0.4f) 
            / _enemySpeed;

        Debug.Log(_rayLength * Mathf.Tan(Mathf.PI / 6));
        _delayTime = a;

        WallCheck();
        

        while (_wallL)
        {
            float _timeCount = 0;

            while (_delayTime > _timeCount)
            {
                _timeCount += Time.deltaTime;
                _rb.MovePosition(Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * _enemySpeed));
                
                yield return null;
            }

            //向きを変える
            transform.Rotate(new Vector3(0, _hitWall.gameObject.transform.rotation.y -60, 0));

            _timeCount = 0;

            while (_delayTime > _timeCount)
            {
                _timeCount += Time.deltaTime;

                _rb.MovePosition(Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * _enemySpeed));

                yield return null;
            }

            WallCheck();
        }

        //Towerの方向に向く
        transform.LookAt(_towerPos);

        _move = false;
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
                Destroy(gameObject);

                //
                //攻撃のアニメーション
                //

                break;

            default:
                break;
        }
    }

}
