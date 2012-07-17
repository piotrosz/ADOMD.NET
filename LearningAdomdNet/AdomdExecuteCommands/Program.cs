using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient;
using AdomdUtilities;
using System.Data;

namespace AdomdExecuteCommands
{
	class Program
	{
		static string connectionString = "Data Source=localhost;Catalog=Analysis Services Tutorial";

		static void Main()
		{
			// Import MDX query from file
			var query = new QueryImporter("../../../../LearningMDX.mdx");

			string mdxQuery = query.FromFile("Query1");

			using (AdomdConnection conn = new AdomdConnection(connectionString))
			{
				conn.Open();

				AdomdCommand command = conn.CreateCommand();

				command.CommandText = mdxQuery;

				CellSet cellSet = command.ExecuteCellSet();

                //CellSetConverter cellSetConverter = new CellSetConverter(cellSet);

                //DataTable dataTable = cellSetConverter.ToDataTable();
			}
		}
	}
}
