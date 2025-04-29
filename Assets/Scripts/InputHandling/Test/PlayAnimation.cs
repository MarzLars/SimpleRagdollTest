using System;
using UnityEngine;
namespace Ragdoll.Logic
{
    [Serializable]
    public struct AnimationKeyPair
    {
        public KeyCode KeyCode;
        public string AnimationName;

    }

    public class PlayAnimation : MonoBehaviour
    {
        public AnimationKeyPair[] AnimationKeyPairs;

        Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            foreach (var animationKeyPair in AnimationKeyPairs)
                if (Input.GetKeyDown(animationKeyPair.KeyCode))
                    _animator.CrossFade(animationKeyPair.AnimationName, 0.2f);
        }
    }
}
