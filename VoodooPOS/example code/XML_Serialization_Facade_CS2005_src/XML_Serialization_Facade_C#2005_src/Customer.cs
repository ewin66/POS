using System;
using System.Xml.Serialization;  // Does XML serializing for a class.
using System.Drawing;            // Required for storing a Bitmap.
using System.IO;			     // Required for using Memory stream objects.
using System.ComponentModel;     // Required for conversion of Bitmap objects.

namespace disc1
{
	/// <summary>
	/// Customer test class to demonstrate how to include custom metadata attributes in a 
	/// class so that it can be serialized/deserialized to/from XML.
	/// 
	/// References: 
	/// XML Serialization at http://samples.gotdotnet.com/:
	/// http://samples.gotdotnet.com/QuickStart/howto/default.aspx?url=/quickstart/howto/doc/xmlserialization/rwobjfromxml.aspx
	/// 
	/// How do I serialize an image file as XML in .NET?
	/// http://www.perfectxml.com/Answers.asp?ID=2
	/// </summary>
	
	// Set this 'Customer' class as the root node of any XML file its serialized to.
	[XmlRootAttribute("Customer", Namespace="", IsNullable=false)]
	public class Customer
	{
		private Bitmap picture;

		/// <summary>
		/// Default constructor for this class (required for serialization).
		/// </summary>
		public Customer()
		{
		}

		// Set this 'DateTimeValue' field to be an attribute of the root node.
		[XmlAttributeAttribute(DataType="date")]
		public System.DateTime DateTimeValue;

		// By NOT specifing any custom Metadata Attributes, fields will be created
		// as an element by default.
		public int CustomerID;
		public string CustomerName;
        public int Age;

		// Set serialization to IGNORE this field (ie. not add it to the XML).
		[XmlIgnoreAttribute()]
		public bool CustomerPaid;

		// Set serialization to IGNORE this property because the 'PictureByteArray' property
		// is used instead to serialize the 'Picture' Bitmap as an array of bytes.
		[XmlIgnoreAttribute()]
		public Bitmap Picture
		{
			get { return picture; }
			set { picture = value; }
		}

		// Serializes the 'Picture' Bitmap to XML.
		[XmlElementAttribute("Picture")]
		public byte[] PictureByteArray
		{
			get 
			{ 
				if (picture != null)
				{
					TypeConverter BitmapConverter = TypeDescriptor.GetConverter(picture.GetType());
					return (byte[])	BitmapConverter.ConvertTo(picture, typeof(byte[]));					
				}
				else
					return null;
			}
			
			set 
			{ 
				if (value != null)
					picture = new Bitmap(new MemoryStream(value)); 
				else
					picture = null; 
			}
		}

		// Serializes an ArrayList as a "Hobbies" array of XML elements of type string named "Hobby".
		[XmlArray ("Hobbies"), XmlArrayItem("Hobby", typeof(string))]
		public System.Collections.ArrayList Hobbies = new System.Collections.ArrayList();

		// Serializes an ArrayList as a "EmailAddresses" array of XML elements of custom type EmailAddress named "EmailAddress".
		[XmlArray ("EmailAddresses"), XmlArrayItem("EmailAddress", typeof(EmailAddress))]
		public System.Collections.ArrayList EmailAddresses = new System.Collections.ArrayList();
	}
}
