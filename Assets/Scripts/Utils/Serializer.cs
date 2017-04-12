using System;
using System.Xml.Serialization;
using System.IO;

public class Serializer 
{
	public static T Deserialize<T>(string toDeserialize)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		StringReader textReader = new StringReader(toDeserialize);
		return (T)xmlSerializer.Deserialize(textReader);
	}

	public static string Serialize<T>(T toSerialize)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		StringWriter textWriter = new StringWriter();
		xmlSerializer.Serialize(textWriter, toSerialize);
		return textWriter.ToString();
	}
}