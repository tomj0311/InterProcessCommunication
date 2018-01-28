namespace MvpChatServer.View
{
    partial class ReceiveForm
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
            this.MessagesList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // MessagesList
            // 
            this.MessagesList.Location = new System.Drawing.Point(13, 13);
            this.MessagesList.Name = "MessagesList";
            this.MessagesList.Size = new System.Drawing.Size(267, 248);
            this.MessagesList.TabIndex = 0;
            this.MessagesList.UseCompatibleStateImageBehavior = false;
            // 
            // ReceiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 355);
            this.Controls.Add(this.MessagesList);
            this.Name = "ReceiveForm";
            this.Text = "ReceiveForm";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView MessagesList;
    }
}