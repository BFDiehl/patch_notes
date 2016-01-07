using UnityEngine;
using System.Collections;

public class RateOfFireUpPatch : Patch {
 public override void execute(PlayerScript.PlayerNumber targetPlayer) {
     PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
     player.RateOfFireUpPatch();
 }
}