
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour {

	public string type, val;

	public void on_click (){
		MM_Map.I.select_node (type, val);
	}
}