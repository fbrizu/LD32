using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerBar : MonoBehaviour {

	public Hoop _playerHoop;
	public int _framesPerColour;
	Image _bar;
	float _currentPower;


	void Start () {
		_bar = GetComponent<Image> ();
	}
	void Update () {
		//Update bar
		_currentPower = (float) _playerHoop._powerPressCount / _playerHoop._totalPowerCount;
		if (_bar.fillAmount != _currentPower) {
			_bar.fillAmount = _currentPower;
		}
		//Make bar all flashy if it's full
		if (_bar.fillAmount == 1) {
			GetComponent<ImageColorTransition>().colorsChanging = true;
		}
	}
}
