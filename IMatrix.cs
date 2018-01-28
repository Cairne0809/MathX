namespace MathematicsX
{
	public interface IMatrix
	{
		int row { get; }
		int column { get; }
		double this[int index] { get; set; }
		double this[int row, int column] { get; set; }
	}
}
