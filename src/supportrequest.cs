using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net;
using System.Drawing;
using Microsoft.Win32;

[assembly: AssemblyTitle("Support Request")]
[assembly: AssemblyDescription("NodePoint Sample Ticketing App")]
[assembly: AssemblyProduct("Support Request")]
[assembly: AssemblyCopyright("(C) 2015 Patrick Lambert")]
[assembly: AssemblyVersion("0.0.1.0")]
[assembly: AssemblyFileVersion("0.0.1.0")]

namespace SupportRequest
{
	public class Program
	{
		public static void submit(string title, string description, string urgent)
		{
			if(title == "" || description == "")
			{
				MessageBox.Show("Please enter a title and description.", "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					RegistryKey rkey = Registry.LocalMachine.OpenSubKey("Software\\SupportRequest");
					if(rkey == null)
					{
						MessageBox.Show("Could not read configuration values. Try reinstalling the application.", "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					WebClient wc = new WebClient();
					wc.QueryString.Add("api", "add_ticket");
					wc.QueryString.Add("key", (string)rkey.GetValue("key"));
					wc.QueryString.Add("product_id", (string)rkey.GetValue("product_id"));
					wc.QueryString.Add("release_id", (string)rkey.GetValue("release_id"));
					wc.QueryString.Add("title", title);
					wc.QueryString.Add("description", "Support Request for: " + Environment.MachineName + "\\" + Environment.UserName + "\n\n" + description);
					wc.QueryString.Add("custom", urgent);
					string result = wc.DownloadString((string)rkey.GetValue("url"));
					if (result.Contains("OK")) { MessageBox.Show("Ticket added.", "Support Request"); }
					else { MessageBox.Show("Ticket creation failed:\n\n" + result, "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ticket creation failed:\n\n" + ex, "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		public static void Main()
		{
			TextBox titlebox = new TextBox();
			Label label1 = new Label();
			TextBox descriptionbox = new TextBox();
			Label label2 = new Label();
			Button button1 = new Button();
			CheckBox urgentbox = new CheckBox();
			Form win = new Form();
			win.SuspendLayout();

			// Title entry
			titlebox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
			titlebox.Location = new System.Drawing.Point(84, 13);
			titlebox.MaxLength = 200;
			titlebox.Name = "titlebox";
			titlebox.Size = new System.Drawing.Size(628, 20);
			titlebox.TabIndex = 0;

			// Title label
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(6, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(72, 13);
			label1.TabIndex = 1;
			label1.Text = "Ticket title:";

			// Description box
			descriptionbox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
			descriptionbox.Location = new System.Drawing.Point(9, 61);
			descriptionbox.Multiline = true;
			descriptionbox.Name = "descriptionbox";
			descriptionbox.ScrollBars = ScrollBars.Vertical;
			descriptionbox.Size = new System.Drawing.Size(703, 195);
			descriptionbox.TabIndex = 2;

			// Description label
			label2.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label2.Location = new System.Drawing.Point(118, 45);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(479, 13);
			label2.TabIndex = 3;
			label2.Text = "Enter your support request here. Please be descriptive and mention any error message you received:";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			// Submit button
			button1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
			button1.Location = new System.Drawing.Point(637, 274);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 4;
			button1.Text = "Submit";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(delegate { button1.Enabled = false; submit(titlebox.Text, descriptionbox.Text, urgentbox.Checked.ToString()); button1.Enabled = true; });

			// Urgent ticket checkbox
			urgentbox.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
			urgentbox.AutoSize = true;
			urgentbox.Location = new System.Drawing.Point(13, 274);
			urgentbox.Name = "urgentbox";
			urgentbox.Size = new System.Drawing.Size(145, 17);
			urgentbox.TabIndex = 5;
			urgentbox.Text = "This is an urgent request.";
			urgentbox.UseVisualStyleBackColor = true;

			// Window
			win.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			win.AutoScaleMode = AutoScaleMode.Font;
			win.ClientSize = new System.Drawing.Size(724, 309);
			win.Controls.Add(urgentbox);
			win.Controls.Add(button1);
			win.Controls.Add(label2);
			win.Controls.Add(descriptionbox);
			win.Controls.Add(label1);
			win.Controls.Add(titlebox);
			win.MinimumSize = new System.Drawing.Size(740, 347);
			win.Name = "Win";
			win.StartPosition = FormStartPosition.CenterScreen;
			win.Text = "Support Request";
			win.ResumeLayout(false);
			win.PerformLayout();
			win.ShowDialog();
		}
	}
}