namespace XREdu.Unity.Character.Player.Movement.Settings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class PlayerMovementSettings : ScriptableObject
	{
		#region Unity Fields
		[Header("Settings")]
		[SerializeField]
		float walkSpeed = 10f;

		[SerializeField]
		float runSpeed = 20f;
		#endregion

		#region Properties
		public float RunSpeed => this.runSpeed;

		public float WalkSpeed => this.walkSpeed;
		#endregion
	}
}
