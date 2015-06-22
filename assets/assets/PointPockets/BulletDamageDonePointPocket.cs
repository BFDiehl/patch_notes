using UnityEngine;
using System.Collections;

public class BulletDamageDonePointPocket : PointPocket {

	public override PlayerScript.PlayerNumber getWinner() {
		PlayerScript.PlayerNumber winner = PlayerScript.PlayerNumber.NONE;
		bool winnerSelected = false;
		int currentHighestBulletDamageDone = 0;
		ArrayList playerList = PlayersManager.getInstance().getPlayerList();

		foreach (PlayerScript.PlayerNumber playerNumber in playerList) {
			PlayerScript player = PlayersManager.getInstance().getPlayerScript(playerNumber);
			int bulletDamageDone = player.getBulletDamageDone();
			Debug.Log("Player " + playerNumber.GetHashCode() + " dealt " + bulletDamageDone + " bullet damage");

			if(winnerSelected && bulletDamageDone == currentHighestBulletDamageDone) {
				winner = PlayerScript.PlayerNumber.NONE;
			} else if(bulletDamageDone > currentHighestBulletDamageDone) {
				winner = player.getPlayerNum();
				currentHighestBulletDamageDone = bulletDamageDone;
				winnerSelected = true;
			}
		}
		return winner;
	}

	public override string getName() {
		return "Bullet Damage Done Winner: ";
	}
}