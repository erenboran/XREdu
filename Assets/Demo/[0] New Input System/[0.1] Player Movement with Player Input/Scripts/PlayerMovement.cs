namespace XREdu.Unity.Demo.NewInputSystem.PlayerMovementWithPlayerInput.Character.Player.Movement
{
	using UnityEngine;
	using UnityEngine.InputSystem;
	using XREdu.Unity.Character.Player.Movement.Settings;

	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{
		#region Unity Fields
		[Header("Settings")]
		[SerializeField]
		PlayerMovementSettings settings;
		#endregion

		#region Properties
		public bool IsRunPressed { get; set; }

		public bool IsWalkPressed { get; set; }
		#endregion

		#region Protected Properties
		protected CharacterController CharacterController { get; set; }

		protected PlayerMovementSettings Settings { get => this.settings; set => this.settings = value; }

		protected Vector3 CurrentMovement { get; set; }
		#endregion

		#region Unity Methods
		/// <inheritdoc />
		protected virtual void Start()
		{
			this.CharacterController = this.GetComponent<CharacterController>();
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
		#endregion

		#region Public Methods
		#region Event Handlers
		public virtual void HandleOnMove(InputAction.CallbackContext callbackContext)
		{
			var walkInput = callbackContext.ReadValue<Vector2>();

			this.IsWalkPressed = walkInput.magnitude > 0;

			var currentMovement = this.CurrentMovement;
			currentMovement.x = walkInput.x;
			currentMovement.z = walkInput.y;
			this.CurrentMovement = currentMovement;
		}

		public virtual void HandleOnRun(InputAction.CallbackContext callbackContext) =>
			this.IsRunPressed = callbackContext.ReadValueAsButton();
		#endregion
		#endregion

		#region Protected Methods
		protected virtual void Move(float speed) =>
			this.CharacterController.Move(speed * Time.deltaTime * this.CurrentMovement);
		#endregion
	}
}
