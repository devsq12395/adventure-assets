
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	[Header ("Name of the node. Should be unique")]
	public string name;

	[Header ("TYPE: dialog, mission, map, to-menu")]
	[Header ("Custom types can also be made. i.e. dialog-vic")]
	[Header ("Check the call stack from MapNode.cs to see how this works")]
	[Header ("Empty Types are just intersections")]
	public string type;

	[Header ("NOTE that neighboring nodes x or y position should be equal to this node")]
	public List<GameObject> nextNodes;
}