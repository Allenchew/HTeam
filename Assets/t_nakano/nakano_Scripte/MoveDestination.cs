// MoveDestination.cs
using UnityEngine.AI;
using UnityEngine;


public class MoveDestination : MonoBehaviour {

	public Transform goal;

	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position; 
	}

	void Update(){
		// 次の位置への方向を求める
		var dir = _agent.nextPosition - transform.position;

		// 方向と現在の前方との角度を計算（スムーズに回転するように係数を掛ける）
		float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
		var angle = Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized)) * Mathf.Rad2Deg * smooth;

		// 回転軸を計算
		var axis = Vector3.Cross(transform.forward, dir);

		// 回転の更新
		var rot = Quaternion.AngleAxis(angle, axis);
		transform.forward = rot * transform.forward;

		// 位置の更新
		transform.position = _agent.nextPosition;
	}
}