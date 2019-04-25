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

		public void MinMax(out T min, out T max)
		{
			min = center.Sub(radius);
			max = center.Add(radius);
		}

		public static readonly Sphere<T> unitSphere = new Sphere<T>(default(T), 1);
	}
}
