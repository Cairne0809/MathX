namespace MathematicsX
{
	public struct AxisAlignedBox<T> where T : IVector, new()
	{
		private T m_pos;
		private T m_size;
		
		public AxisAlignedBox(T pos, T size)
		{
			m_pos = pos;
			m_size = size;
		}

		public T Pos
		{
			get { return m_pos; }
			set { m_pos = value; }
		}
		public T Size
		{
			get { return m_size; }
			set { m_size = VecX.Abs(value); }
		}
		public T Min { get { return m_pos; } }
		public T Max { get { return VecX.Add(m_pos, m_size); } }
		public T Center { get { return VecX.Add(m_pos, VecX.Mul(m_size, 0.5)); } }
		public T Extends { get { return VecX.Mul(m_size, 0.5); } }
	}
}
