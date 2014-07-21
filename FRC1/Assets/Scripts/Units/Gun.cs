using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject prefabProjectile;
	
	bool isFireable = true;
	bool isReadyToFire = true;
	
	public float rateOfFire = 1f;
	
	IEnumerator CR_FireCooldown()
	{
		yield return new WaitForSeconds (rateOfFire);
		isReadyToFire = true;
	}

	public void Shoot(Vector3 dir)
	{
		if(isReadyToFire && isFireable)
		{
			GameObject proj = (GameObject) Instantiate(prefabProjectile, transform.position + transform.forward * 2, transform.rotation);	
			proj.GetComponent<Projectile>().Launch(dir);
			isReadyToFire = false;
			StartCoroutine(CR_FireCooldown());
		}
	}
}
