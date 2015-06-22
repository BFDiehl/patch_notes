using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	public class PlayerInput {
		private string leftStickHorizontal;		//X axis
		private string leftStickVertical;		//Y axis
		private string triggers;				//3rd axis (+ = left - = right)
		private string rightStickHorizontal;	//4th axis
		private string rightStickVertical;		//5th axis
		private string dPadHorizontal;			//6th axis
		private string dPadVertical;			//7th axis
		
		private string aButton;					//Button 0
		private string bBButton;				//Button 1
		private string xButton;					//Button 2
		private string yButton;					//Button 3
		private string leftBumper;				//Button 4
		private string rightBumper;				//Button 5
		private string backButton;				//Button 6
		private string startButton;				//Button 7
		private string l3;						//Button 8
		private string r3;						//Button 9
		
		public PlayerInput(PlayerScript.PlayerNumber playerNum) {
			leftStickHorizontal = 	"Left Stick Horizontal P" + playerNum.GetHashCode();
			leftStickVertical =   	"Left Stick Vertical P" + playerNum.GetHashCode();
			triggers = 				"Triggers P" + playerNum.GetHashCode();
			rightStickHorizontal = 	"Right Stick Horizontal P" + playerNum.GetHashCode();
			rightStickVertical = 	"Right Stick Vertical P" + playerNum.GetHashCode();
			dPadHorizontal = 		"D Pad Horizontal P" + playerNum.GetHashCode();
			dPadVertical = 			"D Pad Vertical P" + playerNum.GetHashCode();
			
			aButton = 				"A Button P" + playerNum.GetHashCode();
			bBButton = 				"B Button P" + playerNum.GetHashCode();
			xButton = 				"X Button P" + playerNum.GetHashCode();
			yButton = 				"Y Button P" + playerNum.GetHashCode();
			leftBumper = 			"Left Bumper P" + playerNum.GetHashCode();
			rightBumper = 			"Right Bumper P" + playerNum.GetHashCode();
			backButton = 			"Back Button P" + playerNum.GetHashCode();
			startButton = 			"Start Button P" + playerNum.GetHashCode();
			l3 = 					"L3 P" + playerNum.GetHashCode();
			r3 = 					"R3 P" + playerNum.GetHashCode();
		}
		
		public float getLeftStickHoriztonal() {
			return Input.GetAxis(leftStickHorizontal);
		}
		
		public float getLeftStickVertical() {
			return Input.GetAxis(leftStickVertical);
		}
		
		public float getTriggers() {
			return Input.GetAxis(triggers);
		}
		
		public float getRightStickHoriztonal() {
			return Input.GetAxis(rightStickHorizontal);
		}
		
		public float getRightStickVertical() {
			return Input.GetAxis(rightStickVertical);
		}
		
		public float getDPadHorizontal() {
			return Input.GetAxis(dPadHorizontal);
		}
		
		public float getDPadVertical() {
			return Input.GetAxis(dPadVertical);
		}
		
		public bool getAButton() {
			return Input.GetButton(aButton);
		}

		public bool getAButtonDown() {
			return Input.GetButtonDown(aButton);
		}
		
		public bool getBButton() {
			return Input.GetButton(bBButton);
		}
		
		public bool getXButton() {
			return Input.GetButton(xButton);
		}
		
		public bool getYButton() {
			return Input.GetButton(yButton);
		}
		
		public bool getLeftBumper() {
			return Input.GetButton(leftBumper);
		}
		
		public bool getRightBumper() {
			return Input.GetButton(rightBumper);
		}
		
		public bool getBackButton() {
			return Input.GetButton(backButton);
		}
		
		public bool getStartButton() {
			return Input.GetButton(startButton);
		}
		
		public bool getL3() {
			return Input.GetButton(l3);
		}
		
		public bool getR3() {
			return Input.GetButton(r3);
		}
	}

	private static InputManager instance;
	private Dictionary<PlayerScript.PlayerNumber, PlayerInput> playerInputs = new Dictionary<PlayerScript.PlayerNumber, PlayerInput>();

	public static InputManager getInstance() {
		return instance;
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Starting input manager");
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(this);

		playerInputs.Add(PlayerScript.PlayerNumber.PLAYER_ONE, new PlayerInput(PlayerScript.PlayerNumber.PLAYER_ONE));
		playerInputs.Add(PlayerScript.PlayerNumber.PLAYER_TWO, new PlayerInput(PlayerScript.PlayerNumber.PLAYER_TWO));
		playerInputs.Add(PlayerScript.PlayerNumber.PLAYER_THREE, new PlayerInput(PlayerScript.PlayerNumber.PLAYER_THREE));
		playerInputs.Add(PlayerScript.PlayerNumber.PLAYER_FOUR, new PlayerInput(PlayerScript.PlayerNumber.PLAYER_FOUR));
	}

	public PlayerInput getPlayerInput(PlayerScript.PlayerNumber playerNum) {
		PlayerInput retVal = null;
		playerInputs.TryGetValue(playerNum, out retVal);
		return retVal;
	}
	

}
