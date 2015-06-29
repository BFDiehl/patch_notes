﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public enum PlayerNumber {
		PLAYER_ONE   = 1,
		PLAYER_TWO   = 2,
		PLAYER_THREE = 3,
		PLAYER_FOUR  = 4,
		NONE         = 5
	}
	
	

	private PlayerNumber playerNumber;
	private CharacterController characterController;
	private InputManager.PlayerInput playerInput;
    public float playerSpeed;

    private Rigidbody bullet;
    public int bulletHealth;
	public int bulletDamage;
	public float bulletSpeed;
	public float bulletRange;
	public float secondsBetweenShots;

	public int maxHealth;
	private int currentHealth;
	
	private int deaths;
	private int deathsSinceLastKill;

	private int kills;
	private int killsSinceLastDeath;

	private int damageDone;
	private int bulletDamageDone;
	private int bulletsDestroyed;

	private bool allowFire = true;
		
	/***********************************
	 *      Base Unity Methods         *
	 ***********************************/
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		checkMovement();
		checkFire();

		if(playerInput.getStartButton()) {
			exitToMenu();
		}
	}

	/***********************************
	 *        Custom Methods           *
	 ***********************************/
	public void init(PlayerNumber playerNumber) {
		this.playerNumber = playerNumber;
		playerInput = InputManager.getInstance().getPlayerInput(playerNumber);

		this.bullet = ((GameObject)Resources.Load("Player/Bullet")).GetComponent<Rigidbody>();
		this.GetComponent<Renderer>().sharedMaterial = 
			(Material)Resources.Load("Player/Mat_Player_" + playerNumber.GetHashCode(), typeof(Material));

		this.transform.position = GameObject.Find("PLAYER " + playerNumber.GetHashCode() + " SPAWN").transform.position;
		this.characterController = GetComponent<CharacterController>();
	}

	public void takeDamage(PlayerNumber damagingPlayer, int damage) {
		damage = Mathf.Min(currentHealth, damage);
		PlayerScript damagingPlayerScript = PlayersManager.getInstance().getPlayerScript(damagingPlayer);
		if( damagingPlayerScript != null) {
			damagingPlayerScript.increaseDamageDone(damage);
		}
		currentHealth -= damage;

		if(currentHealth == 0) {
			die(damagingPlayerScript);
		}
	}

	private void die(PlayerScript killingPlayer) {
		deaths++;
		deathsSinceLastKill++;
		currentHealth = maxHealth;
		this.transform.position = GameObject.Find("PLAYER " + playerNumber.GetHashCode() + " SPAWN").transform.position;
		if(killingPlayer != null) {
			killingPlayer.increaseKills();
		}
	}

    /**
     * Checks for input from the left vertical and horiztonal sticks and moves the player
     * accordingly.
     */
	private void checkMovement() {
		Vector3 moveDir = new Vector3(playerInput.getLeftStickHoriztonal(), 0, -playerInput.getLeftStickVertical());
		moveDir.Normalize();
		moveDir = new Vector3(moveDir.x, -1, moveDir.z);
		characterController.Move(moveDir * Time.deltaTime * playerSpeed);
	}

	/**
     * Checks the horizontal and vertical input from the right stick and fires in a cardinal direction
     */
	private void checkFire() {
		float horizontalInput = playerInput.getRightStickHoriztonal();
		float verticalInput = playerInput.getRightStickVertical();

		if(allowFire && (horizontalInput != 0 || verticalInput != 0)) {
			Vector3 fireDir = new Vector3(horizontalInput, 0, -verticalInput);
			fireDir.Normalize();
			StartCoroutine(Fire(fireDir));
		}
	}

	/**
 	 * Instantiates a bullet and fires it in the direction indicated by the right stick.
 	 */

	private IEnumerator Fire(Vector3 fireDirection) {
		allowFire = false;
		// Instantiate the bullet at the position and rotation of this transform
		Rigidbody clone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);

		clone.velocity = fireDirection * bulletSpeed;
		clone.gameObject.GetComponent<BulletScript>().init(playerNumber, bulletDamage, bulletHealth);
		Physics.IgnoreCollision(clone.GetComponent<Collider>(), this.GetComponent<Collider>());

 		yield return new WaitForSeconds(secondsBetweenShots);
		allowFire = true;
		if(clone != null) {
			Destroy(clone.gameObject, 2);
		}
	}

	private void exitToMenu() {
		Application.LoadLevel(0);
	}

	/***********************************
	 *      Getters and Setters        *
	 ***********************************/
	public PlayerNumber getPlayerNum() {
		return playerNumber;
	}

    public void increaseDamageDone(int damage) {
    	this.damageDone = this.damageDone + damage;
    }

	public int getDamageDone() {
		return damageDone;
	}

	public int getKills() {
		return kills;
	}

	public void increaseKills() {
		kills++;
		killsSinceLastDeath++;
		deathsSinceLastKill = 0;
	}

	public int getDeaths() {
		return deaths;
	}

	public int getBulletDamageDone() {
		return bulletDamageDone;
	}

	public void increaseBulletDamageDone(int damage) {
		this.bulletDamageDone = this.bulletDamageDone + damage;
	}

	public int getBulletsDestroyed() {
		return bulletsDestroyed;
	}

	public void increaseBulletsDestroyed() {
		bulletsDestroyed++;
	}
}
