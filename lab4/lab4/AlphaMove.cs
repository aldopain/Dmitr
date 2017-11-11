using System;

namespace lab4
{
	public class AlphaMove
	{
		public static double y(double alpha, Point p1, Point x0){
			Point p = point(alpha, p1, x0);
			//return 100 * Math.Pow (p.Y - Math.Pow (p.X, 2), 2) + Math.Pow (1 - p.X, 2);
			return 2*p.X*p.X + 2*p.Y*p.Y - p.X*p.Y + p.X + 10;
		}

		public static double dydx1(Point x){
			//return -400 * x.X * x.Y + 400 * Math.Pow (x.X, 3) - 2 + 2 * x.X;
			return 4*x.X - x.Y + 1;
		}

		public static double dydx2(Point x){
			//return 200 * x.Y - 200 * Math.Pow (x.X, 2);
			return 4*x.Y - x.X;
		}

		public static Point point(double alpha, Point p1, Point x0){
			return new Point (x0.X + alpha*p1.X, x0.Y + alpha*p1.Y);
		}

		public static double dy(double alpha, Point p1, Point x0){
			Point p = point (alpha, p1, x0);
			Point g = new Point (dydx1(p), dydx2(p));
			return g.X*p1.X + g.Y*p1.Y;
		}
	}
}

