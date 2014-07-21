using UnityEngine;
using System.Collections;

public abstract class Projectile : Unit {

	public float m_lifeTime= 3.0f;
	public AudioClip[] shootSFX;

	public AudioSource[] sources;
	
	public Vector3 direction;
	
	
	protected void Start()
	{
		transform.parent = GameObject.Find ("LaserContainer").transform;
		Destroy(gameObject, m_lifeTime);	
	}
	
	public abstract void Launch(Vector3 dir);
	//public abstract Projectile Create(Vector3 postion, Quaternion rotation);

	void OnTriggerEnter(Collider trigger)
	{
		Unit unit=trigger.gameObject.GetComponent<Unit>();

		if(unit && (unit.tag== ("Enemy")|| unit.name == "Enemy") && tag == "PlayerBullet")
                {
					Enemy en= trigger.GetComponent<Enemy>();

                    
                    unit.Hit();
                }
               
		if(unit && (unit.tag==("Floating") || unit.tag==("Asteroid"))) 
		{

			unit.Hit ();
			Destroy(gameObject);
		}

		if(trigger.gameObject.GetType() != GetType() && trigger.gameObject.GetType() != Player.Instance.GetType())
		Destroy (gameObject);
	}
}
