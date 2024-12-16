using Logic;

namespace Tests
{
    [TestFixture]
    public class LogicTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParameterTest()
        {
            Parameter parameter = new Parameter(500, 20, 200);
            Assert.That(parameter.MaxValue, Is.EqualTo(500));
            Assert.That(parameter.MinValue, Is.EqualTo(20));
            Assert.That(parameter.Value, Is.EqualTo(200));
        }

        [Test]
        public void ParametersRightTest()
        {
            var topWidth = new Parameter(5000, 500, 500);
            var topDepth = new Parameter(5000, 500, 500);
            var topHeight = new Parameter(100, 16, 20);
            var legsWidth = new Parameter(200, 20, 20);
            var tableHeight = new Parameter(1400, 500, 500);

            var dict = new Dictionary<ParamType, Parameter>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, topDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);

            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(dict);

            Assert.That(incorrect.Count, Is.EqualTo(0));

        }

        [Test]
        public void ParametersIndependentWrongTest()
        {
            var topWidth = new Parameter(5000, 500, 500);
            var wrongTopDepth = new Parameter(5000, 500, 10);
            var topHeight = new Parameter(100, 16, 20);
            var legsWidth = new Parameter(200, 20, 20);
            var tableHeight = new Parameter(1400, 500, 500);

            var dict = new Dictionary<ParamType, Parameter>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, wrongTopDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);
            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(dict);

            Assert.That(incorrect.ElementAt(0), Is.EqualTo(IncorrectParameters.TopDepthIncorrect));
        }

        [Test]
        public void ParametersDependentWrongTest()
        {
            var topWidth = new Parameter(5000, 500, 500);
            var topDepth = new Parameter(5000, 500, 500);
            var topHeight = new Parameter(100, 16, 20);
            var legsWidth = new Parameter(200, 20, 200);
            var tableHeight = new Parameter(1400, 500, 500);

            var dict = new Dictionary<ParamType, Parameter>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, topDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);
            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(dict);

            Assert.That(incorrect.ElementAt(0), Is.EqualTo(IncorrectParameters.TopAndLegsAreaIncorrect));
        }
    }
}
