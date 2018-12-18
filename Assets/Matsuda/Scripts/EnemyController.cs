using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの移動処理
/// </summary>
public class EnemyController : MonoBehaviour {

    Rigidbody _rb;

    #region//タワーに向かって移動する処理
    //移動するかしないか
    [SerializeField]
    bool _move = true;

    //移動開始時のポジション
    Vector3 _startPos;

    //今の時間
    float _timeCount = 0;

    [Tooltip("向かう先"),SerializeField]
    GameObject Tower;

    //向かう先のポジション
    Vector3 _towerPos;

    [Tooltip("移動速度"),SerializeField]
    float EnemySpeed;
    #endregion

    [Tooltip("壁"),SerializeField]
    GameObject Wall;

    void Start () {
        _rb = GetComponent<Rigidbody>();
        _startPos = transform.position;

        //Towerのポジション取得
        _towerPos = Tower.transform.position;
	}

    void Update() {
        gameObject.transform.LookAt(Tower.transform);//Towerの方向に向く

        if (_move){
            EnemyMove();//移動
        }
    }

    /// <summary>
    /// //移動処理
    /// </summary>
    void EnemyMove()
    {
        _timeCount += Time.deltaTime;

        _rb.MovePosition(
            new Vector3(
            Mathf.Lerp(_startPos.x, _towerPos.x, _timeCount * EnemySpeed),//X軸
            _startPos.y,//Y軸
            Mathf.Lerp(_startPos.z, _towerPos.z, _timeCount * EnemySpeed)));//Z軸
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        switch(collision.gameObject.tag){

            //Wallのタグ
            case "Wall":
                Destroy(gameObject);
                break;

            //Towerのタグ
            case "Tower":
                //止まる
                _move = false;
                break;

            default:
                break;
        }
    }

}
