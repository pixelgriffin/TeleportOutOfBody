using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneNow : MonoBehaviour {

    public string level;

	void Start () {
        Invoke("LoadScene", 0.1f);
	}

    void LoadScene()
    {
        SceneManager.LoadScene(level);
    }
}
