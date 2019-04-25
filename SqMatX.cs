namespace MathematicsX
{
	public static class SqMatX
	{
		public static bool IsNaM(this ISquareMatrix m)
		{
			int len = m.Length;
			for (int i = 0; i < len; i++)
				if (double.IsNaN(m[i])) return true;
			return false;
		}

		public static T Copy<T>(T m) where T : ISquareMatrix, new()
		{
			T nm = new T();
			int len = m.Length;
			for (int i = 0; i < len; i++)
				nm[i] = m[i];
			return nm;
		}

		public static T Neg<T>(T m) where T : ISquareMatrix
		{
			int len = m.Length;
			for (int i = 0; i < len; i++)
				m[i] = -m[i];
			return m;
		}

		public static T Inv<T>(T m) where T : ISquareMatrix
		{
			int len = m.Length;
			for (int i = 0; i < len; i++)
				m[i] = 1 / m[i];
			return m;
		}

		public static T Add<T>(T lhs, T rhs) where T : ISquareMatrix
		{
			int len = lhs.Length;
			for (int i = 0; i < len; i++)
				lhs[i] += rhs[i];
			return lhs;
		}

		public static T Sub<T>(T lhs, T rhs) where T : ISquareMatrix
		{
			int len = lhs.Length;
			for (int i = 0; i < len; i++)
				lhs[i] -= rhs[i];
			return lhs;
		}

		public static T Mul<T>(T lhs, double rhs) where T : ISquareMatrix
		{
			int len = lhs.Length;
			for (int i = 0; i < len; i++)
				lhs[i] *= rhs;
			return lhs;
		}
		public static T Mul<T>(T lhs, T rhs) where T : ISquareMatrix
		{
			int len = lhs.Length;
			int col = lhs.Column;
			for (int i = 0; i < len; i++)
			{
				double dot = 0;
				for (int j = 0; j < col; j++)
					dot += lhs[j + i / col] * rhs[i % col + j * col];
				lhs[i] = dot;
			}
			return lhs;
		}
		public static T Mul<T>(this ISquareMatrix lhs, T rhs) where T : IVector
		{
			int dim = rhs.Dimension;
			for (int i = 0; i < dim; i++)
			{
				double dot = 0;
				for (int j = 0; j < dim; j++)
					dot += lhs[j + i] * rhs[j];
				rhs[i] = dot;
			}
			return rhs;
		}

		public static T Transpose<T>(T m) where T : ISquareMatrix
		{
			int len = m.Length;
			int col = m.Column;
			for (int i = 0; i < len; i++)
			{
				int j = i / col + i % col * col;
				if (i == j) continue;
				double t = m[i];
				m[i] = m[j];
				m[j] = t;
			}
			return m;
		}

		public static SqMat3 Transpose(this SqMat3 m)
		{
			SqMat3 nm;
			nm.m00 = m.m00; nm.m01 = m.m10; nm.m02 = m.m20;
			nm.m10 = m.m01; nm.m11 = m.m11; nm.m12 = m.m21;
			nm.m20 = m.m02; nm.m21 = m.m12; nm.m22 = m.m22;
			return nm;
		}
		public static SqMat4 Transpose(this SqMat4 m)
		{
			SqMat4 nm;
			nm.m00 = m.m00; nm.m01 = m.m10; nm.m02 = m.m20; nm.m03 = m.m30;
			nm.m10 = m.m01; nm.m11 = m.m11; nm.m12 = m.m21; nm.m13 = m.m31;
			nm.m20 = m.m02; nm.m21 = m.m12; nm.m22 = m.m22; nm.m23 = m.m32;
			nm.m30 = m.m03; nm.m31 = m.m13; nm.m32 = m.m23; nm.m33 = m.m33;
			return nm;
		}
		
	}
}
