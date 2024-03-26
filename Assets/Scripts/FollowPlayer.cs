using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FollowPlayer : MonoBehaviour
    {
        public Transform player;  // Reference to the player's Transform
        public Vector3 offset = new Vector3(0f, 2f, -5f);  // Adjust this offset as needed
        public float smoothSpeed = 0.125f;  // Adjust this to control the smoothness of the camera follow

        void FixedUpdate()
        {
            if (player != null)
            {
                // Calculate a new position for the camera with the offset
                Vector3 desiredPosition = player.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

                // Set the new position for the camera
                transform.position = smoothedPosition;

            }
        }
    }
}

