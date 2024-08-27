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
        if (isMoving) return; // Prevent input if already moving

        // Check for input and move to the corresponding node
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            TryMoveToNextNode(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            TryMoveToNextNode(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            TryMoveToNextNode(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            TryMoveToNextNode(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
        	enter_area ();
        }
    }

    public void enter_area (){
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
                if (Vector2.Dot(toNode.normalized, direction) > 0.9f) // Check if node is in the same direction
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
        isMoving = true;

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
    }
}
