using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldRenderer : MonoBehaviour {
    // World Renderer
    private float largePlanetOffset = 100.0f;

    private static WorldRenderer _instance;

 	public static WorldRenderer getInstance()
	{
		return _instance;
	}
	
	//Public Properties
    public GameObject player;
    public List<GameObject> backgroundObjects;
    public List<GameObject> prefabs;
	public int[] prefabLimits;
	
    public int maxObjectCount = 30;
    public float spawnInterval = 40.0f;
    public float spawnRadius = 400.0f;
	
    public float minSpawnDistance = 3.0f;
	
	void Awake()
	{
		if(_instance != null)
		{
			print("ERRROR: Deleting WorldRenderer(singleton) instance duplicate");
			Destroy(gameObject);
		}	
		else
			_instance = this;
	}
	
	void Start () 
	{
        largePlanetOffset = Random.Range(50, 100);     
		
		StartCoroutine(CR_DeleteFarObjects());
		StartCoroutine(SpawnBackgroundObjects());
		
		SpawnInitalObjects();
	}
	
	void SpawnInitalObjects()
	{
		for(int i = 0; i < 30; i++)		
		{
			SpawnRandomObject();	
		}
	}
	IEnumerator CR_DeleteFarObjects()
	{
		List<GameObject> removeList = new List<GameObject>();
		Vector3 playerPos;
		float deleteDistance = spawnRadius * 1.5f;
		while(true)
		{
			playerPos = player.transform.position;
			foreach(GameObject obj in backgroundObjects)
			{
				if(Vector2.Distance(playerPos, playerPos) < 2)
				{
					
				}
				if (Vector2.Distance(playerPos, obj.transform.position) > deleteDistance)
					removeList.Add(obj);
			}
			
			foreach(GameObject obj in removeList)
			{
				backgroundObjects.Remove(obj);
                Destroy( obj );
			}
			yield return new WaitForSeconds(2f);
		}    	
	}
	
	IEnumerator SpawnBackgroundObjects()
	{
		while(true)
		{
			SpawnRandomObject();
			yield return new WaitForSeconds(spawnInterval);
		}
	}
	
    float getRandomOffset(float spawnRadius, float minSpawnDistance)
    {
        float rnd = Random.Range(-spawnRadius, spawnRadius);
        while (rnd == 0.0f) 
			rnd = Random.Range(-spawnRadius, spawnRadius);
        return  (Mathf.Abs(rnd) > minSpawnDistance) ? rnd : rnd + ((rnd > 0) ? 1 : -1) * minSpawnDistance;
    }

	float GetRandomDistance()
	{
		float mag = Random.Range(minSpawnDistance, spawnRadius);
		float distance = (Random.Range(0, 100) > 50) ? -mag : mag;
		return distance;
	}
    void SpawnRandomObject()
    {
        if (backgroundObjects.Count <= maxObjectCount)
        {
			GameObject obj = (GameObject)Instantiate(prefabs[Random.Range(0, prefabs.Count)], player.transform.position, Quaternion.identity);
            
			float xrandomOffset = GetRandomDistance();//Random.Range(-1, 1) * Random.Range(minSpawnDistance, spawnRadius);//getRandomOffset(spawnRadius, minSpawnDistance);
	        float yrandomOffset = GetRandomDistance();//Random.Range(-1, 1) *  Random.Range(minSpawnDistance, spawnRadius);//getRandomOffset(spawnRadius, minSpawnDistance);
	        float zrandomOffset = Mathf.Abs(getRandomOffset(spawnRadius, minSpawnDistance));
	        obj.transform.Translate(xrandomOffset, yrandomOffset, zrandomOffset);
	        obj.transform.parent = GameObject.Find("BackgroundContainer").transform;
			
			
			if (obj.tag == "LargePlanet")
				obj.transform.Translate(0, 0, Random.Range(200, 400));

			backgroundObjects.Add(obj);
			
			print("Position: " + obj.transform.position);
        }
    }

    public void AddObject(GameObject obj, bool foreground)
    {
		/*
        if (foreground) 
			foregroundObjects.Add(obj);
        else 
			backgroundObjects.Add(obj);
			*/
	}
}


/*
 * Q1
/////////////////////////////// Why is this bad? /////////////////////////////// 
	WorldRenederer.cs
	
 	void Update () {
           
        DelteFarObjects();
    }
    
	void DelteFarObjects()
    {
        for (int i = backgroundObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = backgroundObjects[i];
            if (Vector3.Distance(player.transform.position, obj.transform.position) > spawnRadius)
            {
                backgroundObjects.RemoveAt(i);
                Destroy( obj );
            }
        }
    }
    
/////////////////////////////// How to make it better? ///////////////////////////////
 	WorldRenederer.cs
 	
 	void Start () {
		StartCoroutine(CR_DeleteFarObjects());
	}
	
	IEnumerator CR_DeleteFarObjects()
	{
		List<GameObject> removeList = new List<GameObject>();
		Vector3 playerPos;
		while(true)
		{
			playerPos = player.transform.position;
			foreach(GameObject obj in backgroundObjects)
			{
				if (Vector3.Distance(playerPos, obj.transform.position) > spawnRadius)
					removeList.Add(obj);
			}
			
			foreach(GameObject obj in removeList)
			{
				backgroundObjects.Remove(obj);
                Destroy( obj );
			}
			yield return new WaitForSeconds(2f);
		}    	
	}
	Q2
	How would you fix this?
	  private static WorldRenderer _instance;
      private static bool _set = false;

        public static WorldRenderer Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                if (!_set)
                {
                    _instance = value;
                    _set = true;
                }
            }
   
	void Start () {
            Instance = this;
	}
	
	
	//////////////////////////////////////////////////////////////
	private static WorldRenderer _instance;

 	public static WorldRenderer getInstance()
	{
		return _instance;
	}
	
	void Awake()
	{
		if(_instance != null)
		{
			print("ERRROR: Deleting WorldRenderer(singleton) instance duplicate");
			Destroy(gameObject);
		}	
		else
			_instance = this;
	}
	
*/