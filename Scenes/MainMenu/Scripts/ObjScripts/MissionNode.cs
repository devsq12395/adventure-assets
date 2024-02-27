
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionNode : MonoBehaviour {

	public string missionID;

	public void on_click (){
		MM_Mission.I.show (missionID);
	}
}