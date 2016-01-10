using UnityEngine;
using System.Collections;

public class SizeDownPatch : Patch {
    public override void execute(PlayerScript.PlayerNumber targetPlayer) {
        PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
        player.SizeDownPatch();
    }

    public override string getName() {
        return "Size Down";
    }
}