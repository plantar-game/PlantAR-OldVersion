using UnityEngine;
using System.Collections;
 
public class ModelSwapper : MonoBehaviour {
     
    public TrackableBehaviour theTrackable;
	
    private bool mSwapModel = false;
	InitialMainMenu initialmm;
     
     
    // Use this for initialization
    void Start () {
        if (theTrackable == null)
        {
            Debug.Log ("Warning: Trackable not set !!");
        }
		
		GameObject thePlayer = GameObject.Find("ARCamera");
        initialmm = thePlayer.GetComponent<InitialMainMenu>();
		
		if (theTrackable != null) {
			SwapModel("Model"+initialmm.myData.modelType);
		}
    }
     
    // Update is called once per frame
    void Update () {		
        if (initialmm.modelChange == true && theTrackable != null) {			
			SwapModel("Model"+initialmm.myData.modelType);			
          	initialmm.modelChange = false;
        }
    }
     
    void OnGUI() {
		
    }
     
    private void SwapModel(string modelName) {
         
        GameObject trackableGameObject = theTrackable.gameObject;
         
        //disable any pre-existing augmentation
        for (int i = 0; i < trackableGameObject.transform.GetChildCount(); i++) 
        {
            Transform child = trackableGameObject.transform.GetChild(i);
			if (string.Compare(child.name, modelName) == 0){
				child.gameObject.active = true;					
			}else{
				child.gameObject.active = false;					
			}
        }
    }
}