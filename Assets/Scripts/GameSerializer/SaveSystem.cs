using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("GameStats")]
public class SaveSystem
{
	[XmlArray("World1"),XmlArrayItem("Level")]
	public List<Level> world1Levels = new List<Level>();
	[XmlArray("World2"),XmlArrayItem("Level")]
	public List<Level> world2Levels = new List<Level>();
	[XmlArrayItem("Stats")]
	public Stats generalStats = new Stats();
}
