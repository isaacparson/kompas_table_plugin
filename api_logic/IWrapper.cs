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
