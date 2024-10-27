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
using api_logic;

namespace plugin
{
    public partial class MainForm : Form
    {
        private Builder _builder;

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

            var dict = new Dictionary<ParamType, Parameter>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, topDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);

            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(dict);

            if (incorrect.Count == 0)
            {
                BuildModel(parameters);
            }
            else
            {
                PrintErrors(incorrect);
            }
        }

        private void PrintErrors(List<IncorrectParameters> incorrect)
        {
            labelError.Text = "";
            textBoxTopWidth.BackColor = Color.White;
            textBoxTopDepth.BackColor = Color.White;
            textBoxTopHeight.BackColor = Color.White;
            textBoxLegsWidth.BackColor = Color.White;
            textBoxTableHeight.BackColor = Color.White;

            foreach (var param in incorrect)
            {
                switch (param)
                {
                    case IncorrectParameters.TopWidthIncorrect:
                        labelError.Text += "Ошибка: параметр \"ширина столешницы\" должен входить в диапазон от 500 до 5000мм\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopDepthIncorrect:
                        labelError.Text += "Ошибка: параметр \"глубина столешницы\" должен входить в диапазон от 500 до 5000мм\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopDepth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopHeightIncorrect:
                        labelError.Text += "Ошибка: параметр \"высота столешницы\" должен входить в диапазон от 16 до 100мм\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.LegWidthIncorrect:
                        labelError.Text += "Ошибка: параметр \"ширина ножек\" должен входить в диапазон от 20 до 200мм\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxLegsWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TableHeightIncorrect:
                        labelError.Text += "Ошибка: параметр \"высота стола\" должен входить в диапазон от 500 до 1400мм\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTableHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopAndLegsAreaIncorrect:
                        labelError.Text += "Ошибка: связанные параметры \"ширина столешницы, глубина столешницы и ширина ножек\"\n" +
                                           "    имеют недопустимые параметры:\n" +
                                           "    площадь столешницы должна быть больше площади сечения ножек";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopWidth.BackColor = Color.LightPink;
                        textBoxTopDepth.BackColor = Color.LightPink;
                        textBoxLegsWidth.BackColor = Color.LightPink;
                        break;
                }
            }
        }

        void BuildModel(Parameters parameters)
        {
            Cad cad;
            if (radioButtonKompas.Checked)
            {
                cad = Cad.Kompas;
            }
            else
            {
                cad = Cad.AutoCad;
            }
            _builder = new Builder(parameters, cad);
            _builder.Build();
        }
    }
}
