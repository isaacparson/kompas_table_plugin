namespace plugin
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonKompas = new System.Windows.Forms.RadioButton();
            this.radioButtonInventor = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTopWidth = new System.Windows.Forms.TextBox();
            this.textBoxTopDepth = new System.Windows.Forms.TextBox();
            this.textBoxTopHeight = new System.Windows.Forms.TextBox();
            this.textBoxLegsHeight = new System.Windows.Forms.TextBox();
            this.textBoxTableHeight = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonKompas
            // 
            this.radioButtonKompas.AutoSize = true;
            this.radioButtonKompas.Location = new System.Drawing.Point(133, 14);
            this.radioButtonKompas.Name = "radioButtonKompas";
            this.radioButtonKompas.Size = new System.Drawing.Size(76, 20);
            this.radioButtonKompas.TabIndex = 0;
            this.radioButtonKompas.TabStop = true;
            this.radioButtonKompas.Text = "Компас";
            this.radioButtonKompas.UseVisualStyleBackColor = true;
            // 
            // radioButtonInventor
            // 
            this.radioButtonInventor.AutoSize = true;
            this.radioButtonInventor.Location = new System.Drawing.Point(215, 14);
            this.radioButtonInventor.Name = "radioButtonInventor";
            this.radioButtonInventor.Size = new System.Drawing.Size(75, 20);
            this.radioButtonInventor.TabIndex = 1;
            this.radioButtonInventor.TabStop = true;
            this.radioButtonInventor.Text = "Inventor";
            this.radioButtonInventor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите САПР:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ширина столешницы:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Глубина столешницы:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Толщина столешницы:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ширина ножек:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Высота стола:";
            // 
            // textBoxTopWidth
            // 
            this.textBoxTopWidth.Location = new System.Drawing.Point(215, 71);
            this.textBoxTopWidth.Name = "textBoxTopWidth";
            this.textBoxTopWidth.Size = new System.Drawing.Size(100, 22);
            this.textBoxTopWidth.TabIndex = 8;
            // 
            // textBoxTopDepth
            // 
            this.textBoxTopDepth.Location = new System.Drawing.Point(215, 116);
            this.textBoxTopDepth.Name = "textBoxTopDepth";
            this.textBoxTopDepth.Size = new System.Drawing.Size(100, 22);
            this.textBoxTopDepth.TabIndex = 9;
            // 
            // textBoxTopHeight
            // 
            this.textBoxTopHeight.Location = new System.Drawing.Point(215, 163);
            this.textBoxTopHeight.Name = "textBoxTopHeight";
            this.textBoxTopHeight.Size = new System.Drawing.Size(100, 22);
            this.textBoxTopHeight.TabIndex = 10;
            // 
            // textBoxLegsHeight
            // 
            this.textBoxLegsHeight.Location = new System.Drawing.Point(215, 206);
            this.textBoxLegsHeight.Name = "textBoxLegsHeight";
            this.textBoxLegsHeight.Size = new System.Drawing.Size(100, 22);
            this.textBoxLegsHeight.TabIndex = 11;
            // 
            // textBoxTableHeight
            // 
            this.textBoxTableHeight.Location = new System.Drawing.Point(215, 249);
            this.textBoxTableHeight.Name = "textBoxTableHeight";
            this.textBoxTableHeight.Size = new System.Drawing.Size(100, 22);
            this.textBoxTableHeight.TabIndex = 12;
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(115, 333);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(140, 47);
            this.buttonRun.TabIndex = 13;
            this.buttonRun.Text = "Построить";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 450);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.textBoxTableHeight);
            this.Controls.Add(this.textBoxLegsHeight);
            this.Controls.Add(this.textBoxTopHeight);
            this.Controls.Add(this.textBoxTopDepth);
            this.Controls.Add(this.textBoxTopWidth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonInventor);
            this.Controls.Add(this.radioButtonKompas);
            this.Name = "MainForm";
            this.Text = "Плагин построения объекта \"Стол\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonKompas;
        private System.Windows.Forms.RadioButton radioButtonInventor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTopWidth;
        private System.Windows.Forms.TextBox textBoxTopDepth;
        private System.Windows.Forms.TextBox textBoxTopHeight;
        private System.Windows.Forms.TextBox textBoxLegsHeight;
        private System.Windows.Forms.TextBox textBoxTableHeight;
        private System.Windows.Forms.Button buttonRun;
    }
}

