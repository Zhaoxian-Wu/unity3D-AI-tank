using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUserGUI : MonoBehaviour {
	IUserAction action;
	// Use this for initialization
	void Start () {
		action = GameDirector.GetInstance().CurrentSceneController as IUserAction;	
	}
	
	// Update is called once per frame
	void Update () {
		if (!action.IsGameOver()) {
			if (Input.GetKey(KeyCode.W)) {
				action.MoveForward();
			}
			
			if (Input.GetKey(KeyCode.S)) {
				action.MoveBackWard();
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				action.Shoot();	
			}

			float offsetX = Input.GetAxis ("Horizontal");//获取水平轴上的增量，目的在于控制玩家坦克的转向
			action.Turn(offsetX);
		}
	}
}
