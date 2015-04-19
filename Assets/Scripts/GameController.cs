using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public bool GameOver = false;

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

	private void ShowCreditsScreen() {

	}
}
