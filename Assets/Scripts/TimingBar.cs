using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimingBar : MonoBehaviour {
	float _currentX;
	Image _knob;

	void Start () {
		transform.localPosition = new Vector3(0,0,0);
		_knob = transform.GetComponent<Image> ();
		_knob.color = Color.white;
	}
	void Update () {
		_currentX = Mathf.PingPong (Time.time, 1) * 100;
		transform.localPosition = new Vector3 (_currentX, 5, 0);
		if (Hoop._canTwist) {
			_knob.color = Color.red;
		} else {
			_knob.color = Color.white;
		}
	}
}