/// <summary>
/// Represents a point in 3-space as a location on a sphere by recording the radial distance
/// and the azimuth and elevation angles.
/// </summary>
public struct SphericalPoint
{
	/// <summary>
	/// The radius in meters; i.e. the distance of the point from the origin.
	/// </summary>
	public readonly float Radius;

	/// <summary>
	/// The angle in radians between the point's projection on the X-Y plane and the Y-axis,
	/// i.e. the angle the point is rotated around the Z-axis; the longitude.
	/// </summary>
	public readonly float AzimuthAngle;

	/// <summary>
	/// The angle in radians between the point's projection on the Y-Z plane and the Y-axis,
	/// i.e. the angle the point is elevated from the horizon; the latitude.
	/// </summary>
	public readonly float ElevationAngle;

	public SphericalPoint(float radius, float elevation, float azimuth)
	{
		Radius = radius;
		ElevationAngle = elevation;
		AzimuthAngle = azimuth;
	}
}
