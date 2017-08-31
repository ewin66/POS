using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Xml;

namespace VoodooPOS
{
    public class XmlData
    {
        string appPath = "";

        //need enums for xml files available (dbTables)
        public enum Tables
        {
            InventoryItems, Transactions, GiftCertificates, GiftCertificateActivity, GiftCertificateStatus, CashRegisterDrawer,
            L_InventoryItemsToFeaturedItems, Categories, L_inventoryItemsToCategories, LabelPrintingSettings, Templates, Employees,
            _SecurityLevels, L_InventoryItemsToQuickButtons, L_InventoryItemsToSalesCalendar
        }

        public XmlData(string AppPath)
        {
            appPath = AppPath;

            if (!System.IO.Directory.Exists(AppPath))
                System.IO.Directory.CreateDirectory(AppPath);
        }

        public DataTable ApplyNewSchema(Tables table)
        {
            string filename = appPath + "/common/xmlData/" + table.ToString() + ".xml";

            DataTable dtNewTable = new DataTable("InventoryItems");
            dtNewTable.ReadXml(filename);

            return applyNewSchema(dtNewTable, filename, filename.Replace(".xml", ".xsd"));
        }

        private DataTable applyNewSchema(DataTable dtOldTable, string tableFile, string newSchemaFile)
        {
            XmlDataDocument xmlNewSchema = new XmlDataDocument();
            xmlNewSchema.DataSet.ReadXmlSchema(newSchemaFile);

            if (dtOldTable.Columns.Count != xmlNewSchema.DataSet.Tables[0].Columns.Count)
            {
                if (dtOldTable.Columns.Count > xmlNewSchema.DataSet.Tables[0].Columns.Count)
                {
                    //iterate datatable columns and match up to schema columns
                    foreach (DataColumn dc in dtOldTable.Columns)
                    {
                        if (!xmlNewSchema.DataSet.Tables[0].Columns.Contains(dc.ColumnName))
                        {
                            //remove column
                            dtOldTable.Columns.Remove(dc.ColumnName);
                        }
                    }
                }
                else
                {
                    //iterate schema columns and match up to datatable columns
                    foreach (DataColumn dc in xmlNewSchema.DataSet.Tables[0].Columns)
                    {
                        //if (dc.ColumnName.ToLower() == "picturepath")
                        //    System.Windows.Forms.MessageBox.Show("found picturePath");

                        if (!dtOldTable.Columns.Contains(dc.ColumnName))
                        {
                            //add column
                            dtOldTable.Columns.Add(dc.ColumnName, Type.GetType(dc.DataType.ToString()));
                            dtOldTable.Columns[dc.ColumnName].SetOrdinal(dtOldTable.Columns[dc.ColumnName].Ordinal - 1);//put column before dateCreated

                            foreach (DataRow dr in dtOldTable.Rows)//add the default value to the new column
                            {
                                if (dr[dc.ColumnName].ToString().Length == 0)
                                    dr[dc.ColumnName] = dc.DefaultValue;
                            }
                        }
                    }
                }

                dtOldTable.ReadXmlSchema(newSchemaFile);

                dtOldTable.WriteXml(tableFile);
            }

            return dtOldTable;
        }

        public DataTable Select(string whereClause, string sort, Tables table)
        {
            return Select(whereClause, sort, "data\\"+ table.ToString());
        }

        public DataTable Select(string whereClause, string sort, string tableName, string schemaFileName)
        {
            DataTable dtReturnTable = null;
            DataSet dsTemp = new DataSet();
            string filename = "";

            //if(!appPath.ToLower().Contains("\\data"))
            //    appPath = appPath + "\\data";
            //else

            filename = appPath + "\\" + tableName.ToString() + ".xml";

            if (System.IO.File.Exists(filename))
            {
                if (!schemaFileName.ToLower().Contains(".xsd"))
                    schemaFileName = schemaFileName + ".xsd";

                try
                {
                    dsTemp.ReadXml(filename);
                }
                catch (Exception ex)
                {

                }

                if (dsTemp.Tables.Count > 0)
                {
                    applyNewSchema(dsTemp.Tables[0], filename, appPath + "\\" + schemaFileName);

                    dsTemp = new DataSet();

                    dsTemp.ReadXmlSchema(appPath + "\\" + schemaFileName);

                    try
                    {
                        dsTemp.ReadXml(filename);
                    }
                    catch (Exception ex)
                    {

                    }

                    DataRow[] drFoundRows = null;

                    if (whereClause.Trim().Length > 0 && whereClause.Trim() != "*")
                        drFoundRows = dsTemp.Tables[0].Select(whereClause, sort);
                    else if (dsTemp.Tables.Count > 0)
                        drFoundRows = dsTemp.Tables[0].Select("", sort);

                    if (drFoundRows != null && drFoundRows.Length > 0)
                    {
                        dtReturnTable = dsTemp.Tables[0].Clone();

                        for (int x = 0; x < drFoundRows.Length; x++)
                            dtReturnTable.Rows.Add(drFoundRows[x].ItemArray);
                    }
                }
            }
            else
                throw new Exception("Didn't find xml file " + filename);

            return dtReturnTable;
        }

