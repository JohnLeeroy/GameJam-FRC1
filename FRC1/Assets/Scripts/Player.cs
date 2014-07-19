﻿using UnityEngine; using System.Collections; 
public class Player : MonoBehaviour
{
	//Variables
        public float m_speed = 1.00f;
        public float r_speed = 300.0f;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0,0,Input.GetAxis("Horizontal") * r_speed);
            transform.Translate(0, Input.GetAxis("Vertical")*Time.deltaTime*m_speed, 0);
        }

		//transform.Rotate(Input.GetButton("Up"),90.0f);

	}



