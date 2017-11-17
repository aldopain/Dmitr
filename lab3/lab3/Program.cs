using System;

namespace lab3
{
	class lab3
	{
		public class Point{
			public double X;
			public double Y;

			public Point(double x, double y){
				X = x;
				Y = y;
			}
		}
		//y=100(x2-x1^2)^2+(1-x1)^2
		Point x0 = new Point(-1, 0), p1 = new Point(5, 1);
		public double ah = 0.0001, A1 = 0, A2, T1 = (-1 + Math.Sqrt(5))/2, T2 = 1 - (-1 + Math.Sqrt(5))/2, Eps = 0.00001;
		int k = 1, M1 = 20, M2 = 30;

		double y(double alpha){
			Point p = point(alpha);
			return 100 * Math.Pow (p.Y - Math.Pow (p.X, 2), 2) + Math.Pow (1 - p.X, 2);
		}

		double dydx1(Point x){
			return -400 * x.X * x.Y + 400 * Math.Pow (x.X, 3) - 2 + 2 * x.X;
		}

		double dydx2(Point x){
			return 200 * x.Y - 200 * Math.Pow (x.X, 2);
		}

		Point point(double alpha){
			return new Point (x0.X + alpha*p1.X, x0.Y + alpha*p1.Y);
		}

		double dy(double alpha){
			Point p = point (alpha);
			Point g = new Point (dydx1(p), dydx2(p));
			return g.X*p1.X + g.Y*p1.Y;
		}

		public void Svenn(){
			A2 = A1 + ah;
			if (y (A2) > y (A1)) {
				ah = -ah;
				A2 = A1 + ah;
			}
			for (; k < M1; k++) {
				Console.WriteLine (dy (A1) + " - " + dy (A2));
				if (dy (A1) * dy (A2) <= 0) {
					Console.WriteLine (k);
					if (A2 < A1) {
						double buf = A1;
						A1 = A2;
						A2 = buf;
					}
					return;
				}
				ah *= 2;
				A1 = A2;
				A2 = A1 + ah;
			}
		}

		//ЗС2
		void GS2(){
			double a = A1, b = A2; 
			for (k = 1; k < M2; k++) {
				double L = Math.Abs(b - a);
				A1 = a + T2 * L;
				A2 = a + b - A1;
				if (y (A1) > y (A2)) {
					if (A1 > A2) {
						b = A1;
						A1 = A2;
					} else {
						a = A1;
						A1 = A2;
					}
				}else{ 
					if(A1 > A2){
						a = A2;
					}else{
						b = A2;
					}
				}
				Console.WriteLine (a + " - " + b);
				if(Math.Abs(a - b) < Eps){
					double minA = (b + a) / 2; 
					Console.WriteLine ("alpha: " + minA);
					Point min = point(minA);
					Console.WriteLine ("min: " + min.X + " - " + min.Y);
					return;
				}
			}
		}

		public static void Main (string[] args)
		{
			lab3 l = new lab3 ();
			l.Svenn ();
			Console.WriteLine (l.A1 + " - " + l.A2);
			l.GS2 ();
		}
	}
}