        public DataTable Select(string whereClause, string sort, string tableName)
        {
            return Select(whereClause, sort, tableName, tableName);
        }

        public int Insert(string valuesToInsert, Tables table)
        {
            return Insert(valuesToInsert, table.ToString());
        }

        public int Insert(string valuesToInsert, string tableName)
        {
            return Insert(valuesToInsert, tableName, tableName);
        }

        public int Insert(string valuesToInsert, string tableFileName, string schemaFileName)
        {
            int newRowID = 0;
            int counter = 0;

            try
            {
                DataTable dtReturnTable = new DataTable(tableFileName.Replace("\\","."));
                string fullTableFileName = appPath + "\\" + tableFileName;

                string fullSchemaFileName = appPath + "\\" + schemaFileName;

                if (!fullTableFileName.ToLower().Contains(".xml"))
                    fullTableFileName += ".xml";

                if (!fullSchemaFileName.ToLower().Contains(".xsd"))
                    fullSchemaFileName += ".xsd";

                checkTableFile(fullTableFileName);

                DataSet dsTemp = new DataSet();
                dsTemp.ReadXmlSchema(fullSchemaFileName);

                try
                {
                    dsTemp.ReadXml(fullTableFileName);
                }
                catch (Exception ex)
                {
                    Common.WriteToFile(ex);
                }
                
                dsTemp.Tables[0].Columns[0].AutoIncrement = true;
                dsTemp.Tables[0].Columns[0].AutoIncrementSeed = 1;
                dsTemp.Tables[0].Columns[0].AutoIncrementStep = 1;

                string[] tempValues = valuesToInsert.Split(new string[] { ") values(" }, StringSplitOptions.None);
                tempValues[0] = tempValues[0].Substring(1);
                tempValues[1] = tempValues[1].Substring(0, tempValues[1].Length - (tempValues[1].Length - tempValues[1].LastIndexOf(')')));

                string[] columns = tempValues[0].Split(new string[] { "|,|" }, StringSplitOptions.None);
                string[] values = tempValues[1].Split(new string[] { "|,|" }, StringSplitOptions.None);

                DataRow drNewRow = dsTemp.Tables[0].NewRow();

                foreach (string column in columns)
                {
                    //if ((column.ToLower().Trim() != "id" || values[counter].ToString().Trim().Length > 0) && values[counter].ToString().Trim() != "999999")
                    //    drNewRow[column] = values[counter];

                    if (tableFileName.Contains("Transactions_") || (column.ToLower().Trim() != "id" && (values[counter].ToString().Trim().Length > 0) && values[counter].ToString().Trim() != "999999"))
                            drNewRow[column] = values[counter];

                    counter++;
                }

                drNewRow["dateCreated"] = DateTime.Now;

                newRowID = int.Parse(drNewRow[0].ToString());

                dsTemp.Tables[0].Rows.Add(drNewRow);
                dsTemp.Tables[0].WriteXml(fullTableFileName);
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);

                newRowID = 0;
            }

            return newRowID;
        }

