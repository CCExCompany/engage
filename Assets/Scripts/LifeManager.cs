using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

	public int startingLives;
	private int lifeCounter;

	private Text theText;
	void Start () {
		theText = GetComponent<Text>();

		lifeCounter = startingLives;
	}
	void Update () {
		theText.text = "x " + lifeCounter;
	}

	public void GiveLife()
	{
		lifeCounter++;
	}

	public void TakeLife()
	{
		LifeCounter--;
	}
}

