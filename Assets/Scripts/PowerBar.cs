using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerBar : MonoBehaviour {

	public Hoop _playerHoop;
	Image _bar;
	float _currentPower;

	void Start () {
		_bar = GetComponent<Image> ();
	}
	void Update () {
		_currentPower = (float) _playerHoop._powerPressCount / _playerHoop._totalPowerCount;
		if (_bar.fillAmount != _currentPower) {
			_bar.fillAmount = _currentPower;
		}
	}
}
