
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour {

	[Header ("ID: of the node. Used to enable/disable this node if unlocked")]
	public string ID;

	[Header ("TYPE: dialog, mission, map, to-menu")]
	[Header ("Custom types can also be made. i.e. dialog-vic")]
	[Header ("Check the call stack from MapNode.cs to see how this works")]
	public string type;

	[Header ("VAL: dialog or mission ID")]
	public string val;

	public void on_click (){
		MM_Map.I.select_node (type, val);
	}
}