using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboBarScrollView : MonoBehaviour {

	public HoopSpinner _playerHoop;
	public Text _text;
	RectTransform _bar;
	float _currentPower;
	
	
	void Start () {
		_bar = GetComponent<RectTransform> ();
	}
	void Update () {
		//Update bar
		_currentPower = (float) _playerHoop._comboCount / _playerHoop._maxCombo;
		_bar.sizeDelta = new Vector2 (_currentPower*150.0f, 30.0f);

		if ( _currentPower == 1) {
			_text.text = _text.text.Replace("POWER", "OVERDRIVE!");
		}
	}

}
