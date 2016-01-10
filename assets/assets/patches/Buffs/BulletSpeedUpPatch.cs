using UnityEngine;
using System.Collections;

public class BulletSpeedUpPatch : Patch {
    public override void execute(PlayerScript.PlayerNumber targetPlayer) {
        PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
        player.BulletSpeedUpPatch();
    }

    public override string getName() {
        return "Bullet Speed Up";
    }
}