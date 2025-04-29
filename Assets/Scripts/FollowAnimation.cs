using UnityEngine;
using UnityEngine.Serialization;

namespace Ragdoll.Physics_Animation
{

    public class FollowAnimation : MonoBehaviour
    {

        [SerializeField] Transform followDummy;
        [HideInInspector] public new Rigidbody rigidbody;
        Transform _followJoint;
        ConfigurableJoint _joint;
        Quaternion _originalRotation;

        void Awake()
        {
            _followJoint = FindChildRecursive(followDummy, name);
            _originalRotation = _followJoint.localRotation;
            _joint = GetComponent<ConfigurableJoint>();
            rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _joint.targetRotation = Quaternion.Inverse(_followJoint.localRotation) * _originalRotation;
        }

        static Transform FindChildRecursive(Transform childTransform, string name)
        {
            for (int i = 0; i < childTransform.childCount; i++)
            {
                if (childTransform.GetChild(i).name == name) return childTransform.GetChild(i);
                // Recursive call
                var transformOfFoundChild = FindChildRecursive(childTransform.GetChild(i), name);
                if (transformOfFoundChild is not null) return transformOfFoundChild;
            }

            return null;
        }
    }
}