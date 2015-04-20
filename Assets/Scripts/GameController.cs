using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public bool GameOver = false;
	public GameObject playerOne;
	public GameObject playerTwo;

	public GameObject _gameOverPanel;
	public Text _winText;

	// Use this for initialization
	void Start () {
		GameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void EndGame(int winnerID) {
		GameOver = true;
		ShowEndGameScreen(winnerID);
	}

	private void ShowEndGameScreen(int winnerID) {
		_gameOverPanel.SetActive(true);
		_winText.text = "Player " + winnerID + " wins!";
	}

	public void ShowCreditsScreen() {
		Application.LoadLevel("Credits");
	}

	public void PlayAgain() {
		Application.LoadLevel("hoopyTest");
	}

	public void ShowMainMenu() {
		Application.LoadLevel("MainMenu");
	}
}
