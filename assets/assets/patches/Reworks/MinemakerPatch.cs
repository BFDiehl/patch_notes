using UnityEngine;
using System.Collections;

public class MinemakerPatch : Patch {
    public override void execute(PlayerScript.PlayerNumber targetPlayer) {
        PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
        player.MinemakerPatch();
    }

    public override string getName() {
        return "Minemaker";
    }
}