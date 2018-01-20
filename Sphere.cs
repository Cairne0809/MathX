namespace MathematicsX
{
	public struct Sphere<T> where T : IVector, new()
	{
		public T center;
		public double radius;

		public Sphere(T center, double radius)
		{
			this.center = center;
			this.radius = radius;
		}
	}
}
