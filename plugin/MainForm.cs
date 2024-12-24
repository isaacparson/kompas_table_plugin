using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ParametersLogic;
using ApiLogic;

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
            var voidTextBoxes = AreTextBoxesVoid();

            var paramList = new Dictionary<ParamType, int>();

            int.TryParse(textBoxTopWidth.Text, out int topWidth);
            int.TryParse(textBoxTopDepth.Text, out int topDepth);
            int.TryParse(textBoxTopHeight.Text, out int topHeight);
            int.TryParse(textBoxLegsWidth.Text, out int legsWidth);
            int.TryParse(textBoxTableHeight.Text, out int tableHeight);

            paramList.Add(ParamType.TopWidth, topWidth);
            paramList.Add(ParamType.TopDepth, topDepth);
            paramList.Add(ParamType.TopHeight, topHeight);
            paramList.Add(ParamType.LegWidth, legsWidth);
            paramList.Add(ParamType.TableHeight, tableHeight);

            var parameters = new Parameters();
            var incorrect = parameters.SetParameters(paramList);

            if (incorrect.Count == 0 && !voidTextBoxes)
            {
                BuildModel(parameters);
            }
            else
            {
                PrintErrors(incorrect);
            }
        }

        private bool AreTextBoxesVoid()
        {
            labelError.Text = "";
            textBoxTopWidth.BackColor = Color.White;
            textBoxTopDepth.BackColor = Color.White;
            textBoxTopHeight.BackColor = Color.White;
            textBoxLegsWidth.BackColor = Color.White;
            textBoxTableHeight.BackColor = Color.White;

            bool rv = false;
            if (textBoxTopWidth.Text == "")
            {
                labelError.Text += "Ошибка: значение ширины столешницы не может быть пустым\n";
                textBoxTopWidth.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxTopDepth.Text == "")
            {
                labelError.Text += "Ошибка: значение глубины столешницы не может быть пустым\n";
                textBoxTopDepth.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxTopHeight.Text == "")
            {
                labelError.Text += "Ошибка: значение толщины столешницы не может быть пустым\n";
                textBoxTopHeight.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxLegsWidth.Text == "")
            {
                labelError.Text += "Ошибка: значение ширины ножек не может быть пустым\n";
                textBoxLegsWidth.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxTableHeight.Text == "")
            {
                labelError.Text += "Ошибка: значение высоты стола не может быть пустым\n";
                textBoxTableHeight.BackColor = Color.LightPink;
                rv = true;
            }

            return rv;
        }

        private void TextBox_OnlyDigitKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PrintErrors(List<IncorrectParameters> incorrect)
        {
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
            Cad cad = radioButtonKompas.Checked
                ? Cad.Kompas
                : Cad.AutoCad;

            _builder = new Builder(parameters, cad);
            _builder.Build();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
