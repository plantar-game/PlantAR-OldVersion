using UnityEngine;
using System.Collections;
using System.Xml;

public class ApplicationModel : MonoBehaviour {

	static public int currentLevel = 0;
	static public float initialTime = 0;
	public static string Username = null;	
	static public bool firstTime = true;
	static public bool notificationsOn = true;
    static public byte[] screenshot;
	public static Texture UserTexture;
	static public PlantARData appData; 
}
