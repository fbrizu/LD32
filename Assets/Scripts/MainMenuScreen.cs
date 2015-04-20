using UnityEngine;
using System.Collections;

public class MainMenuScreen : MonoBehaviour {
	bool _canChangeScreen = false;

	// Use this for initialization
	void Start () {
		_canChangeScreen = false;
		Invoke("CanChangeScreen", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		if(_canChangeScreen && Input.GetKeyDown(KeyCode.Return)) {
			Application.LoadLevel("hoopyTest");
		}
	}

	void CanChangeScreen() {
		_canChangeScreen = true;
	}
}
