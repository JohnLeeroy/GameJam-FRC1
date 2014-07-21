using UnityEngine;
using System.Collections;

public class Laser : Projectile {


	public string soundStamp;

	//Not every frame

        // Factory for laser
        public static GameObject CreateLaser(string owner, GameObject bulletType, Vector3 postion, Quaternion rotation)
        {
            GameObject obj = (GameObject)Instantiate(bulletType,postion,rotation);

            // Try to set the owner with the provided value
            try { obj.tag = owner; }
            catch { obj.tag = "Laser"; }

            return obj;
        }

	// Use this for initialization
	new void Start () {
		base.Start();
		
		//Random i
		int i =Random.Range(0,shootSFX.Length);
		
		if(sources[0].isPlaying)
		{
			sources[1].clip= shootSFX[i];
			sources[1].Play();
		}
		
		else
		{
			sources[0].clip= shootSFX[i];
			sources[0].Play();
		}
		
	}
	public override void Launch(Vector3 dir)
	{
		direction = dir;
	}
	
	void Update () {
		transform.position += direction * Time.deltaTime * m_speed;
	}

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
	//void OnTriggerEnter(Collider trigger)
	//{
	//	Unit unit=trigger.gameObject.GetComponent<Unit>();
    //    //if(unit && unit.tag!= ("Player"))
    //    //    unit.Hit();
	//
    //    //if(unit && unit.tag!= ("Enemy"))
    //    //        {
    //    //            Player.m_score += (int)unit.m_speed;
    //    //            unit.Hit();
    //    //        }
	//
    //            if (unit)
    //            {
    //                switch (unit.tag)
    //                {
    //                    case "Enemy":
    //                        if (tag == "PlayerBullet")
    //                        {
    //                            Enemy e = unit as Enemy;
    //                            int enemytype = e.m_typeIndex;
    //                            EnemySpawner.getInstance().RemoveEnemy(enemytype, unit.gameObject);
    //                        }
    //                        break;
    //                }
    //            }
	//	//Destroy (gameObject);
	//}
}

