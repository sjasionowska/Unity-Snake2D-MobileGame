using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public void OnClick(string name)
    {
        SceneManager.LoadScene(name);
    }
    
}
