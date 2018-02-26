using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for borders.
/// As default height is set by 15f 
/// and width by 10f.
/// borderWidth variable is used 
/// for widening borders so they can be more visible.
/// 
/// At the beginning it generates borders on specific positions
/// Borders are scaled using height and width variables.
/// </summary>
public class LevelGenerator : MonoBehaviour {

    public float height = 15f;

    public float width = 10f;

    [SerializeField]
    GameObject[] Borders;
    [SerializeField]
    float borderWidth = 2f;

   void Start ()
    {
        GenerateBorders();
	}

    void GenerateBorders()
    {
        var borderBottom = Borders[0];
        var borderTop = Borders[1];
        var borderLeft = Borders[2];
        var borderRight = Borders[3];

        var border = Instantiate(borderBottom);
        border.transform.localScale = new Vector2(width, borderWidth);
        border.transform.position = Vector2.down * height / 2;

        border = Instantiate(borderTop);
        border.transform.localScale = new Vector2(width, borderWidth);
        border.transform.position = Vector2.up * height / 2;

        border = Instantiate(borderLeft);
        border.transform.localScale = new Vector2(borderWidth, height);
        border.transform.position = Vector2.left * width / 2;

        border = Instantiate(borderRight);
        border.transform.localScale = new Vector2(borderWidth, height);
        border.transform.position = Vector2.right * width / 2;
    }


}
