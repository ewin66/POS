using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Collections;
using SCTech.Common;

namespace VoodooPOS
{
    class Printer
    {
        int labelRow = -1;
        int labelColumn = -1;
        int labelCounter = 0;
        string appPath = "";
        ArrayList arCart = new ArrayList();
        Voodoo.Objects.InventoryItem itemToPrint = null;
        string nameOnCertificate = "________________________________________";
        int giftCertificateID = -1;
        double giftCertificateAmount = 0;
        Bitmap barcodeToPrint = null;
        int numLabelColumns = 4;
        int receiptWidth = 30;
        XmlData xmlData;

        public Printer(string applicationPath)
        {
            appPath = applicationPath;

            xmlData = new XmlData(appPath);
        }

        public void PrintLabel(Voodoo.Objects.InventoryItem ItemToPrint)
        {
            itemToPrint = ItemToPrint;

            LabelTemplate labelTemplate = new LabelTemplate();

            if (labelTemplate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (itemToPrint.Quantity > 0 && labelTemplate.SelectedColumnIndex >= 0 && labelTemplate.SelectedRowIndex >= 0)
                {
                    labelRow = labelTemplate.SelectedRowIndex;
                    labelColumn = labelTemplate.SelectedColumnIndex;
                    numLabelColumns = labelTemplate.NumberOfColumns;

                    PrintDocument labelDoc;
                    // Create the document and name it
                    labelDoc = new PrintDocument();
                    labelDoc.DocumentName = "Pricing Label";
                    labelDoc.DefaultPageSettings.Landscape = false;

                    labelDoc.PrintPage += new PrintPageEventHandler(this.printTheLabel);

                    PaperSize paperSize = new PaperSize("Label", 850, 1100); //full 8.5 x 11 page
                    //PaperSize paperSize = new PaperSize("Label", 83, 366);
                    labelDoc.DefaultPageSettings.PaperSize = paperSize;

                    Margins margins = new Margins(10, 5, 35, 15);
                    labelDoc.DefaultPageSettings.Margins = margins;
                    labelDoc.PrinterSettings.PrinterName = "HP Deskjet D4200 series";

#if DEBUG
                    // Preview document
                    System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
                    printPreviewDialog1.Document = labelDoc;
                    printPreviewDialog1.ShowDialog();
#else
            //print with no preview
            labelDoc.Print();
#endif

                    //Dispose of document when done printing
                    labelDoc.Dispose();
                }
            }
        }

