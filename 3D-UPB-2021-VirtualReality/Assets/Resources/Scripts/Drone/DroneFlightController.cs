using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFlightController : MonoBehaviour
{
    private const float BASE_TERRAIN_HEIGHT = 25f;

    private const float LINEAR_VELOCITY_MIN = 2f;
    private const float LINEAR_VELOCITY_MAX = 6f;

    private const float GROUND_DISTANCE_MIN = BASE_TERRAIN_HEIGHT + 1.5f;
    private const float GROUND_DISTANCE_MAX = BASE_TERRAIN_HEIGHT + 6f;

    private const float SWAY_FREQUENCY_MIN = 1f, SWAY_AMPLITUDE_MIN = 1f;
    private const float SWAY_FREQUENCY_MAX = 3f, SWAY_AMPLITUDE_MAX = 3f;

    private float linearVelocity;
    private float groundDistance;
    private float xAxisSwayFreq;
    private float xAxisSwayAmp;
    private float yAxisSwayFreq;
    private float yAxisSwayAmp;
    private Vector3 targetPosition;
    private float totalTime = 0f; 

    // Start is called before the first frame update
    void Start()
    {
        // Generate random vars
        linearVelocity = Random.Range(LINEAR_VELOCITY_MIN, LINEAR_VELOCITY_MAX);
        groundDistance = Random.Range(GROUND_DISTANCE_MIN, GROUND_DISTANCE_MAX);
        xAxisSwayFreq = Random.Range(SWAY_FREQUENCY_MIN, SWAY_FREQUENCY_MAX);
        xAxisSwayAmp = Random.Range(SWAY_AMPLITUDE_MIN, SWAY_AMPLITUDE_MAX);
        yAxisSwayFreq = Random.Range(SWAY_FREQUENCY_MIN, SWAY_FREQUENCY_MAX);
        yAxisSwayAmp = Random.Range(SWAY_AMPLITUDE_MIN, SWAY_AMPLITUDE_MAX);

        // Get target position
        targetPosition = transform.parent.GetComponent<DroneSpawner>().GetDroneTargetPosition();

        // Adjust target y (such that the drone only goes after the x and z values)
        targetPosition.y = groundDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // Accumulate time (for sways)
        totalTime += Time.deltaTime;

        // Look at target
        transform.LookAt(targetPosition);

        // Height difference (actual versus where it should be)
        float heightDiff = transform.position.y - groundDistance;

        // Adjust drone height
        transform.position -= new Vector3(0f, heightDiff * Time.deltaTime, 0f);

        // Move direction
        Vector3 dir = Vector3.Normalize(targetPosition - transform.position);

        // Move linearly
        transform.position += (dir * linearVelocity * Time.deltaTime);

        // Make drone sway side to side
        float sideToSideSway = Mathf.Sin(totalTime * xAxisSwayFreq) * Time.deltaTime * xAxisSwayAmp;
        transform.position += sideToSideSway * transform.right;

        // Make drone sway up and down
        float upDownSway = Mathf.Sin(totalTime * yAxisSwayFreq) * Time.deltaTime * yAxisSwayAmp;
        transform.position += upDownSway * transform.up;
    }

    public float GetDistanceToTarget()
    {
        return Vector3.Distance(transform.position, targetPosition);
    }
}
