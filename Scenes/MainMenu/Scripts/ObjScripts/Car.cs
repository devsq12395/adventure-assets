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

    [Header("Audio")]
    public AudioClip engineSound;  // Engine noise clip
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer

    private Vector3 targetPosition;  // Target position for movement
    private Vector3 moveDirection;  // Direction of movement
    private Node nextNode;  // The next node to move to

    private void Awake()
    {
        I = this;
        spriteRenderer = GetComponent<SpriteRenderer>();  // Initialize the SpriteRenderer
        audioSource = GetComponent<AudioSource>();  // Initialize the AudioSource
    }

    private void Start()
    {
        setup();
    }

    private void Update()
    {
        if (isMoving)
        {
            // Check if a popup is showing; pause movement if true
            if (MainMenu.I.check_if_a_popup_is_showing())
            {
                return;  // Pause the movement
            }

            MoveCar();  // Continue the movement
        }
        else
        {
            HandleInput();  // Handle user input when not moving
        }
    }

    public void setup()
    {
        isStarted = true;
        string curNodeName = PlayerPrefs.GetString("start-node");
        find_and_set_node(curNodeName);

        if (!curNode)
        {
            find_and_set_node("1");
        }
    }

    public void find_and_set_node(string curNodeName)
    {
        // Find the node with the given name in ContMap.I.nodes
        foreach (Node node in MM_Map.I.nodes)
        {
            if (node.name == curNodeName)
            {
                Debug.Log($"set curNode to {curNodeName}");
                curNode = node;
                transform.position = curNode.transform.position;
                break;
            }
        }
    }

    private void HandleInput()
    {
        if (MainMenu.I.check_if_a_popup_is_showing()) return;  // Prevent input when a popup is showing

        // Check for input and move to the corresponding node
        if (Input.GetKeyDown(KeyCode.W)) { TryMoveToNextNode(Vector2.up); }
        else if (Input.GetKeyDown(KeyCode.S)) { TryMoveToNextNode(Vector2.down); }
        else if (Input.GetKeyDown(KeyCode.A)) { TryMoveToNextNode(Vector2.left); }
        else if (Input.GetKeyDown(KeyCode.D)) { TryMoveToNextNode(Vector2.right); }
        else if (Input.GetKeyDown(KeyCode.Return)) { enter_area(); }
    }

    public void enter_area()
    {
        if (MainMenu.I.check_if_a_popup_is_showing()) return;

        PlayerPrefs.SetString("start-node", curNode.name);
        MM_Map.I.select_node(curNode.function, curNode.subFunction);
    }

    private void TryMoveToNextNode(Vector2 direction)
    {
        if (curNode == null || curNode.nextNodes.Count == 0 || isMoving) return;

        // Find the next node in the desired direction
        nextNode = FindNextNode(direction);
        if (nextNode != null)
        {
            Debug.Log("moving to node");
            PrepareForMovement(nextNode);
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

    private void PrepareForMovement(Node nextNode)
    {
        isMoving = true;

        if (curNode.nodeBubble) { curNode.nodeBubble.hide(); }

        targetPosition = nextNode.transform.position;
        moveDirection = (targetPosition - transform.position).normalized;

        // Calculate the angle to rotate towards the next node
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Play engine sound
        if (audioSource != null && engineSound != null)
        {
            audioSource.clip = engineSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void MoveCar()
    {
        // Calculate the distance to the target node
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Move the car incrementally
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check if the car has reached or overshot the target
        if (Vector3.Distance(transform.position, targetPosition) >= distanceToTarget)
        {
            CompleteMovement();
        }
    }

    private void CompleteMovement()
    {
        // Snap to target position
        transform.position = targetPosition;
        curNode = nextNode;
        isMoving = false;

        // Stop engine sound after movement ends
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (curNode.function != "")
        {
            MM_TutKey.I.show_one("enter", true);
        }

        if (curNode.nodeBubble)
        {
            curNode.nodeBubble.show();
        }
    }
}
