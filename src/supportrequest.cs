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
		static TextBox titlebox;
		static Label label1;
		static TextBox descriptionbox;
		static Label label2;
		static Button button1;
		static CheckBox urgentbox;
		static Form win;

		// Submit new ticket
		public static void submit()
		{
			if(titlebox.Text == "" || descriptionbox.Text == "")
			{
				MessageBox.Show("Please enter a title and description.", "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				button1.Enabled = false;
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
					wc.QueryString.Add("title", titlebox.Text);
					wc.QueryString.Add("description", "Support Request for: " + Environment.MachineName + "\\" + Environment.UserName + "\n\n" + descriptionbox.Text);
					wc.QueryString.Add("custom", urgentbox.Checked.ToString());
					string result = wc.DownloadString((string)rkey.GetValue("url"));
					if (result.Contains("OK")) { MessageBox.Show("Ticket added.", "Support Request"); }
					else { MessageBox.Show("Ticket creation failed:\n\n" + result, "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ticket creation failed:\n\n" + ex, "Support Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				button1.Enabled = true;
			}
		}

		// Make UI
		public static void Main()
		{
			win = new Form();

			// Title entry
			titlebox = new TextBox();
			titlebox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
			titlebox.Location = new System.Drawing.Point(84, 13);
			titlebox.MaxLength = 200;
			titlebox.Name = "titlebox";
			titlebox.Size = new System.Drawing.Size(628, 20);
			titlebox.TabIndex = 0;

			// Title label
			label1 = new Label();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(6, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(72, 13);
			label1.TabIndex = 1;
			label1.Text = "Ticket title:";

			// Description box
			descriptionbox = new TextBox();
			descriptionbox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
			descriptionbox.Location = new System.Drawing.Point(9, 61);
			descriptionbox.Multiline = true;
			descriptionbox.Name = "descriptionbox";
			descriptionbox.ScrollBars = ScrollBars.Vertical;
			descriptionbox.Size = new System.Drawing.Size(703, 195);
			descriptionbox.TabIndex = 2;

			// Description label
			label2 = new Label();
			label2.Anchor = ((AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right));
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label2.Location = new System.Drawing.Point(118, 40);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(703, 13);
			label2.TabIndex = 3;
			label2.Text = "Enter your support request here. Please be descriptive and mention any error message you received:";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			// Submit button
			button1 = new Button();
			button1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
			button1.Location = new System.Drawing.Point(637, 274);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 4;
			button1.Text = "Submit";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(delegate { submit(); });

			// Urgent ticket checkbox
			urgentbox = new CheckBox();
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

			win.ShowDialog();
		}
	}
}