using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonRope : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject ropeSegmentPrefab;
    public float segmentLength = 0.1f; // Length of each rope segment

    private void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("Player").transform.Find("HarpoonHand").transform.Find("HarpoonArm").transform.Find("RopeStart").transform;
        
    }

    private void Update()
    {
        endPoint = transform;

        if (startPoint == null || endPoint == null || ropeSegmentPrefab == null)
            return;

        // Calculate the distance and direction between the start and end points
        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;
        float distance = Vector3.Distance(start, end);
        Vector3 direction = (end - start).normalized;

        // Calculate the number of segments needed
        int segmentCount = Mathf.CeilToInt(distance / segmentLength);
        float remainingDistance = distance;

        // Destroy previous rope segments
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Create new rope segments
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = Instantiate(ropeSegmentPrefab, transform);
            Vector3 position = start + direction * segmentLength * i + direction * segmentLength / 2;
            segment.transform.position = position;
            segment.transform.up = direction; // Set the segment's orientation
            remainingDistance -= segmentLength;
        }
    }
}
