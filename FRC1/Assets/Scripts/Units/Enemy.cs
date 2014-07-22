using UnityEngine;
using System.Collections;

public class Enemy : Unit {

	public GameObject soundExplosionDummy;
	public GameObject[] powerupPrefabs;
	public ParticleSystem enemyDead;	
	
	const int STATE_SEARCH = 0;
	const int STATE_ATTACK = 1;

	public int m_typeIndex = 0; 
	int m_state = 0;

	public float m_search_distance = 150f;
	public float m_range = 20f;
	public float rotate_rate = 20;
	public float rangeFromPlayer = 10; 
	public int   powerupDropChance = 100;
	
	public Gun gun;

	//Score to player
	public int m_score;

	public static Transform target;
	
	private GameObject player;

	void Awake()
	{
		m_speed = 10;

		gameObject.name = "EnemyShip";
        player = Player.Instance.gameObject;
	}
	void Start()
	{
		if(player == null)
			player = GameObject.Find ("Player");
		target = player.transform;
		ChangeState (STATE_SEARCH);

		m_score=100;
	}

	float GetDistance()
	{
		target = player.transform;
	    float val = Mathf.Abs (Vector3.Distance (transform.position, target.position)); 

		return val;
	}

	IEnumerator CR_SEARCH()
	{
		while (GetDistance() >= m_search_distance)
		{
			yield return 0;
		}
		ChangeState (STATE_ATTACK);
		yield return 0;
	}

	IEnumerator CR_ATTACK()
	{
		float distance;
		while (m_health > 0 ) 
		{
			distance = GetDistance();
			if(distance <= m_search_distance)	//if within sight distance
			{
				if(distance <= m_range) 		//if within attack range
				{
					Attack();					//attack
				}

				if(distance > rangeFromPlayer)
					MoveToTarget();				//move closer
			}
			else 		//outside of sight range, go to Search State
				break;

			yield return 0;
		}
		ChangeState (STATE_SEARCH);
		yield return 0;
	}
	
	public void ChangeState(int NEW_STATE)
	{
		m_state = NEW_STATE;
	    //print("New State " + m_state.ToString());
	    switch (m_state)
	    {
	        case STATE_SEARCH:
	            StartCoroutine("CR_SEARCH");
	            break;

	        case STATE_ATTACK:
	            StartCoroutine("CR_ATTACK");
	            break;
	    }
	}

	void OnGUI()
	{
		Debug.DrawLine (transform.position, transform.position + transform.forward * m_search_distance);
	}

	void MoveToTarget()
	{
		Vector3 targetDir = target.position - transform.position;
		float step = rotate_rate * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, newDir.y, 0));

		transform.position += transform.forward * m_speed * Time.deltaTime;	//HACK because axis is incorrect

		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}

	void Attack()
	{
		if(Player.Instance.m_health > 0)
			gun.Shoot(transform.forward);
	}
	
	private bool isQuitting = false; 
	void OnApplicationQuit() { isQuitting = true; }

	override public void Hit()
	{
		base.Hit();
	}
	
	void OnDestroy()
	{
		StopAllCoroutines ();
		EnemySpawner.getInstance ().RemoveEnemy (m_typeIndex, gameObject);
		Player play = player.GetComponent<Player>();
		if (!isQuitting && player != null) {
			Instantiate (enemyDead, this.transform.position, this.transform.rotation);
			enemyDead.Play ();

			print ("CHance " + powerupDropChance.ToString());
			int chance = Random.Range(0, 100);
			if(chance < powerupDropChance)
				SpawnPowerup();

			
			Instantiate(soundExplosionDummy,this.transform.position,transform.rotation);
		}
		
		Player.m_score+=m_score;
	}

	void SpawnPowerup()
	{
		print ("Spawn Powerup");
		int type = Random.Range (0, 1);
		GameObject powerup = (GameObject)Instantiate (powerupPrefabs [type], transform.position, Quaternion.identity);
		powerup.transform.position = new Vector3(powerup.transform.position.x, powerup.transform.position.y, 0);
	}
}
