using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathematicsX
{
	public class FuncTree
	{
		private const int NULL_NODE = -1;

		public enum EType
		{
			//0
			Void, X, C, E, PI,
			//1
			Neg, Abs, Exp, Log,
			Sin, Cos, Tan,
			Asin, Acos, Atan,
			//2
			Add, Sub, Mul, Div, Pow,
		}

		public static int ETypePrior(EType type)
		{
			switch (type)
			{
				case EType.Abs:
				case EType.Exp: 
				case EType.Log:
				case EType.Sin:
				case EType.Cos:
				case EType.Tan:
				case EType.Asin:
				case EType.Acos:
				case EType.Atan:
					return 1;
				case EType.Pow:
					return 2;
				case EType.Neg:
					return 3;
				case EType.Mul:
				case EType.Div:
					return 4;
				case EType.Add:
				case EType.Sub:
					return 5;
				default:
					return 0;
			}
		}

		private const EType ETYPE0 = EType.Void;
		private const EType ETYPE1 = EType.Neg;
		private const EType ETYPE2 = EType.Add;

		private struct Node
		{
			public int parent;
			public int left;
			public int right;
			public int height;
			public int pos;
			public EType type;
			public bool IsVoid
			{
				get { return type == EType.Void; }
			}
			public bool IsFreed
			{
				get { return height == -1; }
			}
			public int GetDirect(int ptr)
			{
				if (ptr == parent) return 0;
				if (ptr == left) return 1;
				if (ptr == right) return 2;
				return -1;
			}
			public override string ToString()
			{
				return string.Format("{0},{1},{2},{3},{4},{5}", 
					pos, type, parent, left, right, height);
			}
		}

		private int m_nodeCapacity;
		private int m_nodeCount;
		private int m_leafCount;
		private int m_dimension;
		private string m_expression;

		private int m_root;
		private int m_free;
		private Node[] m_nodes;
		private List<double> m_consts;

		public FuncTree()
		{
			m_nodeCapacity = 1;
			m_nodes = new Node[m_nodeCapacity];
			m_consts = new List<double>();
			Init();
		}
		public FuncTree(FuncTree source)
		{
			m_nodeCapacity = source.m_nodeCapacity;
			m_nodeCount = source.m_nodeCount;
			m_leafCount = source.m_leafCount;
			m_dimension = source.m_dimension;
			m_expression = source.m_expression;
			m_root = source.m_root;
			m_free = source.m_free;
			m_nodes = new Node[m_nodeCapacity];
			source.m_nodes.CopyTo(m_nodes, 0);
			m_consts = new List<double>(source.m_consts);
		}

		public int NodeCapacity
		{
			get { return m_nodeCapacity; }
		}
		public int NodeCount
		{
			get { return m_nodeCount; }
		}
		public int LeafCount
		{
			get { return m_leafCount; }
		}
		public int Dimension
		{
			get { return m_dimension; }
		}
		public string Expression
		{
			get { return m_expression; }
		}

		private void Init()
		{
			InitNodes(0);
			m_nodeCount = 0;
			m_leafCount = 0;
			m_root = AllocateNode();
			m_dimension = 0;
			m_expression = "";
		}
		
		private void InitNodes(int start)
		{
			int last = m_nodeCapacity - 1;
			for (int i = start; i < last; i++)
			{
				m_nodes[i].parent = i + 1;
				m_nodes[i].height = -1;
			}
			m_nodes[last].parent = NULL_NODE;
			m_nodes[last].height = -1;
			m_free = start;
		}
		
		private int AllocateNode()
		{
			if (m_free == NULL_NODE)
			{
				m_nodeCapacity <<= 1;
				Array.Resize(ref m_nodes, m_nodeCapacity);
				InitNodes(m_nodeCount);
			}
			int node = m_free;
			m_free = m_nodes[node].parent;
			m_nodes[node].parent = NULL_NODE;
			m_nodes[node].left = NULL_NODE;
			m_nodes[node].right = NULL_NODE;
			m_nodes[node].height = 0;
			m_nodes[node].type = EType.Void;
			m_nodeCount++;
			return node;
		}
		
		private void FreeNode(int node)
		{
			m_nodes[node].parent = m_free;
			m_nodes[node].height = -1;
			m_free = node;
			m_nodeCount--;
		}

		private void UpdateHeight(int ptr)
		{
			ptr = m_nodes[ptr].parent;
			while (ptr != NULL_NODE)
			{
				int left = m_nodes[ptr].left;
				int right = m_nodes[ptr].right;
				if (right==NULL_NODE)
				{
					m_nodes[ptr].height = 1 + m_nodes[left].height;
				}
				else
				{
					m_nodes[ptr].height = 1 + Math.Max(m_nodes[left].height, m_nodes[right].height);
				}
				ptr = m_nodes[ptr].parent;
			}
		}

		private int AddConst(double value)
		{
			int index = m_consts.IndexOf(value);
			if (index == -1)
			{
				index = m_consts.Count;
				m_consts.Add(value);
			}
			return index;
		}
		
		private int Assign(int pos, EType type)
		{
			int ptr = m_root;
			while (!m_nodes[ptr].IsVoid)
			{
				if (m_nodes[ptr].type < ETYPE1)
				{
					throw new Exception("Error: assign time out!");
				}
				if (m_nodes[ptr].type < ETYPE2 || pos < m_nodes[ptr].pos)
				{
					ptr = m_nodes[ptr].left;
				}
				else
				{
					ptr = m_nodes[ptr].right;
				}
			}
			m_nodes[ptr].pos = pos;
			m_nodes[ptr].type = type;
			if (type >= ETYPE2)
			{
				int node = AllocateNode();
				m_nodes[node].parent = ptr;
				m_nodes[ptr].right = node;
			}
			if (type >= ETYPE1)
			{
				int node = AllocateNode();
				m_nodes[node].parent = ptr;
				m_nodes[ptr].left = node;
				m_nodes[ptr].height = 1;
			}
			UpdateHeight(ptr);
			return ptr;
		}

		private int AssignConst(int pos, double value)
		{
			int ptr = Assign(pos, EType.C);
			int constIndex = AddConst(value);
			m_nodes[ptr].left = constIndex;
			m_leafCount++;
			return ptr;
		}

		private int AssignVar(int pos, int varIndex)
		{
			int ptr = Assign(pos, EType.X);
			m_nodes[ptr].left = varIndex;
			m_leafCount++;
			if (m_dimension <= varIndex) { m_dimension = varIndex + 1; }
			return ptr;
		}


		public void Clear()
		{
			m_consts.Clear();
			Init();
		}
		
		public void RecalcPos_Nonrecursion()
		{
			int pos = 0;

			int i = -1;
			int[] stack = new int[m_nodes[m_root].height + 1];
			stack[++i] = m_root;
			int lptr = NULL_NODE; // = root.parent = NULL_NODE
			while (i >= 0)
			{
				int ptr = stack[i];
				EType type = m_nodes[ptr].type;
				if (type < ETYPE1)
				{
					m_nodes[ptr].pos = pos++;
					i--;
				}
				else
				{
					int dir = m_nodes[ptr].GetDirect(lptr);
					if (type < ETYPE2)
					{
						if (dir == 0)
						{
							m_nodes[ptr].pos = pos++;
							stack[++i] = m_nodes[ptr].left;
						}
						else
						{
							i--;
						}
					}
					else
					{
						if (dir < 2)
						{
							if (dir == 0)
							{
								stack[++i] = m_nodes[ptr].left;
							}
							else
							{
								m_nodes[ptr].pos = pos++;
								stack[++i] = m_nodes[ptr].right;
							}
						}
						else
						{
							i--;
						}
					}
				}
				lptr = ptr;
			}
		}

		private void RecalcPosAt(int ptr, ref int pos)
		{
			EType type = m_nodes[ptr].type;
			if (type < ETYPE1)
			{
				m_nodes[ptr].pos = pos++;
			}
			else if (type < ETYPE2)
			{
				m_nodes[ptr].pos = pos++;
				RecalcPosAt(m_nodes[ptr].left, ref pos);
			}
			else
			{
				RecalcPosAt(m_nodes[ptr].left, ref pos);
				m_nodes[ptr].pos = pos++;
				RecalcPosAt(m_nodes[ptr].right, ref pos);
			}
		}
		public void RecalcPos()
		{
			int pos = 0;
			RecalcPosAt(m_root, ref pos);
		}

		private void RecalcExprAt(int ptr, StringBuilder sb)
		{
			EType type = m_nodes[ptr].type;
			if (type < ETYPE1)
			{
				switch (type)
				{
					case EType.X:
						sb.Append('x');
						if (m_dimension > 1) { sb.Append(m_nodes[ptr].left); }
						break;
					case EType.C: sb.Append(m_consts[m_nodes[ptr].left]); break;
					case EType.E: sb.Append('e'); break;
					case EType.PI: sb.Append("pi"); break;
					default: throw new Exception();
				}
			}
			else if (type == EType.Neg)
			{
				int left = m_nodes[ptr].left;
				bool bar = ETypePrior(m_nodes[left].type) > ETypePrior(EType.Neg);
				if (bar) { sb.Append("-("); }
				else { sb.Append('-'); }
				RecalcExprAt(left, sb);
				if (bar) { sb.Append(')'); }
			}
			else if (type < ETYPE2)
			{
				switch (type)
				{
					case EType.Abs: sb.Append("abs("); break;
					case EType.Exp: sb.Append("exp("); break;
					case EType.Log: sb.Append("log("); break;
					case EType.Sin: sb.Append("sin("); break;
					case EType.Cos: sb.Append("cos("); break;
					case EType.Tan: sb.Append("tan("); break;
					case EType.Asin: sb.Append("asin("); break;
					case EType.Acos: sb.Append("acos("); break;
					case EType.Atan: sb.Append("atan("); break;
				}
				RecalcExprAt(m_nodes[ptr].left, sb);
				sb.Append(')');
			}
			else if (type == EType.Add)
			{
				RecalcExprAt(m_nodes[ptr].left, sb);
				sb.Append('+');
				RecalcExprAt(m_nodes[ptr].right, sb);
			}
			else
			{
				int left = m_nodes[ptr].left;
				int right = m_nodes[ptr].right;
				int lpr = ETypePrior(m_nodes[left].type);
				int rpr = ETypePrior(m_nodes[right].type);
				char oper;
				int pr;
				bool lbar, rbar;
				switch (type)
				{
					case EType.Sub:
						oper = '-';
						lbar = false;
						rbar = rpr >= ETypePrior(EType.Sub);
						break;
					case EType.Mul:
						oper = '*';
						pr = ETypePrior(EType.Mul);
						lbar = lpr > pr;
						rbar = rpr > pr;
						break;
					case EType.Div:
						oper = '/';
						pr = ETypePrior(EType.Div);
						lbar = lpr > pr;
						rbar = rpr >= pr;
						break;
					case EType.Pow:
						oper = '^';
						pr = ETypePrior(EType.Pow);
						lbar = lpr >= pr;
						rbar = rpr > pr;
						break;
					default: throw new Exception();
				}
				if (lbar) { sb.Append('('); }
				RecalcExprAt(left, sb);
				if (lbar) { sb.Append(')'); }
				sb.Append(oper);
				if (rbar) { sb.Append('('); }
				RecalcExprAt(right, sb);
				if (rbar) { sb.Append(')'); }
			}
		}
		private void RecalcExpr()
		{
			StringBuilder sb = new StringBuilder();
			RecalcExprAt(m_root, sb);
			m_expression = sb.ToString();
		}

		public double Evaluate_Nonrecursion(double x)
		{
			//理论上如果所有二元运算都先算height大的一边，那么blen的最大值为：
			//设叶节点数最多的一层的叶节点数量为n。
			//blen = 1 + (int)Math.Log(n, 2)
			int blen = m_leafCount;
			int bptr = -1;
			double[] valueBuffer = new double[blen];

			int i = -1;
			int[] stack = new int[m_nodes[m_root].height + 1];
			stack[++i] = m_root;

			int lptr = NULL_NODE; // = root.parent = nullnode
			while (i >= 0)
			{
				int ptr = stack[i];
				EType type = m_nodes[ptr].type;
				if (type < ETYPE1)
				{
					double value;
					switch (type)
					{
						case EType.X: value = x; break;
						case EType.C: value = m_consts[m_nodes[ptr].left]; break;
						case EType.E: value = Math.E; break;
						case EType.PI: value = Math.PI; break;
						default: throw new Exception();
					}
					valueBuffer[++bptr] = value;
					i--;
				}
				else
				{
					int dir = m_nodes[ptr].GetDirect(lptr);
					if (type < ETYPE2)
					{
						if (dir == 0)
						{
							stack[++i] = m_nodes[ptr].left;
						}
						else
						{
							double value = valueBuffer[bptr];
							switch (type)
							{
								case EType.Neg: value = -value; break;
								case EType.Abs: value = Math.Abs(value); break;
								case EType.Exp: value = Math.Exp(value); break;
								case EType.Log: value = Math.Log(value); break;
								case EType.Sin: value = Math.Sin(value); break;
								case EType.Cos: value = Math.Cos(value); break;
								case EType.Tan: value = Math.Tan(value); break;
								case EType.Asin: value = Math.Asin(value); break;
								case EType.Acos: value = Math.Acos(value); break;
								case EType.Atan: value = Math.Atan(value); break;
							}
							valueBuffer[bptr] = value;
							i--;
						}
					}
					else
					{
						int left = m_nodes[ptr].left;
						int right = m_nodes[ptr].right;
						bool reverse = m_nodes[right].height > m_nodes[left].height;
						//bool reverse = false;
						if (dir < 2)
						{
							stack[++i] = (dir == 0 ^ reverse) ? left : right;
						}
						else
						{
							double lv, rv;
							if (reverse)
							{
								lv = valueBuffer[bptr--];
								rv = valueBuffer[bptr];
							}
							else
							{
								rv = valueBuffer[bptr--];
								lv = valueBuffer[bptr];
							}
							switch (type)
							{
								case EType.Add: lv += rv; break;
								case EType.Sub: lv -= rv; break;
								case EType.Mul: lv *= rv; break;
								case EType.Div: lv /= rv; break;
								case EType.Pow: lv = Math.Pow(lv, rv); break;
							}
							valueBuffer[bptr] = lv;
							i--;
						}
					}
				}
				lptr = ptr;
			}
			return valueBuffer[0];
		}
		
		private double EvaluateAt(int ptr, double x)
		{
			int left = m_nodes[ptr].left;
			int right = m_nodes[ptr].right;
			EType type = m_nodes[ptr].type;
			switch (type)
			{
				case EType.X: return x;
				case EType.C: return m_consts[left];
				case EType.E: return Math.E;
				case EType.PI: return Math.PI;
				case EType.Neg: return -EvaluateAt(left, x);
				case EType.Abs: return Math.Abs(EvaluateAt(left, x));
				case EType.Exp: return Math.Exp(EvaluateAt(left, x));
				case EType.Log: return Math.Log(EvaluateAt(left, x));
				case EType.Sin: return Math.Sin(EvaluateAt(left, x));
				case EType.Cos: return Math.Cos(EvaluateAt(left, x));
				case EType.Tan: return Math.Tan(EvaluateAt(left, x));
				case EType.Asin: return Math.Asin(EvaluateAt(left, x));
				case EType.Acos: return Math.Acos(EvaluateAt(left, x));
				case EType.Atan: return Math.Atan(EvaluateAt(left, x));
				case EType.Add: return EvaluateAt(left, x) + EvaluateAt(right, x);
				case EType.Sub: return EvaluateAt(left, x) - EvaluateAt(right, x);
				case EType.Mul: return EvaluateAt(left, x) * EvaluateAt(right, x);
				case EType.Div: return EvaluateAt(left, x) / EvaluateAt(right, x);
				case EType.Pow: return Math.Pow(EvaluateAt(left, x), EvaluateAt(right, x));
				default: throw new Exception("Error: A node is void while evaluating!");
			}
		}
		private double EvaluateAt(int ptr, IList<double> x)
		{
			int left = m_nodes[ptr].left;
			int right = m_nodes[ptr].right;
			EType type = m_nodes[ptr].type;
			switch (type)
			{
				case EType.X: return x[left];
				case EType.C: return m_consts[left];
				case EType.E: return Math.E;
				case EType.PI: return Math.PI;
				case EType.Neg: return -EvaluateAt(left, x);
				case EType.Abs: return Math.Abs(EvaluateAt(left, x));
				case EType.Exp: return Math.Exp(EvaluateAt(left, x));
				case EType.Log: return Math.Log(EvaluateAt(left, x));
				case EType.Sin: return Math.Sin(EvaluateAt(left, x));
				case EType.Cos: return Math.Cos(EvaluateAt(left, x));
				case EType.Tan: return Math.Tan(EvaluateAt(left, x));
				case EType.Asin: return Math.Asin(EvaluateAt(left, x));
				case EType.Acos: return Math.Acos(EvaluateAt(left, x));
				case EType.Atan: return Math.Atan(EvaluateAt(left, x));
				case EType.Add: return EvaluateAt(left, x) + EvaluateAt(right, x);
				case EType.Sub: return EvaluateAt(left, x) - EvaluateAt(right, x);
				case EType.Mul: return EvaluateAt(left, x) * EvaluateAt(right, x);
				case EType.Div: return EvaluateAt(left, x) / EvaluateAt(right, x);
				case EType.Pow: return Math.Pow(EvaluateAt(left, x), EvaluateAt(right, x));
				default: throw new Exception("Error: A node is void while evaluating!");
			}
		}
		public double Evaluate(double x)
		{
			return EvaluateAt(m_root, x);
		}
		public double Evaluate(IList<double> x)
		{
			return EvaluateAt(m_root, x);
		}
		public double Evaluate(params double[] x)
		{
			return EvaluateAt(m_root, x);
		}

		public FuncTree Derivate()
		{
			//TODO
			return null;
		}
		

		//--------------------------------------------------------- parse expression

		private const string REG_INVALID = @"_|\(\)|\)\("
										+ @"|\^[\^\*\/\+\)]|\(\^"
										+ @"|\*[\^\*\/\+\)]|\(\*"
										+ @"|\/[\^\*\/\+\)]|\(\/"
										+ @"|\+[\^\*\/\+\)]|\(\+"
										+ @"|\-[\^\*\/\+\)]"
										+ @"|\)\d|\d\("
										+ @"|\)[a-z]|(?<![a-z])(x|e|pi)\("
										+ @"|\d(x|pi)|pi\d";

		private const string REG_SPARE_BAR = @"(?<![a-z])\(_+\)";

		private const string REG_X = @"(?<![a-z])x\d*(?![a-z])";
		private const string REG_CONST = @"\d\.?\d*(e\-?\d+)?";
		private const string REG_CONST2 = @"(?<![a-z])(e|pi)(?![a-z])";

		private const string REG_BASIC = @"(abs|exp|log|sin|cos|tan|asin|acos|atan)\(_+\)";
		private const string REG_POW = @"_+\^_+";

		private const string REG_NEG = @"(?<![\)_])\-_+(?!_*\^)";
		private const string REG_MULDIV = @"(?<!\^_*)_+[\*\/]_+(?!_*\^)";
		private const string REG_ADDSUB = @"(?<![\^\*\/]_*)_+[\+\-]_+(?!_*[\^\*\/])";

		private struct ExprEle<T>
		{
			public int pos;
			public T value;
			public ExprEle(int pos, T value)
			{
				this.pos = pos;
				this.value = value;
			}
		}

		private static void AssertExpr(string expr)
		{
			Regex reg = new Regex(REG_INVALID);
			Match match = reg.Match(expr);
			if (match.Success)
				throw new Exception(
					string.Format("Expression is invalid! At {0}:\"{1}\"",
					match.Index, match.Value));
		}

		private static void AssertBars(string expr)
		{
			int sum = 0;
			for (int i = 0; i < expr.Length; i++)
			{
				char c = expr[i];
				if (c == '(') {  sum++; }
				else if (c == ')') { sum--; }
				if (sum < 0) { break; }
			}
			if (sum != 0)
				throw new Exception("Brackets count incorrect!");
		}

		private static string Replace(Match match)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append('_', match.Value.Length);
			return sb.ToString();
		}

		private static void ParseX(ref string expr, HashSet<ExprEle<int>> results)
		{
			Regex reg = new Regex(REG_X);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return;
			foreach (Match match in mc)
			{
				int varIndex = match.Value.Length == 1 ? 0 : int.Parse(match.Value.Substring(1));
				results.Add(new ExprEle<int>(match.Index, varIndex));
			}
			expr = reg.Replace(expr, Replace);
		}
		private static void ParseC(ref string expr, HashSet<ExprEle<double>> results)
		{
			Regex reg = new Regex(REG_CONST);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return;
			foreach (Match match in mc)
			{
				double value = double.Parse(match.Value);
				results.Add(new ExprEle<double>(match.Index, value));
			}
			expr = reg.Replace(expr, Replace);
		}
		private static void ParseC2(ref string expr, Stack<ExprEle<EType>> results)
		{
			Regex reg = new Regex(REG_CONST2);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return;
			foreach (Match match in mc)
			{
				EType type = match.Value == "e" ? EType.E : EType.PI;
				results.Push(new ExprEle<EType>(match.Index, type));
			}
			expr = reg.Replace(expr, Replace);
		}

		private static bool ReplaceSpareBar(ref string expr)
		{
			Regex reg = new Regex(REG_SPARE_BAR);
			if (reg.IsMatch(expr))
			{
				expr = reg.Replace(expr, Replace);
				return true;
			}
			return false;
		}

		private static bool ParseBasic(ref string expr, Stack<ExprEle<EType>> results)
		{
			Regex reg = new Regex(REG_BASIC);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return false;
			foreach (Match match in mc)
			{
				string name = match.Value.Substring(0, 4);
				EType type;
				switch (name)
				{
					case "abs(": type = EType.Abs; break;
					case "exp(": type = EType.Exp; break;
					case "log(": type = EType.Log; break;
					case "sin(": type = EType.Sin; break;
					case "cos(": type = EType.Cos; break;
					case "tan(": type = EType.Tan; break;
					case "asin": type = EType.Asin; break;
					case "acos": type = EType.Acos; break;
					case "atan": type = EType.Atan; break;
					default: throw new Exception("Function dosen't exist!");
				}
				results.Push(new ExprEle<EType>(match.Index, type));
			}
			expr = reg.Replace(expr, Replace);
			return true;
		}
		private static bool ParsePow(ref string expr, Stack<ExprEle<EType>> results)
		{
			Regex reg = new Regex(REG_POW, RegexOptions.RightToLeft);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return false;
			foreach (Match match in mc)
			{
				int pos = match.Index + match.Value.IndexOf('^');
				results.Push(new ExprEle<EType>(pos, EType.Pow));
			}
			expr = reg.Replace(expr, Replace);
			return true;
		}

		private static bool ParseNeg(ref string expr, Stack<ExprEle<EType>> results)
		{
			Regex reg = new Regex(REG_NEG);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return false;
			foreach (Match match in mc)
			{
				int pos = match.Index + match.Value.IndexOf('-');
				results.Push(new ExprEle<EType>(pos, EType.Neg));
			}
			expr = reg.Replace(expr, Replace);
			return true;
		}
		private static bool ParseMulDiv(ref string expr, Stack<ExprEle<EType>> results)
		{
			Regex reg = new Regex(REG_MULDIV);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return false;
			foreach (Match match in mc)
			{
				int i = match.Value.IndexOf('/');
				EType type = i == -1 ? EType.Mul : EType.Div;
				if (i == -1) { i = match.Value.IndexOf('*'); }
				results.Push(new ExprEle<EType>(match.Index + i, type));
			}
			expr = reg.Replace(expr, Replace);
			return true;
		}
		private static bool ParseAddSub(ref string expr, Stack<ExprEle<EType>> results)
		{
			Regex reg = new Regex(REG_ADDSUB);
			MatchCollection mc = reg.Matches(expr);
			if (mc.Count == 0) return false;
			foreach (Match match in mc)
			{
				int i = match.Value.IndexOf('-');
				EType type = i == -1 ? EType.Add : EType.Sub;
				if (i == -1) { i = match.Value.IndexOf('+'); }
				results.Push(new ExprEle<EType>(match.Index + i, type));
			}
			expr = reg.Replace(expr, Replace);
			return true;
		}
		

		public void SetExpression(string expr)
		{
			Clear();

			expr = Regex.Replace(expr.Trim().ToLower(), @"\s+", "");

			AssertBars(expr);
			AssertExpr(expr);
			
			Console.WriteLine(expr);

			HashSet<ExprEle<int>> processX = new HashSet<ExprEle<int>>();
			HashSet<ExprEle<double>> processC = new HashSet<ExprEle<double>>();
			Stack<ExprEle<EType>> process = new Stack<ExprEle<EType>>();
			ParseX(ref expr, processX);
			ParseC(ref expr, processC);
			ParseC2(ref expr, process);
			while (true)
			{
				if (ReplaceSpareBar(ref expr)) continue;
				Console.WriteLine(expr);
				if (ParseBasic(ref expr, process)) continue;
				if (ParsePow(ref expr, process)) continue;
				if (ParseNeg(ref expr, process)) continue;
				if (ParseMulDiv(ref expr, process)) continue;
				if (ParseAddSub(ref expr, process)) continue;
				break;
			}
			if (expr[0] != '(' && expr[0] != '_')
				throw new Exception("Expression is invalid!");
			for (int i = 1; i < expr.Length - 1; i++)
				if (expr[i] != '_')
					throw new Exception("Expression is invalid!");
			
			while (process.Count > 0)
			{
				ExprEle<EType> ele = process.Pop();
				Assign(ele.pos, ele.value);
			}
			foreach (ExprEle<double> ele in processC)
			{
				AssignConst(ele.pos, ele.value);
			}
			foreach (ExprEle<int> ele in processX)
			{
				AssignVar(ele.pos, ele.value);
			}

			RecalcExpr();
			RecalcPos();
		}

	}
}
