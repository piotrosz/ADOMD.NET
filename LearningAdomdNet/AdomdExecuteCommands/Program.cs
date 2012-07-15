using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient;

namespace AdomdExecuteCommands
{
	class Program
	{
		static string mdx1 = @"SELECT 
							   FROM [Analysis Services Tutorial]";

		static string mdx2 = @"SELECT
								{
									([Due Date].[Fiscal Date].[Fiscal Year].&[2007]),
									([Due Date].[Fiscal Date].[Fiscal Year].&[2008])
								} ON 0,
									{
										[Product].[Product Model Lines].[Product Line].Members
									}
								*
									{
										([Measures].[Reseller Sales Count]),
										([Measures].[Reseller Sales-Sales Amount])
									} 
								ON 1
								FROM [Analysis Services Tutorial]";

		static void Main()
		{
			using (AdomdConnection conn = new AdomdConnection("Data Source=localhost;Catalog=Analysis Services Tutorial"))
			{
				conn.Open();

				AdomdCommand command = conn.CreateCommand();

				command.CommandText = mdx2;

				CellSet cellSet = command.ExecuteCellSet();

				for (int i = 0; i < cellSet.Axes[1].Set.Tuples.Count; i++)
				{
					//Console.WriteLine(cellSet.Axes[1].Name);
                    //cellSet.Axes[1].Set.Tuples[i].Members;

					for (int j = 0; j < cellSet.Axes[0].Set.Tuples.Count; j++)
					{
						Console.Write(" {0} ", cellSet.Cells[j, i].Value);
					}
					Console.WriteLine();
				}

				//foreach (Axis axis in cellSet.Axes)
				//{
				//    Console.WriteLine("Axis: {0}", axis.Name);
				//    foreach (Position pos in axis.Positions)
				//    {
				//        Console.WriteLine("Oridinal: {0}", pos.Ordinal);
				//        foreach (Member mem in pos.Members)
				//        {
				//            Console.WriteLine("Member: {0}", mem.Name);
				//        }
				//    }

				//    foreach (Microsoft.AnalysisServices.AdomdClient.Tuple tuple in axis.Set.Tuples)
				//    {
				//        Console.WriteLine("Tuple oridinal: {0}", tuple.TupleOrdinal);

				//        foreach (Member mem in tuple.Members)
				//        {
				//            Console.WriteLine("Member: {0}", mem.Name);
				//        }
				//    }

				//    foreach (Hierarchy hier in axis.Set.Hierarchies)
				//    {
				//        Console.WriteLine("Hierarchy: {0}", hier.Name);

				//        foreach (Property prop in hier.Properties)
				//        {
				//            Console.WriteLine("Name: {0}, Value: {1}, Type: {2}", prop.Name, prop.Value, prop.Type);
				//        }
				//    }
				//}

				//AdomdDataReader reader = command.ExecuteReader();

				
			}
		}
	}
}
