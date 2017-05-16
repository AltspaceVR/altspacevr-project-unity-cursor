using UnityEngine;

public class SphericalCursorModule : MonoBehaviour {
	// This is a sensitivity parameter that should adjust how sensitive the mouse control is.
	public float Sensitivity;

	// This is a scale factor that determines how much to scale down the cursor based on its collision distance.
	public float DistanceScaleFactor;

	// This is the layer mask to use when performing the ray cast for the objects.
	// The furniture in the room is in layer 8, everything else is not.
	private const int ColliderMask = (1 << 8);

	// This is the Cursor game object. Your job is to update its transform on each frame.
	private GameObject Cursor;

	// The current location of the cursor on a virtual sphere surrounding the player.
	private SphericalPoint CursorLocation;

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	private Vector3 DefaultCursorScale = new Vector3(10.0f, 10.0f, 10.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Sphere radius to project cursor onto if no raycast hit.
	private const float SphereRadius = 1000.0f;

    private void Awake() {
		Cursor = transform.Find("Cursor").gameObject;
		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
		CursorMeshRenderer.material.color = new Color(0.0f, 0.8f, 1.0f);
    }

	/// <summary>
	/// Updates this transform to be positioned at the given location.
	/// </summary>
	private void SetPosition(Transform transform, SphericalPoint location)
	{
		// TODO: Implement this.
	}

	private void Update()
	{
		// TODO: Handle mouse movement to update CursorLocation.
		// TODO: Perform ray cast to find object cursor is pointing at.

		var cursorHit = new RaycastHit();/* Your cursor hit code should set this properly. */;

		// Update position of the cursor to be the new computed position.
		SetPosition(Cursor.transform, CursorLocation);

		// Update highlighted object based upon the raycast.
		if (cursorHit.collider != null)
		{
			Selectable.CurrentSelection = cursorHit.collider.gameObject;
		}
		else
		{
			Selectable.CurrentSelection = null;
		}
	}
}
