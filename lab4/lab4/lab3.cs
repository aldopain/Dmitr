using System;

namespace lab4
{
	class lab3
	{
		//y=100(A2-A1^2)^2+(1-A1)^2
		public static double ah = 0.0001, T1 = (-1 + Math.Sqrt(5))/2, T2 = 1 - (-1 + Math.Sqrt(5))/2, Eps = 0.00001;
		static int M1 = 20, M2 = 30;

		public static Point Svenn(Point x0, Point p1){
			double A1 = 0, A2 = 0;
			A2 = A1 + ah;
			if (AlphaMove.y (A2, p1, x0) > AlphaMove.y (A1, p1, x0)) {
				ah = -ah;
				A2 = A1 + ah;
			}
			for (int k = 0; k < M1; k++) {
				if (AlphaMove.dy (A1, p1, x0) * AlphaMove.dy (A2, p1, x0) <= 0) {
					break;
				}
				ah *= 2;
				A1 = A2;
				A2 = A1 + ah;
			}
			if (A2 < A1) {
				Console.Write ("Swapped ");
				double buf = A1;
				A1 = A2;
				A2 = buf;
			}
			Console.WriteLine ("SvennLog: " + A1 + " to " + A2);
			return new Point (A1, A2);
		}

		//ЗС2
		static double GS2(Point interval, Point x0, Point p1){
			double A1 = interval.X, A2 = interval.Y, a = A1, b = A2, L; 
			for (int k = 0; k < M2; k++) {
				L = Math.Abs(b - a);
				A1 = a + T2 * L;
				A2 = a + b - A1;
				if (AlphaMove.y (A1, p1, x0) > AlphaMove.y (A2, p1, x0)) {
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
				if(Math.Abs(a - b) < Eps){
					return (b + a) / 2;
				}
			}
			return (b + a) / 2;
		}

		public static double alpha (Point p1, Point x0)
		{
			return GS2 (Svenn (x0, p1), x0, p1);
		}
	}
}
