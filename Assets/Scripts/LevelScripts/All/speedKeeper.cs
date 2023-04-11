using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedKeeper : MonoBehaviour
{
    private Animator animator;
    public float speed = 1f;                // Starting speed of movement

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAnimator());
        StartCoroutine(IncreaseSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetAnimator()
    {
        yield return new WaitForSeconds(2);
        GameObject playerObject = GameObject.FindWithTag("Player");
        animator = playerObject.GetComponent<Animator>();
    }
    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(6);
        float increaseBy = 1f;
        animator.speed += 0.1f;
        speed += increaseBy;
        StartCoroutine(IncreaseSpeed());
    }
}
