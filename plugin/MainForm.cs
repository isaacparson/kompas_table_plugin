using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using logic;

namespace plugin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonInventor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            var topWidth = new Parameter(5000, 500, int.Parse(textBoxTopWidth.Text));
            var topDepth = new Parameter(5000, 500, int.Parse(textBoxTopDepth.Text));
            var topHeight = new Parameter(100, 16, int.Parse(textBoxTopHeight.Text));
            var legsWidth = new Parameter(200, 20, int.Parse(textBoxLegsWidth.Text));
            var tableHeight = new Parameter(1400, 500, int.Parse(textBoxTableHeight.Text));

            var isValidatedBasic = ValidateBasic(topWidth, topDepth, topHeight, legsWidth, tableHeight);

            var dict = new Dictionary<ParamType, Parameter>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, topDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);

            var parameters = new Parameters(dict);
            var isValidatedDependent = ValidateDependent(parameters);

            if (isValidatedBasic && isValidatedDependent)
            {
                BuildModel();
            }
        }

        public bool ValidateBasic(Parameter topWidth, Parameter topDepth, Parameter topHeight, Parameter legsHeight, Parameter tableHeight )
        {
            if (topWidth.Value < topWidth.MinValue || topWidth.Value > topWidth.MaxValue)
            {
                labelError.Text += "Ошибка: параметр \"ширина столешницы\" должен входить в диапазон от " + topWidth.MinValue + " до " + topWidth.MaxValue + "мм\n";
                labelError.BackColor = Color.LightPink;
                textBoxTopWidth.BackColor = Color.LightPink;
                return false;
            }
            if (topDepth.Value < topDepth.MinValue || topDepth.Value > topDepth.MaxValue)
            {
                labelError.Text += "Ошибка: параметр \"глубина столешницы\" должен входить в диапазон от " + topDepth.MinValue + " до " + topDepth.MaxValue + "мм\n";
                labelError.BackColor = Color.LightPink;
                textBoxTopDepth.BackColor = Color.LightPink;
                return false;
            }
            if (topHeight.Value < topHeight.MinValue || topHeight.Value > topHeight.MaxValue)
            {
                labelError.Text += "Ошибка: параметр \"высота столешницы\" должен входить в диапазон от " + topHeight.MinValue + " до " + topHeight.MaxValue + "мм\n";
                labelError.BackColor = Color.LightPink;
                textBoxTopHeight.BackColor = Color.LightPink;
                return false;
            }
            if (legsHeight.Value < legsHeight.MinValue || legsHeight.Value > legsHeight.MaxValue)
            {
                labelError.Text += "Ошибка: параметр \"ширина ножек\" должен входить в диапазон от " + legsHeight.MinValue + " до " + legsHeight.MaxValue + "мм\n";
                labelError.BackColor = Color.LightPink;
                textBoxLegsWidth.BackColor = Color.LightPink;
                return false;
            }
            if (tableHeight.Value < tableHeight.MinValue || tableHeight.Value > tableHeight.MaxValue)
            {
                labelError.Text += "Ошибка: параметр \"высота стола\" должен входить в диапазон от " + tableHeight.MinValue + " до " + tableHeight.MaxValue + "мм\n";
                labelError.BackColor = Color.LightPink;
                textBoxTableHeight.BackColor = Color.LightPink;
                return false;
            }
            return true;
        }

        bool ValidateDependent(Parameters parameters)
        {
            var incorrectParams = parameters.Validate();
            if (incorrectParams.Count > 0)
            {
                foreach (var param in incorrectParams)
                {
                    if (param.Equals(DependentParameters.TopAndLegsArea))
                    {
                        labelError.Text += "Ошибка: связанные параметры \"ширина столешницы, глубина столешницы и ширина ножек\"\n" +
                                           "    имеют недопустимые параметры:\n" +
                                           "    площадь столешницы должна быть больше площади сечения ножек";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopWidth.BackColor = Color.LightPink;
                        textBoxTopDepth.BackColor = Color.LightPink;
                        textBoxLegsWidth.BackColor = Color.LightPink;
                    }
                }
                return false;
            }
            return true;
        }

        void BuildModel()
        {

        }
    }
}
