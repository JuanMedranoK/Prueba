namespace RedSocial
{
    partial class friendsPostList
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblPost = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.btnPost = new System.Windows.Forms.Button();
            this.posPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Crimson;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 109);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Crimson;
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Location = new System.Drawing.Point(139, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(317, 33);
            this.panel2.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(82, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "UserName";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblPost);
            this.panel3.Location = new System.Drawing.Point(139, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(492, 57);
            this.panel3.TabIndex = 2;
            // 
            // lblPost
            // 
            this.lblPost.Location = new System.Drawing.Point(-50, -8);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(545, 46);
            this.lblPost.TabIndex = 0;
            this.lblPost.Text = "Lorem ipsum es el texto que se usa habitualmente en diseño gráfico en demostracio" +
    "nes de tipografías o de borradores de diseño para probar el diseño visual antes " +
    "de insertar el texto fina";
            this.lblPost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(142, 85);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(350, 20);
            this.txtComments.TabIndex = 3;
            // 
            // btnPost
            // 
            this.btnPost.BackColor = System.Drawing.Color.Crimson;
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost.ForeColor = System.Drawing.Color.White;
            this.btnPost.Location = new System.Drawing.Point(499, 81);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(69, 27);
            this.btnPost.TabIndex = 4;
            this.btnPost.Text = "Publicar";
            this.btnPost.UseVisualStyleBackColor = false;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // posPanel
            // 
            this.posPanel.AutoScroll = true;
            this.posPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.posPanel.Location = new System.Drawing.Point(0, 133);
            this.posPanel.Name = "posPanel";
            this.posPanel.Size = new System.Drawing.Size(620, 62);
            this.posPanel.TabIndex = 5;
            // 
            // friendsPostList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.posPanel);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "friendsPostList";
            this.Size = new System.Drawing.Size(620, 195);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.FlowLayoutPanel posPanel;
    }
}