using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFood : MonoBehaviour {
    /// <summary>
    /// waitTime is responsible for how long special food
    /// will be visible normally on the scene,
    /// and waitShortTime is responsible for how fast
    /// special food will sparkle before it will 
    /// disappear.
    /// </summary>
    [SerializeField]
    float waitTime = 5.0f;
    [SerializeField]
    float waitShortTime = 1.0f;

    /// <summary>
    /// When special food is created 
    /// we start coroutine responsible for being visible.
    /// </summary>
    void Start()
    {
        StartCoroutine(Sparkle());
    }


    /// <summary>
    /// If head tagged as Player will make a collision with 
    /// normal food we increment points by one, 
    /// set up currentNormalFood as false so we can spawn 
    /// new one,
    /// we destroy food object and 
    /// add tile to snake's tail. 
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        FindObjectOfType<PointsCounter>().IncrementPoints(10);
        Destroy(gameObject);
        FindObjectOfType<FoodGenerator>().currentSpecialFood = false;
        FindObjectOfType<Snake>().AddTile();
    }

    /// <summary>
    /// Special food visibility time is defined by
    /// waitTime seconds, later it starts blinking
    /// until be destroyed or eaten by snake.
    /// </summary>
    /// <returns></returns>
    IEnumerator Sparkle()
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(waitShortTime);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(waitShortTime);
        }
        Destroy(gameObject);
        FindObjectOfType<FoodGenerator>().currentSpecialFood = false;
        yield break;
    }




}
