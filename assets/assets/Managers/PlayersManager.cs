using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersManager : MonoBehaviour {
	private static PlayersManager instance;
	
	private ArrayList playerList = new ArrayList();
	private Dictionary<PlayerScript.PlayerNumber, PlayerScript> playerScripts;
	private GameObject playerObject;

	private PlayersManager() {}

	public static PlayersManager getInstance() {
		return instance;
	}

	public ArrayList getPlayerList() {
		return playerList;
	}

	public PlayerScript getPlayerScript(PlayerScript.PlayerNumber playerNumber) {
		PlayerScript retVal = null;
		playerScripts.TryGetValue(playerNumber, out retVal);
		return retVal;
	}

	void Start() {
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this);
		instance = this;

		playerObject = (GameObject)Resources.Load("Player/Player", typeof(GameObject));
		playerList.Add(PlayerScript.PlayerNumber.PLAYER_ONE);
		playerList.Add(PlayerScript.PlayerNumber.PLAYER_TWO);
		playerList.Add(PlayerScript.PlayerNumber.PLAYER_THREE);
	}

	void OnLevelWasLoaded(int level) {
		playerScripts = new Dictionary<PlayerScript.PlayerNumber, PlayerScript>();
		if (level != 0) {
			foreach(PlayerScript.PlayerNumber playerNum in this.playerList) {
				GameObject newPlayer = (GameObject)Instantiate(playerObject);
				PlayerScript newPlayerScript = newPlayer.GetComponent<PlayerScript>();
				newPlayerScript.init(playerNum);
				playerScripts.Add(playerNum, newPlayerScript);
			}
		}
	}
}