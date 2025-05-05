using UnityEngine;
namespace Ragdoll.Physics_Animation
{
    // This class makes the ragdoll face a target (input direction) using physics forces.
    public class RotateSpine : MonoBehaviour
    {
        public float Force = 30; // The force applied to make the GameObject face the target.

        public Vector3 InputDirection;
        readonly Vector3 _offset = Vector3.forward; // Forward direction of GameObject, used as offset in the application of forces so that the GameObject stands still--but is able to use the torque in the rotation process.

        Rigidbody _rigidbody;

        void Start()
        { 
            /*
            * The maxAngularVelocity is set to 100 to limit the maximum speed of rotation.
            */
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxAngularVelocity = 100;
        }
        void FixedUpdate()
        {
            RotateTowardsForwardDirection(InputDirection);
        }
        void RotateTowardsForwardDirection(Vector3 inputDirection)
        {
            _rigidbody.AddForceAtPosition(inputDirection * (Time.deltaTime * Force), transform.TransformPoint(_offset),
                ForceMode.Impulse);
            _rigidbody.AddForceAtPosition(-inputDirection * (Time.deltaTime * Force), transform.TransformPoint(-_offset),
                ForceMode.Impulse);
        }
    }
}