using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerBar : MonoBehaviour {

	public Hoop _playerHoop;
	public int _framesPerColour;
	Image _bar;
	float _currentPower;
	int _colourID;
	Color[] _colours;
	int _currentFrameCount;


	void Start () {
		_bar = GetComponent<Image> ();
		_colourID = 0;
		_colours = new Color[] { Color.cyan, Color.red, Color.green, Color.magenta, Color.yellow, Color.blue} ;
		_currentFrameCount = _framesPerColour;
	}
	void Update () {
		//Update bar
		_currentPower = (float) _playerHoop._powerPressCount / _playerHoop._totalPowerCount;
		if (_bar.fillAmount != _currentPower) {
			_bar.fillAmount = _currentPower;
		}
		//Make bar all flashy if it's full
		if (_bar.fillAmount == 1) {
			_currentFrameCount = (_currentFrameCount - 1) % _framesPerColour;
			if (_currentFrameCount == 0) {
				_currentFrameCount = _framesPerColour;
				_bar.color=_colours[_colourID];
				_colourID = (_colourID+1) % _colours.Length;
			}
		}
	}
}
