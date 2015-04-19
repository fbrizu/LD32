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
	public Text status;
	public GameObject boing;
	public int _currentJointIndex;
	public static bool _canTwist;
	KeyCode doTheTwist;
	float _currentAngle;
	float _timeCount;
	float _lastTimePressed;
	int _powerPressCount;

	void Start () {
		_currentJointIndex = 0;
		_currentAngle = 0;
		_timeCount = 0;
		_lastTimePressed = 0;
		_powerPressCount = 0;
		if (id == 1) {
			doTheTwist = KeyCode.W;
		} else {
			doTheTwist = KeyCode.I;
		}
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
					GameObject prefabBoing = Instantiate (boing) as GameObject;
					prefabBoing.transform.position = collisionPoint;
					Destroy (prefabBoing, 1f);
				}
			}
		}
		else if(other.tag == "Player" && other.GetComponent<Body>().id != id) {
			other.GetComponent<Body>().TakeDamage(1);
		}
	}
	void Update () {
		_timeCount = _timeCount + Time.deltaTime;
		if (_timeCount < 0.75f && _timeCount > 0.25f) {
			status.text = "GO";
			status.color = Color.red;
			_canTwist = true;
			if (_timeCount<0.60f && _timeCount>0.40f) {

			}
		} else {
			status.text = "no go";
			status.color = Color.gray;
			_canTwist = false;
		}
		_currentAngle = (_currentAngle + (Time.deltaTime * _currentSpeed)) % 360f;
		
		if (Input.GetKeyDown(doTheTwist)) {
			_lastTimePressed = Time.time;
			if (_canTwist) {
				//Increase speed 
				_currentSpeed = Mathf.Min (_maxSpeed, Mathf.Max(_currentSpeed*_speedUpSpeed, 500));
				if (_currentSpeed == _maxSpeed && _currentJointIndex+1 < _characterJoints.Length) {
					_currentJointIndex++;
				}
			} else {
				//Else slow down 
				_currentSpeed = _currentSpeed/2f;
				if (_currentJointIndex-1 >=0) {
					_currentJointIndex--;
				}
			}
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
		if (Physics.Raycast (transform.position, transform.forward, out hit)) { 
			return hit.point;
		} else if (Physics.Raycast (transform.position, -1f*transform.forward, out hit)) {
			return hit.point;
		} else {
			return new Vector3(-99, -99, -99);
		}
	}
}
