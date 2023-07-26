using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 20;
    public float decelleration = 0.85f;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseHandler.Instance.paused && !ExperianceManager.Instance.isUpgrading)
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;

        velocity += movementSpeed * Time.fixedDeltaTime * input.normalized;
        velocity *= decelleration;

        rb.velocity = velocity;
    }
}
