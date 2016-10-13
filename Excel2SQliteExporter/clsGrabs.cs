using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excel2SQliteExporter
{
	public class clsGrabs
	{
		long fighterId;
		string notes;
		string input;
		string name;
		string avoid;
		string remark;

		public clsGrabs()
		{
		}

		public clsGrabs(long _fighterId, string _notes, string _input, string _name, string _avoid, string _remark)
		{
			this.fighterId = _fighterId;
			this.notes = _notes;
			this.input = _input;
			this.name = _name;
			this.avoid = _avoid;
			this.remark = _remark;
		}

		public long FighterId
		{
			get { return fighterId; }
			set { fighterId = value; }
		}

		public string Notes
		{
			get { return notes; }
			set { notes = value; }
		}

		public string Input
		{
			get { return input; }
			set { input = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		public string Avoid
		{
			get { return avoid; }
			set { avoid = value; }
		}

		public string Remark
		{
			get { return remark; }
			set { remark = value; }
		}
	}
}
