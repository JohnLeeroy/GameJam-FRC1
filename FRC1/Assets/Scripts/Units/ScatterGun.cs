using UnityEngine;
using System.Collections;

//Fires multiple barrels
public class ScatterGun : Gun {
	
	public GameObject[] barrels;
	
	public override void Shoot (Vector3 dir)
	{
		if(isReadyToFire && isFireable)
		{
			foreach(GameObject barrel in barrels)
			{
				Transform tfBarrel = barrel.transform;
				GameObject proj = (GameObject) Instantiate(prefabProjectile, tfBarrel.position + tfBarrel.forward * 2, tfBarrel.rotation);	
				proj.GetComponent<Projectile>().Launch(dir);
			}
			isReadyToFire = false;
			StartCoroutine(CR_FireCooldown());
		}
	}
}