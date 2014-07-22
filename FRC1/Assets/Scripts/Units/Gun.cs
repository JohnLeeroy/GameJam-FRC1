using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject prefabProjectile;
	
	protected bool isFireable = true;
	protected bool isReadyToFire = true;
	
	public float rateOfFire = 1f;
	public float modifier = 1f;
	protected IEnumerator CR_FireCooldown()
	{
		yield return new WaitForSeconds (rateOfFire * modifier);
		isReadyToFire = true;
	}

	public virtual void Shoot(Vector3 dir)
	{
		if(isReadyToFire && isFireable)
		{
			GameObject proj = (GameObject) Instantiate(prefabProjectile, transform.position + transform.forward * 2, transform.rotation);	
			proj.GetComponent<Projectile>().Launch(dir);
			
			//if(audio != null)
			//	Instantiate(audio);
			AudioManager.getInstance().Play(Random.Range(AudioManager.LASER_ONE, AudioManager.LASER_THREE));
			
			isReadyToFire = false;
			
			StartCoroutine(CR_FireCooldown());
		}
	}
	
	
	void OnEnable()
	{
		Reset();
	}
	
	void Reset()
	{
		isFireable = true;
		isReadyToFire = true;
	}
}
