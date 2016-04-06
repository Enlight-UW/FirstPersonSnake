using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	void Update()
    {
        if (Input.anyKeyDown || Cardboard.SDK.Triggered)
        {
            EditorSceneManager.LoadScene(1);
        }
        
    }
}
