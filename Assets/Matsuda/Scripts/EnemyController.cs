using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの移動処理
/// </summary>
public class EnemyController : MonoBehaviour {

    Rigidbody _rb;

    #region//
    //移動開始時のポジション
    Vector3 _startPos;

    //今の時間
    float _timeCount = 0;

    [Tooltip("向かう先")]
    public GameObject Tower;

    //向かう先のポジション
    Vector3 _towerPos;

    [Tooltip("移動速度")]
    public float EnemySpeed;



	void Start () {
        _rb = GetComponent<Rigidbody>();
        _startPos = transform.position;

        //Towerのポジション取得
        _towerPos = Tower.transform.position;
	}

    void Update() {
        #region//移動処理
        _timeCount += Time.deltaTime;

        _rb.MovePosition(
            new Vector3(
            Mathf.Lerp(_startPos.x, _towerPos.x, _timeCount * EnemySpeed),//X軸
            _startPos.y,//Y軸
            Mathf.Lerp(_startPos.z, _towerPos.z, _timeCount * EnemySpeed)));//Z軸
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == Tower.tag)
        {
            Destroy(gameObject);
        }
    }

}
