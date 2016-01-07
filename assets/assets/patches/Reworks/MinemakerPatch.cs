﻿using UnityEngine;
using System.Collections;

public class Minemaker : Patch {
 public override void execute(PlayerScript.PlayerNumber targetPlayer) {
     PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
     player.MinemakerPatch();
 }
}