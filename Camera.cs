using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public class Camera
	{
		public Vec3 position = Vec3.zero;
		public Quat rotation = Quat.identity;
		public double f = 10;
		public double screenWidth = 100;
		public double screenHeight = 100;

		public void RotateAround(Vec3 target, Quat rotation)
		{
			double dist = Vec3.Distance(position, target);
			this.rotation *= rotation;
			position = this.rotation * new Vec3(0, 0, -dist) + target;
		}
		public void RotateAround(Vec3 target, Vec3 euler)
		{
			RotateAround(target, Quat.FromEuler(euler));
		}

		public bool WorldToScreen(Vec3 point, out Vec3 opt)
		{
			Vec3 screenPos = ~rotation * (point - position);
			double div = screenPos.z - f;
			if (div <= 0)
			{
				opt = default(Vec3);
				return false;
			}
			else
			{
				double mul = f / div;
				screenPos.x *= mul;
				screenPos.y *= mul;
				screenPos.x += screenWidth / 2;
				screenPos.y += screenHeight / 2;
				opt = screenPos;
				return true;
			}
		}

		public bool WorldToScreen(double length, Vec3 pos, out double opt)
		{
			Vec3 screenPos = ~rotation * (pos - position);
			double div = screenPos.z - f;
			if (div <= 0)
			{
				opt = default(double);
				return false;
			}
			else
			{
				opt = length * f / div;
				return true;
			}
		}

	}
}
