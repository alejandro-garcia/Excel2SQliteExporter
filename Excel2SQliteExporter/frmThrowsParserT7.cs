using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Excel2SQliteExporter
{
	public partial class frmThrowsParserT7 : Form
	{
		private ExporterConfig config = null;
		SQLiteConnection conn;
		List<clsGrabs> grabList;

        public frmThrowsParserT7()
		{
			XmlSerializer mySerializer = new XmlSerializer(typeof(ExporterConfig));
			FileStream stream = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExporterConfig.xml"), FileMode.Open, FileAccess.Read, FileShare.Read);

			// Load the object saved above by using the Deserialize function
			config = (ExporterConfig)mySerializer.Deserialize(stream);

			// Cleanup
			stream.Close();

			//open bd conn
            conn = new SQLiteConnection(string.Format("Data Source = {0}", config.SqlitePathT7));
			conn.Open();

			InitializeComponent();
		}

		private void btnClean_Click(object sender, EventArgs e)
		{
			this.txtGrabs.Text = String.Empty;
			this.dataGridView1.DataSource = null;
		}

		private void btnProcess_Click(object sender, EventArgs e)
		{
			//obtener el id del peleador
			if (String.IsNullOrEmpty(this.txtFighter.Text))
				return;

			long fighterId = getFighterId();

			String[] grabsList = this.txtGrabs.Text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			string tabSpace = "    ";

			grabList = new List<clsGrabs>();


			String note;

			//Regex regex = new Regex(@"[ ]"

			for (int x = 0; x < grabsList.Length; x++)
			{
				string grabsRow = grabsList[x];


				//get note column
				Match match = Regex.Match(grabsRow, @"^\(\w*\)");
				if (match.Success)
				{
					note = match.Groups[0].ToString();
				}
				else
				{
					match = Regex.Match(grabsRow, @"^\*\w*.+\*");
					if (match.Success)
						note = match.Groups[0].ToString();
					else
						note = "(Front)";
				}

				grabsRow = grabsRow.Replace(note, "").TrimStart();

				//get input column
				match = Regex.Match(grabsRow,@"^.*\s\s");
				string input = string.Empty;
				if (match.Success)
					input = match.Groups[0].ToString().TrimEnd();
				
				if (!string.IsNullOrEmpty(input))
				  grabsRow = grabsRow.Replace(input, "").TrimStart();

				//get name column
				string remark = string.Empty;				
				string name = grabsRow.Replace("{1}", "").Replace("{2}", "").Replace("{1+2}", "").Replace("{1_2}","").Replace("[Tag]", "").TrimEnd();

				if (!string.IsNullOrEmpty(name))
					grabsRow = grabsRow.Replace(name, "").TrimStart();

				//remark //[Tag]
                //remark = string.Empty;
                //if (grabsRow.Contains("[Tag]"))
                //{
                //    remark = "[Tag]";
                //    grabsRow = grabsRow.Replace(remark, "").TrimStart();
                //}


				string avoid = string.Empty;
				if (grabsRow.Trim() != string.Empty)
					avoid = grabsRow.Trim();


				//match = Regex.Match(grabsRow,@"(?<o>\w)\k<o>*");
				//string name = string.Empty;
				//if (match.Success)
				//	name = match.Groups[0].ToString();
				
				clsGrabs grabsRowData = new clsGrabs();
				grabsRowData.FighterId = fighterId;
				grabsRowData.Notes = note;
				grabsRowData.Input = input;
				grabsRowData.Name = name;
				grabsRowData.Remark = remark;
				grabsRowData.Avoid = avoid;
				grabList.Add(grabsRowData);

				
				
				//String[] grabColumns = grabsRow.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
			}

			BindingSource source = new BindingSource();
			source.DataSource = grabList;
			this.dataGridView1.AutoGenerateColumns = true;
			this.dataGridView1.DataSource = source;
			//this.dataGridView1.Refresh();
		}

		private long getFighterId()
		{
			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;
			cmd.CommandText = "SELECT _id FROM characters WHERE lower(shortName) = '" + this.txtFighter.Text + "'";

			SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet);

			long result = dataSet.Tables[0].Rows[0].Field<long>("_id");
			return result;



		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				Match match = Regex.Match(this.textBox1.Text, this.txtRegex.Text);

				if (match.Success)
					this.txtRegexResult.Text = match.Groups[0].ToString();
				else
					this.txtRegexResult.Text = "not matcheds";
			}
			catch
			{
				this.txtRegexResult.Text = "error in regex";
			}
		}

		private void btnSaveToBD_Click(object sender, EventArgs e)
		{
			if (grabList == null || grabList.Count == 0)
				return;

			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;
			int insCount = 0;

			bool transErrors = false;
			SQLiteTransaction trans = conn.BeginTransaction();

			foreach (clsGrabs item in grabList)
			{
				string insSQL = "INSERT INTO grappling (fighterId,note,input,name,avoid,remark) VALUES (";
				insSQL += "@fighterId,@note,@input,@name,@avoid,@remark)";
				cmd.CommandText = insSQL;
				cmd.Parameters.AddWithValue("@fighterId", item.FighterId);
				cmd.Parameters.AddWithValue("@note", item.Notes);
				cmd.Parameters.AddWithValue("@input", item.Input);
				cmd.Parameters.AddWithValue("@name", item.Name);
				cmd.Parameters.AddWithValue("@avoid", item.Avoid);
				cmd.Parameters.AddWithValue("@remark", item.Remark);

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
			//conn.Close();
		}
	}
}
