using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

// プレイヤーを発見するまでランダムに目標座標を定めて移動し続ける
// プレイヤーを感知したら追いかける
public class EnemyAI : MonoBehaviour {

	// 移動速度
	public float walkSpeed = 2f;
	public float runSpeed = 5f;

	// 目標地点の座標
	private Vector3 targetPosition;

	// プレイヤーの座標
	private Vector3 playerPosition;

	// 目標地点のタグの名前 (ランダム移動時) 
	public string targetTagName;

	// 目標地点の感知範囲
	public float changeTargetDistance = 10f;

	// プレイヤーの感知範囲 (視覚)
	public float findPlayerViewingDistance = 10f;

	// ナビゲーションエージェント
	NavMeshAgent agent;

	// アニメーター
	Animator animator;

	// rayを使って視覚を表現
	Camera camera;
	RaycastHit hit;
	Ray ray;
	Vector3 center;
	private bool playerFound;

	// 発見後の追跡時間
	public float followTime = 3.0f;
	private float mfollowTime;

	// 発見時の音
	public AudioClip findSoundClip;
	AudioSource findSoundSource;


	private void Start()
	{	
		playerFound = false;
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = walkSpeed;
		targetPosition = GetRandomPositionOnLevel();
		camera = GetComponentInChildren<Camera> ();
		center = new Vector3(Screen.width/2, Screen.height/2);
		animator = GetComponentInChildren<Animator> ();
		findSoundSource = gameObject.GetComponent<AudioSource> ();
		findSoundSource.clip = findSoundClip;
	}

	private void Update()
	{	
		animator.SetFloat ("Speed", agent.velocity.magnitude);
		// プレイヤーを発見していないときは徘徊行動をとる
		// (同じAIとの衝突を回避する)
		if (playerFound == false) {
			wander ();
		}

		// AIの視覚
		AIEye ();

		// プレイヤーが存在し,プレイヤーを見つけたら追いかける
		if(GameObject.FindWithTag ("Player") && playerFound == true){
			foundPlayer ();
		}

		Debug.Log (playerFound);
		agent.destination = targetPosition;
	}

	// プレイヤーを見つけるまで適当に徘徊
	public void wander(){
		// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);

		if (sqrDistanceToTarget < changeTargetDistance)
		{
			targetPosition = GetRandomPositionOnLevel();
		}
	}

	// AIの視界
	public void AIEye(){
		// AIの目線カメラ
		ray = camera.ScreenPointToRay(center);
		if (Physics.Raycast (ray, out hit, findPlayerViewingDistance)) {
			if (hit.collider.tag == "Player") {
				mfollowTime = followTime;
				playerFound = true;
			} 
			// 追跡状態
			else if (playerFound == true) {
				mfollowTime -=Time.deltaTime;
				agent.speed = runSpeed;
				if (!findSoundSource.isPlaying) {
					findSoundSource.Play ();
				}
				// 追跡時間終了
				if (mfollowTime < 0) {
					playerFound = false;
					agent.speed = walkSpeed;
					targetPosition = GetRandomPositionOnLevel();
					findSoundSource.Stop ();
				}
			}
		}

		Debug.DrawRay (
			transform.position,
			transform.TransformDirection(Vector3.forward)*findPlayerViewingDistance,
			Color.red
		);
	}

	// プレイヤーを見つけたら追いかける
	public void foundPlayer(){
		targetPosition = GameObject.FindWithTag ("Player").transform.position;
	}

	// ランダム移動時の目標地点を定める
	public Vector3 GetRandomPositionOnLevel()
	{

		/**** targerTagNameのしていがされていない時の例外処理を入れること!! ****/

		int targetLength = GameObject.FindGameObjectsWithTag (targetTagName).Length;
		float targetX = GameObject.FindGameObjectsWithTag (targetTagName) [Random.Range (0, targetLength-1)].transform.position.x;
		float targetY = 0;
		float targetZ = GameObject.FindGameObjectsWithTag (targetTagName) [Random.Range (0, targetLength-1)].transform.position.z;
		return new Vector3 (targetX, targetY, targetZ);
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			SceneManager.LoadScene ("Lose");
		}
	}

}
