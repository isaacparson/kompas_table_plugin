using ParametersLogic;

namespace ParametersLogicTests
{
    [TestFixture]
    public class ParametersTests
    {
        [Test]
        public void ParametersRightTest()
        {
            int topWidth = 500;
            int topDepth = 500;
            int topHeight = 20;
            int legsWidth = 20;
            int tableHeight = 500;

            var dict = new Dictionary<ParamType, int>();
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
            int topWidth = 500;
            int wrongTopDepth = 10;
            int topHeight = 20;
            int legsWidth = 20;
            int tableHeight = 500;

            var dict = new Dictionary<ParamType, int>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, wrongTopDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);
            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(dict);

            Assert.That(incorrect.ElementAt(0).Key, Is.EqualTo(IncorrectParameters.TopDepthIncorrect));
        }

        [Test]
        public void ParametersDependentWrongTest()
        {
            int topWidth = 500;
            int topDepth = 500;
            int topHeight = 20;
            int legsWidth = 200;
            int tableHeight = 500;

            var dict = new Dictionary<ParamType, int>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, topDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);
            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(dict);

            Assert.That(incorrect.ElementAt(0).Key, Is.EqualTo(IncorrectParameters.TopAndLegsAreaIncorrect));
        }
    }
}
