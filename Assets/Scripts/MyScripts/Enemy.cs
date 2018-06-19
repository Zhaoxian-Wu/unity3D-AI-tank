using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Tank {//npc坦克
	public delegate void EnemyDestroy(GameObject Tank);
	public static event EnemyDestroy EnemyDestroyEvent;//npc坦克被销毁之后，可通知工厂回收
	
	private Vector3 target;//目标，即玩家坦克的位置

	private bool gameover;//游戏是否结束，决定是否继续运动或射击

	void Start() {
        Init();
	}

	void Update () {
		gameover = GameDirector.GetInstance().CurrentSceneController.IsGameOver();
		if (!gameover) {
			target = GameDirector.GetInstance().CurrentSceneController.GetPlayerPos();		
			if (GetHp() <= 0 && EnemyDestroyEvent != null) {//如果npc坦克被摧毁，则回收它
				EnemyDestroyEvent(this.gameObject);
			} else {//否则向玩家坦克移动
				NavMeshAgent agent = GetComponent<NavMeshAgent>();
				agent.SetDestination(target);
			}
		} else {//游戏结束，停止寻路
			NavMeshAgent agent = GetComponent<NavMeshAgent>();
			agent.velocity = Vector3.zero;
			agent.ResetPath();
		}
		
	}

    public void Init() {
        gameover = false;
        SetHp(100f);//设置初始生命值为100	
        StartCoroutine(Shoot());//开始射击的协程
    }

	IEnumerator Shoot() {//协程实现npc坦克每隔1s进行射击
		while(!gameover) {
			for (float i = 1; i > 0; i -= Time.deltaTime) {
				yield return 0;	
			}
			if (Vector3.Distance(transform.position, target) < 20) {//和玩家坦克距离小于20，则射击
				MyFactory mF = Singleton<MyFactory>.Instance;
				GameObject bullet = mF.GetBullet(tankType.Enemy);//获取子弹，传入的参数表示发射子弹的坦克类型
				bullet.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z) +
					transform.forward * 1.5f;//设置子弹
				bullet.transform.forward = transform.forward;//设置子弹方向
				Rigidbody rb = bullet.GetComponent<Rigidbody>();
				rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);//发射子弹
			}
		}
	}
}
