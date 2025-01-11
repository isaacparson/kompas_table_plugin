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
        /// <summary>
        /// Объект класса построителя стола.
        /// </summary>
        private Builder _builder;

        /// <summary>
        /// ctor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Run.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRun_Click(object sender, EventArgs e)
        {
            var voidTextBoxes = AreTextBoxesVoid();
            if (!voidTextBoxes)
            {
                var paramDict = new Dictionary<ParamType, int>();

                int.TryParse(textBoxTopWidth.Text, out int topWidth);
                int.TryParse(textBoxTopDepth.Text, out int topDepth);
                int.TryParse(textBoxTopHeight.Text, out int topHeight);
                int.TryParse(textBoxLegsWidth.Text, out int legsWidth);
                int.TryParse(textBoxTableHeight.Text, out int tableHeight);

                paramDict.Add(ParamType.TopWidth, topWidth);
                paramDict.Add(ParamType.TopDepth, topDepth);
                paramDict.Add(ParamType.TopHeight, topHeight);
                paramDict.Add(ParamType.LegWidth, legsWidth);
                paramDict.Add(ParamType.TableHeight, tableHeight);

                var parameters = new Parameters();
                var incorrect = parameters.SetParameters(paramDict);

                if (incorrect.Count == 0 && !voidTextBoxes)
                {
                    BuildModel(parameters);
                }
                else
                {
                    PrintErrors(incorrect, parameters);
                }
            }
        }

        /// <summary>
        /// Метод валидации на пустоту textBox-ов.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Метод устанавливает ограничение на ввод в textBox-ы только цифр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_OnlyDigitKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Метод вывода ошибок в форму.
        /// </summary>
        /// <param name="incorrect"></param>
        /// <param name="parameters"></param>
        private void PrintErrors(Dictionary<IncorrectParameters, string> incorrect, Parameters parameters)
        {
            var dict = parameters.GetParameters();

            foreach (var param in incorrect)
            {
                switch (param.Key)
                {
                    case IncorrectParameters.TopWidthIncorrect:
                        labelError.Text += "Ошибка: параметр \"ширина столешницы\" должен входить в диапазон: " + param.Value + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopDepthIncorrect:
                        labelError.Text += "Ошибка: параметр \"глубина столешницы\" должен входить в диапазон: " + param.Value + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopDepth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopHeightIncorrect:
                        labelError.Text += "Ошибка: параметр \"высота столешницы\" должен входить в диапазон: " + param.Value + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.LegWidthIncorrect:
                        labelError.Text += "Ошибка: параметр \"ширина ножек\" должен входить в диапазон: " + param.Value + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxLegsWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TableHeightIncorrect:
                        labelError.Text += "Ошибка: параметр \"высота стола\" должен входить в диапазон: " + param.Value + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTableHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopAndLegsAreaIncorrect:
                        labelError.Text += 
                            "Ошибка: связанные параметры \"ширина столешницы, глубина столешницы и ширина ножек\"\n" +
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

        /// <summary>
        /// Метод построения модели.
        /// </summary>
        /// <param name="parameters"></param>
        void BuildModel(Parameters parameters)
        {
            Cad cad = radioButtonKompas.Checked
                ? Cad.Kompas
                : Cad.AutoCad;

            _builder = new Builder(parameters, cad);
            _builder.Build();
        }
    }
}
