
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	[Header ("Name of the node. Should be unique")]
	public string name;

	[Header ("The area name when entering this node with the ENTER key")]
	[Header ("Will be used by MM_Map.I.select_node, as main value")]
	public string areaName;

	[Header ("VAL is for special codes. i.e. dialog")]
	[Header ("Will be used by MM_Map.I.select_node, as side value")]
	public string val;

	[Header ("NOTE that neighboring nodes x or y position should be equal to this node")]
	public List<GameObject> nextNodes;
}