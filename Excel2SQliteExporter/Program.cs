using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Excel2SQliteExporter
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
			if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExporterConfig.xml")))
			{
				ExporterConfig exporterConfig = new ExporterConfig();
                exporterConfig.SqlitePath = @"H:\proyectos\TTT2SkillHelper\assets\ttt2data.db3";
				exporterConfig.ExcelPath = @"H:\Dropbox\framedata ttt2.xlsx";
                exporterConfig.SqlitePathT7 = @"E:\proyectos\SkillHelperforTekken7\app\src\main\assets\t7data.db3";
                exporterConfig.ExcelPathT7 = @"H:\Dropbox\framedata t7.xlsx";

				XmlSerializer mySerializer = new XmlSerializer(typeof(ExporterConfig));
				// To write to a file, create a StreamWriter object.
				StreamWriter myWriter = new StreamWriter("ExporterConfig.xml");
				mySerializer.Serialize(myWriter, exporterConfig);
				myWriter.Close();
			}

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }
  }
}
