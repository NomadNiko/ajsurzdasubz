using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    // Float variables
    private Vector3 targetPosition;
    private Vector2 startPos;
    private Vector3 originalSize;
    private Animator animator;
    private gameManager gameMan;
    private bool isfingerDown;
    private bool isMoving = false;
    private bool isMiddle = false;
    private bool isScaledUp = false;
    private bool isScaling = false; // Whether the object is currently scaling up or down
    private float scaleMultiplier = 1.5f; // This determines how much to scale the objects.
    private float moveSpeed = 5f;
    private float moveDuration = 1f;
    private float rotateSpeed = 40f;
    private float scaleDuration = 1f; // The duration over which to interpolate the scale
    private float scaleTimer = 0.0f; // The current scale interpolation timer
    private int pixelDistToDetect = 20;


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
        if (!isfingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // If so, we're going to set the startPos to the first touch's position, 
            startPos = Input.touches[0].position;
            // ... and set fingerDown to true to start checking the direction of the swipe.
            isfingerDown = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving && transform.position.x > -6)
        {
            // Move the player to the left
            targetPosition = transform.position + new Vector3(-1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
        else if (isfingerDown && Input.touches[0].position.x <= startPos.x - pixelDistToDetect && !isMoving && transform.position.x > -6)
        {
            isfingerDown = false;
            targetPosition = transform.position + new Vector3(-1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving && transform.position.x < 6)
        {
            // Move the player to the right
            targetPosition = transform.position + new Vector3(1f, 0f, 0f);
            StartCoroutine(MovePlayer(targetPosition));
        }
        else if (isfingerDown && Input.touches[0].position.x >= startPos.x + pixelDistToDetect && !isMoving && transform.position.x < 6)
        {
            isfingerDown = false;
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

        // Gradually rotate the player back to its starting rotation over a period of time.
        float rotationDuration = 0.25f; // Adjust this to control the duration of the rotation animation.
        elapsedTime = 0f;
        Quaternion finalRotation = startingRotation;

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Lerp(targetRotation, finalRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = finalRotation;
        isMoving = false;
    }
    void AdjustSize()
    {
        // Check if the child's position has an x-coordinate of 0.
        if (transform.position.x == 0 && !isMiddle)
        {
            // Set the flag to true to indicate that the child is in the middle section.
            isMiddle = true;

            // Gradually increase the child's scale by the scaleMultiplier over a period of time.
            StartCoroutine(GrowOverTime());
        }
        else if (transform.position.x != 0 && isMiddle)
        {
            // Set the flag to false to indicate that the child is no longer in the middle section.
            isMiddle = false;

            // Gradually decrease the child's scale back to its original size over a period of time.
            StartCoroutine(ShrinkOverTime());
        }
    }

    IEnumerator GrowOverTime()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;
        float duration = 0.5f; // Adjust this to control the duration of the growth animation.
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure that the scale is set to the target value exactly.
    }

    IEnumerator ShrinkOverTime()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalSize;
        float duration = 0.5f; // Adjust this to control the duration of the shrinking animation.
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure that the scale is set to the target value exactly.
    }
}
