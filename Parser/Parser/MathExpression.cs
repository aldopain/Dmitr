using System;
using System.Collections.Generic;

namespace Parser
{
	public class MathExpression
	{
		char expressionID;
		public int resultPosition;
		int[] argNum;

		public double Calculate(List<double> args){
			switch (expressionID) {
			case '+':
				return args [argNum[0]] + args [argNum[1]];
			case '*':
				return args [argNum[0]] * args [argNum[1]];
			case '-':
				return args [argNum[0]] - args [argNum[1]];
			case '/':
				return args [argNum[0]] / args [argNum[1]];
			}
			return 0;
		}

		public void info(){
			Console.WriteLine ("x" + (resultPosition + 1) + " = x" + (argNum[0] + 1) + expressionID + "x" + (argNum[1] + 1));
		}

		public MathExpression (char id, int rp, int[] args){
			expressionID = id;
			argNum = new int[args.Length];
			for (int i = 0; i < argNum.Length; i++) {
				argNum[i] = Int16.Parse(args[i].ToString()) - 1;
			}
			resultPosition = rp--;
		}
	}
}