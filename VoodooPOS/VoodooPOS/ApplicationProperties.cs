using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms.Design;
using System.IO;

namespace VoodooPOS
{
    public partial class ApplicationProperties : Form
    {
        public modes mode = modes.Admin;

        public enum modes
        {
            Inventory, Checkout, Reports, Edit, Locked, Admin
        }

        public ApplicationProperties()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //write property values back to Settings object properties

            Button btn = (Button)sender;
            ApplicationProperties form = (ApplicationProperties)btn.Parent;
            PropertyGrid pg = form.propertyGrid1;

            PropertyTable proptable = pg.SelectedObject as PropertyTable;
            //EMWorkbench.Properties.Settings settings = EMWorkbench.Properties.Settings.Default;

            //get the grid root
            GridItem gi = pg.SelectedGridItem;
            while (gi.Parent != null)
            {
                gi = gi.Parent;
            }

            //transfer all grid item values to Settings class properties
            foreach (GridItem item in gi.GridItems)
            {
                ParseGridItems(item); //recursive
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); //just close w/o doing anything
        }

        private void AppliationProperties_Load(object sender, EventArgs e)
        {
            //create & fill the table.  
            PropertyTable proptable = new PropertyTable();

            //Construct PropertyTable entries from Settings class user-scoped properties 
            VoodooPOS.Properties.Settings settings = VoodooPOS.Properties.Settings.Default;
            Type type = typeof(Properties.Settings);
            MemberInfo[] pi = type.GetProperties();
            foreach (MemberInfo m in pi)
            {
                Object[] myAttributes = m.GetCustomAttributes(true);
                if (myAttributes.Length > 0)
                {
                    for (int j = 0; j < myAttributes.Length; j++)
                    {
                        if (myAttributes[j].ToString() == "System.Configuration.UserScopedSettingAttribute")
                        {
                            PropertySpec ps = new PropertySpec("property name", "System.String");
                            switch (m.Name)
                            {
                                //Files category
                                case "BackgroundImagePath":
                                    ps = new PropertySpec(
                                        "Background Image Path",
                                        "System.String",
                                        "File Locations",
                                        "Background Image Path",
                                        settings.BackgroundImagePath.ToString(),
                                        typeof(System.Windows.Forms.Design.FileNameEditor),
                                        typeof(System.Convert));
                                    break;
                                case "LogoImagePath":
                                    ps = new PropertySpec(
                                        "Logo Image Path",
                                        "System.String",
                                        "File Locations",
                                        "Logo Image Path",
                                        settings.LogoImagePath.ToString(),
                                        typeof(System.Windows.Forms.Design.FileNameEditor),
                                        typeof(System.Convert));
                                    break;
                                case "ImageNotFoundImagePath":
                                    ps = new PropertySpec(
                                        "Image Not Found Image Path",
                                        "System.String",
                                        "File Locations",
                                        "Image Not Found Image Path",
                                        settings.ImageNotFoundImagePath.ToString(),
                                        typeof(System.Windows.Forms.Design.FileNameEditor),
                                        typeof(System.Convert));
                                    break;
                                //doubles
                                case "TaxRate":
                                    ps = new PropertySpec(
                                        "Tax Rate",
                                        typeof(Double),
                                        "Admin Properties",
                                        "Tax Rate",
                                        settings.TaxRate.ToString());
                                    break;
                                case "MinimumCreditCharge":
                                    ps = new PropertySpec(
                                        "Minimum Credit Charge",
                                        typeof(Double),
                                        "Admin Properties",
                                        "Minimum sale amount allowed to pay with a credit card",
                                        settings.MinimumCreditCharge);
                                    break;

                                case "EndOfBusinessDay24HourTime":
                                    ps = new PropertySpec(
                                        "End Of Business Day - 24 Hour Time",
                                        typeof(Int32),
                                        "Admin Properties",
                                        "The time of day considered EOB - 24 Hour Time",
                                        settings.EndOfBusinessDay24HourTime);
                                    break;
                                //Colors
                                //case "pec_color":
                                //    ps = new PropertySpec(
                                //        "PEC Color",
                                //        typeof(System.Drawing.Color),
                                //        "Colors",
                                //        "Color used for PEC model elements",
                                //        settings.pec_color);
                                //    break;

                                //Fonts
                                //case "default_plot_font":
                                //    ps = new PropertySpec(
                                //        "Default Plot Font",
                                //        typeof(Font),
                                //        "Fonts",
                                //        "Default font used for 2-D plots",
                                //        settings.default_plot_font);
                                //    break;
                            }
                            proptable.Properties.Add(ps);
                        }
                    }
                }
            }

            //this line binds the PropertyTable object to the preferences PropertyGrid control
            this.propertyGrid1.SelectedObject = proptable;
        }

        private void pg_Prefs_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Trace.WriteLine(e.ChangedItem.Label);
        }

        private void ParseGridItems(GridItem gi)
        {
            VoodooPOS.Properties.Settings settings = VoodooPOS.Properties.Settings.Default;

            if (gi.GridItemType == GridItemType.Category)
            {
                foreach (GridItem item in gi.GridItems)
                {
                    ParseGridItems(item); //terminates at 1st Property
                }
            }

            switch (gi.Label)
            {
                case "Background Image Path":
                    settings.BackgroundImagePath = gi.Value.ToString();
                    break;
                case "Logo Image Path":
                    settings.LogoImagePath = gi.Value.ToString();
                    break;
                case "Image Not Found Image Path":
                    settings.ImageNotFoundImagePath = gi.Value.ToString();
                    break;
                case "Tax Rate":
                    settings.TaxRate = Double.Parse(gi.Value.ToString());
                    break;
                case "Minimum Credit Charge":
                    settings.MinimumCreditCharge = Double.Parse(gi.Value.ToString());
                    break;
                case "End Of Business Day - 24 Hour Time":
                    settings.EndOfBusinessDay24HourTime = Int32.Parse(gi.Value.ToString());
                    break;
                //case "PEC Color":
                //    settings.pec_color = (Color)gi.Value;
                //    break;
                //case "Default Plot Font":
                //    settings.default_plot_font = (Font)gi.Value;
                //    break;

                default:
                    break;
            }
        }
    }
}
