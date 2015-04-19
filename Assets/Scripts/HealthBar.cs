using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Body _player;
	public float _fillSpeed;
	Image _HPbar;
	float _currentHealth;

	void Start () {
		_HPbar = GetComponent<Image> ();
	}
	
	void Update () {
		_currentHealth = (float)_player._currentHealth / _player._maxHealth;
		if (_HPbar.fillAmount != _currentHealth) {
			_HPbar.fillAmount = Mathf.Lerp(_HPbar.fillAmount, _currentHealth, Time.deltaTime*_fillSpeed);
		}
	}
}
