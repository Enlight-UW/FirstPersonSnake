using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	void Update()
    {
        if (Input.anyKeyDown)
        {
            EditorSceneManager.LoadScene(1);
        }
        
    }
}
