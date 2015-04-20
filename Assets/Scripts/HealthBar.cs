using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Body _player;
	public float _fillSpeed;
	public float _healthBarSize;
	RectTransform _HPbar;
	float _currentHealth;

	void Start () {
		_HPbar = GetComponent<RectTransform> ();
	}
	
	void Update () {
		_currentHealth = (float)_player._currentHealth / _player._maxHealth;
		_HPbar.sizeDelta = new Vector2 (_currentHealth*_healthBarSize, 30.0f);
	}
}
