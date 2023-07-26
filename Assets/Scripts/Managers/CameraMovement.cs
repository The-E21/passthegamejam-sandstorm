using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetTransform;
    public float lerpSpeed = 5;

    private Vector2 targetPosition;
    private float yOffset;

    private void Awake()
    {
        yOffset = transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!targetTransform)
            targetTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform)
            targetPosition = targetTransform.position;

        Vector2 newPos = transform.position;
        newPos = Vector2.Lerp(newPos, targetPosition, lerpSpeed * Time.deltaTime);

        transform.position = new Vector3(newPos.x, newPos.y, yOffset);
    }
}
