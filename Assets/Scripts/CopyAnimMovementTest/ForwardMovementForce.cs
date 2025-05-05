using UnityEngine;

namespace Ragdoll.Physics_Animation
{
    //This class constantly applies a forward force to the Ragdoll. Just for showcase.
    //TODO: Integrate this kind of logic to be handled by the CharacterControllerV2 class. To keep the RotateSpine class separate from the forward movement.
    public class ForwardMovementForce : MonoBehaviour
    {
        public float ForwardForce;

        Rigidbody _rigidBody;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (ForwardForce is < -0.001f or > 0.001f)
                _rigidBody.AddForce(transform.forward.WithY(0).normalized * (ForwardForce * Time.deltaTime), ForceMode.Impulse);
        }
    }
}
