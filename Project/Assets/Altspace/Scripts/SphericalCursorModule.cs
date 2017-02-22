using UnityEngine;
using System.Collections;

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

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	private Vector3 DefaultCursorScale = new Vector3(10.0f, 10.0f, 10.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Sphere radius to project cursor onto if no raycast hit.
	private const float SphereRadius = 1000.0f;

	//Scale and rotate modes
	private GameObject []Modes;

	//To know which mode is chosen on scroll
	private uint scrollIndex = 0;

	//Number of modes
	private const uint NUM_MODES = 2;

	enum EModes{SCALE = 0, ROTATE = 1};

	private bool LeftMouseDown = false;
	private bool RightMouseDown = false;
	private bool firstRot = true;
	
	private Vector3 ObjectClickScale = Vector3.zero;
	private Quaternion ObjectClickRotation = Quaternion.identity;
	
	void Awake() {
		Screen.showCursor = false;
		Cursor = transform.Find("Cursor").gameObject;
		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
        CursorMeshRenderer.renderer.material.color = new Color(0.0f, 0.8f, 1.0f); 
		Modes = new GameObject[2];
		Modes[(int)EModes.SCALE] = GameObject.Find ("Cursor/Scale");
		Modes[(int)EModes.ROTATE] = GameObject.Find ("Cursor/Rotate");
    }	

	void Update()
	{
		var cursorHit = new RaycastHit();

		// Ray to mouse position on screen in world space
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Create a raycast to a max distance of MaxDistance and applies only to objects in the layer ColliderMask
		Physics.Raycast (mouseRay, out cursorHit, MaxDistance, ColliderMask);

		float ScrollValue = Input.GetAxis ("Mouse ScrollWheel");

		// Update highlighted object based upon the raycast.
		if (cursorHit.collider != null)
		{
			CursorMeshRenderer.enabled = false;

			// Detect mouse scroll
			if(ScrollValue!=0f)
				scrollIndex = ++scrollIndex % NUM_MODES;

			Modes[scrollIndex].SetActive(true);
			Modes[(scrollIndex + 1) % NUM_MODES].SetActive(false);

			Selectable.CurrentSelection = cursorHit.collider.gameObject;

			// Get exact point of intersection of ray with collider
			Cursor.transform.position = cursorHit.point;
			// Calculate and apply new scale
			float scaleValue = (Vector3.Distance(cursorHit.point, gameObject.transform.position)  + 1.0f ) / 2.0f;
			Cursor.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

			//Handle Scale and Rotate functions based on LMB, RMB and Mouse scroll
			HandleMouseInput();
		}
		else
		{
			CursorMeshRenderer.enabled = true;
			Modes[scrollIndex].SetActive(false);
			Modes[(scrollIndex + 1) % NUM_MODES].SetActive(false);
			Selectable.CurrentSelection = null;

			// If no object intersection, then map cursor to a virtual sphere of radius SphereRadius
			Ray dirRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Cursor.transform.position = dirRay.GetPoint(SphereRadius / DistanceScaleFactor);
			Cursor.transform.localScale = DefaultCursorScale * DistanceScaleFactor;	

		}

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void HandleMouseInput()
	{
		if (Input.GetMouseButtonDown(0) && LeftMouseDown == false)
		{
			LeftMouseDown = true;
			ObjectClickScale = Selectable.CurrentSelection.transform.localScale;
			ObjectClickRotation = Selectable.CurrentSelection.transform.rotation;
		}
		if (Input.GetMouseButtonUp(0))
		{
			LeftMouseDown = false;
			firstRot = true;
		}
		
		if (Input.GetMouseButtonDown(1) && LeftMouseDown == false)
		{
			RightMouseDown = true;
			ObjectClickScale = Selectable.CurrentSelection.transform.localScale;
			ObjectClickRotation = Selectable.CurrentSelection.transform.rotation;
		}
		if (Input.GetMouseButtonUp(1))
		{
			RightMouseDown = false;
			firstRot = true;
		}
		
		if((scrollIndex== (uint)EModes.SCALE)&& LeftMouseDown == true)
		{
			Selectable.CurrentSelection.transform.localScale = ObjectClickScale * 1.2f;
		}
		
		if((scrollIndex== (uint)EModes.SCALE)&& RightMouseDown == true)
		{
			Selectable.CurrentSelection.transform.localScale = ObjectClickScale * 0.8f;
		}
		
		
		if((scrollIndex== (uint)EModes.ROTATE)&& LeftMouseDown == true && firstRot == true)
		{
			Selectable.CurrentSelection.transform.RotateAround(Selectable.CurrentSelection.transform.position, Vector3.up,  30.0f);
			firstRot = false;
		}
		
		if((scrollIndex== (uint)EModes.ROTATE)&& RightMouseDown == true && firstRot == true)
		{
			Selectable.CurrentSelection.transform.RotateAround(Selectable.CurrentSelection.transform.position, Vector3.up,  -30.0f);
			firstRot = false;
		}

	}
}
