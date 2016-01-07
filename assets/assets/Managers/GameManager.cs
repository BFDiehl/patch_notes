﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
	public static GameManager getInstance() {
		return instance != null ? instance : new GameManager();
	}

	public int gameTimer;
	private bool inGame = false;
	private int gameTimeInSeconds;

	private int currentPatchTime = 0;
	private int timeBetweenPatches;
	private ArrayList buffPatches = new ArrayList();
	private ArrayList nerfPatches = new ArrayList();
	private ArrayList reworkPatches = new ArrayList();

	private ArrayList pointPockets = new ArrayList();

	private GameManager() {}
	
	void OnGUI() {
		if(inGame) {
        	GUI.Label(new Rect(10, 10, 100, 20), (gameTimeInSeconds/60).ToString("0") + ":" + (gameTimeInSeconds % 60).ToString("00"));
    	}
    }
	
	void Start() {
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this);
		instance = this;

		pointPockets.Add(new DamageDonePointPocket());
        pointPockets.Add(new KillsPointPocket());
        pointPockets.Add(new BulletsDestroyedPointPocket());
        pointPockets.Add(new BulletDamageDonePointPocket());
	}

	void OnLevelWasLoaded(int level) {
		inGame = false;

		if (level != 0) {
			inGame = true;
			gameTimeInSeconds = gameTimer * 60;
			timeBetweenPatches = 10;
			StartCoroutine(countdown());
		}
	}

	public void setGameTimeInSeconds(int gameTimeInSeconds) {
		this.gameTimeInSeconds = gameTimeInSeconds;
	}

	public void setTimeBetweenPatches(int timeBetweenPatches) {
	    this.timeBetweenPatches = timeBetweenPatches;
	}

    private IEnumerator countdown() {
        while (gameTimeInSeconds > 0) {
            yield return new WaitForSeconds(1f);
            gameTimeInSeconds--;
            currentPatchTime++;

            if (currentPatchTime >= timeBetweenPatches) {
                applyPatch();
            }
        }
        calculateScores();
        Application.LoadLevel(0);
    }

    private void applyPatch() {
        ArrayList playerList = PlayersManager.getInstance().getPlayerList();

        foreach (PlayerScript.PlayerNumber playerNumber in playerList) {
            int num = Random.Range(1,100);
            ArrayList patches;
            if (num <= 15) {
                patches = reworkPatches;
            } else if (num <= 55) {
                patches = buffPatches;
            } else {
                patches = nerfPatches;
            }
            Debug.Log("Applying patch to " + playerNumber);
            //patches[Random.Range(0,(patches.length - 1))].execute(playerNumber);
        }
        currentPatchTime = 0;
    }

    private void calculateScores() {
    	foreach(PointPocket pointPocket in pointPockets) {
    		Debug.Log(pointPocket.getName() + pointPocket.getWinner());
    	}
    }
	
}
