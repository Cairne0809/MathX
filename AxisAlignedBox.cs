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
			set { VecX.Abs(value, ref m_size); }
		}
		public T min { get { return m_pos; } }
		public T max { get { return VecX.Add(m_pos, m_size); } }
		public T center { get { T temp = VecX.Mul(m_size, 0.5); VecX.Add(m_pos, temp, ref temp); return temp; } }
		public T extends { get { return VecX.Mul(m_size, 0.5); } }
		
		public AxisAlignedBox(T pos, T size)
		{
			m_pos = pos;
			m_size = size;
			VecX.Abs(size, ref m_size);
		}

	}
}