        private void checkTableFile(string fullTableFileName)
        {
            try
            {
                if (!System.IO.File.Exists(fullTableFileName))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(fullTableFileName);

                    System.IO.Directory.CreateDirectory(fi.Directory.FullName);

                    System.IO.FileStream fs = fi.Create();

                    fs.Close();
                    fs.Dispose();

                    //fi = null;
                    //fs = null;
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }

        public int Insert(object objToSave, string tableFileName)
        {
            return Insert(objToSave, tableFileName, tableFileName);
        }

        public int Insert(object objToSave, Tables table)
        {
            //if(table == Tables.Templates)
            //    return Insert(objToSave, "data\\" + table.ToString() + "_" + ((Voodoo.Objects.Template)objToSave).Name +".xml", "data\\" + table.ToString());
            //else
                return Insert(objToSave, "data\\" + table.ToString() +".xml", "data\\" + table.ToString());
        }

        public int Insert(object objToSave, string tableFileName, string schemaFileName)
        {
            int newRowID = 0;
            int counter = 0;
            string insertColumns = "";
            string insertValues = "";

            try
            {
                Type type = objToSave.GetType();
                //var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                var properties = type.GetProperties();

                insertColumns = "(";
                insertValues = " values(";

                foreach (PropertyInfo property in properties)
                {
                    if (insertColumns.Trim().Length > 1)
                    {
                        insertColumns += "|,|";
                        insertValues += "|,|";
                    }

                    insertColumns += property.Name;
                    insertValues += property.GetValue(objToSave, null).ToString();
                }

                insertColumns += ")";
                insertValues += ")";

                newRowID = Insert(insertColumns + insertValues, tableFileName,schemaFileName);
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }

            return newRowID;
        }

        public int Update(object objToUpdate, Tables table)
        {
            int newRowID = 0;
            int counter = 0;
            string updateColumns = "";
            string updateValues = "";
            string updateString = "";
            int numRowsUpdated = 0;

            try
            {
                Type type = objToUpdate.GetType();
                //var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                var properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (updateString.Trim().Length > 1)
                    {
                        updateString += "|,|";
                    }
                    else
                        updateString = "set ";

                    updateColumns += property.Name;
                    updateValues += property.GetValue(objToUpdate, null).ToString();

                    updateString += property.Name + "=" + property.GetValue(objToUpdate, null).ToString();
                }

                if (objToUpdate is Voodoo.Objects.InventoryItem)
                    updateString += " |where| id=" + ((Voodoo.Objects.InventoryItem)objToUpdate).ID;
                else if (objToUpdate is GiftCertificate)
                    updateString += " |where| id=" + ((GiftCertificate)objToUpdate).ID;
                else if (objToUpdate is VoodooPOS.objects.CashRegisterDrawer)
                    updateString += " |where| id=" + ((VoodooPOS.objects.CashRegisterDrawer)objToUpdate).ID;
                else if (objToUpdate is L_inventoryItemsToFeaturedItems)
                    updateString += " |where| inventoryItemID=" + ((L_inventoryItemsToFeaturedItems)objToUpdate).ID;
                else if (objToUpdate is Voodoo.Objects.Template)
                    updateString += " |where| name=" + ((Voodoo.Objects.Template)objToUpdate).Name;

                numRowsUpdated = Update(updateString, table);
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }

            return numRowsUpdated;
        }

        public int Update(string valuesToUpdate, Tables table)
        {
            int numRowsUpdated = 0;
            string whereClause = "";

            try
            {
                DataTable dtReturnTable = new DataTable(table.ToString());
                string filename = appPath + "\\data\\" + table.ToString() + ".xml";

                if (!System.IO.File.Exists(filename))
                {
                    Insert(valuesToUpdate, table);
                }
                else
                {

                    DataSet dsTemp = new DataSet();
                    dsTemp.ReadXmlSchema(filename.Replace(".xml", ".xsd"));
                    dsTemp.ReadXml(filename);

                    dsTemp.Tables[0].Columns[0].AutoIncrement = true;
                    dsTemp.Tables[0].Columns[0].AutoIncrementSeed = 1;
                    dsTemp.Tables[0].Columns[0].AutoIncrementStep = 1;

                    whereClause = valuesToUpdate.Substring(valuesToUpdate.IndexOf(" |where| "));
                    whereClause = whereClause.ToLower().Replace(" |where| ", "");

                    string tempValues = valuesToUpdate.Substring(0, valuesToUpdate.IndexOf(" |where| "));
                    tempValues = tempValues.Substring(4);//remove 'set '
                    string[] whereValues = tempValues.Split(new string[] { "|,|" }, StringSplitOptions.None);

                    foreach (DataRow foundRow in dsTemp.Tables[0].Select(whereClause))
                    {
                        //iterate columns and values and update
                        foreach (string values in whereValues)
                        {
                            string[] keyValue = values.Split('=');

                            if (keyValue[0].Trim().ToLower() != "id")
                                foundRow[keyValue[0].Trim()] = keyValue[1].Trim();
                        }

                        numRowsUpdated++;
                    }

                    dsTemp.Tables[0].WriteXml(filename);
                }
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);

                numRowsUpdated = 0;
            }

            return numRowsUpdated;
        }

        public int Delete(string whereClause, Tables table)
        {
            int numRowsDeleted = 0;

            try
            {
                DataTable dtReturnTable = new DataTable(table.ToString());
                string filename = appPath + "\\data\\" + table.ToString() + ".xml";

                DataSet dsTemp = new DataSet();
                dsTemp.ReadXmlSchema(filename.Replace(".xml", ".xsd"));
                dsTemp.ReadXml(filename);

                dsTemp.Tables[0].Columns[0].AutoIncrement = true;
                dsTemp.Tables[0].Columns[0].AutoIncrementSeed = 1;
                dsTemp.Tables[0].Columns[0].AutoIncrementStep = 1;

                DataRow[] rowsToDelete = null;

                if (whereClause.Trim().Length > 0 && whereClause.Trim() != "*")
                    rowsToDelete = dsTemp.Tables[0].Select(whereClause);
                else if (dsTemp.Tables.Count > 0)
                    rowsToDelete = dsTemp.Tables[0].Select("");

                foreach (DataRow foundRow in rowsToDelete)
                {
                    dsTemp.Tables[0].Rows.Remove(foundRow);

                    numRowsDeleted++;
                }

                dsTemp.Tables[0].WriteXml(filename);
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);

                numRowsDeleted = 0;
            }

            return numRowsDeleted;
        }
    }
}
