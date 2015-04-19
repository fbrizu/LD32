using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static bool GameOver = false;

	// Use this for initialization
	void Start () {
		GameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndGame() {
		GameOver = true;
		StartCreditsScreen();
	}

	public void StartCreditsScreen() {
		
	}
}
