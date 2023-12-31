using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BasketAgent : Agent
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform basket;
    [SerializeField] private Transform backPanel; // Reference to the BackPanel's Mesh Collider
    [SerializeField] private Transform hoop; // Reference to the Hoop's Mesh Collider


    public override void OnEpisodeBegin(){
        Debug.Log("OnEpisodeBegin called");
        System.Random random = new System.Random();
        float initialForce = 25f;
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

        float ballX, ballY, ballZ;
        float dirX, dirY, dirZ;

        ballX = (float)random.NextDouble() * -9f;
        ballZ = (float)random.NextDouble() * 38f -31f;
        ballY = (float)random.NextDouble() * 6f + 14f;
        ball.position = new Vector3(ballX, ballY, ballZ);


        dirX = (basket.position.x - ball.position.x)/ Math.Max(Math.Abs(basket.position.x), Math.Abs(ball.position.x));
        dirY = (float)random.NextDouble();
        dirZ = (basket.position.z - ball.position.z)/ Math.Max(Math.Abs(basket.position.z), Math.Abs(ball.position.z));

        Vector3 customDirection = new Vector3(dirX, dirY, dirZ);
        Vector3 normalizedDirection = customDirection.normalized;

        ballRigidbody.AddForce(normalizedDirection * initialForce, ForceMode.Impulse);

        // int i = 10;
        // while(i>0){
        //     float moveSpeed = 5f;
        //     Vector3 move = new Vector3(dirX, dirY, 0f);
        //     Debug.Log($"transform: {transform.position.x}, {transform.position.y}, {transform.position.z}");
        //     Debug.Log($"move: {move}");
        //     transform.position += move * moveSpeed* Time.deltaTime;

        //     Debug.Log($"new transoform: {transform.position}");
        //     i--;
        // }
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("CollectObservation called");
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

        // Orientation of the basket (6 floats)
        sensor.AddObservation(basket.position);
        sensor.AddObservation(basket.rotation);
       
        // Relative position of the ball to the basket (3 floats)
        sensor.AddObservation(ball.transform.position - basket.transform.position);

        // Velocity of the ball (3 floats)
        sensor.AddObservation(ballRigidbody.velocity);
        // 12 floats total
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Debug.Log("OnActionReceived called");
        float dirX = actionBuffers.ContinuousActions[0];
        float dirY = actionBuffers.ContinuousActions[1];
        float moveSpeed = 5f;
        // transform.localPositon += new Vector3(moveX,moveY,0f) * moveSpeed * Time.deltaTime;

        // var rotX = 3f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
        // var rotY = 3f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
        // var rotZ = 3f * Mathf.Clamp(actionBuffers.ContinuousActions[2], -1f, 1f);
        // var dirX = Mathf.Clamp(actionBuffers.ContinuousActions[3], -1f, 1f);
        // var dirY = Mathf.Clamp(actionBuffers.ContinuousActions[4], -1f, 1f);
        // var dirZ = Mathf.Clamp(actionBuffers.ContinuousActions[5], -1f, 1f);
        // var moveSpeed = Mathf.Clamp(actionBuffers.ContinuousActions[6], 0f, 3f);
        
        // basket.transform.Rotate(new Vector3(0, 0, 1), rotZ);
        // basket.transform.Rotate(new Vector3(1, 0, 0), rotX);
        // basket.transform.Rotate(new Vector3(0, 1, 0), rotY);

        Vector3 move = new Vector3(dirX, dirY, 0f);

        // // Add a debug log to check if the basket is moving
        Debug.Log($"Moving Basket: {move}");

        // basketRB.MovePosition(basketRB.position + move * moveSpeed * Time.deltaTime);
        transform.position += move * moveSpeed* Time.deltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // var continuousActions = actionsOut.ContinuousActions;

        // // Rotation controls
        // var rotX = Input.GetKey(KeyCode.Alpha1) ? 1f : 0f;  // Yaw left

        // // Check for a combination button (Shift + 1) for Yaw right
        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     rotX = Input.GetKey(KeyCode.Alpha1) ? -1f : 0f;  // Yaw right
        // }

        // var rotY = Input.GetKey(KeyCode.Alpha2) ? 1f : 0f;

        // // Check for a combination button (Shift + 2) for additional rotation
        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     rotY = Input.GetKey(KeyCode.Alpha2) ? -1f : 0f;
        // }

        // var rotZ = Input.GetKey(KeyCode.Alpha3) ? 1f : 0f;

        // // Check for a combination button (Shift + 3) for additional rotation
        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     rotZ = Input.GetKey(KeyCode.Alpha3) ? -1f : 0f;
        // }

        // // Translation controls
        // var dirX = Input.GetKey(KeyCode.W) ? 1f : 0f;  // Forward
        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     dirX = Input.GetKey(KeyCode.W) ? -1f : 0f;  // Up
        // }

        // var dirY = Input.GetKey(KeyCode.S) ? 1f : 0f;  // Backward

        // // Check for a combination button (Shift + S) for additional translation
        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     dirY = Input.GetKey(KeyCode.S) ? -1f : 0f;  // Up
        // }

        // var dirZ = Input.GetKey(KeyCode.A) ? 1f : 0f;  // Left

        // // Check for a combination button (Shift + A) for additional translation
        // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        // {
        //     dirZ = Input.GetKey(KeyCode.A) ? -1f : 0f;  // Down
        // }

        // // var dirZ = Input.GetKey(KeyCode.A) ? 1f : 0f; 


        // Debug.Log($"key : {rotX}");
        // // Apply the values to continuous actions
        // continuousActions[0] = rotX;
        // continuousActions[1] = rotY;
        // continuousActions[2] = rotZ;
        // continuousActions[3] = dirX;
        // continuousActions[4] = dirY;
        // continuousActions[5] = dirZ;

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }



}

