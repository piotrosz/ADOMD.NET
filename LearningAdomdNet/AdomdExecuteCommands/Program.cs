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

			string mdxQuery1 = query.FromFile("Query2");

			using (AdomdConnection conn = new AdomdConnection(connectionString))
			{
				conn.Open();

				using (AdomdCommand command = conn.CreateCommand())
				{
					command.CommandText = mdxQuery1;

					CellSet cellSet1 = command.ExecuteCellSet();
				}
			}
		}
	}
}
