using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ParametersLogic;
using ApiLogic;

namespace plugin
{
    /// <summary>
    /// Главная форма для ввода значений параметров.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объект класса построителя стола.
        /// </summary>
        private Builder _builder;

        /// <summary>
        /// Конструктор главной формы.
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
        private void ButtonRunClick(object sender, EventArgs e)
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
        /// <returns>Результат выполнения операции. True если пустые поля для ввода. False если пустых нет.</returns>
        private bool AreTextBoxesVoid()
        {
            labelError.Text = "";

            List<TextBox> textBoxes = new List<TextBox>()
            {
                textBoxTopWidth,
                textBoxTopDepth,
                textBoxTopHeight,
                textBoxLegsWidth,
                textBoxTableHeight,
            };

            bool wereVoid = false;

            foreach ( var textBox in textBoxes )
            {
                textBox.BackColor = Color.White;

                if (textBox.Text == "")
                {
                    var parameterName = GetParameterName(textBox);
                    labelError.Text += "Ошибка: параметр \"" + parameterName + "\" не может быть пустым\n";
                    wereVoid = true;
                }
            }

            return wereVoid;
        }

        /// <summary>
        /// Метод устанавливает ограничение на ввод в textBox-ы только цифр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxOnlyDigitKeyPress(object sender, KeyPressEventArgs e)
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
                if(param.Key != IncorrectParameters.TopAndLegsAreaIncorrect)
                {
                    string parameterName = GetParameterName(param.Key);
                    labelError.Text += "Ошибка: параметр \"" + parameterName + "\" должен входить в диапазон: " + param.Value + "\n";
                    labelError.BackColor = Color.LightPink;
                }

                switch (param.Key)
                {
                    case IncorrectParameters.TopWidthIncorrect:
                        textBoxTopWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopDepthIncorrect:
                        textBoxTopDepth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopHeightIncorrect:
                        textBoxTopHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.LegWidthIncorrect:
                        textBoxLegsWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TableHeightIncorrect:
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
        /// Метод конвертации TextBox-a в строку с названием соответствующего параметра.
        /// </summary>
        /// <param name="textBox">TextBox для которого необходимо узнать имя параметра.</param>
        /// <returns></returns>
        private string GetParameterName(TextBox textBox)
        {
            var dict = new Dictionary<TextBox, string>
            {
                {textBoxTopWidth, "Ширина столешницы"},
                {textBoxTopDepth, "Глубина столешницы"},
                {textBoxTopHeight, "Высота столешницы"},
                {textBoxLegsWidth, "Ширина ножек"},
                {textBoxTableHeight, "Высота стола"},
            };

            return dict[textBox];
        }

        /// <summary>
        /// Метод конвертации перечисления неверного параметра в строку с названием соответствующего параметра.
        /// </summary>
        /// <param name="parameter">Неверный параметр.</param>
        /// <returns></returns>
        private string GetParameterName(IncorrectParameters parameter)
        {
            var dict = new Dictionary<IncorrectParameters, string>
            {
                {IncorrectParameters.TopWidthIncorrect, "Ширина столешницы"},
                {IncorrectParameters.TopDepthIncorrect, "Глубина столешницы"},
                {IncorrectParameters.TopHeightIncorrect, "Высота столешницы"},
                {IncorrectParameters.LegWidthIncorrect, "Ширина ножек"},
                {IncorrectParameters.TableHeightIncorrect, "Высота стола"},
            };

            return dict[parameter];
        }

        /// <summary>
        /// Метод построения модели.
        /// </summary>
        /// <param name="parameters"></param>
        private void BuildModel(Parameters parameters)
        {
            Cad cad = radioButtonKompas.Checked
                ? Cad.Kompas
                : Cad.Inventor;

            _builder = new Builder(parameters, cad);
            _builder.Build();
        }
    }
}
