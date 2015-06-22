using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	private static MenuManager instance;
	
	private MenuManager() {}
	
	public static MenuManager getInstance() {
		return instance != null ? instance : new MenuManager();
	}

	void Start() {
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this);
		instance = this;
	}
}
