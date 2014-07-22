using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour {
	
	Transform player;
	
	public float spawnRadiusFromPlayer = 100;
	public float spawnInterval = 1;
	
	public int maxPowerups = 5;
	
	public GameObject[] powerupPrefabs;
	
	List<GameObject> powerups;
	
	//Singleton
	static PowerupSpawner instance;
	public static PowerupSpawner getInstance()
	{
		return instance;
	}

	//Methods
	void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		powerups = new List<GameObject>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Start()
	{
		StartCoroutine(CR_Spawn());	
	}
	
	IEnumerator CR_Spawn()
	{
		while(true)
		{
			SpawnPowerup();
			yield return new WaitForSeconds(spawnInterval);
		}
	}
	
	void SpawnPowerup()
	{
		if(powerups.Count >= maxPowerups)
			return;
		
		int index = Random.Range(0, powerupPrefabs.Length);
		GameObject newPowerup = (GameObject)Instantiate (powerupPrefabs [index]);
		newPowerup.transform.position = new Vector3 (player.position.x + Random.Range (-spawnRadiusFromPlayer, spawnRadiusFromPlayer), 
		                                          player.position.y + Random.Range (-spawnRadiusFromPlayer, spawnRadiusFromPlayer), 0);
		newPowerup.transform.parent = transform;
		
		powerups.Add(newPowerup);
	}
	
	public void RemovePowerup(Powerup powerup)
	{
		powerups.Remove(powerup.gameObject);	
	}

}
