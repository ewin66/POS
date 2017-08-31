namespace disc1
{	
	using System;
	using System.Drawing;
	using System.Collections;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Data;
    using System.Diagnostics;	
	using System.IO;
	using disc1.XML;

	/// <summary>
	/// Test form to demonstrate loading and saving an object to XML using the ObjectXMLSerializer class.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Name of the test file used to load/save the customer object.
		/// </summary>
		private const string XML_FILE_NAME = "CustomerObject.xml";

		private System.Windows.Forms.Button LoadObjectButton;
		private System.Windows.Forms.Button SaveObjectButton;
		private System.Windows.Forms.Button ViewXMLFileButton;
		private System.Windows.Forms.Label AgeLabel;
		private System.Windows.Forms.TextBox CustomerAgeTextBox;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.TextBox CustomerNameTextBox;
		private System.Windows.Forms.Label IDLabel;
		private System.Windows.Forms.TextBox CustomerIDTextBox;
		private System.Windows.Forms.PictureBox CustomerPictureBox;
		private System.Windows.Forms.Label PictureLabel;
		private System.Windows.Forms.Button LoadPictureButton;
		private System.Windows.Forms.ListBox HobbiesListBox;
		private System.Windows.Forms.Label HobbiesLabel;
		private System.Windows.Forms.Label EmailAddressesLabel;
		private System.Windows.Forms.ListBox EmailsListBox;
		private System.Windows.Forms.Panel BannerPanel;
		private System.Windows.Forms.Label BannerLabel;
		private System.Windows.Forms.Label OptionsLabel;
		private System.Windows.Forms.GroupBox OptionsGroupBox;
		private System.Windows.Forms.GroupBox CustomerGroupBox;
		private System.Windows.Forms.Label CustomerLabel;
		private System.Windows.Forms.PictureBox BannerPictureBox;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.LoadObjectButton = new System.Windows.Forms.Button();
			this.SaveObjectButton = new System.Windows.Forms.Button();
			this.ViewXMLFileButton = new System.Windows.Forms.Button();
			this.EmailAddressesLabel = new System.Windows.Forms.Label();
			this.EmailsListBox = new System.Windows.Forms.ListBox();
			this.HobbiesLabel = new System.Windows.Forms.Label();
			this.LoadPictureButton = new System.Windows.Forms.Button();
			this.PictureLabel = new System.Windows.Forms.Label();
			this.CustomerPictureBox = new System.Windows.Forms.PictureBox();
			this.AgeLabel = new System.Windows.Forms.Label();
			this.CustomerAgeTextBox = new System.Windows.Forms.TextBox();
			this.NameLabel = new System.Windows.Forms.Label();
			this.CustomerNameTextBox = new System.Windows.Forms.TextBox();
			this.IDLabel = new System.Windows.Forms.Label();
			this.CustomerIDTextBox = new System.Windows.Forms.TextBox();
			this.HobbiesListBox = new System.Windows.Forms.ListBox();
			this.BannerPanel = new System.Windows.Forms.Panel();
			this.BannerPictureBox = new System.Windows.Forms.PictureBox();
			this.BannerLabel = new System.Windows.Forms.Label();
			this.OptionsLabel = new System.Windows.Forms.Label();
			this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.CustomerGroupBox = new System.Windows.Forms.GroupBox();
			this.CustomerLabel = new System.Windows.Forms.Label();
			this.BannerPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// LoadObjectButton
			// 
			this.LoadObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.LoadObjectButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoadObjectButton.Location = new System.Drawing.Point(132, 392);
			this.LoadObjectButton.Name = "LoadObjectButton";
			this.LoadObjectButton.Size = new System.Drawing.Size(88, 23);
			this.LoadObjectButton.TabIndex = 18;
			this.LoadObjectButton.Text = "&Load";
			this.LoadObjectButton.Click += new System.EventHandler(this.LoadObjectButton_Click);
			// 
			// SaveObjectButton
			// 
			this.SaveObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SaveObjectButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SaveObjectButton.Location = new System.Drawing.Point(32, 392);
			this.SaveObjectButton.Name = "SaveObjectButton";
			this.SaveObjectButton.Size = new System.Drawing.Size(88, 23);
			this.SaveObjectButton.TabIndex = 17;
			this.SaveObjectButton.Text = "&Save";
			this.SaveObjectButton.Click += new System.EventHandler(this.SaveObjectButton_Click);
			// 
			// ViewXMLFileButton
			// 
			this.ViewXMLFileButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ViewXMLFileButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ViewXMLFileButton.Location = new System.Drawing.Point(232, 392);
			this.ViewXMLFileButton.Name = "ViewXMLFileButton";
			this.ViewXMLFileButton.Size = new System.Drawing.Size(88, 23);
			this.ViewXMLFileButton.TabIndex = 19;
			this.ViewXMLFileButton.Text = "&View";
			this.ViewXMLFileButton.Click += new System.EventHandler(this.ViewXMLFileButton_Click);
			// 
			// EmailAddressesLabel
			// 
			this.EmailAddressesLabel.AutoSize = true;
			this.EmailAddressesLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.EmailAddressesLabel.Location = new System.Drawing.Point(32, 213);
			this.EmailAddressesLabel.Name = "EmailAddressesLabel";
			this.EmailAddressesLabel.Size = new System.Drawing.Size(52, 17);
			this.EmailAddressesLabel.TabIndex = 11;
			this.EmailAddressesLabel.Text = "E-mails:";
			this.EmailAddressesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// EmailsListBox
			// 
			this.EmailsListBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.EmailsListBox.Items.AddRange(new object[] {
															   "joe.b@disc1software.com - Business",
															   "joe.b@athome.com - Home",
															   "joe.b@hotmail.com - Other"});
			this.EmailsListBox.Location = new System.Drawing.Point(96, 213);
			this.EmailsListBox.Name = "EmailsListBox";
			this.EmailsListBox.Size = new System.Drawing.Size(224, 43);
			this.EmailsListBox.TabIndex = 12;
			// 
			// HobbiesLabel
			// 
			this.HobbiesLabel.AutoSize = true;
			this.HobbiesLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HobbiesLabel.Location = new System.Drawing.Point(32, 166);
			this.HobbiesLabel.Name = "HobbiesLabel";
			this.HobbiesLabel.Size = new System.Drawing.Size(55, 17);
			this.HobbiesLabel.TabIndex = 9;
			this.HobbiesLabel.Text = "Hobbies:";
			this.HobbiesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LoadPictureButton
			// 
			this.LoadPictureButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.LoadPictureButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoadPictureButton.Location = new System.Drawing.Point(183, 296);
			this.LoadPictureButton.Name = "LoadPictureButton";
			this.LoadPictureButton.Size = new System.Drawing.Size(137, 23);
			this.LoadPictureButton.TabIndex = 14;
			this.LoadPictureButton.Text = "Load &picture...";
			this.LoadPictureButton.Click += new System.EventHandler(this.LoadPictureButton_Click);
			// 
			// PictureLabel
			// 
			this.PictureLabel.AutoSize = true;
			this.PictureLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PictureLabel.Location = new System.Drawing.Point(32, 260);
			this.PictureLabel.Name = "PictureLabel";
			this.PictureLabel.Size = new System.Drawing.Size(48, 17);
			this.PictureLabel.TabIndex = 13;
			this.PictureLabel.Text = "Picture:";
			this.PictureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CustomerPictureBox
			// 
			this.CustomerPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CustomerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CustomerPictureBox.Image")));
			this.CustomerPictureBox.Location = new System.Drawing.Point(96, 260);
			this.CustomerPictureBox.Name = "CustomerPictureBox";
			this.CustomerPictureBox.Size = new System.Drawing.Size(80, 96);
			this.CustomerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.CustomerPictureBox.TabIndex = 7;
			this.CustomerPictureBox.TabStop = false;
			// 
			// AgeLabel
			// 
			this.AgeLabel.AutoSize = true;
			this.AgeLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AgeLabel.Location = new System.Drawing.Point(32, 141);
			this.AgeLabel.Name = "AgeLabel";
			this.AgeLabel.Size = new System.Drawing.Size(31, 17);
			this.AgeLabel.TabIndex = 7;
			this.AgeLabel.Text = "Age:";
			this.AgeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CustomerAgeTextBox
			// 
			this.CustomerAgeTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CustomerAgeTextBox.Location = new System.Drawing.Point(96, 141);
			this.CustomerAgeTextBox.Name = "CustomerAgeTextBox";
			this.CustomerAgeTextBox.Size = new System.Drawing.Size(48, 21);
			this.CustomerAgeTextBox.TabIndex = 8;
			this.CustomerAgeTextBox.Text = "";
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.NameLabel.Location = new System.Drawing.Point(32, 116);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(42, 17);
			this.NameLabel.TabIndex = 5;
			this.NameLabel.Text = "Name:";
			this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CustomerNameTextBox
			// 
			this.CustomerNameTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CustomerNameTextBox.Location = new System.Drawing.Point(96, 116);
			this.CustomerNameTextBox.Name = "CustomerNameTextBox";
			this.CustomerNameTextBox.Size = new System.Drawing.Size(224, 21);
			this.CustomerNameTextBox.TabIndex = 6;
			this.CustomerNameTextBox.Text = "";
			// 
			// IDLabel
			// 
			this.IDLabel.AutoSize = true;
			this.IDLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.IDLabel.Location = new System.Drawing.Point(32, 91);
			this.IDLabel.Name = "IDLabel";
			this.IDLabel.Size = new System.Drawing.Size(23, 17);
			this.IDLabel.TabIndex = 3;
			this.IDLabel.Text = "ID:";
			this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CustomerIDTextBox
			// 
			this.CustomerIDTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CustomerIDTextBox.Location = new System.Drawing.Point(96, 91);
			this.CustomerIDTextBox.Name = "CustomerIDTextBox";
			this.CustomerIDTextBox.Size = new System.Drawing.Size(48, 21);
			this.CustomerIDTextBox.TabIndex = 4;
			this.CustomerIDTextBox.Text = "";
			// 
			// HobbiesListBox
			// 
			this.HobbiesListBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HobbiesListBox.Items.AddRange(new object[] {
																"Golf",
																"Tennis",
																"Reading"});
			this.HobbiesListBox.Location = new System.Drawing.Point(96, 166);
			this.HobbiesListBox.Name = "HobbiesListBox";
			this.HobbiesListBox.Size = new System.Drawing.Size(224, 43);
			this.HobbiesListBox.TabIndex = 10;
			// 
			// BannerPanel
			// 
			this.BannerPanel.BackColor = System.Drawing.Color.White;
			this.BannerPanel.Controls.Add(this.BannerPictureBox);
			this.BannerPanel.Controls.Add(this.BannerLabel);
			this.BannerPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.BannerPanel.Location = new System.Drawing.Point(0, 0);
			this.BannerPanel.Name = "BannerPanel";
			this.BannerPanel.Size = new System.Drawing.Size(330, 50);
			this.BannerPanel.TabIndex = 0;
			// 
			// BannerPictureBox
			// 
			this.BannerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("BannerPictureBox.Image")));
			this.BannerPictureBox.Location = new System.Drawing.Point(10, 10);
			this.BannerPictureBox.Name = "BannerPictureBox";
			this.BannerPictureBox.Size = new System.Drawing.Size(32, 32);
			this.BannerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.BannerPictureBox.TabIndex = 1;
			this.BannerPictureBox.TabStop = false;
			// 
			// BannerLabel
			// 
			this.BannerLabel.AutoSize = true;
			this.BannerLabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BannerLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.BannerLabel.Location = new System.Drawing.Point(45, 12);
			this.BannerLabel.Name = "BannerLabel";
			this.BannerLabel.Size = new System.Drawing.Size(230, 26);
			this.BannerLabel.TabIndex = 0;
			this.BannerLabel.Text = "Load-Save objects to XML";
			this.BannerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OptionsLabel
			// 
			this.OptionsLabel.AutoSize = true;
			this.OptionsLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OptionsLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.OptionsLabel.Location = new System.Drawing.Point(8, 368);
			this.OptionsLabel.Name = "OptionsLabel";
			this.OptionsLabel.Size = new System.Drawing.Size(53, 17);
			this.OptionsLabel.TabIndex = 15;
			this.OptionsLabel.Text = "Options";
			// 
			// OptionsGroupBox
			// 
			this.OptionsGroupBox.Location = new System.Drawing.Point(64, 368);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(256, 8);
			this.OptionsGroupBox.TabIndex = 16;
			this.OptionsGroupBox.TabStop = false;
			// 
			// CustomerGroupBox
			// 
			this.CustomerGroupBox.Location = new System.Drawing.Point(128, 64);
			this.CustomerGroupBox.Name = "CustomerGroupBox";
			this.CustomerGroupBox.Size = new System.Drawing.Size(192, 8);
			this.CustomerGroupBox.TabIndex = 2;
			this.CustomerGroupBox.TabStop = false;
			// 
			// CustomerLabel
			// 
			this.CustomerLabel.AutoSize = true;
			this.CustomerLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CustomerLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.CustomerLabel.Location = new System.Drawing.Point(8, 64);
			this.CustomerLabel.Name = "CustomerLabel";
			this.CustomerLabel.Size = new System.Drawing.Size(110, 17);
			this.CustomerLabel.TabIndex = 1;
			this.CustomerLabel.Text = "Customer record";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(330, 432);
			this.Controls.Add(this.OptionsGroupBox);
			this.Controls.Add(this.OptionsLabel);
			this.Controls.Add(this.CustomerLabel);
			this.Controls.Add(this.CustomerNameTextBox);
			this.Controls.Add(this.CustomerIDTextBox);
			this.Controls.Add(this.PictureLabel);
			this.Controls.Add(this.CustomerAgeTextBox);
			this.Controls.Add(this.NameLabel);
			this.Controls.Add(this.IDLabel);
			this.Controls.Add(this.EmailAddressesLabel);
			this.Controls.Add(this.HobbiesLabel);
			this.Controls.Add(this.AgeLabel);
			this.Controls.Add(this.BannerPanel);
			this.Controls.Add(this.ViewXMLFileButton);
			this.Controls.Add(this.SaveObjectButton);
			this.Controls.Add(this.LoadObjectButton);
			this.Controls.Add(this.CustomerGroupBox);
			this.Controls.Add(this.HobbiesListBox);
			this.Controls.Add(this.EmailsListBox);
			this.Controls.Add(this.LoadPictureButton);
			this.Controls.Add(this.CustomerPictureBox);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Load-Save objects to XML";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.BannerPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new MainForm());
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			// Load the customer object from the existing XML file (if any)...
			if (File.Exists(XML_FILE_NAME) == true)
			{
				LoadObjectButton_Click(this, null);
			}
			else
			{
				MessageBox.Show(this.CreateHowToMsg(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}

		private void LoadObjectButton_Click(object sender, System.EventArgs e)
		{
			// Load the customer object from the existing XML file (if any)...
			if (File.Exists(XML_FILE_NAME) == true)
			{
				// Load the customer object from the XML file using our custom class...
				Customer customer = ObjectXMLSerializer<Customer>.Load(XML_FILE_NAME);
			
				if (customer == null)
				{
					MessageBox.Show("Unable to load customer object from file '" + XML_FILE_NAME + "'!",  Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else  // Load customer properties into the form...
				{
					this.LoadCustomerIntoForm(customer);
					MessageBox.Show("Customer loaded from file '" + XML_FILE_NAME + "'!",  Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}				
			}
			else
			{
				MessageBox.Show(this.CreateFileDoesNotExistMsg(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}

		private void LoadCustomerIntoForm(Customer customer)
		{
			this.CustomerIDTextBox.Text   = System.Convert.ToString(customer.CustomerID);
			this.CustomerNameTextBox.Text = customer.CustomerName;
			this.CustomerAgeTextBox.Text  = System.Convert.ToString(customer.Age); 
			this.CustomerPictureBox.Image = (Image) customer.Picture;
					
			this.HobbiesListBox.Items.Clear();

			// Add Hobbies collection to the ListBox on the Form...
			foreach(string Item in customer.Hobbies)
			{
				this.HobbiesListBox.Items.Add(Item);
			}

			this.EmailsListBox.Items.Clear();

			// Add EmailAddresses collection to the ListBox on the Form...
			foreach(EmailAddress emailAddress in customer.EmailAddresses)
			{
				// Convert the enumerated object into its string representation.
				string Destination = Enum.GetName(typeof(EmailDestination), emailAddress.Destination);

				this.EmailsListBox.Items.Add(emailAddress.Address + " - " + Destination);
			}
		}

		private void SaveObjectButton_Click(object sender, System.EventArgs e)
		{
			if (this.CustomerIsValid())
			{
				// Create customer object based on Form values.
				Customer customer = this.CreateCustomer();

				//Save customer object to XML file using our ObjectXMLSerializer class...
				try
				{
                    ObjectXMLSerializer<Customer>.Save(customer, XML_FILE_NAME);
					MessageBox.Show("Customer saved to XML file '" + XML_FILE_NAME + "'!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch(Exception ex)
				{
					MessageBox.Show("Unable to save customer object!" + Environment.NewLine +  Environment.NewLine + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}		
		}

		private Customer CreateCustomer()
		{
			Customer customer = new Customer();

			customer.DateTimeValue = System.DateTime.Now;
			customer.CustomerID    = System.Convert.ToInt32(this.CustomerIDTextBox.Text);
			customer.CustomerName  = this.CustomerNameTextBox.Text;
			customer.CustomerPaid  = false;
			customer.Age           = System.Convert.ToInt32(this.CustomerAgeTextBox.Text);
			customer.Picture       = (System.Drawing.Bitmap) this.CustomerPictureBox.Image;

			// Add Hobbies as a collection of strings...
			foreach(string Item in this.HobbiesListBox.Items) 
			{
				customer.Hobbies.Add(Item);
			}

			// Add E-mail addresses as a collection of custom EmailAddress objects.
			customer.EmailAddresses = this.CreateEmailAddresses();

			return customer;
		}

		private ArrayList CreateEmailAddresses()
		{
			ArrayList emailAddresses = new ArrayList();

			foreach(string Item in this.EmailsListBox.Items)
			{
				int    DelimiterPos		  = Item.IndexOf("-"); 
				string Address			  = Item.Substring(0, DelimiterPos - 1);
				string Destination		  = Item.Substring(DelimiterPos + 1);

				EmailAddress emailAddress = new EmailAddress();
				emailAddress.Address      = Address;

				// Convert the string representation of the enumerated type into its equivalent enumerated object.
				emailAddress.Destination  = (EmailDestination) Enum.Parse(typeof(EmailDestination), Destination, true);
				
				emailAddresses.Add(emailAddress);
			}

			return emailAddresses;
		}

		private bool CustomerIsValid()
		{
			bool IsValid = this.CustomerNameTextBox.Text.Length > 0 &&  this.CustomerIDTextBox.Text.Length > 0 && this.CustomerAgeTextBox.Text.Length > 0;
			
			if (IsValid == false)
			{
				MessageBox.Show("You must enter a valid Customer ID, Name, and Age, before you can save the customer!",
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}

			return IsValid;
		}

		private void ViewXMLFileButton_Click(object sender, System.EventArgs e)
		{
			// View the customer XML file in the default web browser (if any)...
			if (File.Exists(XML_FILE_NAME) == true)		
			{
				System.Diagnostics.Process.Start(XML_FILE_NAME);
			}
			else
			{
				MessageBox.Show(this.CreateFileDoesNotExistMsg(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}

		private void LoadPictureButton_Click(object sender, System.EventArgs e)
		{
			string FileName = null;
			
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.RestoreDirectory = true;
					
			openFileDialog.Filter = "All picture files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
						
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				FileName = openFileDialog.FileName;
				this.CustomerPictureBox.Image = Image.FromFile(FileName);
			}
		}

		private string CreateFileDoesNotExistMsg()
		{
			return "The example XML file '" + XML_FILE_NAME +"' does not exist." + "\n\n" + 
			"To create the example XML file, enter customer details then click the 'Save' button.";
		}

		private string CreateHowToMsg()
		{
			return "To demonstrate saving of a 'Customer' object to an XML file ('"
				+ XML_FILE_NAME +"'), enter the customer details then click the 'Save' button.";
		}
	}
}
