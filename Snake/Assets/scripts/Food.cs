using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If head tagged as Player will make a collision with 
/// normal food we increment points by one, 
/// set up currentNormalFood as false so we can spawn 
/// new one,
/// we destroy food object and 
/// add tile to snake's tail. 
/// </summary>
/// <param name="col"></param>
public class Food : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            FindObjectOfType<PointsCounter>().IncrementPoints(1);
            FindObjectOfType<FoodGenerator>().currentNormalFood = false;
            Destroy(gameObject);
            FindObjectOfType<Snake>().AddTile();
        }

    }
}
