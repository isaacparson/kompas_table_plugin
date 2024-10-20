using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_logic
{
	public enum Cad
	{
		Kompas,
		AutoCad,
	}

	public interface IWrapper
	{
		void OpenCad();

		void CreateSketch();

		void CreateRectangle();

		void Extrude();

	}
}