        private void printTheLabel(object sender, PrintPageEventArgs e)
        {
            if (itemToPrint != null && itemToPrint.Quantity > 0)
            {
                int rowHeight = 100;
                int columnWidth = 280;
                int totalSpaces = 200;

                switch (numLabelColumns)
                {
                    case (3):
                        rowHeight = 100;
                        columnWidth = 280;
                        totalSpaces = 200;

                        for (int x = 1; x <= itemToPrint.Quantity; x++)
                        {
                            labelCounter++;

                            if (itemToPrint.Name.Length > 9)
                                totalSpaces = 150;
                            else
                                totalSpaces = 200;

                            Font myFont = new Font("Times New Roman", 12, FontStyle.Regular);

                            //print name
                            Rectangle productName;

                            if (totalSpaces > itemToPrint.Name.Length)
                                totalSpaces = totalSpaces - itemToPrint.Name.Length;

                            if (itemToPrint.Name.Length > 20)
                                productName = new Rectangle(labelColumn * columnWidth + e.MarginBounds.X + (totalSpaces / 2), labelRow * rowHeight + e.MarginBounds.Y + 20, 165, 40);
                            else
                                productName = new Rectangle(labelColumn * columnWidth + e.MarginBounds.X + (totalSpaces / 2), labelRow * rowHeight + e.MarginBounds.Y + 20, 165, 20);

                            e.Graphics.DrawString(itemToPrint.Name, myFont, Brushes.Black, productName);

                            totalSpaces = 200;

                            totalSpaces = totalSpaces - itemToPrint.Price.ToString("C").Length;

                            //print price
                            Rectangle productPrice = new Rectangle(labelColumn * columnWidth + e.MarginBounds.X + (totalSpaces / 2), productName.Y + productName.Height + 10, 175, 20);
                            e.Graphics.DrawString(itemToPrint.Price.ToString("C"), myFont, Brushes.Black, productPrice);

                            labelColumn++;

                            if (labelColumn > 2)
                            {
                                labelColumn = 0;
                                labelRow++;
                            }
                        }
                        break;
                    case (4):
                        rowHeight = 50;
                        columnWidth = 200;
                        totalSpaces = 175;

                        for (int x = 1; x <= itemToPrint.Quantity; x++)
                        {
                            labelCounter++;

                            if (itemToPrint.Name.Length > 9)
                                totalSpaces = 100;
                            else
                                totalSpaces = 175;

                            Font myFont = new Font("Times New Roman", 11, FontStyle.Regular);

                            //print name
                            Rectangle productName;

                            if (totalSpaces > itemToPrint.Name.Length)
                                totalSpaces = totalSpaces - itemToPrint.Name.Length;

                            //if (itemToPrint.Name.Length > 20)
                            //    productName = new Rectangle(labelColumn * columnWidth + e.MarginBounds.X + (totalSpaces / 2), labelRow * rowHeight + e.MarginBounds.Y + 20, 175, int.Parse(Math.Round(myFont.GetHeight() * 2,0).ToString()));
                            //else
                            productName = new Rectangle(labelColumn * columnWidth + e.MarginBounds.X + (totalSpaces / 2), labelRow * rowHeight + e.MarginBounds.Y + 20, 175, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));

                            e.Graphics.DrawString(itemToPrint.Name, myFont, Brushes.Black, productName);

                            totalSpaces = 175;

                            totalSpaces = totalSpaces - itemToPrint.Price.ToString("C").Length;

                            //print price
                            Rectangle productPrice = new Rectangle(labelColumn * columnWidth + e.MarginBounds.X + (totalSpaces / 2), productName.Y + productName.Height, 175, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                            e.Graphics.DrawString(itemToPrint.Price.ToString("C"), myFont, Brushes.Black, productPrice);

                            labelColumn++;

                            if (labelColumn > numLabelColumns - 1)
                            {
                                labelColumn = 0;
                                labelRow++;
                            }
                        }
                        break;
                }

                itemToPrint = null;
            }

            ////save the new default row and column
            //objects.LabelPrintingSettings settings = new VoodooPOS.objects.LabelPrintingSettings();
            //settings.ID = 1;
            //settings.DateCreated = DateTime.Now;
            //settings.NumColumns = numLabelColumns;
            //settings.StartingColumnIndex = labelColumn;
            //settings.StartingRowIndex = labelRow;

            //xmlData.Insert(settings, XmlData.Tables.LabelPrintingSettings);
        }

        public void PrintReceipt(bool giftReceipt, ArrayList currentCart)
        {
            arCart = currentCart;

            PrintDocument recordDoc;
            // Create the document and name it
            recordDoc = new PrintDocument();
            recordDoc.DocumentName = "Customer Receipt";
            recordDoc.DefaultPageSettings.Landscape = false;

            if (giftReceipt)
                recordDoc.PrintPage += new PrintPageEventHandler(this.printGiftReceiptPage);
            else
                recordDoc.PrintPage += new PrintPageEventHandler(this.printReceipt);

            PaperSize paperSize = null;

            switch (receiptWidth)
            {
                case (30)://new thermal printer
                    paperSize = new PaperSize("Receipt", 230, 1100);
                    break;
                default:
                    paperSize =  new PaperSize("Receipt", 275, 1100);
                    break;
            }

            recordDoc.DefaultPageSettings.PaperSize = paperSize;

            Margins margins = new Margins(5, 8, 5, 5);
            recordDoc.DefaultPageSettings.Margins = margins;
            recordDoc.PrinterSettings.PrinterName = "Receipt Printer";

#if DEBUG
            // Preview document
            System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printPreviewDialog1.Document = recordDoc;
            printPreviewDialog1.ShowDialog();
#else
            //print with no preview
            recordDoc.Print();
#endif

            //Dispose of document when done printing
            recordDoc.Dispose();
        }

