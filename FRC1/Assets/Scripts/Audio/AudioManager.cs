using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioClip[] clips;
	
	public const int LASER_ONE = 0;
	public const int LASER_TWO = 1;
	public const int LASER_THREE = 2;
	public const int PLAYER_DEATH = 3;
	public const int ENEMY_DEATH = 4;
	public const int POWERUP_PICKUP = 5;
	
	private static AudioManager instance;
	
	public static AudioManager getInstance()
	{
		return instance;	
	}
	
	void Awake()
	{
		if(instance != null)
			Destroy(gameObject);
		
		instance = this;
	}
	
	public void Play(int clipIndex)
	{
		if(clipIndex < clips.Length)	
			audio.PlayOneShot(clips[clipIndex], .5f);
		
	}
}
