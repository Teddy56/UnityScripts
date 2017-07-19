using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/***************************************
 * ・感知範囲内にプレイヤーが入るとプレイヤーを追いかける (敵キャラ)
 *   speedsensingRange
 * 
****************************************/

public class Enemy_PlayerFollow : MonoBehaviour {

	// 移動速度
	public float speed = 3f;

	// プレイヤーの座標
	private Vector3 playerPosition;
	// 感知範囲
	public float sensingRange = 30f;
	// プレイヤーと敵の距離 (Distance of player and enemy)
	float distanceOfPandE;

	// ナビゲーションエージェント
	NavMeshAgent agent;

	private void Start()
	{	
		// アタッチされたをNavMeshAgent取得
		agent = GetComponent<NavMeshAgent> ();
		// 移動速度を設定
		agent.speed = speed;
	}

	private void Update()
	{	
		Sensing();
	}

	// プレイヤーが感知範囲に入ると追いかけ始める
	public void Sensing(){
		// プレイヤーの位置を取得
		playerPosition = GameObject.FindWithTag ("Player").transform.position;
		// プレイヤーと敵の距離を取得
		distanceOfPandE = Vector3.SqrMagnitude(transform.position - playerPosition);

		// プレイヤーと敵の距離が感知範囲より小さくなったら追いかける
		if (distanceOfPandE < sensingRange)
		{
			agent.destination = playerPosition;		
		}
	}

}
