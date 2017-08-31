using System;
using System.Diagnostics; 
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DirectX.Capture;
using Microsoft.VisualBasic;
using System.Data;
using System.IO;

namespace VoodooPOS
{

	public class BarcodeScanner : System.Windows.Forms.Form
	{
		private Capture capture = null;
        private Filters filters = new Filters();
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem mnuDevices;
		private System.Windows.Forms.MenuItem mnuVideoDevices;
		private System.Windows.Forms.MenuItem mnuAudioDevices;
		private System.Windows.Forms.MenuItem mnuVideoCompressors;
		private System.Windows.Forms.MenuItem mnuAudioCompressors;
		private System.Windows.Forms.MenuItem mnuVideoSources;
		private System.Windows.Forms.MenuItem mnuAudioSources;
		private System.Windows.Forms.Panel pnlVideo;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mnuAudioChannels;
		private System.Windows.Forms.MenuItem mnuAudioSamplingRate;
		private System.Windows.Forms.MenuItem mnuAudioSampleSizes;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mnuFrameSizes;
        private System.Windows.Forms.MenuItem mnuFrameRates;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem mnuPreview;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem mnuPropertyPages;
		private System.Windows.Forms.MenuItem mnuVideoCaps;
		private System.Windows.Forms.MenuItem mnuAudioCaps;
		private System.Windows.Forms.MenuItem mnuChannel;
		private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mnuInputType;
        private Button btnExit;
        private IContainer components;
        private ArrayList barCodes = new ArrayList();
        private Panel pnlRight;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        bool foundBarcode = false;
        string lastFoundBarcode = "";
        private Timer timer1;
        XmlData xmlData;
        private Button btnScanTest;
        CashRegister register;
        string[] registerSounds;
        ArrayList previousSoundsList = new ArrayList();
        Common common;
        DateTime lastFoundTime = DateTime.Now;

        public delegate void foundUPC(string upc);
        public event foundUPC FoundUPC;

        public CashRegister Register
        {
            set { register = value; }
        }

        public BarcodeScanner()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            xmlData = new XmlData(Application.StartupPath + "\\data");

            common = new Common(Application.StartupPath);

