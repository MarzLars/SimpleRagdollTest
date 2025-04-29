using System.Linq;
using UnityEngine;
namespace Ragdoll.Utility
{
    public class IgnoreAllCollidersOnObjects : MonoBehaviour
    {
        public GameObject[] OtherObjects;
        public bool IgnoreAllChildColliders = true;

        void Awake()
        {
            var colliderComponentsOne = OtherObjects.SelectMany(otherObject => otherObject.GetComponents<Collider>()).ToList();
            var colliderComponentsTwo = GetComponentsInChildren<Collider>();

            foreach (var otherCollider in colliderComponentsOne)
                if (IgnoreAllChildColliders)
                    foreach (var thisCollider in colliderComponentsTwo)
                        Physics.IgnoreCollision(otherCollider, thisCollider, true);
                else
                    Physics.IgnoreCollision(otherCollider, GetComponentInChildren<Collider>(), true);
        }
    }
}