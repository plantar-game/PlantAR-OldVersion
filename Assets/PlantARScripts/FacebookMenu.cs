using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FacebookMenu : MonoBehaviour
{
	public GUIText TextFB;
	string stringToEdit = "¡Genial mi planta magica esta creciendo!";
	
	
	float salud = 100;
	int mainMenuX = 0;
	int mainMenuY = 0;
	int menuOpW = 0;
	int menuOpH = 0;
	
	bool bVisiblePlant = true;
	bool fst = true;
	
	// opPlant = null;
	
	public GUISkin achieveSkin = null;
	
	//Para agregar iconos
	public Texture2D iPlanta;
	public Texture2D iLogros;
	public Texture2D iFoto;
	public Texture2D iConfig;

	 #region FB.Init() example
   private bool isInit = false;

    private void CallFBInit()
    {
        FB.Init(OnInitComplete, OnHideUnity);
		fst = false;
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
        isInit = true;
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    #endregion

    #region FB.Login() example

    private void CallFBLogin()
    {
        FB.Login("email,publish_actions", Callback);
    }

    #endregion

    #region FB.PublishInstall() example

    private void CallFBPublishInstall()
    {
        FB.PublishInstall(PublishComplete);
    }

    private void PublishComplete(FBResult result)
    {
        Debug.Log("publish response: " + result.Text);
    }
	#endregion
    void Start()
    {
   
    }
	
	 void Awake()
    {        
		CallFBInit();
    }

	void OnGUI(){

		GUI.skin = achieveSkin;
		
		
		GUI.Box(new Rect(0,0,Screen.width*4/5+10,Screen.height), "",achieveSkin.GetStyle("PanelContent"));
			
		TextFB.text = "Inicia Sesion para compartir fotos";
		
		mainMenuX = Screen.width*4/5;
		mainMenuY = Screen.height;
		
		//if(FB.IsLoggedIn){
			TextFB.text = "Bienvenido "+ApplicationModel.Username;
			
			if (ApplicationModel.UserTexture != null) 
				GUI.DrawTexture(new Rect(0, 0, mainMenuX, mainMenuY*2/4), ApplicationModel.UserTexture);
			
			stringToEdit = GUI.TextArea(new Rect(mainMenuX*3/8,mainMenuY*2/4+30,mainMenuX*5/8, mainMenuY*2/4-50), stringToEdit, 200);
			
			//mainMenuX+260
			if(GUI.Button(new Rect(0, mainMenuY*2/4+30, mainMenuX*2/8,mainMenuY*1/8),"Compartir Foto")){				
				 StartCoroutine(TakeScreenshot(stringToEdit));
			}
		//}
		
		/*if(!FB.IsLoggedIn) {
		    if(GUI.Button(new Rect(0, 50, 100, 20),"Iniciar Sesion")){	
		        FB.Login("email,publish_actions", Callback);
				FB.PublishInstall(PublishComplete);
		    }
		}*/
		
		
			
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

    void Update()
    {
       
    }
	
	private IEnumerator TakeScreenshot(string caption) 
	{
	    yield return new WaitForEndOfFrame();
	
	    var wwwForm = new WWWForm();
		wwwForm.AddField("message", caption);
	    wwwForm.AddBinaryData("image", ApplicationModel.screenshot, "InteractiveConsole.png");
	
	    FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
	}
	
	void Callback(FBResult result) // store user profile pic
	{
	    if (result.Error != null)
	    {
	        Debug.LogError(result.Error);
	        return;
	    }

	}
	 
}