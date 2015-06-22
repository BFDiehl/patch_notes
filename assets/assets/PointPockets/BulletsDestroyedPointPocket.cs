using UnityEngine;
using System.Collections;

public class BulletsDestroyedPointPocket : PointPocket {

	public override PlayerScript.PlayerNumber getWinner() {
		PlayerScript.PlayerNumber winner = PlayerScript.PlayerNumber.NONE;
		bool winnerSelected = false;
		int currentHighestBulletsDestroyed = 0;
		ArrayList playerList = PlayersManager.getInstance().getPlayerList();

		foreach (PlayerScript.PlayerNumber playerNumber in playerList) {
			PlayerScript player = PlayersManager.getInstance().getPlayerScript(playerNumber);
			int playerBulletsDestroyed = player.getBulletsDestroyed();
			Debug.Log("Player " + playerNumber.GetHashCode() + " destroyed " + playerBulletsDestroyed + " bullets");

			if(winnerSelected && playerBulletsDestroyed == currentHighestBulletsDestroyed) {
				winner = PlayerScript.PlayerNumber.NONE;
			} else if(playerBulletsDestroyed > currentHighestBulletsDestroyed) {
				winner = player.getPlayerNum();
				currentHighestBulletsDestroyed = playerBulletsDestroyed;
				winnerSelected = true;
			}
		}
		return winner;
	}

	public override string getName() {
		return "Bulelts Destroyed Winner: ";
	}
}