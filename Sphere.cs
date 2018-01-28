namespace MathematicsX
{
	public struct Sphere<T> where T : struct, IVector
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
			min = VecX.Sub(center, radius);
			max = VecX.Add(center, radius);
		}

		public void CenterRadius(out T center, out double radius)
		{
			center = this.center;
			radius = this.radius;
		}


		public static readonly Sphere<T> unitSphere = new Sphere<T>(default(T), 1);
	}
}
