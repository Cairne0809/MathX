using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public abstract class Function
	{
		public abstract Function Clone();
		public abstract double Evaluate(double x);
		public abstract Function Derivative();

		public bool IsElem() { return this is ElementaryFunction; }
		public bool IsComp() { return this is CompositeFunction; }
		public bool IsConst() { return this is ConstFunc; }
		public bool IsX() { return this is XFunc; }
		public bool IsPow() { return this is PowFunc; }
		public bool IsExp() { return this is ExpFunc; }
		public bool IsLog() { return this is LogFunc; }
		public bool IsSin() { return this is SinFunc; }
		public bool IsCos() { return this is CosFunc; }
		public bool IsTan() { return this is TanFunc; }

		public ElementaryFunction AsElem() { return this as ElementaryFunction; }
		public CompositeFunction AsComp() { return this as CompositeFunction; }
		public ConstFunc AsConst() { return this as ConstFunc; }
		public XFunc AsX() { return this as XFunc; }
		public PowFunc AsPow() { return this as PowFunc; }
		public ExpFunc AsExp() { return this as ExpFunc; }
		public LogFunc AsLog() { return this as LogFunc; }
		public SinFunc AsSin() { return this as SinFunc; }
		public CosFunc AsCos() { return this as CosFunc; }
		public TanFunc AsTan() { return this as TanFunc; }

		protected static string CoefStr(double value)
		{
			if (value == Math.E) return "e*";
			if (value == 1) return "";
			if (value == -1) return "-";
			return value + "*";
		}
	}
	
	public abstract class ElementaryFunction : Function
	{
		protected double m_coef;
		public virtual double Coef
		{
			get { return m_coef; }
		}
		public virtual ElementaryFunction SetCoef(double value)
		{
			m_coef = value;
			return this;
		}
		public virtual ElementaryFunction ScaleCoef(double value)
		{
			m_coef *= value;
			return this;
		}
		public abstract string NestExpr(string expr);
	}

	public enum CompType
	{
		Add = 0,
		Sub = 1,
		Mul = 2,
		Div = 3,
		Exp = 4,
		Nest = 5
	}

	public abstract class CompositeFunction : Function
	{
		protected CompType m_type;
		public virtual CompType CompType { get { return m_type; } }
		public abstract CompositeFunction PrevComp(CompType type, Function f);
		public abstract CompositeFunction NextComp(CompType type, Function f);
		public abstract Function Simplify();
	}

	public class ConstFunc : ElementaryFunction
	{
		public ConstFunc(double value)
		{
			m_coef = value;
		}
		public override Function Clone()
		{
			return new ConstFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef;
		}
		public override Function Derivative()
		{
			return new ConstFunc(0);
		}
		public override string ToString()
		{
			return m_coef == Math.E ? "e" : m_coef.ToString();
		}
		public override string NestExpr(string expr)
		{
			return ToString();
		}
	}

	public class XFunc : ElementaryFunction
	{
		public XFunc()
		{
			m_coef = 1;
		}
		public XFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new XFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * x;
		}
		public override Function Derivative()
		{
			return new ConstFunc(m_coef);
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "x";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + expr;
		}
	}

	public class PowFunc : ElementaryFunction
	{
		private double m_exponent;
		public PowFunc()
		{
			m_coef = 1;
			m_exponent = 2;
		}
		public PowFunc(double exponent)
		{
			m_coef = 1;
			m_exponent = exponent;
		}
		public PowFunc(double coefficient, double exponent)
		{
			m_coef = coefficient;
			m_exponent = exponent;
		}
		public override Function Clone()
		{
			return new PowFunc(m_coef, m_exponent);
		}
		public double Exponent
		{
			get { return m_exponent; }
			set { m_exponent = value; }
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Pow(x, m_exponent);
		}
		public override Function Derivative()
		{
			if (m_coef == 0 || m_exponent == 0) return new ConstFunc(0);
			if (m_exponent == 2) return new XFunc(2 * m_coef);
			if (m_exponent == 1) return new ConstFunc(m_coef);
			return new PowFunc(m_coef * m_exponent, m_exponent - 1);
		}
		public override string ToString()
		{
			return CoefStr(m_coef)
				+ (m_exponent == 1 ? "x" : m_exponent == Math.E ? "x^e" : "x^" + m_exponent);
		}
		public override string NestExpr(string expr)
		{
			if (m_exponent == 1)
			{
				return CoefStr(m_coef) + expr;
			}
			return CoefStr(m_coef) + "(" + expr
				+ (m_exponent == Math.E ? ")^e" : ")^" + m_exponent);
		}
	}

	public class ExpFunc : ElementaryFunction
	{
		private double m_base;
		public ExpFunc()
		{
			m_coef = 1;
			m_base = Math.E;
		}
		public ExpFunc(double baseNum)
		{
			m_coef = 1;
			m_base = baseNum;
		}
		public ExpFunc(double coefficient, double baseNum)
		{
			m_coef = coefficient;
			m_base = baseNum;
		}
		public override Function Clone()
		{
			return new ExpFunc(m_coef, m_base);
		}
		public double Base
		{
			get { return m_base; }
			set { m_base = value; }
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Pow(m_base, x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0 || m_base == 1 || m_base == 0) return new ConstFunc(0);
			if (m_base == Math.E) return new ExpFunc(m_coef, m_base);
			return new ExpFunc(m_coef * Math.Log(m_base), m_base);
		}
		public override string ToString()
		{
			return CoefStr(m_coef)
				+ (m_base == Math.E ? "e^x" : m_base + "^x");
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef)
				+ (m_base == Math.E ? "e^(" : m_base + "^(") + expr + ")";
		}
	}

	public class LogFunc : ElementaryFunction
	{
		public LogFunc()
		{
			m_coef = 1;
		}
		public LogFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new LogFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Log(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new PowFunc(m_coef, -1);
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "log(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "log("+ expr + ")";
		}
	}

	public class SinFunc : ElementaryFunction
	{
		public SinFunc()
		{
			m_coef = 1;
		}
		public SinFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new SinFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Sin(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CosFunc(m_coef);
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "sin(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "sin(" + expr + ")";
		}
	}

	public class CosFunc : ElementaryFunction
	{
		public CosFunc()
		{
			m_coef = 1;
		}
		public CosFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new CosFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Cos(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new SinFunc(-m_coef);
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "cos(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "cos(" + expr + ")";
		}
	}

	public class TanFunc : ElementaryFunction
	{
		public TanFunc()
		{
			m_coef = 1;
		}
		public TanFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new TanFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Tan(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Nest, new PowFunc(m_coef, 2), new SecFunc());
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "tan(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "tan(" + expr + ")";
		}
	}

	public class CotFunc : ElementaryFunction
	{
		public CotFunc()
		{
			m_coef = 1;
		}
		public CotFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new CotFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef / Math.Tan(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Nest, new PowFunc(-m_coef, 2), new CscFunc());
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "cot(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "cot(" + expr + ")";
		}
	}

	public class SecFunc : ElementaryFunction
	{
		public SecFunc()
		{
			m_coef = 1;
		}
		public SecFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new SecFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef / Math.Cos(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Mul, new SecFunc(m_coef), new TanFunc());
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "sec(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "sec(" + expr + ")";
		}
	}

	public class CscFunc : ElementaryFunction
	{
		public CscFunc()
		{
			m_coef = 1;
		}
		public CscFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new CscFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef / Math.Sin(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Mul, new CscFunc(-m_coef), new CotFunc());
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "csc(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "csc(" + expr + ")";
		}
	}

	public class AsinFunc : ElementaryFunction
	{
		public AsinFunc()
		{
			m_coef = 1;
		}
		public AsinFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new AsinFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Asin(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Nest, new PowFunc(m_coef, -0.5), 
				new CompFunc(CompType.Add, new ConstFunc(1), new PowFunc(-1, 2)));
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "asin(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "asin(" + expr + ")";
		}
	}

	public class AcosFunc : ElementaryFunction
	{
		public AcosFunc()
		{
			m_coef = 1;
		}
		public AcosFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new AcosFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Acos(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Nest, new PowFunc(-m_coef, -0.5),
				new CompFunc(CompType.Add, new ConstFunc(1), new PowFunc(-1, 2)));
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "acos(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "acos(" + expr + ")";
		}
	}

	public class AtanFunc : ElementaryFunction
	{
		public AtanFunc()
		{
			m_coef = 1;
		}
		public AtanFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new AtanFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * Math.Atan(x);
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Nest, new PowFunc(m_coef, -1),
				new CompFunc(CompType.Add, new ConstFunc(1), new PowFunc(1, 2)));
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "atan(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "atan(" + expr + ")";
		}
	}

	public class AcotFunc : ElementaryFunction
	{
		public AcotFunc()
		{
			m_coef = 1;
		}
		public AcotFunc(double coefficient)
		{
			m_coef = coefficient;
		}
		public override Function Clone()
		{
			return new AcotFunc(m_coef);
		}
		public override double Evaluate(double x)
		{
			return m_coef * (Math.PI * 0.5 - Math.Atan(x));
		}
		public override Function Derivative()
		{
			if (m_coef == 0) return new ConstFunc(0);
			return new CompFunc(CompType.Nest, new PowFunc(-m_coef, -1),
				new CompFunc(CompType.Add, new ConstFunc(1), new PowFunc(1, 2)));
		}
		public override string ToString()
		{
			return CoefStr(m_coef) + "acot(x)";
		}
		public override string NestExpr(string expr)
		{
			return CoefStr(m_coef) + "acot(" + expr + ")";
		}
	}


	public class CompFunc : CompositeFunction
	{
		public Function lhs;
		public Function rhs;

		public CompFunc(CompType type, Function lhs, Function rhs)
		{
			if (type == CompType.Nest && lhs.IsComp())
			{
				throw new Exception();
			}
			m_type = type;
			this.lhs = lhs;
			this.rhs = rhs;
		}

		public override Function Clone()
		{
			return new CompFunc(m_type, lhs.Clone(), rhs.Clone());
		}

		private string ExprBarcket(int level, CompType type, string expr)
		{
			int itype = (int)type;
			for (int i = 0; i < level; i++)
			{
				if (i >= itype)
				{
					return "(" + expr + ")";
				}
			}
			return expr;
		}

		public override string ToString()
		{
			string le = lhs.ToString();
			string re = rhs.ToString();
			switch (m_type)
			{
				case CompType.Sub:
					if (rhs.IsComp())
					{
						re = ExprBarcket(2, rhs.AsComp().CompType, re);
					}
					return le + "-" + re;
				case CompType.Mul:
					if (lhs.IsComp())
					{
						le = ExprBarcket(2, lhs.AsComp().CompType, le);
					}
					if (rhs.IsComp())
					{
						re = ExprBarcket(2, rhs.AsComp().CompType, re);
					}
					return le + "*" + re;
				case CompType.Div:
					if (lhs.IsComp())
					{
						le = ExprBarcket(2, lhs.AsComp().CompType, le);
					}
					if (rhs.IsComp())
					{
						re = ExprBarcket(3, rhs.AsComp().CompType, re);
					}
					else
					{
						re = "(" + re + ")";
					}
					return le + "/" + re;
				case CompType.Exp:
					return "(" + le + ")^(" + re + ")";
				case CompType.Nest:
					if (lhs.IsElem())
					{
						return lhs.AsElem().NestExpr(rhs.ToString());
					}
					throw new Exception();
				default:
					return le + "+" + re;
			}
		}

		public override double Evaluate(double x)
		{
			switch (m_type)
			{
				case CompType.Sub: return lhs.Evaluate(x) - rhs.Evaluate(x);
				case CompType.Mul: return lhs.Evaluate(x) * rhs.Evaluate(x);
				case CompType.Div: return lhs.Evaluate(x) / rhs.Evaluate(x);
				case CompType.Exp: return Math.Pow(lhs.Evaluate(x), rhs.Evaluate(x));
				case CompType.Nest: return lhs.Evaluate(rhs.Evaluate(x));
				default: return lhs.Evaluate(x) + rhs.Evaluate(x);
			}
		}

		public override Function Derivative()
		{
			switch (m_type)
			{
				case CompType.Add:
				case CompType.Sub:
					return new CompFunc(m_type, lhs.Derivative(), rhs.Derivative())
						.Simplify();
				case CompType.Mul:
					return new CompFunc(CompType.Add,
						new CompFunc(CompType.Mul, lhs.Derivative(), rhs)
						.Simplify(), 
						new CompFunc(CompType.Mul, lhs, rhs.Derivative())
						.Simplify())
						.Simplify();
				case CompType.Div:
					return new CompFunc(CompType.Div,
						new CompFunc(CompType.Sub,
						new CompFunc(CompType.Mul, lhs.Derivative(), rhs)
						.Simplify(),
						new CompFunc(CompType.Mul, lhs, rhs.Derivative())
						.Simplify())
						.Simplify(),
						new CompFunc(CompType.Mul, rhs, rhs)
						.Simplify())
						.Simplify();
				case CompType.Exp:
					return new CompFunc(CompType.Mul,
						new CompFunc(CompType.Exp, lhs, rhs)
						.Simplify(),
						new CompFunc(CompType.Add,
						new CompFunc(CompType.Mul, 
						rhs.Derivative(),
						new CompFunc(CompType.Nest, new LogFunc(), lhs)
						.Simplify())
						.Simplify(),
						new CompFunc(CompType.Mul,
						rhs,
						new CompFunc(CompType.Div, lhs.Derivative(), lhs)
						.Simplify())
						.Simplify())
						.Simplify())
						.Simplify();
				case CompType.Nest:
					return new CompFunc(CompType.Mul,
						new CompFunc(CompType.Nest, lhs.Derivative(), rhs)
						.Simplify(),
						rhs.Derivative())
						.Simplify();
			}
			return null;
		}

		public override CompositeFunction PrevComp(CompType type, Function f)
		{
			return new CompFunc(type, f, this);
		}

		public override CompositeFunction NextComp(CompType type, Function f)
		{
			return new CompFunc(type, this, f);
		}

		public override Function Simplify()
		{
			if (lhs.IsElem() && lhs.AsElem().Coef == 0)
			{
				if (m_type == CompType.Add)
				{
					return rhs.Clone();
				}
				if (m_type == CompType.Sub && rhs.IsElem())
				{
					return rhs.Clone().AsElem().ScaleCoef(-1);
				}
			}
			if (m_type == CompType.Add || m_type == CompType.Sub)
			{
				if (rhs.IsElem() && rhs.AsElem().Coef == 0)
				{
					return lhs.Clone();
				}
				/*if (lhs.IsElem() && lhs.GetType() == rhs.GetType())
				{
					return lhs.Clone().AsElem().ScaleCoef(2);
				}*/
			}

			if (m_type == CompType.Mul)
			{
				if (lhs.IsConst() && lhs.AsElem().Coef == 0 ||
					rhs.IsConst() && rhs.AsElem().Coef == 0)
				{
					return new ConstFunc(0);
				}
				if (lhs.IsConst() && lhs.AsElem().Coef == 1)
				{
					return rhs.Clone();
				}
				if (rhs.IsConst() && rhs.AsElem().Coef == 1)
				{
					return lhs.Clone();
				}
			}

			if (m_type == CompType.Nest)
			{
				if (lhs.IsConst())
				{
					if (lhs.AsElem().Coef == 0)
					{
						return new ConstFunc(0);
					}
					else if (lhs.AsElem().Coef == 1)
					{
						return rhs.Clone();
					}
					else if (rhs.IsElem())
					{
						return rhs.Clone().AsElem().ScaleCoef(lhs.AsElem().Coef);
					}
				}
				if (rhs.IsX())
				{
					if (rhs.AsElem().Coef == 0)
					{
						return new ConstFunc(0);
					}
					else if (rhs.AsElem().Coef == 1)
					{
						return lhs.Clone();
					}
				}
				if (rhs.IsPow())
				{
					if (rhs.AsPow().Exponent == 0)
					{
						return new ConstFunc(lhs.Evaluate(rhs.AsElem().Coef));
					}
					else if (rhs.AsElem().Coef == 1 && rhs.AsPow().Exponent == 1)
					{
						return lhs.Clone();
					}
				}
			}
			return this;
		}

	}
	
}
