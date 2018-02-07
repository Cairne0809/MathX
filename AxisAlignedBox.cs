namespace MathematicsX
{
	public struct AxisAlignedBox<T> where T : struct, IVector
	{
		T m_pos;
		T m_size;

		public T pos
		{
			get { return m_pos; }
			set { m_pos = value; }
		}
		public T size
		{
			get { return m_size; }
			set { m_size = VecX.Abs(value); }
		}
		public T min { get { return m_pos; } }
		public T max { get { return VecX.Add(m_pos, m_size); } }
		public T center { get { return VecX.Add(VecX.Mul(m_size, 0.5), m_pos); } }
		public T extends { get { return VecX.Mul(m_size, 0.5); } }
		
		public AxisAlignedBox(T pos, T size)
		{
			m_pos = pos;
			m_size = VecX.Abs(size);
		}

	}
}
