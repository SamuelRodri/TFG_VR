using System;
using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using TFG.Utils;
using UnityEngine;

namespace TFG.Behaviour.Column
{
    public class MountableObject : MonoBehaviour
    {
        private Vector3 finalPosition;
        private Quaternion finalRotation;

        [SerializeField] float maxDistance;

        [SerializeField] float movementSpeed;
        [SerializeField] float rotationalSpeed;


        private bool isMoving;
        private bool exploited;

        private Vector3 playerPosition;

        private void Start()
        {
            SimulationController.OnMountMode += GetRandomPosition;

            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        private void GetRandomPosition()
        {
            Vector3 pos = MathOperations.GetRandomVector3(-maxDistance, maxDistance);
            finalPosition = playerPosition + pos;

            Quaternion rot = MathOperations.GetRandomQuaternion();
            finalRotation = rot;
            isMoving = true;
        }

        private void Update()
        {
            if(transform.position != finalPosition && isMoving)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    finalPosition, 
                    movementSpeed * Time.deltaTime);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    finalRotation,
                    rotationalSpeed * Time.deltaTime);

                return;
            }

            if(transform.position == finalPosition)
            {
                isMoving = false;
                exploited = true;
            }

            if (!isMoving && exploited)
            {
                if(GetComponent<CartilaginousJoint>())
                {
                    var jointComponent = GetComponent<JointComponent>();

                    float actualDistance = Vector3.Distance(transform.position, jointComponent.prev.transform.position);
                    float initialDistance = jointComponent.initialDistancePrev;

                    if (actualDistance <= initialDistance)
                    {
                        GetComponent<CartilaginousJoint>().RestoreLinksPrev();
                    }

                    actualDistance = Vector3.Distance(transform.position, jointComponent.next.transform.position);
                    initialDistance = jointComponent.initialDistanceNext;

                    if (actualDistance <= initialDistance)
                    {
                        GetComponent<CartilaginousJoint>().RestoreLinksNext();
                    }
                }
            }
        }
    }
}