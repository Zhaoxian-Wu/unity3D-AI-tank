using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {
    SceneController sc;

    private void Start() {
        GetComponent<Button>().onClick.AddListener(Restart);
        sc = GameDirector.GetInstance().CurrentSceneController;
    }

    private void Restart() {
        sc.BeginGame();
    }
}
