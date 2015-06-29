using UnityEngine;
using System.Collections.Generic;

public class MenuScreen : MonoBehaviour {

	public GameObject[] MenuOptions;
	public GameObject Indicator;
	public float IndicatorMovementSpeed;

	private List<MenuOption> menuOptionScripts = new List<MenuOption>();
	private int currentOptionIndex = 0;

	private float leftStickThreshold = 0.5f;

	private bool shouldDelay = false;
	public float DelayBetweenSelection;
	private float currentDelayTime;

	// Use this for initialization
	void Start () {
		foreach (GameObject option in MenuOptions) {
			menuOptionScripts.Add(option.GetComponent<MenuOption>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		checkInput();
		Indicator.transform.position = Vector3.Lerp (Indicator.transform.position,
		                                             menuOptionScripts[currentOptionIndex].SelectionIndicator.position,
		                                             IndicatorMovementSpeed);
	}

	public void checkInput() {
		InputManager.PlayerInput playerInput = InputManager.getInstance().getPlayerInput(PlayerScript.PlayerNumber.PLAYER_ONE);
		Debug.Log(playerInput.getLeftStickVertical());
		float leftStickVertical = playerInput.getLeftStickVertical();
		bool aButtonPressed = playerInput.getAButtonDown();

		if(shouldDelay) {
			currentDelayTime += Time.deltaTime;
		}
		if(currentDelayTime >= DelayBetweenSelection || 
		   (leftStickVertical < leftStickThreshold && 
		    leftStickVertical > -leftStickThreshold)) {
			shouldDelay = false;
			currentDelayTime = 0;
		} else if(!shouldDelay && leftStickVertical > leftStickThreshold) {
			shouldDelay = true;
			currentOptionIndex++;
			if(currentOptionIndex >= menuOptionScripts.Count) {
				currentOptionIndex = 0;
			}
		} else if (!shouldDelay && leftStickVertical < -leftStickThreshold) {
			shouldDelay = true;
			currentOptionIndex--;
			if(currentOptionIndex < 0) {
				currentOptionIndex = menuOptionScripts.Count - 1;
			}
		}
		
		if(aButtonPressed) {
			menuOptionScripts[currentOptionIndex].execute();
		}
	}
}
