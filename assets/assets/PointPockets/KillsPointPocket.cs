using UnityEngine;
using System.Collections;

public class KillsPointPocket : PointPocket {

	public override PlayerScript.PlayerNumber getWinner() {
		PlayerScript.PlayerNumber winner = PlayerScript.PlayerNumber.NONE;
		bool winnerSelected = false;
		int currentHighestKills = 0;
		ArrayList playerList = PlayersManager.getInstance().getPlayerList();

		foreach (PlayerScript.PlayerNumber playerNumber in playerList) {
			PlayerScript player = PlayersManager.getInstance().getPlayerScript(playerNumber);
			int playerKills = player.getKills();
			Debug.Log("Player " + playerNumber.GetHashCode() + " killed " + playerKills + " players");

			if(winnerSelected && playerKills == currentHighestKills) {
				winner = PlayerScript.PlayerNumber.NONE;
			} else if(playerKills > currentHighestKills) {
				winner = player.getPlayerNum();
				currentHighestKills = playerKills;
				winnerSelected = true;
			}
		}
		return winner;
	}

	public override string getName() {
		return "Kills Winner: ";
	}
}