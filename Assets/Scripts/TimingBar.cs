using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimingBar : MonoBehaviour {
	float _currentX;
	Image _knob;
	float initialTime;

	void Start () {
		transform.localPosition = new Vector3(0,0,0);
		_knob = transform.GetComponent<Image> ();
		_knob.color = Color.white;
		initialTime = Time.time;
	}
	void Update () {
		_currentX = Mathf.PingPong (Time.time - initialTime, 1) * 100;
		transform.localPosition = new Vector3 (_currentX, 5, 0);
		if (HoopSpinner._timeToTwist) {
			_knob.color = Color.red;
		} else {
			_knob.color = Color.white;
		}
	}
}