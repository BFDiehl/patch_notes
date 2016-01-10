using UnityEngine;
using System.Collections;

public class SpeedUpPatch : Patch {
    public override void execute(PlayerScript.PlayerNumber targetPlayer) {
        PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
        player.SpeedUpPatch();
    }

    public override string getName() {
        return "Speed Up";
    }
}