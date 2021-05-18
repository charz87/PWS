using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Rounds")]
public class LevelManager
{
	[XmlArray("Level1Rounds"),XmlArrayItem("Round")]
	public List<Round> level_1_rounds = new List<Round>();
	
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(LevelManager));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static LevelManager Load(string path)
	{

		var serializer = new XmlSerializer(typeof(LevelManager));
		/*using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as LevelManager;
		}*/
		using(var reader = new System.IO.StringReader(path))
		{
			return serializer.Deserialize(reader) as LevelManager;
		}
	}
}
