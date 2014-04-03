using UnityEngine;
using System.Collections;

public class AchievmentsMenu : MonoBehaviour {
	
	public AchievementsData  myData; 
   	private string _data; 
	
	int mainMenuX = 0;
	int mainMenuY = 0;
	int menuOpW = 0;
	int menuOpH = 0;
	
	int messageY = 0;
	int scrollHeight = 0;
		
	bool bVisiblePlant = true;
	
	//Para agregar iconos
	public Texture2D iPlanta;
	public Texture2D iLogros;
	public Texture2D iFoto;
	public Texture2D iConfig;
	
	public Vector2 scrollPosition = Vector2.zero;
	public GUISkin achieveSkin = null;
	
	void Awake () 
	{ 
			
		myData=new AchievementsData();
		
		if (GameStateXML.IfFileExist ("Achievements.xml"))
			LoadData ();
		else 
			CreateFile();
		
		int ndatos = myData.MessageStorage.Count;
				
		if (ndatos < 8)
				scrollHeight = Screen.height;
		if (ndatos >= 8 && ndatos < 15)
				scrollHeight = Screen.height *2;
		if (ndatos >= 15 )
				scrollHeight = Screen.height*3;
    }
	
	public string Data
	{
		get{return _data;}
		set{_data = value;}
	}
	
	public void CreateFile ()
	{		
		myData.MessageStorage.Add("Haz obtenido una semilla nueva!");
		
		// Time to creat our XML! 
		_data = GameStateXML.SerializeObject (myData, "AchievementsData"); 
		// This is the final resulting XML from the serialization process 
		GameStateXML.CreateXML ("Achievements.xml", _data);
		Debug.Log (_data);
		
	}
	
		
	public void LoadData()
	{
		_data = GameStateXML.LoadXML("Achievements.xml");
		if(_data.ToString() != "") 
	    {
	      myData = (AchievementsData)GameStateXML.DeserializeObject(_data,"AchievementsData");
	    }
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){	
	
		GUI.skin = achieveSkin;
		
		GUI.Box(new Rect(0,0,Screen.width*4/5+10,Screen.height), "",achieveSkin.GetStyle("PanelContent"));
		
		scrollPosition = GUI.BeginScrollView(new Rect(0, 0,Screen.width-Screen.width/5, Screen.height-Screen.height*1/50), scrollPosition, new Rect(0, 0, Screen.width-Screen.width*3/50-Screen.width/5, scrollHeight));
		
		float width1 = Screen.width-Screen.width/5;
		float width2 = width1*1/8;
		float width3 = width1*7/8;		
		float height1 = Screen.height*1/8;
		int i = 1;
		
		
        foreach (string ob in myData.MessageStorage){			
			
			string styleType = "";
						
			switch(i){		
				case 1:
					styleType = "badget1";
					break;
				case 2:
					styleType = "badget1";
					break;
				case 3:
					styleType = "badget1";
					break;
				case 4:
					styleType = "badget1";
					break;
				case 5:
					styleType = "badget1";
					break;
				case 6:
					styleType = "badget1";
					break;
				case 7:
					styleType = "badget1";
					break;
			}
			
			GUI.Label(new Rect(0,messageY, width2,height1), "", achieveSkin.GetStyle(styleType));
			GUI.Label(new Rect(width2,messageY,width3,height1), ob);
			
			messageY += Screen.height*1/8+Screen.height*1/32;
			i++;
		}
		GUI.EndScrollView();

		messageY = 0;
		
		
		//Background box Rect(x,y,width,height)
		//Main Menu		
		
		GUI.Box(new Rect(Screen.width-Screen.width/5,0,Screen.width/5,Screen.height), "",achieveSkin.GetStyle("PanelLeft"));
		
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
	}
}
