using System;
using System.Collections.Generic;


namespace Parser
{
	class Parser
	{
		List<MathExpression> me = new List<MathExpression>();
		List<double> args = new List<double>();
		int meCount = 0, argCount = 0;
		String ex;
		int[] toDel = new int[2];

		double y(double[] a){
			for (int i = 0; i < a.Length; i++) {
				args [i] = a [i];
			}
			foreach (MathExpression curr in me) {
				curr.info ();
				args [curr.resultPosition] = curr.Calculate (args);
			}
			return args[args.Count - 1];
		}

		int Priority(int pos){
			char c = ex [pos];
			if (c == '*' || c == '/')
				return 10;
			if (c == '+' || c == '-')
				return 1;
			if (c == '^')
				return 20;
			if(c == 's' || c == 'c' || c == 'l' || c == 't' || c == 'a'){
				String buf = "";
				for (int i = pos; ex [i] < ex.Length && ex [i] != '('; i++)
					buf += ex [i];
				switch (buf) {
				case "sin":
					return 20;
				case "cos":
					return 20;
				case "log":
					return 20;
				case "ln":
					return 20;
				case "arctg":
					return 20;
				case "tg":
					return 20;
				case "ctg":
					return 20;
				case "arcsin":
					return 20;
				case "arccos":
					return 20;
				case "arcctg":
					return 20;
				case "summ":
					return 20;
				}
			}
			return 0;
		}

		void Inizialize(){
			for(int i = 0; i < ex.Length; i++){
				if (ex [i] == 'x') {
					String a = "";
					for (int j = i + 1;  j < ex.Length && ex [j] >= '0' && ex [j] <= '9'; j++) {
						a += ex [j];
					}
					int p = Int16.Parse (a);
					if(p > argCount)
						argCount = p;
				}
				char c = ex [i];
				if (c == '*' || c == '/' || c == '+' || c == '-')
					meCount++;
			}
		}

		int[] FindArgs(int position){
			char c = ex [position];
			int i;
			if(c == '*' || c == '/' || c == '+' || c == '-'){
				String a1 = "", a2 = "";
				for (i = position - 1; i >= 0 && ex [i] != 'x'; i--) {
					a1 += ex [i];
				}
				toDel [0] = i;
				char[] buf = a1.ToCharArray ();
				Array.Reverse (buf);
				a1 = new String (buf);
				for (i = position + 2; i < ex.Length && ex [i] <= '9' && ex[i] >= '0'; i++)
					a2 += ex [i];
				toDel [1] = i;
				return new int[]{Int16.Parse(a1), Int16.Parse(a2)};
			}
			return null;
		}

		void ParseBrackets(int pos){
			String buf = "";
			int i;
			int brCount = 1;
			for (i = pos + 1; i < ex.Length && brCount != 0; i++) {
				if (ex [i] == '(')
					brCount++;
				if (ex [i] == ')')
					brCount--;
				if(brCount != 0)
					buf += ex [i];
			}
			Parser p = new Parser (buf, args, argCount);
			me.AddRange (p.getMathExpressions());
			ex = ex.Replace(ex[pos] + buf + ')', p.getExpression ());
			argCount = p.getArgCount ();
		}

		bool DetectNum(int pos, out double num, out int newPos){
			newPos = pos;
			num = 0;
			if (pos != 0 && ex [pos - 1] == 'x')
				return false;
			String buf = "";
			for (int i = pos; i < ex [i]; i++) {
				if ((ex [i] >= 48 && ex [i] <= 59) || ex[i] == '.')
					buf += ex [i];
				else{
					newPos = i;
					break;
				}
			}
			double retVal;
			if (Double.TryParse (buf, out num))
				return true;
			return false;
		}

		void ParseNums(){
			for (int i = 0; i < ex.Length; i++) {
				//if()
			}
		}

		void Parse(){
			int p = 10;
			Console.WriteLine ("ex = " + ex);
			for (int i = 0; i < ex.Length; i++) 
				if (ex [i] == '(') 
					ParseBrackets (i);
			while(meCount != 0 && p != 0){
				for (int i = 0; i < ex.Length; i++) {
					if (Priority (i) == p) {
						if (ex.Length > 5) {
							me.Add (new MathExpression (ex [i], argCount++, FindArgs(i)));
							Console.WriteLine ("ex = " + ex);
							ex = ex.Substring (0, toDel[0]) + "x" + argCount + ex.Substring (toDel[1]);
							Console.WriteLine ("ex = " + ex);
							meCount--;
							i = 0;
						} else {
							me.Add (new MathExpression (ex [i], argCount++, FindArgs(i)));
							ex = "x" + argCount;
							Console.WriteLine ("!ex = " + ex);
							meCount--;
							break;
						}
					}
				}
				p--;
			}
			int countToAdd = argCount - args.Count;
			for (int i = 0; i < countToAdd; i++)
				args.Add (0);
		}

		int getArgCount(){
			return argCount;
		}

		String getExpression(){
			return ex;
		}

		List<MathExpression> getMathExpressions(){
			return me;
		}

		public Parser(String expression, List<double> a, int ac){
			ex = expression;
			Inizialize ();
			argCount = ac;
			args = a;
			Parse ();
		}

		public Parser(String expression){
			ex = expression;
			Inizialize ();
			Parse ();
		}

		public static void Main (string[] args)
		{
			Parser p = new Parser ("(((((x1*x2)))))");
			Console.WriteLine("y = " + p.y (new double[]{12,5}));
			Console.WriteLine("y = " + p.y (new double[]{1,5}));
		}
	}
}