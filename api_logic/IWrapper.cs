using KompasAPI7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace ApiLogic
{
	public enum Cad
	{
		Kompas,
		AutoCad,
	}

	public interface IWrapper
	{
		void OpenCad();

		void CreatePart();

		void NewRectangle(double x, double y, int width, int height, string name);

		void Extrude(int height, string name, bool positiveDirection);


    }
}
