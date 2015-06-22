using UnityEngine;
using System.Collections;

public class StartGameOption : MenuOption {

	public override void execute() {
		Debug.Log ("You have selected the Start option!");
		Application.LoadLevel("PlayArena");
	}
}
