using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 目標に向かってうろうろするだけ
public class AI_Sample01 : MonoBehaviour {

	// AIの移動(目標地点の発生)範囲
	public float levelSize = 10f;
	// 移動速度
	public float speed = 3f;

	// 目標地点の座標
	private Vector3 targetPosition;
	public  GameObject targetPosition01;
	public  GameObject targetPosition02;

	// 目標地点の感知範囲
	public float changeTargetSqrDistance = 10f;

	private int posFlag = 1;

	// ナビゲーションエージェント
	NavMeshAgent agent;

	private void Start()
	{	
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = speed;
		targetPosition = GetRandomPositionOnLevel();
	}

	private void Update()
	{	
		wander ();

		agent.destination = targetPosition;
	
	}

	// プレイヤーを見つけるまで適当に徘徊
	public void wander(){
		// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);

		if (sqrDistanceToTarget < changeTargetSqrDistance)
		{
			targetPosition = GetRandomPositionOnLevel();
		}
	}
		
	// ランダム移動時の目標地点を定める
	public Vector3 GetRandomPositionOnLevel()
	{

		if (posFlag == 2) {
			posFlag = 1;
			return new Vector3 (targetPosition02.transform.position.x, targetPosition02.transform.position.y, targetPosition02.transform.position.z);
		}
		else if(posFlag == 1){
			posFlag = 2;
			return new Vector3 (targetPosition01.transform.position.x, targetPosition01.transform.position.y, targetPosition02.transform.position.z);
		}

		return new Vector3(0,0,0);
	}

}
