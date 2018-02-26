using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour {
    /// <summary>
    /// If player clicks quit button, he'll go to 
    /// main menu scene.
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
        SceneManager.LoadScene("mainMenu");
    }
}
