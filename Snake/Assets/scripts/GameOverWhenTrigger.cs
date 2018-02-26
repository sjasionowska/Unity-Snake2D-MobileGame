using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWhenTrigger : MonoBehaviour {

    /// <summary>
    /// If snake's head will make a collision with tail 
    /// or borders, game ends and player sees game over scene.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            SceneManager.LoadScene("gameOver");
        }
    }
}
