using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour {
    /// <summary>
    /// Class responsible for scores.
    /// As default player has 0 points.
    /// </summary>
    private int points = 0;

    /// <summary>
    /// At the beginning it saves points in "current_points"
    /// and then just refreshes them.
    /// </summary>
    void Start()
    {
        SavePoints();
        RefreshText();
    }

    /// <summary>
    /// Method used in Food and SpecialFood classes. Responsible for 
    /// incrementing points, saving this and refreshing text that shows points.
    /// </summary>
    /// <param name="points"></param>
    public void IncrementPoints(int points)
    {
        this.points += points;
        SavePoints();
        RefreshText();
    }

    /// <summary>
    /// Method that refreshes text each time score changes.
    /// </summary>
    void RefreshText()
    {
        if (points == 1)
        {
            GetComponent<Text>().text = points.ToString() + " point";
        }
        else
            GetComponent<Text>().text = points.ToString() + " points";

    }

    /// <summary>
    /// PlayerPrefs is responsible for settings for whole game. 
    /// We set here player's points as current_points so we can use it 
    /// on game and game over scenes.
    /// </summary>
    void SavePoints()
    {
        PlayerPrefs.SetInt("current_points", points);
    }
}
