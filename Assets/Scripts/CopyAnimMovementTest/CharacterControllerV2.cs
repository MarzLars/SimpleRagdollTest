using UnityEngine;
using InputHandling;
namespace Ragdoll.Logic
{
    using Physics_Animation;
    public class CharacterControllerV2 : MonoBehaviour
    {
        [SerializeField] RotateSpine rotateSpine;
        [SerializeField] InputReader inputReader;

        void OnEnable()
        {
            inputReader.EnablePlayerActions();
        }
        void Update()
        {
            Vector2 moveInput = inputReader.moveInput;
            var inputDirection = new Vector3(moveInput.x, 0, moveInput.y);

            if (inputDirection.Equals(Vector3.zero)) return;
            inputDirection.Normalize();
            rotateSpine.InputDirection = Vector3.RotateTowards(rotateSpine.InputDirection, inputDirection, Time.deltaTime * 6, Time.deltaTime * 6);
        }
        void OnDisable()
        {
            inputReader.DisablePlayerActions();
        }
    }
}
