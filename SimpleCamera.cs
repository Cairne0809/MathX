using System;

namespace MathematicsX
{
	public class SimpleCamera
	{
		private double m_halfFOV = 22.5 * MathX.D2R;
		private Vec2 m_halfWH = new Vec2(100, 100);
		private Vec3 m_position = Vec3.zero;
		private Quat m_rotation = Quat.identity;

		public Vec3 Position
		{
			get { return m_position; }
			set { m_position = value; }
		}
		public Quat Rotation
		{
			get { return m_rotation; }
			set { m_rotation = value; }
		}

		public double FieldOfView
		{
			get { return m_halfFOV * 2 * MathX.R2D; }
			set { m_halfFOV = MathX.Clamp(value, 1, 179) * 0.5 * MathX.D2R; }
		}
		public double ScreenWidth
		{
			get { return m_halfWH.x * 2; }
			set { m_halfWH.x = Math.Abs(value) * 0.5; }
		}
		public double ScreenHeight
		{
			get { return m_halfWH.y * 2; }
			set { m_halfWH.y = Math.Abs(value) * 0.5; }
		}

		public void RotateAround(Vec3 target, Quat rotation)
		{
			double dist = VecX.Distance(m_position, target);
			m_rotation *= rotation;
			m_position = m_rotation * new Vec3(0, 0, -dist) + target;
		}
		public void RotateAround(Vec3 target, Vec3 euler)
		{
			RotateAround(target, Quat.FromEuler(euler));
		}

		public bool WorldToScreen(Vec3 point, out Vec3 result)
		{
			Vec3 screenPos = ~m_rotation * (point - m_position);
			double tan = Math.Tan(m_halfFOV);
			double mul = tan * screenPos.z;
			if (mul > 0)
			{
				screenPos.xy = m_halfWH.y / mul * screenPos.xy + m_halfWH;
				result = screenPos;
				return true;
			}
			else
			{
				result = default(Vec3);
				return false;
			}
		}

		public bool WorldToScreen(double length, Vec3 pos, out double result)
		{
			Vec3 screenPos = ~m_rotation * (pos - m_position);
			double tan = Math.Tan(m_halfFOV);
			double mul = tan * screenPos.z;
			if (mul > 0)
			{
				result = length / mul * m_halfWH.y;
				return true;
			}
			else
			{
				result = default(double);
				return false;
			}
		}

	}
}
