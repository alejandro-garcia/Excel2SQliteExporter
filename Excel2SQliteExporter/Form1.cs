using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Excel2SQliteExporter
{
  public partial class Form1 : Form
  {

	//const string SQLITE_PATH = @"E:\alejandro\Android\ws\TTT2SkillHelper\assets\ttt2data.db3";
	//const string EXCEL_PATH = @"E:\Dropbox\framedata ttt2.xlsx";
		private ExporterConfig config = null;

	public Form1()
	{
			XmlSerializer mySerializer = new XmlSerializer(typeof(ExporterConfig));
			FileStream stream = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExporterConfig.xml"), FileMode.Open, FileAccess.Read, FileShare.Read);

			// Load the object saved above by using the Deserialize function
			config = (ExporterConfig)mySerializer.Deserialize(stream);

			// Cleanup
			stream.Close();

	  InitializeComponent();
	}

	private DataTable GetExcelTable(string game)
	{

	  string connectionString = string.Empty;
	  if (game == "ttt2")
		  connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=Excel 12.0;", config.ExcelPath);
	  else
		  connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=Excel 12.0;", config.ExcelPathT7);

	  OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", this.textBox1.Text), connectionString);
	  DataSet ds = new DataSet();

	  try
	  {
		adapter.Fill(ds);
	  }
	  catch (System.Exception)
	  {
		MessageBox.Show("Error abriendo la tabla de excel");
		adapter.Dispose();
		return null;
	  }

	  DataTable table = ds.Tables[0];

	  if (table.Rows.Count == 0)
	  {
		table.Dispose();
		ds.Dispose();
		adapter.Dispose();
		return null;
	  }

	  return table;
	}

	private void FillGrabs()
	{
	  DataTable table = this.GetExcelTable("ttt2");

	  SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source = {0}", config.SqlitePath));
	  conn.Open();

	  SQLiteCommand cmd = new SQLiteCommand();
	  cmd.Connection = conn;
	  int insCount = 0;

	  bool transErrors = false;
	  SQLiteTransaction trans = conn.BeginTransaction();

	  foreach (DataRow row in table.Rows)
	  {
		if (string.IsNullOrEmpty(row["fighterId"].ToString()))
		  continue;

		string insSQL = "INSERT INTO grappling (fighterId,note,input, name, avoid, remark) ";
		insSQL += "values (@fighterId,@note,@input,@name,@avoid,@remark)";

		cmd.CommandText = insSQL;
		cmd.Parameters.AddWithValue("@fighterId", row["fighterId"].ToString());
		cmd.Parameters.AddWithValue("@note", row["note"].ToString());
		cmd.Parameters.AddWithValue("@input", row["input"].ToString());
		cmd.Parameters.AddWithValue("@name", row["name"].ToString());
		cmd.Parameters.AddWithValue("@avoid", row["avoid"].ToString());
		cmd.Parameters.AddWithValue("@remark", row["remark"].ToString());

		try
		{
		  insCount += cmd.ExecuteNonQuery();
		}
		catch (System.Exception)
		{
		  MessageBox.Show("error insertando en SQLite");
		  transErrors = true;
		  trans.Rollback();
		  break;
		}

	  }

	  if (!transErrors)
		trans.Commit();

	  MessageBox.Show(string.Format("{0} registros agregados", insCount));

	  trans.Dispose();
	  conn.Close();
	  table.Dispose();
	}

	private void FillTagAssault()
	{
		DataTable table = this.GetExcelTable("ttt2");

		SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source = {0}", config.SqlitePath));
		conn.Open();

		SQLiteCommand cmd = new SQLiteCommand();
		cmd.Connection = conn;
		int insCount = 0;

		bool transErrors = false;
		SQLiteTransaction trans = conn.BeginTransaction();

		foreach (DataRow row in table.Rows)
		{
			if (string.IsNullOrEmpty(row["fighterId"].ToString()))
				continue;

			string insSQL = "INSERT INTO tafillers (fighterId,input, isForWall) ";
			insSQL += "values (@fighterId,@input,@isforwall)";

			cmd.CommandText = insSQL;
			cmd.Parameters.AddWithValue("@fighterId", row["fighterId"].ToString());
			cmd.Parameters.AddWithValue("@input", row["input"].ToString());
			cmd.Parameters.AddWithValue("@isforwall", row["isForWall"].ToString());

			try
			{
				insCount += cmd.ExecuteNonQuery();
			}
			catch (System.Exception)
			{
				MessageBox.Show("error insertando en SQLite");
				transErrors = true;
				trans.Rollback();
				break;
			}

		}

		if (!transErrors)
			trans.Commit();

		MessageBox.Show(string.Format("{0} registros agregados", insCount));

		trans.Dispose();
		conn.Close();
		table.Dispose();
	}

	private void button1_Click(object sender, EventArgs e)
	{
	  if (this.textBox1.Text == "agarres")
	  {
		this.FillGrabs();
		return;
	  }

	  if (this.textBox1.Text == "TAG")
	  {
		  this.FillTagAssault();
		  return;
	  }

	  DataTable table = this.GetExcelTable("ttt2");
			if (table == null)
			{
				MessageBox.Show("Error abriendo la tabla de excel");
				return;
			}

	  SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source = {0}", config.SqlitePath));
	  conn.Open();

	  SQLiteCommand cmd = new SQLiteCommand();
	  cmd.Connection = conn;
	  int insCount = 0;

	  bool transErrors = false;
	  SQLiteTransaction trans = conn.BeginTransaction();

	  foreach (DataRow row in table.Rows)
	  {
		if (string.IsNullOrEmpty(row["fighterId"].ToString()))
		  continue;

		string insSQL = "INSERT INTO moves (fighterId, input, hitbox, damage, speed, hit, block, counter, ";
		insSQL += "notes, isHoming, isBound, isJuggleStarter, isTagBuffereable, isPunisher, isWallSplatter, isHighCrush, isLowCrush, isNc, isNcc) ";
		insSQL += "values (@fighterId,@input,@hitbox,@damage,@speed,@hit,@block,@counter,@notes,@isHoming,";
		insSQL += "@isBound,@isJuggleStarter,@isTagBuffereable,@isPunisher,@isWallSplatter,@isHighCrush,@isLowCrush,@isNc,@isNcc)";

		cmd.CommandText = insSQL;
		cmd.Parameters.AddWithValue("@fighterId", row["fighterId"].ToString());
		cmd.Parameters.AddWithValue("@input", row["input"].ToString());
		cmd.Parameters.AddWithValue("@hitbox", row["hitbox"].ToString());
		cmd.Parameters.AddWithValue("@damage",row["damage"].ToString());
		cmd.Parameters.AddWithValue("@speed",row["speed"].ToString());
		cmd.Parameters.AddWithValue("@hit",row["hit"].ToString());
		cmd.Parameters.AddWithValue("@block",row["block"].ToString());
		cmd.Parameters.AddWithValue("@counter",row["counter"].ToString());
		cmd.Parameters.AddWithValue("@notes",row["notes"].ToString());
		cmd.Parameters.AddWithValue("@isHoming",row["isHoming"].ToString());
		cmd.Parameters.AddWithValue("@isBound",row["isBound"].ToString());
		cmd.Parameters.AddWithValue("@isJuggleStarter",row["isJuggleStarter"].ToString());
		cmd.Parameters.AddWithValue("@isTagBuffereable",row["isTagBuffereable"].ToString());
		cmd.Parameters.AddWithValue("@isPunisher",row["isPunisher"].ToString());
		cmd.Parameters.AddWithValue("@isWallSplatter",row["isWallSplatter"].ToString());
		cmd.Parameters.AddWithValue("@isHighCrush", row["isHighCrush"].ToString());
		cmd.Parameters.AddWithValue("@isLowCrush", row["isLowCrush"].ToString());
		cmd.Parameters.AddWithValue("@isNc", row["isNc"].ToString());
		cmd.Parameters.AddWithValue("@isNcc", row["isNcc"].ToString());

		try
		{
			insCount += cmd.ExecuteNonQuery();
		}
		catch (System.Exception)
		{
		  MessageBox.Show("error insertando en SQLite");
		  transErrors = true;
		  trans.Rollback();
		  break;
		}

	  }

	  if (!transErrors)
		trans.Commit();

	  MessageBox.Show(string.Format("{0} registros agregados", insCount));

	  trans.Dispose();
	  conn.Close();
	  table.Dispose();
	}

		private void button2_Click(object sender, EventArgs e)
		{
			frmThrowsParser form2 = new frmThrowsParser();
			form2.ShowDialog();
		}

		private string parseFieldValue(object fieldValue)
		{
			string result = "0";

			if (!string.IsNullOrEmpty((fieldValue ?? "").ToString()))
				result = fieldValue.ToString();

			return result;
		}


		private void button3_Click(object sender, EventArgs e)
		{
			if (this.textBox1.Text == "agarres")
			{
				this.FillGrabs();
				return;
			}			

			DataTable table = this.GetExcelTable("t7");
			if (table == null)
			{
				MessageBox.Show("Error abriendo la tabla de excel");
				return;
			}

			SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source = {0}", config.SqlitePathT7));
			conn.Open();

			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;
			int insCount = 0;

			bool transErrors = false;
			SQLiteTransaction trans = conn.BeginTransaction();

			foreach (DataRow row in table.Rows)
			{
				if (string.IsNullOrEmpty(row["fighterId"].ToString()))
					continue;

				string insSQL = "INSERT INTO moves (fighterId, input, hitbox, damage, speed, hit, block, counter, ";
				insSQL += "notes, isHoming, isTailSpin, isJuggleStarter, isPowerCrush, isPunisher, isWallSplatter, isHighCrush, isLowCrush, isNc, isNcc, isRageArt) ";
				insSQL += "values (@fighterId,@input,@hitbox,@damage,@speed,@hit,@block,@counter,@notes,@isHoming,";
				insSQL += "@isTailSpin,@isJuggleStarter,@isPowerCrush,@isPunisher,@isWallSplatter,@isHighCrush,@isLowCrush,@isNc,@isNcc, @isRageArt)";

				cmd.CommandText = insSQL;

				string tailSpinValue = "0";
				string homingValue = "0";
				string powerCrushValue = "0";
				string rageArtValue = "0";
				string launcherValue = "0";

				string notesFld = row["notes"].ToString().ToLower();

				if (notesFld.Contains("tail spin"))
					tailSpinValue = "1";
				
				if (notesFld.Contains("power crush"))
					powerCrushValue = "1";

				if (notesFld.Contains("homing"))
					homingValue = "1";

				if (notesFld.Contains("rage art"))
					rageArtValue = "1";

				if (row["hit"].ToString().ToLower().Contains("jg"))
					launcherValue = "1";              

				cmd.Parameters.AddWithValue("@fighterId", row["fighterId"].ToString());
				cmd.Parameters.AddWithValue("@input", row["input"].ToString());
				cmd.Parameters.AddWithValue("@hitbox", row["hitbox"].ToString());
				cmd.Parameters.AddWithValue("@damage", row["damage"].ToString());
				cmd.Parameters.AddWithValue("@speed", row["speed"].ToString());
				cmd.Parameters.AddWithValue("@hit", row["hit"].ToString());
				cmd.Parameters.AddWithValue("@block", row["block"].ToString());
				cmd.Parameters.AddWithValue("@counter", row["counter"].ToString());
				cmd.Parameters.AddWithValue("@notes", row["notes"].ToString());
				cmd.Parameters.AddWithValue("@isHoming", homingValue);
				cmd.Parameters.AddWithValue("@isTailSpin", tailSpinValue);  //row["isTailSpin"].ToString());
				cmd.Parameters.AddWithValue("@isJuggleStarter", launcherValue);
				cmd.Parameters.AddWithValue("@isPowerCrush", powerCrushValue);
				cmd.Parameters.AddWithValue("@isPunisher", parseFieldValue(row["isPunisher"]));
				cmd.Parameters.AddWithValue("@isWallSplatter", parseFieldValue(row["isWallSplatter"]));
				cmd.Parameters.AddWithValue("@isHighCrush", parseFieldValue(row["isHighCrush"]));
				cmd.Parameters.AddWithValue("@isLowCrush", parseFieldValue(row["isLowCrush"]));
				cmd.Parameters.AddWithValue("@isNc", parseFieldValue(row["isNc"]));
				cmd.Parameters.AddWithValue("@isNcc", parseFieldValue(row["isNcc"]));
				cmd.Parameters.AddWithValue("@isRageArt", rageArtValue);

				try
				{
					insCount += cmd.ExecuteNonQuery();
				}
				catch (System.Exception)
				{
					MessageBox.Show("error insertando en SQLite");
					transErrors = true;
					trans.Rollback();
					break;
				}

			}

			if (!transErrors)
				trans.Commit();

			MessageBox.Show(string.Format("{0} registros agregados", insCount));

			trans.Dispose();
			conn.Close();
			table.Dispose();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			frmThrowsParserT7 form2 = new frmThrowsParserT7();
			form2.ShowDialog();
		}
  }
}
