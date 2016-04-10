using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	void Update()
    {
        if (Input.anyKeyDown || Cardboard.SDK.Triggered)
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
