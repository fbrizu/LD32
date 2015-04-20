using UnityEngine;
using System.Collections;

public class BackgroundSroll : MonoBehaviour {

	public int speed;
	public int _range;
	RectTransform _image;

	void Start() {
		_image = GetComponent<RectTransform> ();
	}
	void Update () {
		_image.anchoredPosition = new Vector3((_image.anchoredPosition.x + speed)% _range, 0, 0);
	}
}
