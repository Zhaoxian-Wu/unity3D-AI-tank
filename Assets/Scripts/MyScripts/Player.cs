using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Tank {//玩家坦克
	public delegate void destroy();
	public static event destroy DestroyEvent;

    private readonly Vector3 originPos = new Vector3(27, 0, -4);

    void Start() {
        TankStart();
	}
	void Update () {
		if (GetHp() <= 0 ) {//生命值<=0,表示玩家坦克被摧毁
            Camera.main.transform.parent = null;
			this.gameObject.SetActive(false);
			if (DestroyEvent != null) {//执行委托事件
				DestroyEvent();
			}
		}
	}
    public void TankStart() {
        Camera.main.transform.parent = gameObject.transform;
        gameObject.transform.position = originPos;
        gameObject.SetActive(true);
        SetHp(500f);//设置初始生命值为500
    }
}

