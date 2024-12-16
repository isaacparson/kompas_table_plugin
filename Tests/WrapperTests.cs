using api_logic;
using ApiLogic;

namespace Tests
{
    [TestFixture]
    public class WrapperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(Cad.AutoCad)]
        [TestCase(Cad.Kompas)]
        public void CreatePartTest(Cad cad)
        {
            WrapperFactory factory = new WrapperFactory();
            var wrapper = factory.MakeWrapper(cad);
            Assert.Throws<WrapperCreatePartException>(() => wrapper.CreatePart());

            wrapper.OpenCad();
            Assert.DoesNotThrow(() => wrapper.CreatePart());
        }

        [TestCase(Cad.AutoCad)]
        [TestCase(Cad.Kompas)]
        public void NewRectangleTest(Cad cad)
        {
            WrapperFactory factory = new WrapperFactory();
            var wrapper = factory.MakeWrapper(cad);
            wrapper.OpenCad();

            Assert.Throws<WrapperNewRectangleException>(() => wrapper.NewRectangle(0, 0, 10, 10, "Some name"));
            Assert.DoesNotThrow(() => wrapper.CreatePart());

            Assert.Throws<WrapperNewRectangleException>(() => wrapper.NewRectangle(0, 0, 0, 0, "Some name"));
            Assert.DoesNotThrow(() => wrapper.NewRectangle(0, 0, 10, 10, "Some name"));

        }

        [TestCase(Cad.AutoCad)]
        [TestCase(Cad.Kompas)]
        public void ExtrudeTest(Cad cad)
        {
            WrapperFactory factory = new WrapperFactory();
            var wrapper = factory.MakeWrapper(cad);
            wrapper.OpenCad();

            Assert.DoesNotThrow(() => wrapper.CreatePart());

            Assert.Throws<WrapperExtrudeException>(() => wrapper.Extrude(10, "Some name", true));

            Assert.DoesNotThrow(() => wrapper.NewRectangle(0, 0, 10, 10, "Some name"));

            Assert.Throws<WrapperExtrudeException>(() => wrapper.Extrude(-10, "Some name", true));

            Assert.DoesNotThrow(() => wrapper.Extrude(10, "Some name", true));
        }
    }
}