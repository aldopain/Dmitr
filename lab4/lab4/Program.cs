using System;

namespace lab4
{
	class lab4
	{
		double Eps = 0.001;
		int M = 10;
		Point x = new Point(0, 0);
		lab3 l = new lab3();
		Point force, x2;

		Point g1(Point x0){
			return new Point(AlphaMove.dydx1(x0), 0);
		}

		Point g2(Point x0){
			return new Point(0, AlphaMove.dydx2(x0));
		}

		void simpleStep(){
			Console.WriteLine ("x0 = " + x.X + " ; " + x.Y);
			Point x1 = AlphaMove.point(lab3.alpha(g1(x), x), g1(x), x);
			Console.WriteLine ("x1 = " + x1.X + " ; " + x1.Y + "\nalpha = " + lab3.alpha(g1(x), x));
			x1 = AlphaMove.point(lab3.alpha(g1(x), x), g1(x), x);
			Console.WriteLine ("x1 = " + x1.X + " ; " + x1.Y + "\nalpha = " + lab3.alpha(g1(x), x));
			x1 = AlphaMove.point(lab3.alpha(g1(x), x), g1(x), x);
			Console.WriteLine ("x1 = " + x1.X + " ; " + x1.Y + "\nalpha = " + lab3.alpha(g1(x), x));
			x2 = AlphaMove.point(lab3.alpha(g2(x1), x1), g2(x1), x1);
			Console.WriteLine ("x2 = " + x2.X + " ; " + x2.Y + "\nalpha = " + lab3.alpha(g2(x1), x1));
			force = new Point (x2.X - x.X, x2.Y - x.Y);
			x = x2;
		}

		void forceStep(){
			x2 = AlphaMove.point(lab3.alpha(force, x), force, x);
			Console.WriteLine ("x3 = " + x2.X + " ; " + x2.Y);
			Console.WriteLine ("");
		}

		void GZ(){
			for(int k = 0; k < M; k++){
				simpleStep ();
				forceStep ();
				Point dist = new Point (x2.X - x.X, x2.Y - x.Y);
				if (Math.Sqrt (Math.Pow (dist.X, 2) + Math.Pow (dist.Y, 2)) < Eps) {
					Console.WriteLine ("min = " + x2.X + " ; " + x2.Y);
					return;
				}
				x = x2;
			}
		}

		public static void Main (string[] args)
		{
			lab4 l = new lab4 ();
			l.GZ ();
		}
	}
}
