using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {
    /// <summary>
    /// Class responsible for play button on 
    /// main menu scene. 
    /// If player clicks the button, he goes to 
    /// game scene and start the game.
    /// </summary>
    [SerializeField]
    Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("game");
    }

}
