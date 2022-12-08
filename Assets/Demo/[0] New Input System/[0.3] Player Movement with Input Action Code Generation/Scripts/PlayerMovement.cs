namespace XREdu.Unity.Demo.NewInputSystem.PlayerMovementWithInputActionCodeGeneration.Character.Player.Movement
{
	using UnityEngine;
	using UnityEngine.InputSystem;
	using XREdu.Unity.Character.Player.Movement.Settings;
	using XREdu.Unity.InputSystem;

	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{
		#region Unity Fields
		[Header("Settings")]
		[SerializeField]
		PlayerMovementSettings settings;
		#endregion

		#region Fields
		InputAction runInputAction;

		InputAction walkInputAction;
		#endregion

		#region Properties
		public bool IsRunPressed { get; set; }

		public bool IsWalkPressed { get; set; }
		#endregion

		#region Protected Properties
		protected CharacterController CharacterController { get; set; }

		protected InputAction RunInputAction
		{
			get => this.runInputAction ?? (this.runInputAction = new DefaultInputAction().Player.Run);
			set => this.runInputAction = value;
		}

		protected InputAction MoveInputAction
		{
			get => this.walkInputAction ?? (this.walkInputAction = new DefaultInputAction().Player.Walk);
			set => this.walkInputAction = value;
		}

		protected PlayerMovementSettings Settings { get => this.settings; set => this.settings = value; }

		protected Vector3 CurrentMovement { get; set; }
		#endregion

		#region Unity Methods
		/// <inheritdoc />
		protected virtual void OnEnable() => this.MoveInputAction.Enable();

		/// <inheritdoc />
		protected virtual void Start()
		{
			this.CharacterController = this.GetComponent<CharacterController>();

			this.MoveInputAction.canceled += this.HandleOnMove;
			this.MoveInputAction.performed += this.HandleOnMove;
			this.MoveInputAction.started += this.HandleOnMove;

			this.RunInputAction.canceled += this.HandleOnRun;
			this.RunInputAction.started += this.HandleOnRun;
		}

		/// <inheritdoc />
		protected virtual void FixedUpdate()
		{
			if (this.IsRunPressed)
			{
				this.Move(this.Settings.RunSpeed);

				return;
			}

			if (this.IsWalkPressed)
			{
				this.Move(this.Settings.WalkSpeed);
			}
		}

		/// <inheritdoc />
		protected virtual void OnDisable() => this.MoveInputAction.Disable();
		#endregion

		#region Protected Methods
		#region Event Handlers
		protected virtual void HandleOnMove(InputAction.CallbackContext callbackContext)
		{
			var walkInput = callbackContext.ReadValue<Vector2>();

			this.IsWalkPressed = walkInput.magnitude > 0;

			var currentMovement = this.CurrentMovement;
			currentMovement.x = walkInput.x;
			currentMovement.z = walkInput.y;
			this.CurrentMovement = currentMovement;
		}

		protected virtual void HandleOnRun(InputAction.CallbackContext callbackContext) =>
			this.IsRunPressed = callbackContext.ReadValueAsButton();
		#endregion

		protected virtual void Move(float speed) =>
			this.CharacterController.Move(speed * Time.deltaTime * this.CurrentMovement);
		#endregion
	}
}
