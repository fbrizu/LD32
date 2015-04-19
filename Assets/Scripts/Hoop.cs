using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hoop : MonoBehaviour {

	public int id;
	public GameObject[] _characterJoints;
	public float _currentSpeed;
	public float _speedUpSpeed;
	public float _slowDownSpeed;
	public float _maxSpeed;
	public float _lerpSmoothing = 0.02f;
	public Text _status;
	public GameObject _boing;
	public int _currentJointIndex;
	public static bool _timeToTwist;
	public static bool _canPowerUp;
	public int _powerPressCount;
	public int _maxPowerCount;
	public int _comboCount;
	public int _maxCombo;
	public Mesh _basicHoop;
	public Mesh _powerHoop1;
	public Mesh _powerHoop2;
	public SkinnedMeshRenderer _hoopMesh;
	public bool _isComboActive = false;
	public bool _isPowerUpActive = false;
	bool _canTwist;
	bool _power1On;
	bool _power2On;
	KeyCode doTheTwist;
	float _currentAngle;
	float _timeCount;
	float _lastTimePressed;
	float _comboMultiplier = 1.5f;
	float _powerUpMultiplier = 2f;

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
		if (id == 1) {
			doTheTwist = KeyCode.W;
		} else {
			doTheTwist = KeyCode.I;
		}
		_hoopMesh.sharedMesh = _basicHoop;
	}
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Hoop") {
			if (other.GetComponent<Hoop> ()._currentJointIndex < _currentJointIndex) {
				//Push the other player's hoop down
				if (other.GetComponent<Hoop> ()._currentJointIndex - 1 >= 0) {
					Debug.Log ("Denied!");
					other.GetComponent<Hoop> ()._currentJointIndex = other.GetComponent<Hoop> ()._currentJointIndex - 1;
				}
			} else if (other.GetComponent<Hoop>()._currentJointIndex == _currentJointIndex && id==1) {
				Vector3 collisionPoint = GetPointOfContact();
				if (!collisionPoint.Equals(new Vector3(-99,-99,-99))) {
					GameObject prefabBoing = Instantiate (_boing) as GameObject;
					prefabBoing.transform.position = collisionPoint;
					Destroy (prefabBoing, 1f);
				}
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if(other.tag == "Player" && other.GetComponent<Body>().id != id) {
			int powerUpMult = _isPowerUpActive ? 1 : 0;
			int comboMult = _isComboActive ? 1 : 0;
			other.GetComponent<Body>().TakeDamage(_currentJointIndex + comboMult * _comboMultiplier + powerUpMult * _powerUpMultiplier);
		}
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
					_power1On = false;
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
				if (_powerPressCount == _maxPowerCount && !_power2On) {
					_power1On = true;
				}
			}
		}
		//Update hoop 
		Debug.Log (_power1On + ", " + _power2On);
		if (_power2On) {
			Debug.Log ("CHANGING TO POWER2 HOOP");
			_hoopMesh.sharedMesh = _powerHoop2;
		} else if (!_power2On && _power1On) { 
			_hoopMesh.sharedMesh = _powerHoop1;
			Debug.Log ("CHANGING TO POWER1 HOOP");
		} else if (!_power1On && !_power2On && _hoopMesh.sharedMesh != _basicHoop) {
			_hoopMesh.sharedMesh = _basicHoop;
			Debug.Log ("CHANGING TO NORMAL HOOP");
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
		this.transform.position = Vector3.Lerp (this.transform.position, _characterJoints [_currentJointIndex].transform.position, _lerpSmoothing);
		this.transform.rotation = _characterJoints [_currentJointIndex].transform.rotation;
		this.transform.Rotate (0, 0, 90, Space.Self);
		this.transform.Rotate (0, _currentAngle, 0, Space.Self);

	}

	Vector3 GetPointOfContact()	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit) && hit.transform.tag.Equals("Hoop")) { 
			return hit.point;
		} else if (Physics.Raycast (transform.position, -1f*transform.forward, out hit) && hit.transform.tag.Equals("Hoop")) {
			return hit.point;
		} else {
			return new Vector3(-99, -99, -99);
		}
	}
}
