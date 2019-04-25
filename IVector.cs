namespace MathematicsX
{
	public interface IVector
	{
		int Dimension { get; }
		double this[int index] { get; set; }
	}
}
