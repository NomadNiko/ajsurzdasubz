using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    private float scaleMultiplier = 1.7f; // This determines how much to scale the objects.
    public float moveSpeed = 15f;
    public float moveDuration = 0.3f;
    public float rotateSpeed = 20f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Animator animator;
    public gameManager gameMan;
    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;
    private bool isScaledUp = false;
    private Vector3 originalSize;

    private void Start()
    {
        originalSize = transform.localScale;
        animator = GetComponent<Animator>();
        gameMan = FindObjectOfType<gameManager>();
    }

    void Update()
    {
        AdjustSize();
        // check if the object is not moving
        if (gameMan.isGameOver)
        {
            // set the "isWalking" parameter to false to trigger the idle animation
            animator.SetBool("isRunning", false);
        }
        else
        {
            ControlPlayer();
        }

    }
    void ControlPlayer()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // If so, we're going to set the startPos to the first touch's position, 
            startPos = Input.touches[0].position;
            // ... and set fingerDown to true to start checking the direction of the swipe.
            fingerDown = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving && transform.position.x > -6)
        {
            // Move the player to the left
            targetPosition = transform.position + new Vector3(-1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
        else if (fingerDown && Input.touches[0].position.x <= startPos.x - pixelDistToDetect && !isMoving && transform.position.x > -6)
        {
            fingerDown = false;
            targetPosition = transform.position + new Vector3(-1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving && transform.position.x < 6)
        {
            // Move the player to the right
            targetPosition = transform.position + new Vector3(1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
        else if (fingerDown && Input.touches[0].position.x >= startPos.x + pixelDistToDetect && !isMoving && transform.position.x < 6)
        {
            fingerDown = false;
            targetPosition = transform.position + new Vector3(1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
    }

    IEnumerator MovePlayer(Vector3 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;
        Vector3 direction = (targetPosition - startingPosition).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = startingRotation;
        isMoving = false;
    }
    void AdjustSize()
    {
        // Check if the child's position has an x-coordinate of 0.
        if (transform.position.x == 0 && !isScaledUp)
        {
            // Increase the child's scale by the scaleMultiplier.
            transform.localScale *= scaleMultiplier;
            isScaledUp = true;
        }
        if (transform.position.x != 0)
        {
            transform.localScale = originalSize;
            isScaledUp = false;
        }

    }
}