        private void printReceipt(object sender, PrintPageEventArgs e)
        {
            Bitmap logo;
            Rectangle logoArea;
            Rectangle displayArea;
            Font myFont;
            Rectangle address;
            Rectangle city;
            Rectangle dateAndTime;
            Rectangle phoneArea;
            Font smallFont;

            string tempName = "";
            double tempPrice = 0;
            double cartTotal = 0;
            int itemCounter = 0;
            bool onSale = false;

            switch (receiptWidth)
            {
                case (30)://new thermal printer
                    logo = new Bitmap(appPath + "\\images\\VoodooDollTransparent.gif");
                    
                    logoArea = new Rectangle(e.MarginBounds.Width / 2 - 60,e.MarginBounds.Y + 20, 95, 150);

                    //print logo
                    e.Graphics.DrawImage(logo, logoArea);

                    //print company name
                    myFont = new Font("Hipnotik", 25, FontStyle.Regular);
                    displayArea = new Rectangle(e.MarginBounds.X, logoArea.Bottom + 2, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("Voodoo Toy Shop", myFont, Brushes.DarkRed, displayArea);

                    //print phone number
                    myFont = new Font("Teen", 8, FontStyle.Regular);
                    phoneArea = new Rectangle(e.MarginBounds.X, displayArea.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("720~722~4FUN", myFont, Brushes.Green, phoneArea);

                    //print address
                    myFont = new Font("Teen", 8, FontStyle.Regular);
                    address = new Rectangle(e.MarginBounds.X, phoneArea.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("20 Lakeview Dr #111", myFont, Brushes.Green, address);

                    //print city
                    myFont = new Font("Teen", 8, FontStyle.Regular);
                    city = new Rectangle(e.MarginBounds.X, address.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("Nederland, CO 80466", myFont, Brushes.Green, city);

                    //print date and time
                    myFont = new Font("Teen", 7, FontStyle.Regular);
                    dateAndTime = new Rectangle(e.MarginBounds.X, city.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), myFont, Brushes.Black, dateAndTime);

                    // Print receipt
                    displayArea = new Rectangle(e.MarginBounds.X, dateAndTime.Bottom + 10, e.MarginBounds.Width - 20, e.MarginBounds.Height);
                    myFont = new Font("Teen", 10, FontStyle.Regular);
                    
                    smallFont = new Font("Teen", 6, FontStyle.Regular);

                    tempName = "";
                    tempPrice = 0;
                    cartTotal = 0;
                    itemCounter = 0;
                    onSale = false;

                    foreach (Voodoo.Objects.InventoryItem item in arCart)
                    {
                        if (!item.Name.StartsWith("New Cart_"))
                        {
                            //check length of name and add to string
                            tempName = item.Name;

                            if (tempName.Length > 12)
                                tempName = tempName.Substring(0, 12) + "...";

                            if (item.OnSale && item.SalePrice > 0)
                            {
                                tempPrice = item.SalePrice * item.Quantity;

                                onSale = true;
                            }
                            else
                            {
                                tempPrice = item.Price * item.Quantity;

                                onSale = false;
                            }

                            //print product name
                            e.Graphics.DrawString(tempName, myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                            //print product quantity
                            e.Graphics.DrawString(item.Quantity.ToString() + " @ ", smallFont, Brushes.Black, e.MarginBounds.Right - 100, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())) + 5);

                            //print product price
                            if (onSale)
                            {
                                //e.Graphics.DrawString("$" + string.Format("{0:C2}", (tempPrice.ToString("N"))), myFont, Brushes.Red, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));
                                e.Graphics.DrawString("$" + string.Format("{0:C2}", (item.SalePrice.ToString("N"))), myFont, Brushes.Red, e.MarginBounds.Right - 80, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                                //cartTotal += item.SalePrice;
                            }
                            else
                            {
                                //e.Graphics.DrawString("$" + string.Format("{0:C2}", (tempPrice.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));
                                e.Graphics.DrawString("$" + string.Format("{0:C2}", (item.Price.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 80, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                                //cartTotal += item.Price;
                            }

                            cartTotal += tempPrice;

                            itemCounter++;
                        }
                    }

                    myFont = new Font("Teen", 11, FontStyle.Bold);

                    e.Graphics.DrawString("~~~~~~~~~~~~~~~~~~~~~~~~~~~", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    myFont = new Font("Teen", 10, FontStyle.Regular);

                    itemCounter++;
                    itemCounter++;

                    //e.Graphics.DrawString("Discount", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    //e.Graphics.DrawString("$" + string.Format("{0:C2}", ("0")), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * 20));

                    //itemCounter++;

                    e.Graphics.DrawString("Subtotal", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    itemCounter++;

                    e.Graphics.DrawString("Tax", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    itemCounter++;

                    e.Graphics.DrawString("Total", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal + SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));
                    
                    itemCounter++;

                    e.Graphics.DrawString(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine, myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));
                    break;
                default: //using the hand cut paper receipts and regular printer
                    logo = new Bitmap(appPath + "\\images\\VoodooDollTransparent.gif");
                    logoArea = new Rectangle((e.MarginBounds.Width - 100) / 2, e.MarginBounds.Y, 95, 150);

                    //print logo
                    e.Graphics.DrawImage(logo, logoArea);

                    //print company name
                    displayArea = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + logoArea.Height + 10, e.MarginBounds.Width, 60);
                    myFont = new Font("Hipnotik", 32, FontStyle.Regular);
                    e.Graphics.DrawString("Voodoo Toy Shop", myFont, Brushes.DarkRed, displayArea);

                    //print address
                    address = new Rectangle(e.MarginBounds.X + 80, e.MarginBounds.Y + logoArea.Height + displayArea.Height, e.MarginBounds.Width, 10);
                    myFont = new Font("Times New Roman", 8, FontStyle.Regular);
                    e.Graphics.DrawString("20 Lakeview Dr #111", myFont, Brushes.Green, address);

                    //print city
                    city = new Rectangle(e.MarginBounds.X + 80, e.MarginBounds.Y + logoArea.Height + displayArea.Height + address.Height, e.MarginBounds.Width, 20);
                    myFont = new Font("Times New Roman", 8, FontStyle.Regular);
                    e.Graphics.DrawString("Nederland, CO 80466", myFont, Brushes.Green, city);

                    //print date and time
                    dateAndTime = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + logoArea.Height + displayArea.Height + address.Height + city.Height, e.MarginBounds.Width, 20);
                    myFont = new Font("Times New Roman", 8, FontStyle.Regular);
                    e.Graphics.DrawString(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), myFont, Brushes.Black, dateAndTime);

                    // Print receipt
                    displayArea = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + logoArea.Height + displayArea.Height + dateAndTime.Height + address.Height + city.Height + 10, e.MarginBounds.Width, e.MarginBounds.Height - logoArea.Height - displayArea.Height);
                    myFont = new Font("Times New Roman", 15, FontStyle.Regular);
                    //e.Graphics.DrawString(receipt.ToString(), myFont, Brushes.Black, displayArea);

                    smallFont = new Font("Times New Roman", 8, FontStyle.Regular);

                    tempName = "";
                    tempPrice = 0;
                    cartTotal = 0;
                    itemCounter = 0;
                    onSale = false;

                    foreach (Voodoo.Objects.InventoryItem item in arCart)
                    {
                        if (!item.Name.StartsWith("New Cart_"))
                        {
                            //check length of name and add to string
                            tempName = item.Name;

                            if (tempName.Length > 12)
                                tempName = tempName.Substring(0, 12) + "...";

                            if (item.OnSale && item.SalePrice > 0)
                            {
                                tempPrice = item.SalePrice * item.Quantity;

                                onSale = true;
                            }
                            else
                            {
                                tempPrice = item.Price * item.Quantity;

                                onSale = false;
                            }

                            //print product name
                            e.Graphics.DrawString(tempName, myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                            //print product quantity
                            e.Graphics.DrawString(item.Quantity.ToString() + " @ ", smallFont, Brushes.Black, e.MarginBounds.Right - 87, displayArea.Top + (itemCounter * 20) + 6);

                            //print product price
                            if (onSale)
                            {
                                //e.Graphics.DrawString("$" + string.Format("{0:C2}", (tempPrice.ToString("N"))), myFont, Brushes.Red, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));
                                e.Graphics.DrawString("$" + string.Format("{0:C2}", (item.SalePrice.ToString("N"))), myFont, Brushes.Red, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));

                                //cartTotal += item.SalePrice;
                            }
                            else
                            {
                                //e.Graphics.DrawString("$" + string.Format("{0:C2}", (tempPrice.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));
                                e.Graphics.DrawString("$" + string.Format("{0:C2}", (item.Price.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));

                                //cartTotal += item.Price;
                            }

                            cartTotal += tempPrice;

                            itemCounter++;
                        }
                    }

                    e.Graphics.DrawString("-----------------------------------", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    itemCounter++;

                    //e.Graphics.DrawString("Discount", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    //e.Graphics.DrawString("$" + string.Format("{0:C2}", ("0")), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));

                    //itemCounter++;

                    e.Graphics.DrawString("Subtotal", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));

                    itemCounter++;

                    e.Graphics.DrawString("Tax", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));

                    itemCounter++;

                    e.Graphics.DrawString("Total", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal + SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));
                    break;
            }
            
        }

        private void printGiftReceiptPage(object sender, PrintPageEventArgs e)
        {
            Bitmap logo;
            Rectangle logoArea;
            Rectangle displayArea;
            Font myFont;
            Rectangle address;
            Rectangle city;
            Rectangle dateAndTime;
            Rectangle phoneArea;
            Font smallFont;

            string tempName = "";
            double tempPrice = 0;
            double cartTotal = 0;
            int itemCounter = 0;
            bool onSale = false;

            logo = new Bitmap(appPath + "\\images\\VoodooDollTransparent.gif");
            logoArea = new Rectangle((e.MarginBounds.Width - 120) / 2, e.MarginBounds.Y, 95, 150);

            //print logo
            e.Graphics.DrawImage(logo, logoArea);

            //print company name
            myFont = new Font("Hipnotik", 25, FontStyle.Regular);
            displayArea = new Rectangle(e.MarginBounds.X, logoArea.Bottom + 2, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
            e.Graphics.DrawString("Voodoo Toy Shop", myFont, Brushes.DarkRed, displayArea);

            //print phone number
            myFont = new Font("Teen", 8, FontStyle.Regular);
            phoneArea = new Rectangle(e.MarginBounds.X, displayArea.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
            e.Graphics.DrawString("720~722~4FUN", myFont, Brushes.Green, phoneArea);

            //print address
            myFont = new Font("Teen", 8, FontStyle.Regular);
            address = new Rectangle(e.MarginBounds.X, phoneArea.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
            e.Graphics.DrawString("20 Lakeview Dr #111", myFont, Brushes.Green, address);

            //print city
            myFont = new Font("Teen", 8, FontStyle.Regular);
            city = new Rectangle(e.MarginBounds.X, address.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
            e.Graphics.DrawString("Nederland, CO 80466", myFont, Brushes.Green, city);

            //print date and time
            myFont = new Font("Teen", 7, FontStyle.Regular);
            dateAndTime = new Rectangle(e.MarginBounds.X, city.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
            e.Graphics.DrawString(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), myFont, Brushes.Black, dateAndTime);

            // Print receipt
            displayArea = new Rectangle(e.MarginBounds.X, dateAndTime.Bottom + 10, e.MarginBounds.Width - 20, e.MarginBounds.Height);
            myFont = new Font("Teen", 10, FontStyle.Regular);

            smallFont = new Font("Teen", 6, FontStyle.Regular);

            tempName = "";
            tempPrice = 0;
            cartTotal = 0;
            itemCounter = 0;
            onSale = false;

            foreach (Voodoo.Objects.InventoryItem item in arCart)
            {
                if (!item.Name.StartsWith("New Cart_"))
                {
                    //check length of name and add to string
                    tempName = item.Name;

                    if (tempName.Length > 20)
                        tempName = tempName.Substring(0, 20) + "...";

                    if (item.OnSale && item.SalePrice > 0)
                    {
                        tempPrice = item.SalePrice * item.Quantity;

                        onSale = true;
                    }
                    else
                    {
                        tempPrice = item.Price * item.Quantity;

                        onSale = false;
                    }

                    //print product name
                    e.Graphics.DrawString(tempName + " x " + item.Quantity.ToString(), myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    itemCounter++;
                }
            }
        }

        public void PrintGiftCertificate(double GiftCertificateAmount, int GiftCertificateID, Bitmap barcode)
        {
            PrintGiftCertificate(nameOnCertificate, GiftCertificateAmount, GiftCertificateID, barcode);
        }

        public void PrintGiftCertificate(string NameOnCertificate, double Amount, int GiftCertificateID, Bitmap barcode)
        {
            PrintDocument labelDoc;

            nameOnCertificate = NameOnCertificate;
            giftCertificateID = GiftCertificateID;
            giftCertificateAmount = Amount;
            barcodeToPrint = barcode;

            // Create the document and name it
            labelDoc = new PrintDocument();
            labelDoc.DocumentName = "Gift Certificate";
            labelDoc.DefaultPageSettings.Landscape = true;

            labelDoc.PrintPage += new PrintPageEventHandler(this.printGiftCertificate);

            PaperSize paperSize = new PaperSize("GiftCertificate", 1100, 850); //full 8.5 x 11 page
            //PaperSize paperSize = new PaperSize("Label", 83, 366);
            labelDoc.DefaultPageSettings.PaperSize = paperSize;

            Margins margins = new Margins(5, 5, 5, 5);
            labelDoc.DefaultPageSettings.Margins = margins;

#if DEBUG
            // Preview document
            System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printPreviewDialog1.Document = labelDoc;
            printPreviewDialog1.ShowDialog();
#else
            //print with no preview
            labelDoc.Print();
#endif

            //Dispose of document when done printing
            labelDoc.Dispose();

        }

        private void printGiftCertificate(object sender, PrintPageEventArgs e)
        {
            DateTime dtPrintTime = DateTime.Now;

            //small rectangle
            e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), e.MarginBounds);

            //large rectangle
            e.Graphics.DrawRectangle(new Pen(Color.Blue, 6), new Rectangle(e.MarginBounds.X + 6, e.MarginBounds.Y + 6, e.MarginBounds.Width - 12, e.MarginBounds.Height - 12));

            //small rectangle
            e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), new Rectangle(e.MarginBounds.X + 12, e.MarginBounds.Y + 12, e.MarginBounds.Width - 24, e.MarginBounds.Height - 24));

            Bitmap logo = new Bitmap(appPath + "\\images\\VoodooDollTransparent.gif");
            Rectangle logoArea = new Rectangle((e.MarginBounds.Width - 100) / 2, e.MarginBounds.Y + 20, 95, 150);

            //print logo
            e.Graphics.DrawImage(logo, logoArea);

            //print company name
            Rectangle displayArea = new Rectangle((e.MarginBounds.Width - 100) / 2 - 80, e.MarginBounds.Y + logoArea.Height + 30, e.MarginBounds.Width, 60);
            Font myFont = new Font("Hipnotik", 32, FontStyle.Regular);
            e.Graphics.DrawString("Voodoo Toy Shop", myFont, Brushes.DarkRed, displayArea);

            //print address
            Rectangle address = new Rectangle((e.MarginBounds.Width - 100) / 2 - 10, logoArea.Y + logoArea.Height + displayArea.Height + 10, e.MarginBounds.Width, 10);
            myFont = new Font("Times New Roman", 8, FontStyle.Regular);
            e.Graphics.DrawString("20 Lakeview Dr #111", myFont, Brushes.Green, address);

            //print city
            Rectangle city = new Rectangle((e.MarginBounds.Width - 100) / 2 - 10, e.MarginBounds.Y + logoArea.Height + displayArea.Height + address.Height + 10, e.MarginBounds.Width, 20);
            myFont = new Font("Times New Roman", 8, FontStyle.Regular);
            e.Graphics.DrawString("Nederland, CO 80466", myFont, Brushes.Green, city);

            //print date and time
            Rectangle dateAndTime = new Rectangle((e.MarginBounds.Width - 100) / 2 - 10, e.MarginBounds.Y + logoArea.Height + displayArea.Height + address.Height + city.Height + 10, e.MarginBounds.Width, 20);
            myFont = new Font("Times New Roman", 8, FontStyle.Regular);
            e.Graphics.DrawString(dtPrintTime.ToShortDateString() + " " + dtPrintTime.ToShortTimeString(), myFont, Brushes.Black, dateAndTime);

            //print amount of certificate
            Rectangle amountArea = new Rectangle((e.MarginBounds.Width - 100) / 2 - 75, dateAndTime.Bottom + 20, e.MarginBounds.Width, 80);
            myFont = new Font("Castellar", 48, FontStyle.Regular);
            e.Graphics.DrawString(giftCertificateAmount.ToString("C"), myFont, Brushes.Green, amountArea);

            // Print certificate
            displayArea = new Rectangle((e.MarginBounds.Width - 100) / 2 - 175, amountArea.Bottom + 10, e.MarginBounds.Width, 80);
            myFont = new Font("Times New Roman", 48, FontStyle.Regular);
            e.Graphics.DrawString("Gift Certificate", myFont, Brushes.Black, displayArea);

            displayArea = new Rectangle((e.MarginBounds.Width - 100) / 2, displayArea.Bottom + 10, e.MarginBounds.Width, 40);
            myFont = new Font("Times New Roman", 32, FontStyle.Regular);
            e.Graphics.DrawString("for", myFont, Brushes.Black, displayArea);

            //print name
            Rectangle nameArea = new Rectangle((e.MarginBounds.Width - 100) / 2 - (nameOnCertificate.Length * 17), displayArea.Bottom + 20, e.MarginBounds.Width, 150);
            myFont = new Font("Curlz MT", 85, FontStyle.Regular);
            e.Graphics.DrawString(nameOnCertificate, myFont, Brushes.Black, nameArea);

            //print barcode
            Rectangle barcodeArea = new Rectangle((e.MarginBounds.Width - 100) / 2, nameArea.Bottom + 50, 2500, 2000);
            e.Graphics.DrawImage(barcodeToPrint, barcodeArea);
        }

        public void PrintReport()
        {
            
            PrintDocument reportDoc;
            // Create the document and name it
            reportDoc = new PrintDocument();
            reportDoc.DocumentName = "Report";
            reportDoc.DefaultPageSettings.Landscape = false;

            reportDoc.PrintPage += new PrintPageEventHandler(this.printTheReport);

            PaperSize paperSize = null;

            switch (receiptWidth)
            {
                case (30)://new thermal printer
                    paperSize = new PaperSize("Receipt", 230, 1100);
                    break;
                default:
                    paperSize = new PaperSize("Receipt", 275, 1100);
                    break;
            }

            reportDoc.DefaultPageSettings.PaperSize = paperSize;

            Margins margins = new Margins(5, 8, 5, 5);
            reportDoc.DefaultPageSettings.Margins = margins;
            reportDoc.PrinterSettings.PrinterName = "Receipt Printer";

#if DEBUG
            // Preview document
            System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printPreviewDialog1.Document = reportDoc;
            printPreviewDialog1.ShowDialog();
#else
            //print with no preview
            reportDoc.Print();
#endif

            //Dispose of document when done printing
            reportDoc.Dispose();
        }

        private void printTheReport(object sender, PrintPageEventArgs e)
        {
            Bitmap logo;
            Rectangle logoArea;
            Rectangle displayArea;
            Font myFont;
            Rectangle address;
            Rectangle city;
            Rectangle dateAndTime;
            Rectangle phoneArea;
            Font smallFont;

            string tempName = "";
            double tempPrice = 0;
            double cartTotal = 0;
            int itemCounter = 0;
            bool onSale = false;

            switch (receiptWidth)
            {
                case (30)://new thermal printer
                    logo = new Bitmap(appPath + "\\images\\VoodooDollTransparent.gif");

                    logoArea = new Rectangle(e.MarginBounds.Width / 2 - 60, e.MarginBounds.Y + 20, 95, 150);

                    //print logo
                    e.Graphics.DrawImage(logo, logoArea);

                    //print company name
                    myFont = new Font("Hipnotik", 25, FontStyle.Regular);
                    displayArea = new Rectangle(e.MarginBounds.X, logoArea.Bottom + 2, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("Voodoo Toy Shop", myFont, Brushes.DarkRed, displayArea);

                    //print phone number
                    myFont = new Font("Teen", 8, FontStyle.Regular);
                    phoneArea = new Rectangle(e.MarginBounds.X, displayArea.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("720~722~4FUN", myFont, Brushes.Green, phoneArea);

                    //print address
                    myFont = new Font("Teen", 8, FontStyle.Regular);
                    address = new Rectangle(e.MarginBounds.X, phoneArea.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("20 Lakeview Dr #111", myFont, Brushes.Green, address);

                    //print city
                    myFont = new Font("Teen", 8, FontStyle.Regular);
                    city = new Rectangle(e.MarginBounds.X, address.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString("Nederland, CO 80466", myFont, Brushes.Green, city);

                    //print date and time
                    myFont = new Font("Teen", 7, FontStyle.Regular);
                    dateAndTime = new Rectangle(e.MarginBounds.X, city.Bottom, e.MarginBounds.Width, int.Parse(Math.Round(myFont.GetHeight(), 0).ToString()));
                    e.Graphics.DrawString(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), myFont, Brushes.Black, dateAndTime);

                    // Print receipt
                    displayArea = new Rectangle(e.MarginBounds.X, dateAndTime.Bottom + 10, e.MarginBounds.Width - 20, e.MarginBounds.Height);
                    myFont = new Font("Teen", 10, FontStyle.Regular);

                    smallFont = new Font("Teen", 6, FontStyle.Regular);

                    //print the report
                    

                    myFont = new Font("Teen", 11, FontStyle.Bold);

                    e.Graphics.DrawString("~~~~~~~~~~~~~~~~~~~~~~~~~~~", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    myFont = new Font("Teen", 10, FontStyle.Regular);

                    itemCounter++;
                    itemCounter++;

                    //e.Graphics.DrawString("Discount", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    //e.Graphics.DrawString("$" + string.Format("{0:C2}", ("0")), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * 20));

                    //itemCounter++;

                    e.Graphics.DrawString("Subtotal", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    itemCounter++;

                    e.Graphics.DrawString("Tax", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    itemCounter++;

                    e.Graphics.DrawString("Total", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal + SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 85, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));

                    itemCounter++;

                    e.Graphics.DrawString(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine, myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * int.Parse(Math.Round(myFont.GetHeight(), 0).ToString())));
                    break;
                default: //using the hand cut paper receipts and regular printer
                    logo = new Bitmap(appPath + "\\images\\VoodooDollTransparent.gif");
                    logoArea = new Rectangle((e.MarginBounds.Width - 100) / 2, e.MarginBounds.Y, 95, 150);

                    //print logo
                    e.Graphics.DrawImage(logo, logoArea);

                    //print company name
                    displayArea = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + logoArea.Height + 10, e.MarginBounds.Width, 60);
                    myFont = new Font("Hipnotik", 32, FontStyle.Regular);
                    e.Graphics.DrawString("Voodoo Toy Shop", myFont, Brushes.DarkRed, displayArea);

                    //print address
                    address = new Rectangle(e.MarginBounds.X + 80, e.MarginBounds.Y + logoArea.Height + displayArea.Height, e.MarginBounds.Width, 10);
                    myFont = new Font("Times New Roman", 8, FontStyle.Regular);
                    e.Graphics.DrawString("20 Lakeview Dr #111", myFont, Brushes.Green, address);

                    //print city
                    city = new Rectangle(e.MarginBounds.X + 80, e.MarginBounds.Y + logoArea.Height + displayArea.Height + address.Height, e.MarginBounds.Width, 20);
                    myFont = new Font("Times New Roman", 8, FontStyle.Regular);
                    e.Graphics.DrawString("Nederland, CO 80466", myFont, Brushes.Green, city);

                    //print date and time
                    dateAndTime = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + logoArea.Height + displayArea.Height + address.Height + city.Height, e.MarginBounds.Width, 20);
                    myFont = new Font("Times New Roman", 8, FontStyle.Regular);
                    e.Graphics.DrawString(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), myFont, Brushes.Black, dateAndTime);

                    // Print receipt
                    displayArea = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + logoArea.Height + displayArea.Height + dateAndTime.Height + address.Height + city.Height + 10, e.MarginBounds.Width, e.MarginBounds.Height - logoArea.Height - displayArea.Height);
                    myFont = new Font("Times New Roman", 15, FontStyle.Regular);
                    //e.Graphics.DrawString(receipt.ToString(), myFont, Brushes.Black, displayArea);

                    smallFont = new Font("Times New Roman", 8, FontStyle.Regular);

                    tempName = "";
                    tempPrice = 0;
                    cartTotal = 0;
                    itemCounter = 0;
                    onSale = false;

                    foreach (Voodoo.Objects.InventoryItem item in arCart)
                    {
                        if (!item.Name.StartsWith("New Cart_"))
                        {
                            //check length of name and add to string
                            tempName = item.Name;

                            if (tempName.Length > 12)
                                tempName = tempName.Substring(0, 12) + "...";

                            if (item.OnSale && item.SalePrice > 0)
                            {
                                tempPrice = item.SalePrice * item.Quantity;

                                onSale = true;
                            }
                            else
                            {
                                tempPrice = item.Price * item.Quantity;

                                onSale = false;
                            }

                            //print product name
                            e.Graphics.DrawString(tempName, myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                            //print product quantity
                            e.Graphics.DrawString(item.Quantity.ToString() + " @ ", smallFont, Brushes.Black, e.MarginBounds.Right - 87, displayArea.Top + (itemCounter * 20) + 6);

                            //print product price
                            if (onSale)
                            {
                                //e.Graphics.DrawString("$" + string.Format("{0:C2}", (tempPrice.ToString("N"))), myFont, Brushes.Red, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));
                                e.Graphics.DrawString("$" + string.Format("{0:C2}", (item.SalePrice.ToString("N"))), myFont, Brushes.Red, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));

                                //cartTotal += item.SalePrice;
                            }
                            else
                            {
                                //e.Graphics.DrawString("$" + string.Format("{0:C2}", (tempPrice.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));
                                e.Graphics.DrawString("$" + string.Format("{0:C2}", (item.Price.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 70, displayArea.Top + (itemCounter * 20));

                                //cartTotal += item.Price;
                            }

                            cartTotal += tempPrice;

                            itemCounter++;
                        }
                    }

                    e.Graphics.DrawString("-----------------------------------", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    itemCounter++;

                    //e.Graphics.DrawString("Discount", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    //e.Graphics.DrawString("$" + string.Format("{0:C2}", ("0")), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));

                    //itemCounter++;

                    e.Graphics.DrawString("Subtotal", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal.ToString("N"))), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));

                    itemCounter++;

                    e.Graphics.DrawString("Tax", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));

                    itemCounter++;

                    e.Graphics.DrawString("Total", myFont, Brushes.Black, e.MarginBounds.Left, displayArea.Top + (itemCounter * 20));

                    e.Graphics.DrawString("$" + string.Format("{0:C2}", (cartTotal + SalesTax.GetTax(cartTotal)).ToString("N")), myFont, Brushes.Black, e.MarginBounds.Right - 75, displayArea.Top + (itemCounter * 20));
                    break;
            }
        }
    }
}
