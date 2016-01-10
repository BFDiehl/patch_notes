using UnityEngine;
using System.Collections;

public class BulletSizeUpPatch : Patch {
    public override void execute(PlayerScript.PlayerNumber targetPlayer) {
        PlayerScript player = PlayersManager.getInstance().getPlayerScript(targetPlayer);
        player.BulletSizeUpPatch();
    }

    public override string getName() {
        return "Bullet Size Up";
    }
}