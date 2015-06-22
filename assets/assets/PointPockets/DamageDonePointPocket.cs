using UnityEngine;
using System.Collections;

public class DamageDonePointPocket : PointPocket {

	public override PlayerScript.PlayerNumber getWinner() {
		PlayerScript.PlayerNumber winner = PlayerScript.PlayerNumber.NONE;
		bool winnerSelected = false;
		int currentHighestDamage = 0;
		ArrayList playerList = PlayersManager.getInstance().getPlayerList();

		foreach (PlayerScript.PlayerNumber playerNumber in playerList) {
			PlayerScript player = PlayersManager.getInstance().getPlayerScript(playerNumber);
			int playerDamageDone = player.getDamageDone();
			Debug.Log("Player " + playerNumber.GetHashCode() + " dealt " + playerDamageDone);

			if(winnerSelected && playerDamageDone == currentHighestDamage) {
				winner = PlayerScript.PlayerNumber.NONE;
			} else if(playerDamageDone > currentHighestDamage) {
				winner = player.getPlayerNum();
				currentHighestDamage = playerDamageDone;
				winnerSelected = true;
			}
		}

		return winner;
	}

	public override string getName() {
		return "Damage Done Winner: ";
	}

}