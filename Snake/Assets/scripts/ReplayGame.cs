using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayGame : MonoBehaviour {
    /// <summary>
    /// If player clicks replay button, he'll go to 
    /// game scene.
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
