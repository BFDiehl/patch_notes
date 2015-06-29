using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
	public static GameManager getInstance() {
		return instance != null ? instance : new GameManager();
	}

	public int gameTimer;
	private bool inGame = false;
	private int gameTimeInSeconds;
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
	}

	void OnLevelWasLoaded(int level) {
		inGame = false;

		if (level != 0) {
			inGame = true;
			gameTimeInSeconds = gameTimer * 60;
			StartCoroutine(countdown());
		}
	}

	public void setGameTimeInSeconds(int gameTimeInSeconds) {
		this.gameTimeInSeconds = gameTimeInSeconds;
	}

    private IEnumerator countdown() {
        while (gameTimeInSeconds > 0) {
            yield return new WaitForSeconds(1f); 
            gameTimeInSeconds -= 1;
        }
        calculateScores();
        Application.LoadLevel(0);
    }

    private void calculateScores() {
    	pointPockets.Add(new DamageDonePointPocket());
    	pointPockets.Add(new KillsPointPocket());
    	pointPockets.Add(new BulletsDestroyedPointPocket());
    	pointPockets.Add(new BulletDamageDonePointPocket());

    	foreach(PointPocket pointPocket in pointPockets) {
    		Debug.Log(pointPocket.getName() + pointPocket.getWinner());
    	}
    }
	
}
