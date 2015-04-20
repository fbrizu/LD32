using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoopSpinner : MonoBehaviour {

	public int _id;
	public GameObject[] _characterJoints;
	public float _currentSpeed;
	public float _speedUpSpeed;
	public float _slowDownSpeed;
	public float _maxSpeed;
	public float _lerpSmoothing = 0.02f;
	public Text _status;
	public int _currentJointIndex;
	public static bool _timeToTwist;
	public static bool _canPowerUp;
	public int _powerPressCount;
	public int _maxPowerCount;
	public int _comboCount;
	public int _maxCombo;
	public GameObject _basicHoop;
	public GameObject _powerHoop1;
	public GameObject _powerHoop2;
	public bool _isComboActive = false;
	public bool _isPowerUpActive = false;
	public float _comboMultiplier = 1.5f;
	public float _powerUpMultiplier = 2f;
	public bool _power1On;
	public bool _power2On;
	bool _canTwist;
	KeyCode doTheTwist;
	float _currentAngle;
	float _timeCount;
	float _lastTimePressed;
	GameObject _currentHoop;
	Transform _currentHoopJoint;

	void Start () {
		_canTwist = true;
		_currentJointIndex = 0;
		_currentAngle = 0;
		_timeCount = 0;
		_lastTimePressed = 0;
		_powerPressCount = 0;
		_comboCount = 0;
		_power1On = false;
		_power2On = false;
		if (_id == 1) {
			doTheTwist = KeyCode.W;
		} else {
			doTheTwist = KeyCode.I;
		}
		_currentHoop = _basicHoop;
		_currentHoopJoint = _currentHoop.transform.FindChild ("joint_hoop1");
		_powerHoop1.SetActive (false);
		_powerHoop2.SetActive (false);
	}
	void Update () {
		_timeCount = _timeCount + Time.deltaTime;
		if (_timeCount < 0.75f && _timeCount > 0.25f) {
			_status.text = "GO";
			_status.color = Color.red;
			_timeToTwist = true;
		} else {
			_status.text = "no go";
			_status.color = Color.gray;
			_timeToTwist = false;
			_canTwist = true;
		}
		if (_timeCount < 0.55f && _timeCount > 0.45f) {
			_canPowerUp = true;
		} else {
			_canPowerUp = false;
		}
		_currentAngle = (_currentAngle + (Time.deltaTime * _currentSpeed)) % 360f;
		
		if (Input.GetKeyDown(doTheTwist)) {
			_lastTimePressed = Time.time;
			if (_timeToTwist && _canTwist) {
				//Increase speed 
				_currentSpeed = Mathf.Min (_maxSpeed, Mathf.Max(_currentSpeed*_speedUpSpeed, 500));
				if (_currentSpeed == _maxSpeed && _currentJointIndex+1 < _characterJoints.Length) {
					_currentJointIndex++;
				}
				_canTwist = false;
				if (_comboCount < _maxCombo) {
					_comboCount++;
				}
				if (_comboCount == _maxCombo) {
					_power2On = true;
				}
			} else if(!_timeToTwist) {
				//Else slow down 
				_currentSpeed = _currentSpeed/2f;
				if (_currentJointIndex-1 >=0) {
					_currentJointIndex--;
				}
				//Break combo!
				_comboCount=0;
				if (_power2On) {
					_power2On = false;
				}
				if (_powerPressCount == _maxPowerCount) {
					//Set power 1 on again if it was set to false because of power 2 previously
					_power1On = true;
				}
			}
			if (_canPowerUp) {
				_powerPressCount = Mathf.Clamp(_powerPressCount+1, 0, _maxPowerCount);
				if (_powerPressCount == _maxPowerCount) {
					_power1On = true;
				}
			}
		}
		//Update hoop 
		Debug.Log (_power1On + ", " + _power2On);
		if (_power1On && _currentHoop!=_powerHoop1) {
			Debug.Log ("CHANGING TO POWER1 HOOP");
			_basicHoop.SetActive(false);
			_powerHoop1.SetActive(true);
			_powerHoop2.SetActive(false);
			_currentHoop = _powerHoop1;
			_currentHoopJoint = _currentHoop.transform.FindChild ("joint_hoop1");
		} else if (!_power1On && _power2On && _currentHoop!=_powerHoop2) { 
			Debug.Log ("CHANGING TO POWER2 HOOP");
			_basicHoop.SetActive(false);
			_powerHoop1.SetActive(false);
			_powerHoop2.SetActive(true);
			_currentHoop = _powerHoop2;
			_currentHoopJoint = _currentHoop.transform.FindChild ("joint_hoop1");
		} else if (!_power1On && !_power2On && _currentHoop!=_basicHoop) {
			Debug.Log ("CHANGING TO NORMAL HOOP");
			_basicHoop.SetActive(true);
			_powerHoop1.SetActive(false);
			_powerHoop2.SetActive(false);
			_currentHoop = _basicHoop;
			_currentHoopJoint = _currentHoop.transform.FindChild ("joint_hoop1");
		}
		_currentSpeed = _currentSpeed * _slowDownSpeed;
		
		//If no actions for 2 sec, slow down hoop speed
		if (_timeCount > 1f) {
			_timeCount = _timeCount % 1;

		}
		//If player doesn't press in 2 seconds, hoop falls down by 1 joint
		if (Time.time - _lastTimePressed > 2f) {
			if (_currentJointIndex-1 >=0) {
				_currentJointIndex--;
			}
			_lastTimePressed = Time.time;
		}
		//If hoop speed is below 250, fall 
		if (_currentSpeed < 250) {
			if (_currentJointIndex-1 >=0) {
				_currentJointIndex--;
			}
		}
		_currentHoopJoint.transform.position = Vector3.Lerp (_currentHoopJoint.transform.position, _characterJoints [_currentJointIndex].transform.position, _lerpSmoothing);
		_currentHoopJoint.transform.rotation = _characterJoints [_currentJointIndex].transform.rotation;
		_currentHoopJoint.transform.Rotate (0, 0, 90, Space.Self);
		_currentHoopJoint.transform.Rotate (0, _currentAngle, 0, Space.Self);

	}
}
