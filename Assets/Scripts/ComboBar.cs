using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboBar : MonoBehaviour {

	public HoopSpinner _playerHoop;
	public int _framesPerColour;
	public Text _text;
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
		_currentPower = (float) _playerHoop._comboCount / _playerHoop._maxCombo;
		if (_bar.fillAmount != _currentPower) {
			_bar.fillAmount = _currentPower;
		}
		//Make bar all flashy if it's full
		if (_bar.fillAmount == 1) {
			_playerHoop._isComboActive = true;
			_currentFrameCount = (_currentFrameCount - 1) % _framesPerColour;
			if (_currentFrameCount == 0) {
				_currentFrameCount = _framesPerColour;
				_bar.color=_colours[_colourID];
				_colourID = (_colourID+1) % _colours.Length;
			}
			_text.text = _text.text.Replace("POWER", "OVERDRIVE!");
		}
		else {
			_playerHoop._isComboActive = false;
		}
	}
}
