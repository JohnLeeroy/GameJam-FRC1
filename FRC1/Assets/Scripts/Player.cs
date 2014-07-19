﻿using UnityEngine; using System.Collections; 
public class Player : MonoBehaviour
{
	//Variables
        public float m_speed = 10.00f;
        public float r_speed = 20.0f;
        public bool moving = false;
        public static Player instance;

        // Use this for initialization
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            moving = Mathf.Abs(Input.GetAxis("Vertical")) > 1;
            transform.Rotate(0,0,-Input.GetAxis("Horizontal") *Time.deltaTime* r_speed);
            transform.Translate(0, Input.GetAxis("Vertical")*Time.deltaTime*m_speed, 0);
        }
	
	void OnTriggerEnter(Collider other)
	{
		print (other.name);
		switch (other.name) {
			case "EnemyShip":
			Destroy(other.gameObject);
			break;
		}
	}

		//transform.Rotate(Input.GetButton("Up"),90.0f);

	}



