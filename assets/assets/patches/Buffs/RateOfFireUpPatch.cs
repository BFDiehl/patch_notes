using UnityEngine;
using System.Collections;

public class RateOfFireUpPatch : Patch {
    public override void execute(PlayerScript.PlayerNumber targetPlayer) {
        PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
        player.RateOfFireUpPatch();
    }

    public override string getName() {
        return "Rate Of Fire Up";
    }
}