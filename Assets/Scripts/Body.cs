using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {
	public int id;
	Animator _animator;
	public float _jumpTime = 2.0f;
	public float _jumpDistance = 1.0f;
	public int _currentHealth = 5; 
	public int _maxHealth = 5; 
	public float _bendSpeed;
	Vector3 _targetPosition;
	bool _isMoving = false;
	KeyCode _leftArrow;
	KeyCode _rightArrow;
	bool _canTakeDamage = true;
	public float _recoveryTime = 2f;
	KeyCode _tiltLeft;
	KeyCode _tiltRight;
	float _tilt;

	void Start () {
		_tilt = 0.5f;
		_animator = this.GetComponent<Animator>();
		_animator.Play("wobblyDude_bend", 0, 0.5f);
		_animator.speed = 0.0f;
		if (id == 1) {
			_leftArrow = KeyCode.A;
			_rightArrow = KeyCode.D;
			_tiltLeft = KeyCode.Q;
			_tiltRight = KeyCode.E;
		} else {
			//Player two has reversed directions because it is facing the opposite way
			_leftArrow = KeyCode.L;
			_rightArrow = KeyCode.J;
			_tiltLeft = KeyCode.O;
			_tiltRight = KeyCode.U;
		}

		_canTakeDamage = true;
	}
	
	void Update () {
		if (Input.GetKey (_tiltLeft) && !_isMoving) {
			_tilt = Mathf.Min(1, _tilt + _bendSpeed);
			_animator.Play ("wobblyDude_bend", 0, _tilt);
			_animator.speed = 0f;
		} else if (!_animator.GetBool ("isJumping") && Input.GetKey (_tiltRight)) {
			_tilt = Mathf.Max (0, _tilt - _bendSpeed);
			_animator.Play ("wobblyDude_bend", 0, _tilt);
			_animator.speed = 0f;
		}
		if (Input.GetKey (_tiltRight) && !_isMoving) {
			_tilt = Mathf.Max (0, _tilt - _bendSpeed);
		}
		if(!_animator.GetBool("isJumping")){
			_animator.Play("wobblyDude_bend", 0, _tilt);
			_animator.speed = 0.0f;
		}

	if(Input.GetKey(_rightArrow) && !_isMoving){
			_targetPosition = this.transform.position + this.transform.right * _jumpDistance * -1;
			_animator.SetBool("isJumping", true);
			_animator.speed = 1.0f;
			_isMoving = true;
			_tilt = 0.5f;
		}
		else if(Input.GetKey(_leftArrow) && !_isMoving){
			_targetPosition = this.transform.position + this.transform.right * _jumpDistance;
			_animator.SetBool("isJumping", true);
			_animator.speed = 1.0f;
			_isMoving = true;
			_tilt = 0.5f;
		}
		if(_animator.GetCurrentAnimatorStateInfo(0).IsName("wobblyDude_jump_loop")){
			this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, _jumpTime * Time.deltaTime);
		}
		else if(_animator.GetCurrentAnimatorStateInfo(0).IsName("wobblyDude_jump_exit")){
			_animator.SetBool("isJumping", false);
			_animator.speed = 1.0f;
			_isMoving = false;
		}

		if(_currentHealth <= 0) {
			GameController gameController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
			gameController.GameOver = true;
			gameController.EndGame((id+1)==2? 2 : 1);
		}
	}

	public void TakeDamage(int damage) {
		if(_canTakeDamage) {
			_currentHealth -= damage;
			_canTakeDamage = false;
			Invoke("AllowDamageTaken", _recoveryTime);
		}
	}

	private void AllowDamageTaken() {
		_canTakeDamage = true;
	}
}