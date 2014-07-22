using UnityEngine; using System.Collections; 
public class Player : Unit
{
	//Class Instance 
	public static Player Instance;
	public static GameObject MainPlayer;
	public GameObject weapon;

	//Boolean
	public bool isAlive;
	public GameObject soundDummy;

   	public Gun[] stageZeroGuns;
   	public Gun[] stageOneGuns;
   	public Gun[] stageTwoGuns;

    public int Score;
	public int scoreOT=10; //Score over time
	public float scoreTimer=0.0f;
	
	public static int m_score=0;
	public GUIStyle scoreStyle;
	public Rect m_scoreRect;

    public float r_speed = 1000.0f;
     
    public float m_drag = -1.0f;    public float v_Input = 0f;    public float h_Input = 0f;    public bool moving = false;
	
	int stage = 0; // 3 total stages
	public GameObject[] stagePieces;
	
	Gun[] selectedGuns;// = stageZeroGuns;
	
    void Awake()
    {
    	Instance = this;
    }
        
	void Start()
	{
		//Health
		m_health = 1.0f;

        string text= m_score.ToString();
		
		int widthTextOffset= text.Length;
		
		
		m_scoreRect.x=Screen.width/4;
		m_scoreRect.y=Screen.height*3/4;
		
		m_scoreRect.width = m_scoreRect.x+100;
		m_scoreRect.height= m_scoreRect.y +20;

		isAlive=true;
		
		SwitchStage(0);
	}
	
	//Transformation
	void SwitchStage(int newStage)
	{
		stage = newStage;
		switch(newStage)
		{
		case 0:
			for(int i = 1; i < stagePieces.Length; i++)
				stagePieces[i].SetActive(false);	
			
			selectedGuns = stageZeroGuns;
			break;
		case 1:
			for(int i = 0; i <= newStage; i++)
				stagePieces[i].SetActive(true);	
			selectedGuns = stageOneGuns;
			m_health = 2f;
			
			break;
		case 2:
			for(int i = 0; i <= newStage; i++)
				stagePieces[i].SetActive(true);	
			selectedGuns = stageTwoGuns;
			m_health = 3f;
			break;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!isAlive)
			return;
		
		Score = m_score;

		scoreTimer+=Time.deltaTime;

		if(scoreTimer >=1.0f)
		{
			m_score+=scoreOT;
			scoreTimer=0;
		}

		// Store the input from the player
	    v_Input = Input.GetAxis("Vertical");
	    h_Input = -Input.GetAxis("Horizontal");
	    
	    // translate the input read from player this iteration 
	    transform.Rotate(0,0, h_Input * Time.deltaTime * r_speed);
	   	transform.position += transform.up * v_Input * Time.deltaTime * m_speed;
		
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		
		if(Input.GetKey(KeyCode.Space))
		{
			for(int i = 0; i < selectedGuns.Length; i++)
				selectedGuns[i].Shoot(transform.up);	
		}
	}

	void OnTriggerEnter(Collider other)
	{
		print (other.tag);
		switch (other.tag) {
			case "Enemy":
				Hit();
			break;
			case "Laser":
				//if(other.gameObject.tag!="PlayerProj")
				//	Hit();

			break;
			case "Powerup":
				Powerup collected = other.GetComponent<Powerup>();
				CollectedPowerup(collected);
			break;
			case "Floating":
			//Destroy(other.gameObject);
				Hit();
			break;
			case "EnemyBullet":
				Hit();
			break;
		}

	}

	void CollectedPowerup(Powerup powerup)
	{
		int type = powerup.type;
		print ("Collected Powerup " + type.ToString ());
		switch (type) {
			case Powerup.TYPE_LASER:

			break;
			case Powerup.TYPE_BULLET:
			
			break;
			case Powerup.TYPE_ARMOR:
				if(stage == 0)
				{
					SwitchStage(stage + 1);
					powerup.OnCollected();
				}
			
			break;
			case Powerup.TYPE_ARMORTWO:
				if(stage == 1)
				{
					SwitchStage(stage + 1);
					powerup.OnCollected();

				}break;
		}
	}

	void OnGUI()
	{
		if(m_health<=0)
			GUI.Label(new Rect(0,0,30,30),"YOU LOSE",scoreStyle);

		GUI.Label(new Rect(0,0,30,30),m_health.ToString(),scoreStyle);
	}

    public override void Hit()
    {
        m_health-=1;
		if(stage != 0)
			SwitchStage(stage - 1);
		
        if (m_health<=0)
        {
			isAlive=false;
			Instantiate(soundDummy,this.transform.position,this.transform.rotation);
            //Destroy(gameObject);
        }
    }

	void Die()
	{
		Instantiate(soundDummy,this.transform.position,this.transform.rotation);
	}
}




