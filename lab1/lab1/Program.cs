using System;
using System.Windows;
using System.Drawing;
namespace lab1
{
	//Свенн + ЗС2
	class lab1
	{
		double a, min, b, L, Eps = 0.0001, x1 = 0, x2, h = 0.001, T1 = (-1 + Math.Sqrt(5))/2, T2 = 1 - (-1 + Math.Sqrt(5))/2;
		int k = 1, M = 20;

		double df(double x){
			return Math.Pow(Math.E, -x)*(x*Math.Log(x)-1)/x;
		}

		double f(double x){
			return -Math.Pow(Math.E, -x)*Math.Log(x);
		}

		//Свенн
		void Svenn(){
			x2 = x1 + h;
			if (f (x2) > f (x1)) {
				h = -h;
				x2 = x1 + h;
			}
			for (; k < M; k++) {
				if (df (x1) * df (x2) <= 0) {
					if (x2 < x1) {
						double buf = x1;
						x1 = x2;
						x2 = buf;
					}
					return;
				}
				h *= 2;
				x1 = x2;
				x2 = x1 + h;
			}
		}

		//ЗС2
		void GS2(){
			a = x1; 
			b = x2; 
			Console.WriteLine (T1 + " - " + T2);
			for (k = 1; k < M; k++) {
				L = Math.Abs(b - a);
				x1 = a + T2 * L;
				x2 = a + b - x1;
				if (f (x1) > f (x2)) {
					if (x1 > x2) {
						b = x1;
						x1 = x2;
					} else {
						a = x1;
						x1 = x2;
					}
				}else{ 
					if(x1 > x2){
						a = x2;	//1
					}else{
						b = x2; //1
					}
				}
				Console.WriteLine (a + " - " + b);
				if(Math.Abs(a - b) < Eps){
					min = (b + a) / 2;
					return;
				}
			}
		}

		public static void Main (string[] args)
		{
			lab1 l = new lab1 ();
			l.Svenn (); //получаем интервал с помощью метода Свенна
			Console.WriteLine (l.x1 + " - " + l.x2);
			l.GS2 (); //находим минимум с помощью метода ЗС-2
			Console.WriteLine (l.min);
		}
	}
}
