using System;

namespace MathematicsX
{
	public class SimpleCamera
	{
		double m_halfFOV = 22.5 * MathX.Deg2Rad;
		double m_halfW = 100;
		double m_halfH = 100;

		public Vec3 position = Vec3.zero;
		public Quat rotation = Quat.identity;

		public double fieldOfView
		{
			get { return m_halfFOV * 2 * MathX.Rad2Deg; }
			set { m_halfFOV = MathX.Clamp(value, 1, 179) * 0.5 * MathX.Deg2Rad; }
		}
		public double screenWidth
		{
			get { return m_halfW * 2; }
			set { m_halfW = Math.Abs(value) * 0.5; }
		}
		public double screenHeight
		{
			get { return m_halfH * 2; }
			set { m_halfH = Math.Abs(value) * 0.5; }
		}

		public void RotateAround(Vec3 target, Quat rotation)
		{
			double dist = VecX.Distance(position, target);
			this.rotation *= rotation;
			position = this.rotation * new Vec3(0, 0, -dist) + target;
		}
		public void RotateAround(Vec3 target, Vec3 euler)
		{
			RotateAround(target, Quat.FromEuler(euler));
		}

		public bool WorldToScreen(Vec3 point, out Vec3 result)
		{
			Vec3 screenPos = ~rotation * (point - position);
			double tan = Math.Tan(m_halfFOV);
			double mul = tan * screenPos.z;
			if (mul > 0)
			{
				Vec2 halfWH = new Vec2(m_halfW, m_halfH);
				screenPos.xy = halfWH.y / mul * screenPos.xy + halfWH;
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
			Vec3 screenPos = ~rotation * (pos - position);
			double tan = Math.Tan(m_halfFOV);
			double mul = tan * screenPos.z;
			if (mul > 0)
			{
				result = length / mul * m_halfH;
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
