using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    // Class that represents objects joined by joints and control their rotation
    public class JointComponent : MonoBehaviour
    {
        [SerializeField] JointComponent prev;
        [SerializeField] JointComponent next;

        private Vector3 rotation;
        public Transform cube;
        public Quaternion cubeOffset;
        public Vector3 cubePosOffset;

        private void Awake()
        {
            if (!gameObject.name.Equals("Axis (C2)")) return;
            cubeOffset = Quaternion.Inverse(cube.rotation) * transform.rotation;
            Debug.Log(cubeOffset);
        }

        public void SetPrev(JointComponent p)
        {
            prev = p;
        }

        public void SetNext(JointComponent n)
        {
            next = n;
        }

        public bool HasPrev() { return prev != null; }
        public bool HasNext() { return next != null; }

        private void Update()
        {
            if (GetComponent<XROffsetGrabInteractable>().isGrabbed) // The object is beign grabed
            {
                var rot = GetComponent<XROffsetGrabInteractable>().interactor.transform.rotation * GetComponent<XROffsetGrabInteractable>().offset;
                rotation = rot.eulerAngles;
            }

            //if (gameObject.name.Equals("Axis (C2)"))
            //{
            //    var rot = (cube.rotation * cubeOffset);
            //    //rotation = CheckHardLimits(rot).eulerAngles;
            //    transform.rotation = rot;
            //}
            else
            {
                #region Rotation
                Quaternion prevRot = transform.rotation;
                Quaternion nextRot = transform.rotation;

                if (HasPrev()) { prevRot = FollowComponentRotation(prev); }
                if (HasNext()) { nextRot = FollowComponentRotation(next); }

                rotation = Quaternion.Slerp(prevRot, nextRot, 0.5f).eulerAngles;
                
                #endregion
            }

            transform.rotation = Quaternion.Euler(rotation);
        }

        // Returns the rotation neccesary to follow a component
        private Quaternion FollowComponentRotation(JointComponent jc)
        {
            Vector3 rotation = transform.rotation.eulerAngles;

            var angleX = Vector3.SignedAngle(transform.right, jc.transform.right, Vector3.right);
            var angleY = Vector3.SignedAngle(transform.up, jc.transform.up, Vector3.up);
            var angleZ = Vector3.SignedAngle(transform.forward, jc.transform.forward, Vector3.forward);

            // Tolerance
            if (Mathf.Abs(angleX) > 2 || Mathf.Abs(angleY) > 2 || Mathf.Abs(angleZ) > 1)
            {
                rotation = Quaternion.Slerp(transform.rotation, jc.transform.rotation, 0.5f).eulerAngles;
            }

            return Quaternion.Euler(rotation);
        }

        
        private Quaternion CheckHardLimits(Quaternion finalRot)
        {
            return new Quaternion();
        }

        public static Vector3 NormalizeAngles(Vector3 angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);

            return angles;
        }

        public static float NormalizeAngle(float angle)
        {
            return Mathf.Repeat(angle, 360f);
        }
    }
}