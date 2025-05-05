using UnityEngine;

namespace Ragdoll.Physics_Animation
{
    public class FollowAnimation : MonoBehaviour
    {
        [SerializeField] Transform followDummy;
        
        Transform _followJoint;
        ConfigurableJoint _configurableJoint;
        Quaternion _originalRotation;

        void Awake()
        {
            _followJoint = FindChildRecursive(followDummy, name);
            _originalRotation = _followJoint.localRotation;
            _configurableJoint = GetComponent<ConfigurableJoint>();
        }

        void FixedUpdate()
        {
            _configurableJoint.targetRotation = Quaternion.Inverse(_followJoint.localRotation) * _originalRotation;
        }

        static Transform FindChildRecursive(Transform childTransform, string name)
        {
            for (int i = 0; i < childTransform.childCount; i++)
            {
                if (childTransform.GetChild(i).name == name) return childTransform.GetChild(i);
                var transformOfFoundChild = FindChildRecursive(childTransform.GetChild(i), name);
                if (transformOfFoundChild is not null) return transformOfFoundChild;
            }
            return null;
        }
    }
}