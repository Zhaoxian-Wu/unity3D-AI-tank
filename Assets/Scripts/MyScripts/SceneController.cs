using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, IUserAction {

	public GameObject player;//玩家坦克
    public Text scoreBar;
    public Image HP;
    public Image MAX_HP;

	private bool gameOver = false;//游戏是否结束 

	private readonly int enemyConut = 6;//游戏的npc数量
	private MyFactory mF;//工厂
    private Scorer scorer;

    void Awake() {//一些初始的设置
		GameDirector director = GameDirector.GetInstance();
		director.CurrentSceneController = this;
		mF = Singleton<MyFactory>.Instance;
		player = mF.GetPlayer();
		Player.DestroyEvent += SetGameOver;//如果玩家坦克被摧毁，则设置游戏结束
        scorer = Scorer.getInstance();
        Enemy.EnemyDestroyEvent += DestoryEnemy;
	}
	void Start() {
        BeginGame();
	}

    private void Update() {
        scoreBar.text = "得分：" + scorer.getScore();
        HP.transform.localScale = new Vector3(
            Mathf.Clamp(player.GetComponent<Player>().GetHp() / 500, 0, 1), 
            1, 
            1
        );
    }

    public void BeginGame() {
        mF.RecycleAllTank();
        for (int i = 0; i < enemyConut; i++) {//获取npc坦克
            mF.GetTank();
        }
        player.GetComponent<Player>().TankStart();
        scorer.clear();
        gameOver = false;
    }

    void DestoryEnemy(GameObject t) {
        mF.GetTank();
        scorer.addScore(1);
    }

	public Vector3 GetPlayerPos() {//返回玩家坦克的位置
		return player.transform.position;
	}

	public bool IsGameOver() {//返回游戏是否结束
		return gameOver;
	}
	public void SetGameOver() {//设置游戏结束
		gameOver = true;
	}

	public void MoveForward() {
		player.GetComponent<Rigidbody>().velocity = player.transform.forward * 20;
	}
	public void MoveBackWard() {
		player.GetComponent<Rigidbody>().velocity = player.transform.forward * -20;
	}
	public void Turn(float offsetX) {//通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
		float x = player.transform.localEulerAngles.y + offsetX * 3;
        float y = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3 (y, x, 0);
	}	

	public void Shoot() {
		GameObject bullet = mF.GetBullet(tankType.Player);//获取子弹，传入的参数表示发出子弹的坦克类型
		bullet.transform.position = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z) +
			player.transform.forward * 1.5f;//设置子弹位置
		bullet.transform.forward = player.transform.forward;//设置子弹方向
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);//发射子弹
	}

}
