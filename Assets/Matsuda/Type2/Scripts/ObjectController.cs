using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    [Tooltip("MapManager"),SerializeField]
    GameObject _mapManager;

    [SerializeField]
    GameObject _target = null;

    NavMeshAgent _navAgent = null;
    
    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Tower");
        if (_target != null)
        {
            _navAgent.destination = _target.transform.position;
        }
    }

    void Update()
    {
        /*if(_mapManager.***********){
        NaviUpdate();
        _mapManager.******* = false;
        }*/
    }

    void NaviUpdate()
    {
        // 次の位置への方向を求める
        var dir = _navAgent.nextPosition - transform.position;

        // 方向と現在の前方との角度を計算（スムーズに回転するように係数を掛ける）
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        var angle = Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized)) * Mathf.Rad2Deg * smooth;

        // 回転軸を計算
        var axis = Vector3.Cross(transform.forward, dir);

        // 回転の更新
        var rot = Quaternion.AngleAxis(angle, axis);
        transform.forward = rot * transform.forward;

        // 位置の更新
        transform.position = _navAgent.nextPosition;
    }

}
