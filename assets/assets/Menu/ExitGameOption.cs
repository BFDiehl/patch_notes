using UnityEngine;
using System.Collections;

public class ExitGameOption : MenuOption {

	public override void execute() {
		Debug.Log("You have selected the Exit Game button!");
		Application.Quit();
	}
}
