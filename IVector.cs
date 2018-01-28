namespace MathematicsX
{
	public interface IVector
	{
		int dimension { get; }
		double this[int index] { get; set; }
	}
}
