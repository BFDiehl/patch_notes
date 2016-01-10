using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    private int bulletHealth;
	private int bulletDamage;
	private PlayerScript.PlayerNumber firingPlayer;

	void OnTriggerEnter (Collider col) {
		PlayerScript collidedPlayer = col.gameObject.GetComponent<PlayerScript>();
		BulletScript collidedBullet = col.gameObject.GetComponent<BulletScript>();
    	if(collidedPlayer != null) {
            dealDamage(collidedPlayer);
        } else if(collidedBullet != null) {
        	dealDamage(collidedBullet);
        } else {
        	Destroy(this.gameObject);
        }
    }

    public void dealDamage(PlayerScript player) {
        player.takeDamage(firingPlayer, bulletDamage);
        Destroy(this.gameObject);
    }

    public void dealDamage(BulletScript bullet) {
        PlayerScript.PlayerNumber damagingPlayer = bullet.getFiringPlayer();
        int collidedBulletDamage = bullet.getBulletDamage();
        bullet.takeDamage(firingPlayer, bulletDamage);
        takeDamage(damagingPlayer, collidedBulletDamage);
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
		this.GetComponent<Renderer>().sharedMaterial = 
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
