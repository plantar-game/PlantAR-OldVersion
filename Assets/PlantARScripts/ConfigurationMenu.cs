using UnityEngine;
using System.Collections;

public class ConfigurationMenu : MonoBehaviour {
	
	int mainMenuX = 0;
	int mainMenuY = 0;
	int menuOpW = 0;
	int menuOpH = 0;
	
	int optionMenu = 0;
	
	int buttonSaveX = 0;
	int buttonSaveY = 0;
	
	bool bVisiblePlant = true;
	private bool notificationOn = false;
	
	public PlantARData myConfiguration; 
	public AchievementsData  myAchievements; 
	private string _data; 
	public string plantName = "Escribe mi nuevo nombre";
	public string playerName = "Escribe mi nuevo nombre";
	
	// opPlant = null;
	
	public GUISkin configurationSkin = null;
	
	//Para agregar iconos
	public Texture2D iPlanta;
	public Texture2D iLogros;
	public Texture2D iFoto;
	public Texture2D iConfig;
	public Texture2D iAlertas;
	public Texture2D iFB;
	public Texture2D iAcerca;
	public Texture2D iHistorial;
	public Texture2D iMensajes;
	public Texture2D iSonidos;
	public int toPlant = 0;

	// Use this for initialization
	void Start () {
		myConfiguration=new PlantARData();
		LoadConfiguration();
		plantName = myConfiguration.plantName;
		playerName = myConfiguration.playerName;
		myAchievements =new AchievementsData();
		LoadAchievements();
		if (myConfiguration.notificationOn)
			notificationOn = true;
		else
			notificationOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android){
            if (Input.GetKey(KeyCode.Escape)){ 
				optionMenu = 0;               
            }
		}		
	}
	
	void OnGUI(){
		
		GUI.skin = configurationSkin;
				
		//Background box Rect(x,y,width,height)
		//Main Menu
		Color32 colorAux = GUI.backgroundColor;
		
		GUI.Box(new Rect(0,0,Screen.width*4/5+10,Screen.height), "",configurationSkin.GetStyle("PanelContent"));
		
		GUI.Box(new Rect(Screen.width-Screen.width/5,0,Screen.width/5,Screen.height), "",configurationSkin.GetStyle("PanelLeft"));
		
		//Creating Buttons
		mainMenuX = Screen.width-Screen.width/5;
		mainMenuY = 0;
		menuOpW = Screen.width/5;
		menuOpH = Screen.height/4;
		
		Color32 color = new Color(0, 0, 0, 0);
		GUI.backgroundColor = color;
		
		if (bVisiblePlant){
			if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iPlanta)){
				ApplicationModel.currentLevel = 1;
				Application.LoadLevel("InitialMainMenu");
			}
		}
		
		mainMenuY += Screen.height/4;
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iLogros)){
			Application.LoadLevel("AchievementsMenu");			
		}
		
		mainMenuY += Screen.height/4;
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iFoto)){
			Application.LoadLevel("FacebookMenu");
		}
		
		mainMenuY += Screen.height/4;
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iConfig)){
			  Application.LoadLevel("ConfigurationMenu");
		}
		
		/*
		 * 
		 * Panel Configuration
		 * 
		 * */
		
		int auxX = 0;
		int auxY = 0;
		
		GUI.backgroundColor = colorAux;
		
		GUI.Box(new Rect(Screen.width*4/5*1/38,Screen.height*1/27,Screen.width*4/5*36/38,Screen.height*25/27), "",configurationSkin.GetStyle("PanelCenter"));
			
		//Creating Buttons 3 iconos , 4 espaces
		mainMenuX = (Screen.width*4/5)*1/8;
		auxX = mainMenuX;
		mainMenuY = (Screen.height/3)*1/2;
		menuOpW = (Screen.width*4/5)*1/6;
		menuOpH = Screen.height/3*3/4;
		auxY = Screen.height/3+Screen.height/3*1/4;
		
		GUI.backgroundColor = color;
		
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iAlertas))
			optionMenu = 1;
		GUI.Label(new Rect(mainMenuX+menuOpW*1/4,mainMenuY+menuOpH,menuOpW,menuOpH),"Alertas");
		
		mainMenuX += auxX+menuOpW;
		
		GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iFB);
		GUI.Label(new Rect(mainMenuX+menuOpW*1/5,mainMenuY+menuOpH,menuOpW,menuOpH),"Cuenta de Facebook");
		
		mainMenuX += auxX+menuOpW;
		
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iAcerca))
			optionMenu = 3;
		GUI.Label(new Rect(mainMenuX+menuOpW*1/4,mainMenuY+menuOpH,menuOpW,menuOpH),"A Cerca De");
			
		
		mainMenuX = auxX;
		mainMenuY += auxY;
		
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iHistorial))
			optionMenu = 4;
		
		GUI.Label(new Rect(mainMenuX+menuOpW*1/4,mainMenuY+menuOpH,menuOpW,menuOpH),"Cambiar Nombre a Planta");
		
		mainMenuX += auxX+menuOpW;
		
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iMensajes))
			optionMenu = 5;
		GUI.Label(new Rect(mainMenuX+menuOpW*1/5,mainMenuY+menuOpH,menuOpW,menuOpH),"Cambiar Nombre Jugador");
		
		mainMenuX += auxX+menuOpW;
		
		if (GUI.Button(new Rect(mainMenuX,mainMenuY,menuOpW,menuOpH),iSonidos))
			optionMenu = 6;
		GUI.Label(new Rect(mainMenuX+menuOpW*1/4,mainMenuY+menuOpH,menuOpW,menuOpH),"Reiniciar");			
		
		
		/**
		 * Creating Options
		 * */
		
		GUI.backgroundColor = colorAux;
		
		mainMenuX = (Screen.width*4/5)*1/8;
		auxX = mainMenuX;
		mainMenuY = (Screen.height/3)*1/2;
		menuOpW = (Screen.width*4/5)*1/6;
		menuOpH = Screen.height/3*3/4;
		auxY = Screen.height/3+Screen.height/3*1/4;
		
		if (optionMenu > 0){
			GUI.Box(new Rect(mainMenuX,mainMenuY,menuOpW*5,menuOpH*3), "");
		}
		
		switch(optionMenu){
			case 1:
					buttonSaveX = mainMenuX+menuOpW*5/2-menuOpW/2;
					buttonSaveY = mainMenuY+menuOpH*3/2-menuOpH/4;
			
					notificationOn = GUI.Toggle(new Rect(buttonSaveX,buttonSaveY, 150, 15), notificationOn, "Activar/Desactivar");
					
					Debug.Log("Toggle "+notificationOn);
					if (GUI.Button(new Rect(buttonSaveX,buttonSaveY+30,menuOpW,menuOpH/2),"Guardar")){
						ELANManager.CancelRepeatingNotification ();
						if(notificationOn){
							myConfiguration.notificationOn = true;
							ELANManager.ScheduleRepeatingNotification("PlantAR",":'( Agua",0,10);
							Debug.Log("Notification ON");
						}else{
							myConfiguration.notificationOn = false;
							Debug.Log("Notification OFF");
						}
						
						SaveConfiguration();
						optionMenu = 0;
					}			
					break;
			
			case 2:
					break;
			
			case 3:
					buttonSaveX = mainMenuX+menuOpW*5/2-menuOpW/2;
					buttonSaveY = mainMenuY+menuOpH*2;
					string about ="PlantAR @Copyrights";
					GUI.Label(new Rect(mainMenuX+menuOpW,buttonSaveY-menuOpH*3/2,menuOpW*3,menuOpH*2),about,configurationSkin.GetStyle("TextAbout"));
					
					if (GUI.Button(new Rect(buttonSaveX,buttonSaveY+30,menuOpW,menuOpH/4),"Aceptar")){
						optionMenu = 0;						
					}			
					break;
			case 4:
					buttonSaveX = mainMenuX+menuOpW*5/2-menuOpW/2;
					buttonSaveY = mainMenuY+menuOpH*2;
					plantName = GUI.TextField(new Rect(buttonSaveX-50,buttonSaveY-menuOpH,200,25), plantName,25);
					//playerName = GUI.TextField(new Rect(dialogDivX*2,dialogDivY+dialogDivH*4/10,200,25), playerName ,25);
					if (GUI.Button(new Rect(buttonSaveX,buttonSaveY+30,menuOpW,menuOpH/4),"Guardar")){
						myConfiguration.plantName = plantName;
						optionMenu = 0;
						SaveConfiguration();
					}			
					break;
			case 5:
					buttonSaveX = mainMenuX+menuOpW*5/2-menuOpW/2;
					buttonSaveY = mainMenuY+menuOpH*2;
					playerName = GUI.TextField(new Rect(buttonSaveX-50,buttonSaveY-menuOpH,200,25), playerName,25);
					//playerName = GUI.TextField(new Rect(dialogDivX*2,dialogDivY+dialogDivH*4/10,200,25), playerName ,25);
					if (GUI.Button(new Rect(buttonSaveX,buttonSaveY+30,menuOpW,menuOpH/4),"Guardar")){
						myConfiguration.playerName = playerName;
						SaveConfiguration();
						optionMenu = 0;						
					}			
					break;
			case 6:
					buttonSaveX = mainMenuX+menuOpW*5/2-menuOpW/2;
					buttonSaveY = mainMenuY+menuOpH*2;
					//playerName = GUI.TextField(new Rect(dialogDivX*2,dialogDivY+dialogDivH*4/10,200,25), playerName ,25);
					if (GUI.Button(new Rect(buttonSaveX,buttonSaveY+30,menuOpW,menuOpH/4),"Reiniciar")){
						myConfiguration=new PlantARData();
						myAchievements =new AchievementsData();	
						CreateConfiguration();
						CreateAchievements();					
						ApplicationModel.initialTime = 0;
						optionMenu = 0;
					}			
					break;
		}
		
		
	}
	
	public void SaveConfiguration(){
	     _data = GameStateXML.SerializeObject(myConfiguration,"PlantARData"); 
	     GameStateXML.CreateXML("Configuration.xml",_data);		
	}
	
	public void LoadConfiguration(){
		_data = GameStateXML.LoadXML("Configuration.xml");
		
		if(_data.ToString() != ""){
	      myConfiguration = (PlantARData)GameStateXML.DeserializeObject(_data,"PlantARData");
	    }
	}
	
	public void CreateConfiguration()
	{		
		myConfiguration.currentLevel = 1;
		myConfiguration.compostLevelHealth = 0;
		myConfiguration.sunLevelHealth = 0;
		myConfiguration.waterLevelHealth = 0;
		myConfiguration.plantName  = "Default";
		myConfiguration.playerName = "Default";
		myConfiguration.plantarTime = 0;
		myConfiguration.modelAuxChangeTime = 0;
		myConfiguration.waterAuxTime = 0;
		myConfiguration.modelType = 1;
		myConfiguration.notificationOn = false;
		// Time to creat our XML! 
		_data = GameStateXML.SerializeObject (myConfiguration, "PlantARData"); 
		// This is the final resulting XML from the serialization process 
		GameStateXML.CreateXML ("Configuration.xml", _data);
		Debug.Log (_data);
		
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
}
