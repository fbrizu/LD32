using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageColorTransition : MonoBehaviour {
	public Color[] colors;
	public float transitionTime;
	public bool colorsChanging;

	private Image img;
	private int currentColor, nextColor;
	private float timer;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image>();

		if(colors.Length > 0) 
		{			
			currentColor = 0;

			if(colors.Length > 1)
				nextColor = 1;
		}
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if(colorsChanging)
		{
			if(colors.Length > 1)
			{
				img.color = Color.Lerp(colors[currentColor], colors[nextColor], Mathf.Clamp(timer / transitionTime, 0, 1));
			}

			if(timer > transitionTime)
			{
				timer = timer % transitionTime;
				currentColor = nextColor;
				nextColor = (nextColor + 1 < colors.Length) ? nextColor + 1 : 0;
			}
		}
	}
}