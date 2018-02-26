using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for showing points on 
/// game over scene.
/// It takes points from "current_points" and
/// transfer them to text in UI.
/// </summary>
public class GameOverScene : MonoBehaviour {

    // Field responsible for scores on game over scene scene.
    [SerializeField]
    Text Score;

    // At start it refreshes points.
    void Start ()
    {
        RefreshPoints();
    }

    // "current_points" are set as 0 by default
    int GetCurrentPoints()
    {
        return PlayerPrefs.GetInt("current_points", 0);
    }

    void RefreshPoints()
    {
        var points = GetCurrentPoints();
        if (points == 1)
        {
            Score.text = points + " point!";
        }
        else
            Score.text = points + " points!";
    }
}
