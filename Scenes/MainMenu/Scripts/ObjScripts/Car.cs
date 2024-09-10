using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public static Car I;
    public Node curNode;
    public bool isMoving, isStarted;

    [Header("Speed at which the car moves between nodes")]
    public float moveSpeed = 5f;

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer

    private void Awake()
    {
        I = this;
        spriteRenderer = GetComponent<SpriteRenderer>();  // Initialize the SpriteRenderer
    }

    private void Start()
    {
        setup();
    }

    private void Update()
    {
        HandleInput();
    }

    public void setup()
    {
        isStarted = true;
        string curNodeName = PlayerPrefs.GetString("start-node");

        // Find the node with the given name in ContMap.I.nodes
        foreach (Node node in MM_Map.I.nodes)
        {
            if (node.name == curNodeName)
            {
                curNode = node;
                transform.position = curNode.transform.position;
                break;
            }
        }
    }

    private void HandleInput()
    {
        if (isMoving) {
            MM_TutKey.I.show_one ("enter", false);
            return; 
        }

        // Check for input and move to the corresponding node
        if (Input.GetKeyDown(KeyCode.W)) {
            TryMoveToNextNode(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            TryMoveToNextNode(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            TryMoveToNextNode(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            TryMoveToNextNode(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
        	enter_area ();
        }
    }

    public void enter_area (){
        if (MainMenu.I.check_if_a_popup_is_showing ()) return;

        PlayerPrefs.SetString("start-node", curNode.name);
    	MM_Map.I.select_node (curNode.areaName, curNode.val);
    }

    private void TryMoveToNextNode(Vector2 direction)
    {
        if (curNode == null || curNode.nextNodes.Count == 0)
            return;

        // Find the next node in the desired direction
        Node nextNode = FindNextNode(direction);
        if (nextNode != null)
        {
            Debug.Log ("moving to node");
            StartCoroutine(MoveToNode(nextNode));
        }
    }

    private Node FindNextNode(Vector2 direction)
    {
        Node closestNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject nodeObj in curNode.nextNodes)
        {
            Node node = nodeObj.GetComponent<Node>();
            if (node != null)
            {
                Vector2 toNode = (Vector2)node.transform.position - (Vector2)curNode.transform.position;
                Debug.Log (Vector2.Dot(toNode.normalized, direction));
                if (Vector2.Dot(toNode.normalized, direction) > 0.4f) // Check if node is in the same direction
                {
                    float distance = toNode.magnitude;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNode = node;
                    }
                }
            }
        }

        return closestNode;
    }

    private IEnumerator MoveToNode(Node nextNode)
    {
        if (curNode.nodeBubble){
            curNode.nodeBubble.hide ();
        }

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = nextNode.transform.position;
        float elapsedTime = 0f;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);

        while (elapsedTime < journeyLength / moveSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime * moveSpeed) / journeyLength);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        curNode = nextNode;
        isMoving = false;

        if (curNode.areaName != ""){
            MM_TutKey.I.show_one ("enter", true);
        }
        if (curNode.nodeBubble){
            curNode.nodeBubble.show ();
        }
    }
}
