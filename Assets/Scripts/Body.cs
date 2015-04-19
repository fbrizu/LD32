using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {
	public int id;
	Animator _animator;
	public float _jumpTime = 2.0f;
	public float _jumpDistance = 1.0f;
	public int health = 5; 
	Vector3 _targetPosition;
	bool _isMoving = false;
	KeyCode _leftArrow;
	KeyCode _rightArrow;
	bool _canTakeDamage = true;
	public float _recoveryTime = 2f;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator>();
		_animator.Play("wobblyDude_bend", 0, 0.8f);
		_animator.speed = 0.0f;
		if (id == 1) {
			_leftArrow = KeyCode.A;
			_rightArrow = KeyCode.D;
		} else {
			//Player two has reversed directions because it is facing the opposite way
			_leftArrow = KeyCode.RightArrow;
			_rightArrow = KeyCode.LeftArrow;
		}

		_canTakeDamage = true;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(!_animator.GetBool("isJumping")){
			_animator.Play("wobblyDude_bend", 0, (ray.direction.x + 1.0f)/2);
			_animator.speed = 0.0f;
		}

		if(Input.GetKey(_rightArrow) && !_isMoving){
			_targetPosition = this.transform.position + this.transform.right * _jumpDistance * -1;
			_animator.SetBool("isJumping", true);
			_animator.speed = 1.0f;
			_isMoving = true;
		}
		else if(Input.GetKey(_leftArrow) && !_isMoving){
			_targetPosition = this.transform.position + this.transform.right * _jumpDistance;
			_animator.SetBool("isJumping", true);
			_animator.speed = 1.0f;
			_isMoving = true;
		}

		if(_animator.GetCurrentAnimatorStateInfo(0).IsName("wobblyDude_jump_loop")){
			this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, _jumpTime * Time.deltaTime);
		}
		else if(_animator.GetCurrentAnimatorStateInfo(0).IsName("wobblyDude_jump_exit")){
			_animator.SetBool("isJumping", false);
			_animator.speed = 0.0f;
			_isMoving = false;
		}

		if(health <= 0) {
			GameController.GameOver = true;
			Debug.Log("Player " + id + " lost!!");
		}
	}

	public void TakeDamage(int damage) {
		if(_canTakeDamage) {
			health -= damage;
			_canTakeDamage = false;
			Invoke("AllowDamageTaken", _recoveryTime);
			Debug.Log("Test");
		}
	}

	private void AllowDamageTaken() {
		_canTakeDamage = true;
	}
}