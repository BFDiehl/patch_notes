using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    private int bulletHealth;
	private int bulletDamage;
	private PlayerScript.PlayerNumber firingPlayer;

	void OnCollisionEnter (Collision col) {
		PlayerScript collidedPlayer = col.gameObject.GetComponent<PlayerScript>();
		BulletScript collidedBullet = col.gameObject.GetComponent<BulletScript>();
    	if(collidedPlayer != null) {
            collidedPlayer.takeDamage(firingPlayer, bulletDamage);
            Destroy(this.gameObject);
        } else if(collidedBullet != null) {
        	PlayerScript.PlayerNumber damagingPlayer = collidedBullet.getFiringPlayer();
        	int collidedBulletDamage = collidedBullet.getBulletDamage();
        	collidedBullet.takeDamage(firingPlayer, bulletDamage);
        	takeDamage(damagingPlayer, collidedBulletDamage);
        } else {
        	Destroy(this.gameObject);
        }

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(PlayerScript.PlayerNumber firingPlayer, int bulletDamage, int bulletHealth) {
		this.firingPlayer = firingPlayer;
		this.bulletDamage = bulletDamage;
		this.bulletHealth = bulletHealth;
		this.renderer.sharedMaterial = 
			(Material)Resources.Load("Player/Mat_Player_" + firingPlayer.GetHashCode(), typeof(Material));
	}

	public int getBulletDamage() {
		return bulletDamage;
	}

	public PlayerScript.PlayerNumber getFiringPlayer() {
		return firingPlayer;
	}

	public void takeDamage(PlayerScript.PlayerNumber damagingPlayer, int damage) {
		damage = Mathf.Min(bulletHealth, damage);
		PlayerScript damagingPlayerScript = PlayersManager.getInstance().getPlayerScript(damagingPlayer);
		if(damagingPlayerScript != null) {
			damagingPlayerScript.increaseBulletDamageDone(damage);
		}
		bulletHealth -= damage;

		if(bulletHealth == 0 && damagingPlayerScript != null) {
			damagingPlayerScript.increaseBulletsDestroyed();
			Destroy(this.gameObject);
		}
	}
}
