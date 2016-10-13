using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Excel2SQliteExporter
{

	[Serializable]
	[XmlRoot("Exporter-Config")]
	public class ExporterConfig
	{
		[XmlElement("SQLite-Path")]
		public string SqlitePath
		{
			get;
			set;
		}

		[XmlElement("Excel-Path")]
		public string ExcelPath
		{
			get;
			set;
		}

		[XmlElement("SQLite-Path-T7")]
		public string SqlitePathT7
		{
			get;
			set;
		}

		[XmlElement("Excel-Path-T7")]
		public string ExcelPathT7
		{
			get;
			set;
		}
	}
}
