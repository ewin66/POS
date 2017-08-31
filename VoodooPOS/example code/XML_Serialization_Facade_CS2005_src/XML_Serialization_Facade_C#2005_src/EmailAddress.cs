using System;
using System.Xml.Serialization;	 //For serialization of an object to an XML Document file.

namespace disc1
{
	/// <summary>
	/// Type of destinations an e-mail address relates to.
	/// </summary>
	public enum EmailDestination
	{
		Home     = 0,
		Business = 1,
		Other    = 2
	}

	/// <summary>
	/// Custom class used to store an E-mail address.
	/// </summary>
	
	// Mark class as serializable.
	[Serializable]
	public class EmailAddress
	{
		/// <summary>
		/// Default constructor for this class (required for serialization).
		/// </summary>
		public EmailAddress()
		{
		}

		// Specify that this field should be serialized as an XML attribute 
		// instead of an element to demonstrate the formatting differences in an XML file. 
		[XmlAttribute]
		public string Address = null;

		// Specify that this field should be serialized as an XML attribute 
		// instead of an element to demonstrate the formatting differences in an XML file. 
		[XmlAttribute]
		public EmailDestination Destination = EmailDestination.Home;
	}
}
