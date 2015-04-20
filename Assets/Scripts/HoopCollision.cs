using UnityEngine;
using System.Collections;

public class HoopCollision : MonoBehaviour {

	public HoopSpinner _myHoop;
	public GameObject _boing;
	int _id;

	// Use this for initialization
	void Start () {
		_id = _myHoop._id;
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Hoop") {
//			Debug.Log("My ("+_id+") joint index " +  _myHoop._currentJointIndex + ", other ("+ other.transform.parent.parent.GetComponent<HoopSpinner>()._id+ "): " + other.transform.parent.parent.GetComponent<HoopSpinner>()._currentJointIndex);
			if (other.transform.parent.parent.GetComponent<HoopSpinner> ()._currentJointIndex < _myHoop._currentJointIndex) {
				//Push the other player's hoop down
				if (other.transform.parent.parent.GetComponent<HoopSpinner> ()._currentJointIndex - 1 >= 0) {
					Debug.Log ("Denied!");
					other.transform.parent.parent.GetComponent<HoopSpinner> ()._currentJointIndex = other.transform.parent.parent.GetComponent<HoopSpinner> ()._currentJointIndex - 1;
				}
			} else if (other.transform.parent.parent.GetComponent<HoopSpinner>()._currentJointIndex == _myHoop._currentJointIndex && _id == 1) {
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
//		Debug.Log (_id + " trigger stay hits " + other.tag);
		if(other.tag.Contains("P") && !other.tag.Contains(_id.ToString())) {
			Debug.Log ("Player " + _id + " collided with other: " + other.tag);
			int powerUpMult = _myHoop._isPowerUpActive ? 1 : 0;
			int comboMult = _myHoop._isComboActive ? 1 : 0;

			Transform myParent = other.transform.parent;
			while (!myParent.tag.Equals("Player")) {
				myParent = myParent.transform.parent;
			}
			myParent.GetComponent<Body>().TakeDamage(_myHoop._currentJointIndex + comboMult * _myHoop._comboMultiplier + powerUpMult * _myHoop._powerUpMultiplier);
		}
	}
	Vector3 GetPointOfContact()	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, new Vector3(1,0,0), out hit) && hit.transform.tag.Equals("Hoop")) { 
			return hit.point;
		} else if (Physics.Raycast (transform.position, new Vector3(-1,0,0), out hit) && hit.transform.tag.Equals("Hoop")) {
			return hit.point;
		} else {
			return new Vector3(-99, -99, -99);
		}
	}
}
