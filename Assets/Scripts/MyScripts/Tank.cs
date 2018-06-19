using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : MonoBehaviour {
	private float hp; //生命值

	public float GetHp() {
		return hp;
	}

	public void SetHp(float hp) {
		this.hp = hp; 
	}

}

