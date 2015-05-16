namespace DemoClient
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonSend = new System.Windows.Forms.Button();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxResponse = new System.Windows.Forms.TextBox();
			this.labelCounter = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(493, 28);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(75, 23);
			this.buttonSend.TabIndex = 0;
			this.buttonSend.Text = "&Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Location = new System.Drawing.Point(12, 28);
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(475, 22);
			this.textBoxMessage.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Message:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Server Response:";
			// 
			// textBoxResponse
			// 
			this.textBoxResponse.Location = new System.Drawing.Point(15, 86);
			this.textBoxResponse.Multiline = true;
			this.textBoxResponse.Name = "textBoxResponse";
			this.textBoxResponse.ReadOnly = true;
			this.textBoxResponse.Size = new System.Drawing.Size(553, 162);
			this.textBoxResponse.TabIndex = 4;
			// 
			// labelCounter
			// 
			this.labelCounter.AutoSize = true;
			this.labelCounter.Location = new System.Drawing.Point(146, 67);
			this.labelCounter.Name = "labelCounter";
			this.labelCounter.Size = new System.Drawing.Size(82, 16);
			this.labelCounter.TabIndex = 5;
			this.labelCounter.Text = "(Counter = ?)";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 260);
			this.Controls.Add(this.labelCounter);
			this.Controls.Add(this.textBoxResponse);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.buttonSend);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxResponse;
		private System.Windows.Forms.Label labelCounter;
	}
}

