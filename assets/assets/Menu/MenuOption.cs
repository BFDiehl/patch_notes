using UnityEngine;
using System.Collections;

public abstract class MenuOption : MonoBehaviour {

	public Transform SelectionIndicator;

	public abstract void execute();
}
