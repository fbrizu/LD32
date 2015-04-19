using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public bool GameOver = false;

	public GameObject _gameOverPanel;

	// Use this for initialization
	void Start () {
		GameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndGame() {
		GameOver = true;
		ShowEndGameScreen();
	}

	private void ShowEndGameScreen() {
		_gameOverPanel.SetActive(true);
	}

	private void ShowCreditsScreen() {

	}
}
