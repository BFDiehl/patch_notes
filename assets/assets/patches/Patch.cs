using UnityEngine;
using System.Collections;

public abstract class Patch {
	public abstract void execute(PlayerScript.PlayerNumber targetPlayer);
	public abstract string getName();
}
