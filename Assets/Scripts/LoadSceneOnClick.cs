using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour {

	public string sceneToLoadOnKey;
	// Update is called once per frame
	void Update () {
		if (Input.anyKey)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene( sceneToLoadOnKey );
		}
	}
}
