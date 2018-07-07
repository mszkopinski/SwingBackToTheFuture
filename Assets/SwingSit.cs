using UnityEngine;

public class SwingSit : MonoBehaviour
{
	[SerializeField] float baseSwingReleaseForce = 500f;
	[SerializeField] float swingVelocityDivider = 2f;
	[SerializeField] float playerEnterForceMultiplier = 2f;

	Swing parentSwing;
	GameObject placedPlayer;
	Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		parentSwing = GetComponentInParent<Swing>();

		StartGamePanel.Instance.GameStarted += (sender, args) => 
		{
			if (placedPlayer != null)
			{
				rb.AddForce((Random.Range(0, 10) > 5 ? transform.right : -transform.right) * .5f, ForceMode.Impulse);
			}
		};

		if (parentSwing == null)
			Debug.LogWarning("Parent swing is null");
	}

	void FixedUpdate() 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ReleasePlayer();
		}
	}

	void ReleasePlayer()
	{
		if (placedPlayer == null || !parentSwing.IsControlledSwing)
			return;

		Debug.Log("Release the kraken");

		placedPlayer.gameObject.transform.parent = null;

		var playerRigidbody = placedPlayer.GetComponent<Rigidbody>();

		if (playerRigidbody != null)
		{
			playerRigidbody.isKinematic = false;
			playerRigidbody.useGravity = true;
			var pushForce = CalculateForce() * (rb.velocity.x > 0 ? transform.right : -transform.right);

			playerRigidbody.AddForce(pushForce * Time.deltaTime, ForceMode.Impulse);
			playerRigidbody.angularVelocity = new Vector3(0f, 0f, rb.velocity.x / 3f);
		}

		placedPlayer = null;
	}

	float CalculateForce()
	{
		var squaredVelocityMagnitude = rb.velocity.sqrMagnitude;

		return baseSwingReleaseForce * squaredVelocityMagnitude / swingVelocityDivider;
	}

	void OnTriggerEnter(Collider col) 
	{
		var playerComponent = col.GetComponentInParent(typeof(Player));

		if (playerComponent != null && placedPlayer == null)
		{
			Debug.Log("Player is in range of sit");
			var player = playerComponent as Player;
			var playerRigidbody = player.GetComponent<Rigidbody>();

			if (playerRigidbody != null)
			{
				rb.AddForce(playerRigidbody.transform.forward * playerRigidbody.velocity.sqrMagnitude * playerEnterForceMultiplier);
			}

			player.SetPlayerPlacement(this.transform);
			placedPlayer = player.gameObject;
			parentSwing.IsControlledSwing = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		var playerComponent = col.GetComponentInParent(typeof(Player));

		if (playerComponent != null && placedPlayer == null)
		{
			Debug.Log("Player has left trigger");
			parentSwing.IsControlledSwing = false;
		}
	}
}

