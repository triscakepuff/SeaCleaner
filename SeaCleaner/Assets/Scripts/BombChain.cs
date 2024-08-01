using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChain : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject ropeSegmentPrefab;
    public float segmentLength = 0.1f; // Length of each rope segment

    private bool isCompleted = false;
    private void Start()
    {

    }

    private void Update()
    {
        if (!isCompleted)
        {
            endPoint = transform;

            if (startPoint == null || endPoint == null || ropeSegmentPrefab == null)
                return;

            // Calculate the distance and direction between the start and end points
            Vector3 start = new Vector3(transform.position.x, startPoint.transform.position.y);
            Vector3 end = endPoint.position;
            float distance = Vector3.Distance(start, end);
            Vector3 direction = (end - start).normalized;

            // Calculate the number of segments needed
            int segmentCount = Mathf.CeilToInt(distance / segmentLength);

            // Create new rope segments
            for (int i = 0; i < segmentCount; i++)
            {
                GameObject segment = Instantiate(ropeSegmentPrefab, transform);
                Vector3 position = start + direction * segmentLength * i + direction * segmentLength / 2;
                segment.transform.position = position;
                segment.transform.up = direction; // Set the segment's orientation
            }

            isCompleted = true;
        }
        


    }
}
