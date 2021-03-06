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
		public bool holdjump;
		public bool shooting;
		private InputAction holdJumpAction;
		private InputAction holdShootAction;
		private InputAction mouseWheelAction;
		PlayerInput input;
		public bool sprint;
		public int newWeaponDirection =0;
		public float mouseWheel;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

		private void Awake()
		{
			input = GetComponent<PlayerInput>();
			holdJumpAction = input.currentActionMap.FindAction("Jump");
			holdJumpAction.started += OnHoldJumping;
			holdJumpAction.canceled += OnReleaseJump;
			holdShootAction = input.currentActionMap.FindAction("Shoot");
			holdShootAction.started += OnHoldShoot;
			holdShootAction.canceled += OnReleaseShoot;
			mouseWheelAction = input.currentActionMap.FindAction("MouseWheel");
			mouseWheelAction.started += OnMouseWheeling;
		}

        private void OnDisable()
        {
			holdJumpAction.started -= OnHoldJumping;
			holdJumpAction.canceled -= OnReleaseJump;
			holdShootAction.started -= OnHoldShoot;
			holdShootAction.canceled -= OnReleaseShoot;
			mouseWheelAction.started -= OnMouseWheeling;
		}

		private void OnEnable()
		{
			holdJumpAction.started += OnHoldJumping;
			holdJumpAction.canceled += OnReleaseJump;
			holdShootAction.started += OnHoldShoot;
			holdShootAction.canceled += OnReleaseShoot;
			mouseWheelAction.started += OnMouseWheeling;
		}


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

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnHoldJump(InputValue value)
		{
		}

		public void OnEscape(InputValue value)
        {
			Application.Quit();
        }

		public void OnHoldJumping(InputAction.CallbackContext obj)
		{
			holdjump = true;
		}

		public void OnMouseWheeling(InputAction.CallbackContext obj)
		{
			NewWeaponInput(obj.ReadValue<Vector2>().y);
		}

		public void OnReleaseJump(InputAction.CallbackContext obj)
		{
			holdjump = false;
		}

		public void OnHoldShoot(InputAction.CallbackContext obj)
		{
			shooting = true;
		}
		public void OnReleaseShoot(InputAction.CallbackContext obj)
		{
			shooting = false;
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

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void NewWeaponInput(float direction)
		{
			mouseWheel = direction;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}