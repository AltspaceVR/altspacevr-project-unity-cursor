using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	public static GameObject CurrentSelection { get; set; }

	public Material NormalMaterial;
	public Material HighlightMaterial;

	private MeshRenderer[] meshRenderers;

	void Start()
	{
		this.meshRenderers = GetComponentsInChildren<MeshRenderer>();
	}

	void Update () {
		if (gameObject == CurrentSelection)
		{
			if (meshRenderers[0].sharedMaterial != HighlightMaterial)
			{
				foreach (var renderer in meshRenderers)
				{
					renderer.sharedMaterial = HighlightMaterial;
				}
			}
		}
		else
		{
			if (meshRenderers[0].sharedMaterial != NormalMaterial)
			{
				foreach (var renderer in meshRenderers)
				{
					renderer.sharedMaterial = NormalMaterial;
				}
			}
		}
	}
}
