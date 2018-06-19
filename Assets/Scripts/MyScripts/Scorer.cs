using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer {
    static private Scorer _instance;
    private int _score;
    private Scorer() { }
    static public Scorer getInstance() {
        if (_instance == null) {
            _instance = new Scorer();
            _instance._score = 0;
        }
        return _instance;
    }

    public void addScore(int x) {
        _score += x;
    }
    public int getScore() {
        return _score;
    }
    public void clear() {
        _score = 0;
    }
}
