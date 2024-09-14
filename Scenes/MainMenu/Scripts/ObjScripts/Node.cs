
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	[Header ("Name of the node. Should be unique")]
	public string name;

	[Header ("The area's function when entering this node with the ENTER key")]
	[Header ("Will be used by MM_Map.I.select_node, as main value")]
	public string function;

	[Header ("Will be used by MM_Map.I.select_node, as side value")]
	public string subFunction;

	[Header ("The Node Bubble for this node")]
	public NodeBubble nodeBubble;

	[Header ("NOTE that neighboring nodes x or y position should be equal to this node")]
	public List<GameObject> nextNodes;
}