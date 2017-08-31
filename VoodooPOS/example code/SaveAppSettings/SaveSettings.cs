using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SaveAppSettings
{
	/// <summary>
	/// Summary description for frmSaveSettings.
	/// </summary>
	public class frmSaveSettings : System.Windows.Forms.Form
	{
    private System.Windows.Forms.ComboBox cbColors;
		private System.ComponentModel.Container components = null;
    private DataSet ds;
    private string m_xmlPath;
    private int m_Left;
    private int m_Top;
    private int m_Width;
    private int m_Height;
    private FormWindowState m_WindowState;
    private string m_bgColor;

    // Constructor
		public frmSaveSettings()
		{
      m_xmlPath = Application.StartupPath + "\\Settings.xml";
			InitializeComponent();
      m_Left = this.Left;
      m_Top = this.Top;
      m_Width = this.Width;
      m_Height = this.Height;
      m_bgColor = this.BackColor.Name;
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
      this.cbColors = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // cbColors
      // 
      this.cbColors.Items.AddRange(new object[] {
                                                  "Black",
                                                  "Blue",
                                                  "Green",
                                                  "LightGray",
                                                  "Red",
                                                  "White",
                                                  "Yellow"});
      this.cbColors.Location = new System.Drawing.Point(8, 0);
      this.cbColors.Name = "cbColors";
      this.cbColors.Size = new System.Drawing.Size(121, 21);
      this.cbColors.TabIndex = 0;
      this.cbColors.Text = "Select BGColor";
      this.cbColors.SelectedIndexChanged += new System.EventHandler(this.cbColors_SelectedIndexChanged);
      // 
      // frmSaveSettings
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(292, 273);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.cbColors});
      this.Name = "frmSaveSettings";
      this.Text = "Save Settings";
      this.Resize += new System.EventHandler(this.frmSaveSettings_Resize);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSaveSettings_Closing);
      this.Load += new System.EventHandler(this.frmSaveSettings_Load);
      this.Move += new System.EventHandler(this.frmSaveSettings_Move);
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmSaveSettings());
		}

    private void frmSaveSettings_Load(object sender, System.EventArgs e)
    {
      ds = new DataSet();
      ds.ReadXml(m_xmlPath);
      DataRow dr = ds.Tables[0].Rows[0];
      Left = (int)dr["Left"];
      Top = (int)dr["Top"];
      m_Width = (int)dr["Width"];
      m_Height = (int)dr["Height"];
      m_WindowState = (FormWindowState)dr["WindowState"];
      m_bgColor = dr["bgColor"].ToString();

      this.Location = new Point(m_Left, m_Top);
      this.Size = new Size(m_Width, m_Height);
      this.WindowState = m_WindowState;
      this.BackColor = Color.FromName(m_bgColor);
    }

    private void cbColors_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      this.BackColor = Color.FromName(cbColors.SelectedItem.ToString());
      m_bgColor = this.BackColor.Name;
    }

    private void frmSaveSettings_Resize(object sender, System.EventArgs e)
    {
      // set size
      m_Width = this.Width;
      m_Height = this.Height;
    }

    private void frmSaveSettings_Move(object sender, System.EventArgs e)
    {
      // set location and windowstate
      m_Left = this.Left;
      m_Top = this.Top;
      m_WindowState = this.WindowState;
    }

    private void frmSaveSettings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // save settings
      ds.Tables[0].Rows[0]["Left"] = m_Left;
      ds.Tables[0].Rows[0]["Top"] = m_Top;
      ds.Tables[0].Rows[0]["Width"] = m_Width;
      ds.Tables[0].Rows[0]["Height"] = m_Height;
      ds.Tables[0].Rows[0]["WindowState"] = (int)m_WindowState;
      ds.Tables[0].Rows[0]["bgColor"] = m_bgColor;
			
      ds.WriteXml(m_xmlPath, XmlWriteMode.WriteSchema);
    }

  }
}
