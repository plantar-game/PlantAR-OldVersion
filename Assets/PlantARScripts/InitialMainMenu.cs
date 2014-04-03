using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class InitialMainMenu : MonoBehaviour {
		
	//Constants Time Show/Hide
	const float modelChangeTimeTaken = 51f;
	const float waterTimeTaken = 10f;
	const float waterOne = 0.20f;
	const float compostOne = 0.20f;
	const int waterFull = 5;
	const int compostFull = 5;
	const float penaltyTime = 30f;
	
	//Current Bar Progress 
	float myTime =  0;
	float myAuxTime =  0;
	float initialTime = 0;
	float compostHealth = 0;
	float sunHealth = 0;
	float waterHealth = 0;
	
	float compostTotalHealth = 0;
	float sunTotalHealth = 0;
	float waterTotalHealth = 0;
	
	float waterTime = 0;

	float waterTimeCountdown = 0;
	
	//Time To Change
	float modelChangeTime = 0;
	
	//float salud = 100;
	bool bVisiblePlant = true;
	bool dialogButton = false;
	bool dialogPanel = false;
	bool timeReset = false;
	bool animationHealth = false;
	
	//Is Model Time to Change
	public bool modelChange = false;
	
	//Positions Menu
	int mMenuX = 0;
	int mMenuY = 0;
	int mMenuOpW = 0;
	int mMenuOpH = 0;
	int nMessage = 0;
	
	//Model Type
	
	int waterCount = 0;
	int compostCount = 0;
		
	public string plantName = "Escribe mi nuevo nombre";
	public string playerName = "Escribe mi nuevo nombre";
	public string texto = "";
	
	// opPlant = null;
	
	public GUISkin mainMenuSkin = null;
	
	//Para agregar iconos
	public Texture2D iPlanta;
	public Texture2D iLogros;
	public Texture2D iFoto;
	public Texture2D iConfig;
	public Texture2D iAbonar;
	public Texture2D iRegar;
	public Texture2D iPlantar;
	public Texture2D iOverPlantar;
	
	//Health Bars - Textures
	public Texture2D compostTextureEmpty;
    public Texture2D compostTextureFull;
	public Texture2D sunTextureEmpty;
    public Texture2D sunTextureFull;
	public Texture2D waterTextureEmpty;
    public Texture2D waterTextureFull;
	
	//Posotion
	Vector2 compostPosition = new Vector2(-4,40);
    Vector2 compostSize = new Vector2(230,250);
	Vector2 waterPosition = new Vector2(-4,75);
    Vector2 waterSize = new Vector2(230,250);
	Vector2 sunPosition = new Vector2(-4,110);
    Vector2 sunSize = new Vector2(230,250);
	
	//GUI Text
	public GUIText dialogText;
	public GUIText informationText;
	public GUIText timerText;
	
	//Data
	public PlantARData myData; 
	public AchievementsData  myAchievements; 
	private string _data; 
	string _textMessage = "Ya voy Creciendo";
	TimeSpan timeSpan;
	
	//Once Run
	void Awake(){	
		myData=new PlantARData();
		
		if (GameStateXML.IfFileExist ("Configuration.xml"))
			LoadData ();
			//CreateFile();
		else 
			CreateFile();
		
		compostCount = myData.compostLevelHealth;
		waterCount = myData.waterLevelHealth;		
		compostHealth = compostCount*2f/10f;
		sunHealth = (float)(myData.sunLevelHealth*2/10);
		waterHealth= waterCount*2f/10f;
				
		if (ApplicationModel.firstTime){
			initialTime = myData.plantarTime;
			ApplicationModel.initialTime = initialTime;
			ApplicationModel.firstTime = false;
		}else{
			initialTime = ApplicationModel.initialTime;
		}
		
		nMessage = 0;
		informationText.text = "";
		dialogText.text = "";
		timerText.text = "";
		timeReset = false;
		animationHealth = false;
		dialogPanel = false;
		modelChange = false;
		
		switch(myData.currentLevel){
			case 0:					
					dialogPanel = true;
					break;
			case 1:
					dialogPanel = true;
					dialogButton = true;
					nMessage = 1;
					break;
			case 2:
					dialogPanel = true;
					dialogButton = true;
					nMessage = 9;
					break;
			case 3:
					nMessage = 17;
					break;	
			case 4:
					nMessage = 18;
					break;
			case 5:
					nMessage = 19;
					break;
			case 6:
					nMessage = 20;
					break;
			case 7:
					nMessage = 21;
					break;
			case 8:
					nMessage = 22;
					break;
		}
				
		/*ManageData();
		SaveData();*/
		myAchievements =new AchievementsData();
		
		if (GameStateXML.IfFileExist ("Achievements.xml"))
			LoadAchievements();
		else 
			CreateAchievements();
	}
	
	
	
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (animationHealth){
			if (timeReset){
				myAuxTime = Time.time;
				myTime = 0;
			}
			
			if (myTime <= 4f){
				myTime = Time.time - myAuxTime;
				timeReset = false;
			}
			
			if (compostHealth <= compostTotalHealth){
				compostHealth = myTime*0.30f;
			}
			
			if (sunHealth <= sunTotalHealth){
				sunHealth = myTime*0.30f;
			}
			
			if (waterHealth <= waterTotalHealth){
				waterHealth = myTime*0.30f;
			}
		}
		
	}
	
	public string Data{
		get{return _data;}
		set{_data = value;}
	}
	
	public void CreateFile ()
	{		
		myData.currentLevel = 1;
		myData.compostLevelHealth = 0;
		myData.sunLevelHealth = 0;
		myData.waterLevelHealth = 0;
		myData.plantName  = "Default";
		myData.playerName = "Default";
		myData.plantarTime = 0;
		myData.modelAuxChangeTime = 0;
		myData.waterAuxTime = 0;
		myData.modelType = 1;
		myData.notificationOn = false;
		// Time to creat our XML! 
		_data = GameStateXML.SerializeObject (myData, "PlantARData"); 
		// This is the final resulting XML from the serialization process 
		GameStateXML.CreateXML ("Configuration.xml", _data);
		Debug.Log (_data);
		
	}
	
	public void SaveData(){
	     _data = GameStateXML.SerializeObject(myData,"PlantARData"); 
	     GameStateXML.CreateXML("Configuration.xml",_data);		
	}
	
	public void LoadData(){
		_data = GameStateXML.LoadXML("Configuration.xml");
		
		if(_data.ToString() != ""){
	      myData = (PlantARData)GameStateXML.DeserializeObject(_data,"PlantARData");
	    }
	}
	
	
	/*
	 * Achievementes Handle
	 * */
	
	public void CreateAchievements()
	{
		myAchievements.MessageStorage.Add("Haz obtenido una semilla nueva!");
		
		// Time to creat our XML! 
		_data = GameStateXML.SerializeObject (myAchievements, "AchievementsData"); 
		// This is the final resulting XML from the serialization process 
		GameStateXML.CreateXML ("Achievements.xml", _data);		
	}
	
		
	public void LoadAchievements()
	{
		_data = GameStateXML.LoadXML("Achievements.xml");
		if(_data.ToString() != "") 
	    {
	      myAchievements = (AchievementsData)GameStateXML.DeserializeObject(_data,"AchievementsData");
	    }
	}
	
	public void SaveAchievements(){
	     _data = GameStateXML.SerializeObject(myAchievements,"AchievementsData"); 
	     GameStateXML.CreateXML("Achievements.xml",_data);		
	}
	
	
	void OnGUI(){		
		
		//Debug.Log("test..test");
		if (mainMenuSkin != null)
			GUI.skin = mainMenuSkin;
		
		
		/*
		 * Dialog PlantAR
		 * */
		
		
		//Introduction Dialog Box		
		int dialogDivW = Screen.width*4/5;
		int dialogDivH = Screen.height*1/4;
		int dialogDivX = dialogDivW*1/6;
		int dialogDivY = dialogDivH*3;
		
		Color32 colorAux = GUI.backgroundColor;
			
		if (dialogPanel){
			
			Color32 colorPanel = new Color(1, 1, 1, 0.25f);
			
			GUI.backgroundColor = colorPanel;
			
			if(myData.currentLevel == 1){			
				//x=0.21,y=0.25,font=18
				GUI.Box(new Rect(dialogDivX+50,dialogDivY,dialogDivX*4,dialogDivH-dialogDivH*3/10), "");	
			}else{			
				//0.16,0.36,fsize=16,center
				GUI.Box(new Rect(dialogDivX+50,dialogDivY-dialogDivH*1/2,dialogDivX*4,dialogDivH+dialogDivH*3/10), "");
			}		
			
			switch (nMessage){
				case 0:
						dialogText.text = "Fin del Juego";
						break;
				case 1:
						dialogText.text = "Hola...  ¿Que hora es? ¿puedo seguir durmiendo? \nCreo que no";
						break;
				case 2: 
						dialogText.text = "¡Está bien empecemos!";
						break;
				case 3: 
						dialogText.text = "Soy una semilla muy rara de Planta de Gato \nSolo puedo crecer con agua luz solar \ny mucho cuidado";
						break;
				case 4: 
						dialogText.text = "¿Me ayudas? \nsi / si";
						break;
				case 5: 
						dialogText.text = "¡Genial! \nPero aun no tengo nombre ¿me das uno?";
						plantName = GUI.TextField(new Rect(dialogDivX*2,dialogDivY+dialogDivH*4/10,250,50), plantName ,25);
						break;
				case 6: 
						dialogText.text = "¡Me gusta mucho mi nombre!\nen especial cuando le agregas \n“La planta asombrosa” al final!";
						break;
				case 7: 
						dialogText.text = "¡"+plantName+" la planta asombrosa!";
						break;
				case 8: 
						dialogText.text = "Yo ya tengo nombre, pero yo no conozco el tuyo \n¿como te llamas?";
						playerName = GUI.TextField(new Rect(dialogDivX*2,dialogDivY+dialogDivH*4/10,250,50), playerName ,25);
						break;
				case 9: 
						dialogText.text = "¡Excelente!\nAhora si podemos iniciar";
						myData.plantName = plantName;
						myData.playerName = playerName;
						myData.currentLevel = 2;
						SaveData();
						break;
				case 10:
						dialogText.text = "";
						informationText.text = "¡Hola "+myData.playerName+"!\nSabias que las semillas necesitamos un lugar con tierra fértil\nde donde tomamos los nutrientes para vivir.\n¿Vez lo que está detrás de mi? \nEste será mi hogar, el cual ya tiene tierra para que me alimente.";
						break;
				case 11:
						informationText.text = "\nTengo hambre. ¿De casualidad traes abono contigo?\nEso es lo que comemos las plantas cuando nos sentimos débiles\ny se me antoja una buena porción.\nPara darme de comer, debes de seleccionar dentro\ndel menú \"Planta\" la opción que dice \"Abonar\"";
						dialogButton = false;
						break;
				case 12:
						informationText.text = "\n\n\n                 ¡Mmmmm Mmmmm! Mucho mejor, gracias";
						dialogButton = true;
						break;
				case 13:
						informationText.text = "\n\nSiento un poco de sed. ¿Me puedes dar agua, por favor?\nPara darme de beber debes seleccionar dentro\ndel menú \"Planta\" la opción que dice \"Regar\"";
						dialogButton = false;
						break;
				case 14:
						informationText.text = "\n\n\n                 ¡Ahh! Eso se siente mucho mejor";
						dialogButton = true;
						break;
				case 15:
						informationText.text = "¡Listo! Ya tengo todo para crecer bien y tener una vida genial.\n Pero recuerda sin tus cuidados no podré sobrevivir.\nEn el reloj de abajo puedes ver cuanto tiempo falta\npara que tome mi nueva forma.\nMientras tanto debes estar al pendiente, porque voy a necesitar\nmucho de tu ayuda. ";
						break;
				case 16:
						informationText.text = "n\n\nPienso que seremos buenos amigos, aunque no seas una planta ;)";
						animationHealth = false;
						break;
				case 18:
						_textMessage = "          Ya voy Creciendo";						
						break;
				case 19:
						_textMessage = "          Ya voy Creciendo.. mas";
						break;
				case 20:
						_textMessage = "          Ya voy Creciendo.. ++";
						break;
				case 21:
						_textMessage = "          Ya voy Creciendo.. +++";
						break;
				case 22:
						_textMessage = "          Ya voy Creciendo.. ++++";						
						break;
				case 23:
						_textMessage = "          Felicidades eres un Campeon";						
						break;				
			}
			
			GUI.backgroundColor = colorAux;
			
			if (nMessage >= 18){
				informationText.text = _textMessage;
				GUI.Button(new Rect(dialogDivX+dialogDivX*4,dialogDivY-dialogDivH*1/2,100,20), "Share FB");
			}
		}
		
		Color32 color = new Color(0, 0, 0, 0);
		GUI.backgroundColor = color;
		
		if (dialogButton){
			if(myData.currentLevel == 1){				
				if (GUI.Button(new Rect(dialogDivX,dialogDivY,dialogDivX*4,dialogDivH-dialogDivH*4/10), ""))
					nMessage++;
			}else{
				if (GUI.Button(new Rect(dialogDivX,dialogDivY-dialogDivH*1/2,dialogDivX*4,dialogDivH+dialogDivH*3/10), "")){
					nMessage++;
					
					//Level Start Game
					if (nMessage==17){
						myData.waterAuxTime = (float)Time.realtimeSinceStartup;
						myData.modelAuxChangeTime = (float)Time.realtimeSinceStartup;
						ELANManager.ScheduleRepeatingNotification("PlantAR",":'( Agua",0,10);
						myData.notificationOn = true;
						dialogButton = false;
						dialogPanel = false;
						informationText.text = "";
						waterCount = waterFull;
						compostCount = compostFull;
						myData.compostLevelHealth = compostFull;
						myData.waterLevelHealth = waterFull;					
						myData.sunLevelHealth = waterFull;
						myData.plantarTime = (float)Time.realtimeSinceStartup;
						myData.currentLevel = 3;
						compostHealth = 1f;
						waterHealth = 1f;
						sunHealth = 1f;
						
						SaveData();
					}
					
					if(nMessage >= 18){
						dialogButton = false;
						dialogPanel = false;
						informationText.text = "";
					}				
				}					
			}
		}		
		
		/*
		 * Menu Planta Options
		 * */
		
		//Main Menu	
		GUI.backgroundColor = colorAux;
		GUI.Box(new Rect(Screen.width-Screen.width/5,0,Screen.width/5,Screen.height), "",mainMenuSkin.GetStyle("Panel"));
		
		//Creating Buttons
		mMenuX = Screen.width-Screen.width/5;
		mMenuY = 0;
		mMenuOpW = Screen.width/5;
		mMenuOpH = Screen.height/4;
		
		GUI.backgroundColor = color;
		
		if (bVisiblePlant){
			if (GUI.Button(new Rect(mMenuX,mMenuY,mMenuOpW,mMenuOpH),iPlanta)){
				bVisiblePlant = false;
			}
		}
		
		//Plant Options (Abonar,Regar)
		if (!bVisiblePlant){		
			int plantMenuX = mMenuX-mMenuOpW*2;
			int plantMenuw = mMenuOpW;
			int plantMenuH = mMenuOpH;
		
			GUI.backgroundColor = colorAux;
			
			GUI.Box(new Rect(plantMenuX,mMenuY,plantMenuw*2,plantMenuH), "",mainMenuSkin.GetStyle("Panel"));		
		
			GUI.backgroundColor = color;
			
			//Abonar
			if (GUI.Button(new Rect(plantMenuX,mMenuY,plantMenuw,plantMenuH),iAbonar)){
				bVisiblePlant = true;				
				if (myData.currentLevel == 2 && nMessage == 11){
					animationHealth = true;
					compostTotalHealth = 1f;
					timeReset = true;
					nMessage++;
				}				
			}	
			
			plantMenuX += plantMenuw;
			
			//Regar
			if (GUI.Button(new Rect(plantMenuX,mMenuY,plantMenuw,plantMenuH),iRegar)){
				bVisiblePlant = true;
				if (myData.currentLevel == 2 && nMessage == 13){
					waterTotalHealth = 1f;
					timeReset = true;
					nMessage++;
				}else{					
					if(myData.currentLevel >= 3){
						waterHealth += waterOne;
						
						if (waterCount <= waterFull)
							waterCount ++;
						
						myData.waterLevelHealth = waterCount;
					}
				}
			}
			plantMenuX += plantMenuw;
			
			if (GUI.Button(new Rect(plantMenuX,mMenuY,plantMenuw,plantMenuH),iOverPlantar))
				bVisiblePlant = true;
		}
		
		
		
		/*
		 * Animation Health Bars
		 * */
		
		//Background
		GUI.BeginGroup(new Rect(compostPosition.x, compostPosition.y, compostSize.x, compostSize.y));
			GUI.Box(new Rect(0,0, compostSize.x, compostSize.y), compostTextureEmpty);

			//Filling:
			GUI.BeginGroup(new Rect(0,0, compostSize.x * compostHealth, compostSize.y));
			GUI.Box(new Rect(0,0, compostSize.x, compostSize.y), compostTextureFull);
			GUI.EndGroup();
		GUI.EndGroup();
		
		
		//Background
		GUI.BeginGroup(new Rect(sunPosition.x, sunPosition.y, sunSize.x, sunSize.y));
			GUI.Box(new Rect(0,0, sunSize.x, sunSize.y), sunTextureEmpty);

			//Filling:
			GUI.BeginGroup(new Rect(0,0, sunSize.x * sunHealth, sunSize.y));
			GUI.Box(new Rect(0,0, sunSize.x, sunSize.y), sunTextureFull);
			GUI.EndGroup();
		GUI.EndGroup();
		
		
		//Background
		GUI.BeginGroup(new Rect(waterPosition.x, waterPosition.y, waterSize.x, waterSize.y));
			GUI.Box(new Rect(0,0, waterSize.x, waterSize.y), waterTextureEmpty);

			//Filling:
			GUI.BeginGroup(new Rect(0,0, waterSize.x * waterHealth, waterSize.y));
			GUI.Box(new Rect(0,0, waterSize.x, waterSize.y), waterTextureFull);
			GUI.EndGroup();
		GUI.EndGroup();
		
		
		mMenuY += Screen.height/4;
		if (GUI.Button(new Rect(mMenuX,mMenuY,mMenuOpW,mMenuOpH),iLogros)){
			Application.LoadLevel("AchievementsMenu");
		}
		
		mMenuY += Screen.height/4;
		if (GUI.Button(new Rect(mMenuX,mMenuY,mMenuOpW,mMenuOpH),iFoto)){
			StartCoroutine(TakeScreenshot());
			Application.LoadLevel("FacebookMenu");
		}
		
		mMenuY += Screen.height/4;
		
		if (GUI.Button(new Rect(mMenuX,mMenuY,mMenuOpW,mMenuOpH),iConfig)){
			  Application.LoadLevel("ConfigurationMenu");
		}
		
		/*
		 * Timer Countdown
		 * */
		/*DateTime time = DateTime.Now;
		string hour = LeadingZero( time.Hour );
		string minute = LeadingZero( time.Minute );
		string second = LeadingZero( time.Second );
		timerText.text = hour + ":" + minute + ":" +  second;*/
		
		if (myData.currentLevel >=3 && myData.currentLevel < 9){
			waterTime = myData.plantarTime - myData.waterAuxTime;
			
			modelChangeTime = myData.plantarTime - myData.modelAuxChangeTime;
			waterTimeCountdown = modelChangeTimeTaken - modelChangeTime;
			
			if (waterTime >= waterTimeTaken){			
				myData.waterAuxTime = myData.plantarTime;
				waterHealth -=waterOne;
				waterCount --;
				ELANManager.SendNotification("PlantAR","Me estoy quedando sin agua.. :'(",0);
				myData.waterLevelHealth = waterCount;
				Debug.Log("water Count "+waterCount);
				Debug.Log("Water Health "+waterHealth);
				
				if (waterCount == 0){
					
					myData.modelAuxChangeTime += penaltyTime;
					compostHealth -= compostOne;
					compostCount --;
					myData.compostLevelHealth = compostCount;
					
					waterHealth = 1f;
					waterCount = waterFull;
					myData.waterLevelHealth = waterCount;
					
					//------ GAME OVER ------
					if(compostCount == 0){
						dialogPanel = true;
						nMessage = 0;
						myData.currentLevel = 0;
					}
				}		
			}else{	
				timeSpan = TimeSpan.FromSeconds(waterTimeCountdown);
				string timeText = string.Format("{0:D2}/{1:D2}:{2:D2}:{3:D2}",timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				timerText.text = timeText;
			}
			
			if (modelChangeTime > modelChangeTimeTaken){			
				Debug.Log("modelChangeTime " +modelChangeTime);
				Debug.Log("modelChangeTimeTaken "+modelChangeTimeTaken);
				myData.modelAuxChangeTime = myData.plantarTime;
				myData.modelType++;
				myData.currentLevel++;
				nMessage++;
				
				Debug.Log("Mensaje -> "+_textMessage);
				myAchievements.MessageStorage.Add(_textMessage);
				SaveAchievements();
				
				modelChange = true;
				dialogButton = true;
				dialogPanel = true;
			}
			
			myData.plantarTime = (float)Time.realtimeSinceStartup+initialTime;
			SaveData();
		}
	}
	
	private string LeadingZero(int n)
	{
		return n.ToString().PadLeft(2, '0');
	}
	
	private IEnumerator TakeScreenshot() 
	{
	    yield return new WaitForEndOfFrame();
	
	    var width = Screen.width;
	    var height = Screen.height;
	    var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
	    // Read screen contents into the texture
	    tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
	    tex.Apply();
	    ApplicationModel.screenshot = tex.EncodeToPNG();
		ApplicationModel.UserTexture = tex;
	}
}