			// Start camera
            if (filters.VideoInputDevices.Count >= 1)//our scanner gun is connected
            {
#if DEBUG
                if(filters.VideoInputDevices.Count > 1)
                    capture = new Capture(filters.VideoInputDevices[1], filters.AudioInputDevices[0]);
                else
                    capture = new Capture(filters.VideoInputDevices[0], filters.AudioInputDevices[0]);
#else
                capture = new Capture(filters.VideoInputDevices[0], filters.AudioInputDevices[0]);
#endif

                // Update the main menu
                // Much of the interesting work of this sample occurs here
                try { updateMenu(); }
                catch (Exception ex)
                {
                }

                capture.PreviewWindow = pnlVideo;

                startScanning();

                //register = Register;

                registerSounds = Directory.GetFiles(Application.StartupPath + "\\sounds\\scanner");
            }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuDevices = new System.Windows.Forms.MenuItem();
            this.mnuVideoDevices = new System.Windows.Forms.MenuItem();
            this.mnuAudioDevices = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuVideoCompressors = new System.Windows.Forms.MenuItem();
            this.mnuAudioCompressors = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuVideoSources = new System.Windows.Forms.MenuItem();
            this.mnuFrameSizes = new System.Windows.Forms.MenuItem();
            this.mnuFrameRates = new System.Windows.Forms.MenuItem();
            this.mnuVideoCaps = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuAudioSources = new System.Windows.Forms.MenuItem();
            this.mnuAudioChannels = new System.Windows.Forms.MenuItem();
            this.mnuAudioSamplingRate = new System.Windows.Forms.MenuItem();
            this.mnuAudioSampleSizes = new System.Windows.Forms.MenuItem();
            this.mnuAudioCaps = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuChannel = new System.Windows.Forms.MenuItem();
            this.mnuInputType = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuPropertyPages = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuPreview = new System.Windows.Forms.MenuItem();
            this.pnlVideo = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnExit = new VoodooPOS.GelButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnScanTest = new VoodooPOS.GelButton();
            this.pnlVideo.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.mnuDevices,
            this.menuItem7});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuExit});
            this.menuItem1.Text = "File";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 0;
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuDevices
            // 
            this.mnuDevices.Index = 1;
            this.mnuDevices.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuVideoDevices,
            this.mnuAudioDevices,
            this.menuItem4,
            this.mnuVideoCompressors,
            this.mnuAudioCompressors});
            this.mnuDevices.Text = "Devices";
            // 
            // mnuVideoDevices
            // 
            this.mnuVideoDevices.Index = 0;
            this.mnuVideoDevices.Text = "Video Devices";
            // 
            // mnuAudioDevices
            // 
            this.mnuAudioDevices.Index = 1;
            this.mnuAudioDevices.Text = "Audio Devices";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // mnuVideoCompressors
            // 
            this.mnuVideoCompressors.Index = 3;
            this.mnuVideoCompressors.Text = "Video Compressors";
            // 
            // mnuAudioCompressors
            // 
            this.mnuAudioCompressors.Index = 4;
            this.mnuAudioCompressors.Text = "Audio Compressors";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuVideoSources,
            this.mnuFrameSizes,
            this.mnuFrameRates,
            this.mnuVideoCaps,
            this.menuItem5,
            this.mnuAudioSources,
            this.mnuAudioChannels,
            this.mnuAudioSamplingRate,
            this.mnuAudioSampleSizes,
            this.mnuAudioCaps,
            this.menuItem3,
            this.mnuChannel,
            this.mnuInputType,
            this.menuItem6,
            this.mnuPropertyPages,
            this.menuItem8,
            this.mnuPreview});
            this.menuItem7.Text = "Options";
            // 
            // mnuVideoSources
            // 
            this.mnuVideoSources.Index = 0;
            this.mnuVideoSources.Text = "Video Sources";
            // 
            // mnuFrameSizes
            // 
            this.mnuFrameSizes.Index = 1;
            this.mnuFrameSizes.Text = "Video Frame Size";
            // 
            // mnuFrameRates
            // 
            this.mnuFrameRates.Index = 2;
            this.mnuFrameRates.Text = "Video Frame Rate";
            this.mnuFrameRates.Click += new System.EventHandler(this.mnuFrameRates_Click);
            // 
            // mnuVideoCaps
            // 
            this.mnuVideoCaps.Index = 3;
            this.mnuVideoCaps.Text = "Video Capabilities...";
            this.mnuVideoCaps.Click += new System.EventHandler(this.mnuVideoCaps_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "-";
            // 
            // mnuAudioSources
            // 
            this.mnuAudioSources.Index = 5;
            this.mnuAudioSources.Text = "Audio Sources";
            // 
            // mnuAudioChannels
            // 
            this.mnuAudioChannels.Index = 6;
            this.mnuAudioChannels.Text = "Audio Channels";
            // 
            // mnuAudioSamplingRate
            // 
            this.mnuAudioSamplingRate.Index = 7;
            this.mnuAudioSamplingRate.Text = "Audio Sampling Rate";
            // 
            // mnuAudioSampleSizes
            // 
            this.mnuAudioSampleSizes.Index = 8;
            this.mnuAudioSampleSizes.Text = "Audio Sample Size";
            // 
            // mnuAudioCaps
            // 
            this.mnuAudioCaps.Index = 9;
            this.mnuAudioCaps.Text = "Audio Capabilities...";
            this.mnuAudioCaps.Click += new System.EventHandler(this.mnuAudioCaps_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 10;
            this.menuItem3.Text = "-";
            // 
            // mnuChannel
            // 
            this.mnuChannel.Index = 11;
            this.mnuChannel.Text = "TV Tuner Channel";
            // 
            // mnuInputType
            // 
            this.mnuInputType.Index = 12;
            this.mnuInputType.Text = "TV Tuner Input Type";
            this.mnuInputType.Click += new System.EventHandler(this.mnuInputType_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 13;
            this.menuItem6.Text = "-";
            // 
            // mnuPropertyPages
            // 
            this.mnuPropertyPages.Index = 14;
            this.mnuPropertyPages.Text = "PropertyPages";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 15;
            this.menuItem8.Text = "-";
            // 
            // mnuPreview
            // 
            this.mnuPreview.Index = 16;
            this.mnuPreview.Text = "Preview";
            this.mnuPreview.Click += new System.EventHandler(this.mnuPreview_Click);
            // 
            // pnlVideo
            // 
            this.pnlVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVideo.Controls.Add(this.panel3);
            this.pnlVideo.Controls.Add(this.panel2);
            this.pnlVideo.Controls.Add(this.panel1);
            this.pnlVideo.Controls.Add(this.pnlRight);
            this.pnlVideo.Location = new System.Drawing.Point(10, 9);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(305, 270);
            this.pnlVideo.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(35, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 154);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(35, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 5);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(35, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 5);
            this.panel1.TabIndex = 1;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRight.Location = new System.Drawing.Point(263, 57);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(5, 154);
            this.pnlRight.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 356);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(144, 28);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnScanTest
            // 
            this.btnScanTest.Location = new System.Drawing.Point(197, 356);
            this.btnScanTest.Name = "btnScanTest";
            this.btnScanTest.Size = new System.Drawing.Size(82, 23);
            this.btnScanTest.TabIndex = 7;
            this.btnScanTest.Text = " scan test";
            this.btnScanTest.UseVisualStyleBackColor = true;
            this.btnScanTest.Click += new System.EventHandler(this.btnScanTest_Click);
            // 
            // BarcodeScanner
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(327, 414);
            this.Controls.Add(this.btnScanTest);
            this.Controls.Add(this.pnlVideo);
            this.Controls.Add(this.btnExit);
            this.Menu = this.mainMenu;
            this.Name = "BarcodeScanner";
            this.Text = "Barcode Scanner";
            this.pnlVideo.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			if ( capture != null )
				capture.Stop();
			Application.Exit(); 
		}

		private void updateMenu()
		{
			MenuItem m;
			Filter f;
			Source s;
			Source current;
			PropertyPage p;
			Control oldPreviewWindow = null;
 
			// Disable preview to avoid additional flashes (optional)
			if ( capture != null )
			{
				oldPreviewWindow = capture.PreviewWindow;
				capture.PreviewWindow = null;
			}

			// Load video devices
			Filter videoDevice = null;
			if ( capture != null )
				videoDevice = capture.VideoDevice;
			mnuVideoDevices.MenuItems.Clear();
			m = new MenuItem( "(None)", new EventHandler( mnuVideoDevices_Click ) );
			m.Checked = ( videoDevice == null );
			mnuVideoDevices.MenuItems.Add( m );
			for ( int c = 0; c < filters.VideoInputDevices.Count; c++ )
			{
				f = filters.VideoInputDevices[c];
				m = new MenuItem( f.Name, new EventHandler( mnuVideoDevices_Click ) );
				m.Checked = ( videoDevice == f );
				mnuVideoDevices.MenuItems.Add( m );
			}
			mnuVideoDevices.Enabled = ( filters.VideoInputDevices.Count > 0 );

			// Load audio devices
			Filter audioDevice = null;
			if ( capture != null )
				audioDevice = capture.AudioDevice;
			mnuAudioDevices.MenuItems.Clear();
			m = new MenuItem( "(None)", new EventHandler( mnuAudioDevices_Click ) );
			m.Checked = ( audioDevice == null );
			mnuAudioDevices.MenuItems.Add( m );
			for ( int c = 0; c < filters.AudioInputDevices.Count; c++ )
			{
				f = filters.AudioInputDevices[c];
				m = new MenuItem( f.Name, new EventHandler( mnuAudioDevices_Click ) );
				m.Checked = ( audioDevice == f );
				mnuAudioDevices.MenuItems.Add( m );
			}
			mnuAudioDevices.Enabled = ( filters.AudioInputDevices.Count > 0 );


			// Load video compressors
			try
			{
				mnuVideoCompressors.MenuItems.Clear();
				m = new MenuItem( "(None)", new EventHandler( mnuVideoCompressors_Click ) );
				m.Checked = ( capture.VideoCompressor == null );
				mnuVideoCompressors.MenuItems.Add( m );
				for ( int c = 0; c < filters.VideoCompressors.Count; c++ )
				{
					f = filters.VideoCompressors[c];
					m = new MenuItem( f.Name, new EventHandler( mnuVideoCompressors_Click ) );
					m.Checked = ( capture.VideoCompressor == f );
					mnuVideoCompressors.MenuItems.Add( m );
				}
				mnuVideoCompressors.Enabled = ( ( capture.VideoDevice != null ) && ( filters.VideoCompressors.Count > 0 ) );
			}
			catch { mnuVideoCompressors.Enabled = false; }
			
			// Load audio compressors
			try
			{
				mnuAudioCompressors.MenuItems.Clear();
				m = new MenuItem( "(None)", new EventHandler( mnuAudioCompressors_Click ) );
				m.Checked = ( capture.AudioCompressor == null );
				mnuAudioCompressors.MenuItems.Add( m );
				for ( int c = 0; c < filters.AudioCompressors.Count; c++ )
				{
					f = filters.AudioCompressors[c];
					m = new MenuItem( f.Name, new EventHandler( mnuAudioCompressors_Click ) );
					m.Checked = ( capture.AudioCompressor == f );
					mnuAudioCompressors.MenuItems.Add( m );
				}
				mnuAudioCompressors.Enabled = ( ( capture.AudioDevice != null ) && ( filters.AudioCompressors.Count > 0 ) );
			}
			catch { mnuAudioCompressors.Enabled = false; }

			// Load video sources
			try 
			{
				mnuVideoSources.MenuItems.Clear();
				current = capture.VideoSource;
				for ( int c = 0; c < capture.VideoSources.Count; c++ )
				{
					s = capture.VideoSources[c];
					m = new MenuItem( s.Name, new EventHandler( mnuVideoSources_Click ) );
					m.Checked = ( current == s );
					mnuVideoSources.MenuItems.Add( m );
				}
				mnuVideoSources.Enabled = ( capture.VideoSources.Count > 0 );
			}
			catch { mnuVideoSources.Enabled = false; }

			// Load audio sources
			try
			{
				mnuAudioSources.MenuItems.Clear();
				current = capture.AudioSource;
				for ( int c = 0; c < capture.AudioSources.Count; c++ )
				{
					s = capture.AudioSources[c];
					m = new MenuItem( s.Name, new EventHandler( mnuAudioSources_Click ) );
					m.Checked = ( current == s );
					mnuAudioSources.MenuItems.Add( m );
				}
				mnuAudioSources.Enabled = ( capture.AudioSources.Count > 0 );
			}
			catch { mnuAudioSources.Enabled = false; }

			// Load frame rates
			try
			{
				mnuFrameRates.MenuItems.Clear();
				int frameRate = (int) (capture.FrameRate * 1000);
				m = new MenuItem( "15 fps", new EventHandler( mnuFrameRates_Click ) );
				m.Checked = ( frameRate == 15000 );
				mnuFrameRates.MenuItems.Add( m );
				m = new MenuItem( "24 fps (Film)", new EventHandler( mnuFrameRates_Click ) );
				m.Checked = ( frameRate == 24000 );
				mnuFrameRates.MenuItems.Add( m );
				m = new MenuItem( "25 fps (PAL)", new EventHandler( mnuFrameRates_Click ) );
				m.Checked = ( frameRate == 25000 );
				mnuFrameRates.MenuItems.Add( m );
				m = new MenuItem( "29.997 fps (NTSC)", new EventHandler( mnuFrameRates_Click ) );
				m.Checked = ( frameRate == 29997 );
				mnuFrameRates.MenuItems.Add( m );
				m = new MenuItem( "30 fps (~NTSC)", new EventHandler( mnuFrameRates_Click ) );
				m.Checked = ( frameRate == 30000 );
				mnuFrameRates.MenuItems.Add( m );
				m = new MenuItem( "59.994 fps (2xNTSC)", new EventHandler( mnuFrameRates_Click ) );
				m.Checked = ( frameRate == 59994 );
				mnuFrameRates.MenuItems.Add( m );
				mnuFrameRates.Enabled = true;
			}
			catch { mnuFrameRates.Enabled = false; }

			// Load frame sizes
			try
			{
				mnuFrameSizes.MenuItems.Clear();
				Size frameSize = capture.FrameSize;
				m = new MenuItem( "160 x 120", new EventHandler( mnuFrameSizes_Click ) );
				m.Checked = ( frameSize == new Size( 160, 120 ) );
				mnuFrameSizes.MenuItems.Add( m );
				m = new MenuItem( "320 x 240", new EventHandler( mnuFrameSizes_Click ) );
				m.Checked = ( frameSize == new Size( 320, 240 ) );
				mnuFrameSizes.MenuItems.Add( m );
				m = new MenuItem( "640 x 480", new EventHandler( mnuFrameSizes_Click ) );
				m.Checked = ( frameSize == new Size( 640, 480 ) );
				mnuFrameSizes.MenuItems.Add( m );
				m = new MenuItem( "1024 x 768", new EventHandler( mnuFrameSizes_Click ) );
				m.Checked = ( frameSize == new Size( 1024, 768 ) );
				mnuFrameSizes.MenuItems.Add( m );
				mnuFrameSizes.Enabled = true;
			}
			catch { mnuFrameSizes.Enabled = false; }

			// Load audio channels
			try
			{
				mnuAudioChannels.MenuItems.Clear();
				short audioChannels = capture.AudioChannels;
				m = new MenuItem( "Mono", new EventHandler( mnuAudioChannels_Click ) );
				m.Checked = ( audioChannels == 1 );
				mnuAudioChannels.MenuItems.Add( m );
				m = new MenuItem( "Stereo", new EventHandler( mnuAudioChannels_Click ) );
				m.Checked = ( audioChannels == 2 );
				mnuAudioChannels.MenuItems.Add( m );
				mnuAudioChannels.Enabled = true;
			}
			catch { mnuAudioChannels.Enabled = false; }

			// Load audio sampling rate
			try
			{
				mnuAudioSamplingRate.MenuItems.Clear();
				int samplingRate = capture.AudioSamplingRate;
				m = new MenuItem( "8 kHz", new EventHandler( mnuAudioSamplingRate_Click ) );
				m.Checked = ( samplingRate == 8000 );
				mnuAudioSamplingRate.MenuItems.Add( m );
				m = new MenuItem( "11.025 kHz", new EventHandler( mnuAudioSamplingRate_Click ) );
				m.Checked = ( capture.AudioSamplingRate == 11025 );
				mnuAudioSamplingRate.MenuItems.Add( m );
				m = new MenuItem( "22.05 kHz", new EventHandler( mnuAudioSamplingRate_Click ) );
				m.Checked = ( capture.AudioSamplingRate == 22050 );
				mnuAudioSamplingRate.MenuItems.Add( m );
				m = new MenuItem( "44.1 kHz", new EventHandler( mnuAudioSamplingRate_Click ) );
				m.Checked = ( capture.AudioSamplingRate == 44100 );
				mnuAudioSamplingRate.MenuItems.Add( m );
				mnuAudioSamplingRate.Enabled = true;
			}
			catch { mnuAudioSamplingRate.Enabled = false; } 

			// Load audio sample sizes
			try
			{
				mnuAudioSampleSizes.MenuItems.Clear();
				short sampleSize = capture.AudioSampleSize;
				m = new MenuItem( "8 bit", new EventHandler( mnuAudioSampleSizes_Click ) );
				m.Checked = ( sampleSize == 8 );
				mnuAudioSampleSizes.MenuItems.Add( m );
				m = new MenuItem( "16 bit", new EventHandler( mnuAudioSampleSizes_Click ) );
				m.Checked = ( sampleSize == 16 );
				mnuAudioSampleSizes.MenuItems.Add( m );
				mnuAudioSampleSizes.Enabled = true;
			}
			catch { mnuAudioSampleSizes.Enabled = false; }

			// Load property pages
			try
			{
				mnuPropertyPages.MenuItems.Clear();
				for ( int c = 0; c < capture.PropertyPages.Count; c++ )
				{
					p = capture.PropertyPages[c];
					m = new MenuItem( p.Name + "...", new EventHandler( mnuPropertyPages_Click ) );
					mnuPropertyPages.MenuItems.Add( m );
				}
				mnuPropertyPages.Enabled = ( capture.PropertyPages.Count > 0 );
			}
			catch { mnuPropertyPages.Enabled = false; }

			// Load TV Tuner channels
			try
			{
				mnuChannel.MenuItems.Clear();
				int channel = capture.Tuner.Channel;
				for ( int c = 1; c <= 25; c++ )
				{
					m = new MenuItem( c.ToString(), new EventHandler( mnuChannel_Click ) );
					m.Checked = ( channel == c );
					mnuChannel.MenuItems.Add( m );
				}
				mnuChannel.Enabled = true;
			}
			catch { mnuChannel.Enabled = false; }

			// Load TV Tuner input types
			try
			{
				mnuInputType.MenuItems.Clear();
				m = new MenuItem( TunerInputType.Cable.ToString(), new EventHandler( mnuInputType_Click ) );
				m.Checked = ( capture.Tuner.InputType == TunerInputType.Cable );
				mnuInputType.MenuItems.Add( m );
				m = new MenuItem( TunerInputType.Antenna.ToString(), new EventHandler( mnuInputType_Click ) );
				m.Checked = ( capture.Tuner.InputType == TunerInputType.Antenna );
				mnuInputType.MenuItems.Add( m );
				mnuInputType.Enabled = true;
			}
			catch { mnuInputType.Enabled = false; }

			// Enable/disable caps
			mnuVideoCaps.Enabled = ( ( capture != null ) && ( capture.VideoCaps != null ) );
			mnuAudioCaps.Enabled = ( ( capture != null ) && ( capture.AudioCaps != null ) );

			// Check Preview menu option
			mnuPreview.Checked = ( oldPreviewWindow != null );
			mnuPreview.Enabled = ( capture != null );

			// Reenable preview if it was enabled before
			if ( capture != null )
				capture.PreviewWindow = oldPreviewWindow;
        }

        #region Menu click handlers
        private void mnuVideoDevices_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Get current devices and dispose of capture object
				// because the video and audio device can only be changed
				// by creating a new Capture object.
				Filter videoDevice = null;
				Filter audioDevice = null;
				if ( capture != null )
				{
					videoDevice = capture.VideoDevice;
					audioDevice = capture.AudioDevice;
					capture.Dispose();
					capture = null;
				}

				// Get new video device
				MenuItem m = sender as MenuItem;
				videoDevice = ( m.Index>0 ? filters.VideoInputDevices[m.Index-1] : null );

				// Create capture object
				if ( ( videoDevice != null ) || ( audioDevice != null ) )
				{
					capture = new Capture( videoDevice, audioDevice );
					//capture.CaptureComplete += new EventHandler( OnCaptureComplete );
				}

                capture.PreviewWindow = pnlVideo;

                startScanning();

                MenuItem selectedItem = (MenuItem)sender;

                foreach (MenuItem menuItem in mnuVideoDevices.MenuItems)
                {
                    if (selectedItem == menuItem)
                        selectedItem.Checked = true;
                    else
                        menuItem.Checked = false;
                }

				// Update the menu
                //updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Video device not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuAudioDevices_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Get current devices and dispose of capture object
				// because the video and audio device can only be changed
				// by creating a new Capture object.
				Filter videoDevice = null;
				Filter audioDevice = null;
				if ( capture != null )
				{
					videoDevice = capture.VideoDevice;
					audioDevice = capture.AudioDevice;
					capture.Dispose();
					capture = null;
				}

				// Get new audio device
				MenuItem m = sender as MenuItem;
				audioDevice = ( m.Index>0 ? filters.AudioInputDevices[m.Index-1] : null );

				// Create capture object
				if ( ( videoDevice != null ) || ( audioDevice != null ) )
				{
					capture = new Capture( videoDevice, audioDevice );
					//capture.CaptureComplete += new EventHandler( OnCaptureComplete );
				}

				// Update the menu
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Audio device not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuVideoCompressors_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Change the video compressor
				// We subtract 1 from m.Index beacuse the first item is (None)
				MenuItem m = sender as MenuItem;
				capture.VideoCompressor = ( m.Index>0 ? filters.VideoCompressors[m.Index-1] : null );
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Video compressor not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}

		}

		private void mnuAudioCompressors_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Change the audio compressor
				// We subtract 1 from m.Index beacuse the first item is (None)
				MenuItem m = sender as MenuItem;
				capture.AudioCompressor = ( m.Index>0 ? filters.AudioCompressors[m.Index-1] : null );
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Audio compressor not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuVideoSources_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Choose the video source
				// If the device only has one source, this menu item will be disabled
				MenuItem m = sender as MenuItem;
				capture.VideoSource = capture.VideoSources[m.Index];
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable to set video source. Please submit bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuAudioSources_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Choose the audio source
				// If the device only has one source, this menu item will be disabled
				MenuItem m = sender as MenuItem;
				capture.AudioSource = capture.AudioSources[m.Index];
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable to set audio source. Please submit bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}


		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			if ( capture != null )
				capture.Stop();
			Application.Exit(); 
		}

		private void mnuFrameSizes_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Disable preview to avoid additional flashes (optional)
				bool preview = (capture.PreviewWindow != null);
				capture.PreviewWindow = null;

				// Update the frame size
				MenuItem m = sender as MenuItem;
				string[] s = m.Text.Split( 'x' );
				Size size = new Size( int.Parse( s[0] ), int.Parse( s[1] ) );
				capture.FrameSize = size;

				// Update the menu
				updateMenu();

				// Restore previous preview setting
				capture.PreviewWindow = ( preview ? pnlVideo : null );
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Frame size not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuFrameRates_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				string[] s = m.Text.Split( ' ' );
				capture.FrameRate = double.Parse( s[0] );
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Frame rate not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuAudioChannels_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				capture.AudioChannels = (short) Math.Pow( 2, m.Index );
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Number of audio channels not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuAudioSamplingRate_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				string[] s = m.Text.Split( ' ' );
				int samplingRate = (int) (double.Parse( s[0] ) * 1000);
				capture.AudioSamplingRate = samplingRate;
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Audio sampling rate not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuAudioSampleSizes_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				string[] s = m.Text.Split( ' ' );
				short sampleSize = short.Parse( s[0] );
				capture.AudioSampleSize = sampleSize;
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Audio sample size not supported.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuPreview_Click(object sender, System.EventArgs e)
		{
			try
			{
				if ( capture.PreviewWindow == null )
				{
					capture.PreviewWindow = pnlVideo;
					mnuPreview.Checked = true;
				}
				else
				{
					capture.PreviewWindow = null;
					mnuPreview.Checked = false;
				}
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable to enable/disable preview. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuPropertyPages_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				capture.PropertyPages[m.Index].Show( this );
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable display property page. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuChannel_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				capture.Tuner.Channel = m.Index+1;
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable change channel. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuInputType_Click(object sender, System.EventArgs e)
		{
			try
			{
				MenuItem m = sender as MenuItem;
				capture.Tuner.InputType = (TunerInputType) m.Index;
				updateMenu();
			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable change tuner input type. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuVideoCaps_Click(object sender, System.EventArgs e)
		{
			try
			{
				string s;
				s = String.Format(
					"Video Device Capabilities\n" +
					"--------------------------------\n\n" +
					"Input Size:\t\t{0} x {1}\n" +
					"\n" +
					"Min Frame Size:\t\t{2} x {3}\n" +
					"Max Frame Size:\t\t{4} x {5}\n" +
					"Frame Size Granularity X:\t{6}\n" +
					"Frame Size Granularity Y:\t{7}\n" +
					"\n" +
					"Min Frame Rate:\t\t{8:0.000} fps\n" +
					"Max Frame Rate:\t\t{9:0.000} fps\n", 
					capture.VideoCaps.InputSize.Width, capture.VideoCaps.InputSize.Height,
					capture.VideoCaps.MinFrameSize.Width, capture.VideoCaps.MinFrameSize.Height, 
					capture.VideoCaps.MaxFrameSize.Width, capture.VideoCaps.MaxFrameSize.Height, 
					capture.VideoCaps.FrameSizeGranularityX, 
					capture.VideoCaps.FrameSizeGranularityY,
					capture.VideoCaps.MinFrameRate, 
					capture.VideoCaps.MaxFrameRate );
				MessageBox.Show( s );

			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable display video capabilities. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
		}

		private void mnuAudioCaps_Click(object sender, System.EventArgs e)
		{
			try
			{
				string s;
				s = String.Format(
					"Audio Device Capabilities\n" +
					"--------------------------------\n\n" +
					"Min Channels:\t\t{0}\n" +
					"Max Channels:\t\t{1}\n" +
					"Channels Granularity:\t{2}\n" +
					"\n" +
					"Min Sample Size:\t\t{3}\n" +
					"Max Sample Size:\t\t{4}\n" +
					"Sample Size Granularity:\t{5}\n" +
					"\n" +
					"Min Sampling Rate:\t\t{6}\n" +
					"Max Sampling Rate:\t\t{7}\n" +
					"Sampling Rate Granularity:\t{8}\n",
					capture.AudioCaps.MinimumChannels,
					capture.AudioCaps.MaximumChannels,
					capture.AudioCaps.ChannelsGranularity,
					capture.AudioCaps.MinimumSampleSize, 
					capture.AudioCaps.MaximumSampleSize,
					capture.AudioCaps.SampleSizeGranularity,
					capture.AudioCaps.MinimumSamplingRate,
					capture.AudioCaps.MaximumSamplingRate,
					capture.AudioCaps.SamplingRateGranularity );
				MessageBox.Show( s );

			}
			catch (Exception ex)
			{
                Common.WriteToFile(ex);
				MessageBox.Show( "Unable display audio capabilities. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString() );
			}
        }
        #endregion

		private void startScanning()
        {
            foundBarcode = false;

            capture.FrameEvent2 += new Capture.HeFrame(CaptureDone);
            capture.GrapImg();
        }

        private void CaptureDone(System.Drawing.Bitmap imgBarCode)
		{
            try
            {
                int fileCounter = 1;

                while (System.IO.File.Exists(Application.StartupPath + "\\temp\\framshots\\frameshot_" + fileCounter.ToString() + ".bmp"))
                    fileCounter++;

                if (!foundBarcode)
                {
                    if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp\\frameshots"))
                        System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp\\frameshots");

                    //imgBarCode.Save(Application.StartupPath + "\\framshots\\frameshot_" + fileCounter.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                    imgBarCode.Save(Application.StartupPath + "\\temp\\frameshot_" + fileCounter.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                    Bitmap imgCropped = imgBarCode.Clone(new Rectangle(100, 70, 450, 200), imgBarCode.PixelFormat);
                    imgCropped.Save(Application.StartupPath + "\\temp\\frameshot_" + fileCounter.ToString() + "_cropped.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                    //BarcodeImaging.FullScanPage(ref barCodes, imgCropped, 10);
                    BarcodeImaging.ScanPage(ref barCodes, imgCropped, 50, BarcodeImaging.ScanDirection.Vertical, BarcodeImaging.BarcodeType.EAN);

                    if (barCodes.Count == 0)
                        BarcodeImaging.ScanPage(ref barCodes, imgCropped, 50, BarcodeImaging.ScanDirection.Horizontal, BarcodeImaging.BarcodeType.EAN);

                    if (barCodes.Count > 0)
                    {
                        if (DateTime.Now.Subtract(lastFoundTime) > new TimeSpan(0, 0, 5))
                            lastFoundBarcode = "";
                    }                    

                    if (barCodes.Count > 0 && lastFoundBarcode != barCodes[0].ToString() && barCodes[0].ToString().Length > 5)//found barcode
                    {
                        //playSound();

                        lastFoundBarcode = barCodes[0].ToString();

                        //timer1.Enabled = true;
                        //timer1.Start();

                        FoundUPC(barCodes[0].ToString().Trim());

                        //Invoke((MethodInvoker)delegate
                        //{
                        //    try
                        //    {
                        //        register.FoundUpc(barCodes[0].ToString().Trim());
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Common.WriteToFile(ex);
                        //    }
                        //});
                    }
                    
                    barCodes.Clear();
                }
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
		}

        //private void foundUPC(string upc)
        //{
        //    //check inventory for item
        //    DataTable retTable = xmlData.Select("upc = '" + upc + "'", "Name asc", XmlData.Tables.InventoryItems);

        //    if (retTable != null && retTable.Rows.Count > 0)//display item information or add to cash register
        //    {
        //        double price = 0;
        //        double.TryParse(retTable.Rows[0]["Price"].ToString(), out price);

        //        int quantity = 1;
                
        //        InventoryItem item = new InventoryItem();
        //        item.Name = retTable.Rows[0]["name"].ToString();
        //        item.Description = retTable.Rows[0]["Description"].ToString();
        //        item.Manufacturer = retTable.Rows[0]["Manufacturer"].ToString();
        //        item.Model = retTable.Rows[0]["Model"].ToString();
        //        item.Price = price;
        //        item.Color = retTable.Rows[0]["Color"].ToString();
        //        item.Size = retTable.Rows[0]["Size"].ToString();
        //        item.PicturePath = retTable.Rows[0]["PicturePath"].ToString();
        //        item.UPC = retTable.Rows[0]["UPC"].ToString();
        //        item.Quantity = quantity;

        //        register.AddItem(item);
        //    }
        //    else //display new inventory item form
        //    {
        //        NewInventoryItem newItem = new NewInventoryItem();
        //        newItem.UPC = upc;
        //        newItem.ShowDialog();
        //    }
        //}

        private void playSound()
        {
            Random random = new Random();

            int randomNumber = 0;

            randomNumber = random.Next(0, registerSounds.Length);

            while(previousSoundsList.Contains(randomNumber))
                randomNumber = random.Next(0, registerSounds.Length);

            common.PlaySound(registerSounds[randomNumber]);

            if (previousSoundsList.Count > registerSounds.Length/2)
                previousSoundsList.RemoveAt(0);

            previousSoundsList.Add(randomNumber);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lastFoundBarcode = "";
            //timer1.Stop();
            timer1.Enabled = false;
        }

        private void btnScanTest_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            int randomNumber = 1;

            randomNumber = random.Next(1, 10);

            //check inventory for item
            DataTable retTable = xmlData.Select("id = '"+ randomNumber.ToString() +"'", "Name asc", XmlData.Tables.InventoryItems);

            if (retTable != null && retTable.Rows.Count > 0)//display item information or add to cash register
            {
                double price = 0;
                double.TryParse(retTable.Rows[0]["Price"].ToString(), out price);

                int quantity = 1;

                Voodoo.Objects.InventoryItem item = new Voodoo.Objects.InventoryItem();
                item.Name = retTable.Rows[0]["name"].ToString();
                item.Description = retTable.Rows[0]["Description"].ToString();
                item.Manufacturer = retTable.Rows[0]["Manufacturer"].ToString();
                item.Model = retTable.Rows[0]["Model"].ToString();
                item.Price = price;
                item.Color = retTable.Rows[0]["Color"].ToString();
                item.Size = retTable.Rows[0]["Size"].ToString();
                item.PicturePath = retTable.Rows[0]["PicturePath"].ToString();
                item.UPC = retTable.Rows[0]["UPC"].ToString();
                item.Quantity = quantity;

                //playSound();

                //register.AddItem(item);

                FoundUPC(item.UPC.ToString());
            }
            else
                MessageBox.Show("Nothing in Inventory");
        }
	}
}
