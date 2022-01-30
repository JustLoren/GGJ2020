using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool interact;
		public bool swapPlaces;
		public bool sprint;
		public bool exit;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnExit(InputValue value)
        {
			ExitInput(value.isPressed);
        }

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSwapPlaces(InputValue value)
        {
			SwapPlacesInput(value.isPressed);
        }

		public void OnInteract(InputValue value)
        {
			InteractInput(value.isPressed);
        }

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void ExitInput(bool newExitState)
        {
			exit = newExitState;
        }

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;			
		}

		public void InteractInput(bool newInteractState)
        {
			interact = newInteractState;
        }

		public void SwapPlacesInput(bool newSwapPlacesState)
        {
			swapPlaces = newSwapPlacesState;
        }

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

        private void OnDestroy()
        {
			SetCursorState(false);
        }

        private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}