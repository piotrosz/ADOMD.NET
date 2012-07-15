using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AnalysisServices.AdomdClient;
using System.ComponentModel;
using System.Reflection;
using System.IO;

namespace LearningAdomdNet
{
    class Program 
    {
        // Reding some cube properties

        static void Main()
        {
            using (TextWriter tw = File.CreateText("output.txt"))
            {
                using (AdomdConnection adomdConnection = new AdomdConnection("Data Source=localhost;Catalog=Analysis Services Tutorial"))
                {
                    adomdConnection.Open();

                    tw.WriteLine(ConnectionInfo(adomdConnection));

                    tw.WriteLine("#region Cubes");

                    foreach (CubeDef cube in adomdConnection.Cubes)
                    {
                        if (cube.IsHidden())
                            continue;

                        tw.WriteLine("#region Cube {0}", cube.Name);
                        tw.WriteLine(CubeInfo(cube));

                        tw.WriteLine("#region Measures");
                        foreach (Measure measure in cube.Measures)
                        {
                            tw.WriteLine("#region Measure {0}", measure.Name);
                            tw.WriteLine(MeasureInfo(measure));
                            tw.WriteLine("#endregion");
                        }
                        tw.WriteLine("#endregion");

                        tw.WriteLine("#region Properties");
                        foreach (Property prop in cube.Properties)
                        {
                            tw.WriteLine(PropertyInfo(prop));
                        }
                        tw.WriteLine("#endregion //Properties");

                        tw.WriteLine("#region Dimensions");
                        foreach (Dimension dim in cube.Dimensions)
                        {
                            tw.WriteLine("#region Dimension {0}", dim.Name);
                            tw.WriteLine(DimensionInfo(dim));

                            tw.WriteLine("#region Hierarchies");
                            foreach (Hierarchy hier in dim.AttributeHierarchies)
                            {
                                tw.WriteLine("#region Hierarchy {0}", hier.Name);
                                tw.WriteLine(HierarchyInfo(hier));

                                tw.WriteLine("#region Levels");
                                foreach (Level level in hier.Levels)
                                {
                                    tw.WriteLine(LevelInfo(level));
                                }
                                tw.WriteLine("#endregion");

                                tw.WriteLine("#endregion");
                            }
                            tw.WriteLine("#endregion // Hierarchies");

                            tw.WriteLine("#endregion // Dimension");
                           
                        }
                        tw.WriteLine("#endregion // Dimensions");


                        tw.WriteLine("#endregion // Cube");
                    }

                    tw.WriteLine("#endregion");
                }

                tw.WriteLine("Koniec");
            }
        }

        static string ConnectionInfo(AdomdConnection conn)
        {
            return new StringBuilder()
            .AppendLine("#region Connection")
            .AppendProp("ClientVersion", conn.ClientVersion)
            .AppendProp("ConnectionString", conn.ConnectionString)
            .AppendProp("ConnectionTimeout", conn.ConnectionTimeout)
            .AppendProp("Database", conn.Database)
            .AppendProp("ProviderVersion", conn.ProviderVersion)
            .AppendProp("ServerVersion", conn.ServerVersion)
            .AppendProp("SessionID", conn.SessionID)
            .AppendProp("ShowHiddenObjects", conn.ShowHiddenObjects)
            .AppendProp("State", conn.State)
            .AppendLine("#endregion")
            .ToString();
        }

        static string CubeInfo(CubeDef cube)
        {
            return new StringBuilder()
            .AppendProp("Is hidden", cube.IsHidden())
            .AppendProp("Caption", cube.Caption)
            .AppendProp("Description", cube.Description)
            .AppendProp("LastProcessed", cube.LastProcessed)
            .AppendProp("LastUpdated", cube.LastUpdated)
            .AppendProp("Name", cube.Name)
            .AppendProp("Type", cube.Type)
            .ToString();
        }

        static string DimensionInfo(Dimension dim)
        {
            return new StringBuilder()
            .AppendProp("Caption", dim.Caption)
            .AppendProp("Name", dim.Name)
            .AppendProp("Description", dim.Description)
            .AppendProp("DimensionType", dim.DimensionType)
            .AppendProp("UniqueName", dim.UniqueName)
            .AppendProp("WriteEnabled", dim.WriteEnabled)
            .ToString();
        }

        static string HierarchyInfo(Hierarchy hier)
        {
            return new StringBuilder()
            .AppendProp(" Caption", hier.Caption)
            .AppendProp(" DefaultMember", hier.DefaultMember)
            .AppendProp(" Description", hier.Description)
            .AppendProp(" DisplayFolder", hier.DisplayFolder)
            .AppendProp(" Name", hier.Name)
            .AppendProp(" UniqueName", hier.UniqueName)
            .ToString();
        }

        static string LevelInfo(Level level)
        {
            return new StringBuilder()
            .AppendProp("Name", level.Name)
            .AppendProp("LevelNumber", level.LevelNumber)
            .AppendProp("LevelType",level.LevelType)
            .AppendProp("MemberCount", level.MemberCount)
            .ToString();
        }

        static string MeasureInfo(Measure measure)
        {
            return new StringBuilder()
            .AppendProp("Expression", measure.Expression)
            .AppendProp("Units", measure.Units)
            .AppendProp("NumericPrecision", measure.NumericPrecision)
            .AppendProp("NumericScale", measure.NumericScale)
            .ToString();
        }

        static string PropertyInfo(Property prop)
        {
            return new StringBuilder()
            .AppendProp("Name", prop.Name)
            .AppendProp("Value", prop.Value)
            .AppendProp("Type", prop.Type)
            .ToString();
        }

        static void Print(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }

    public static class StringBuilderExt
    {
        public static StringBuilder AppendProp(this StringBuilder sb, string propName, object propValue)
        {
            return sb.AppendFormat("{0} = {1}\r\n", propName, propValue);
        }
    }

    public static class ADOMDNETExt
    {
        public static bool IsHidden(this CubeDef cube)
        {
            return cube.Name.StartsWith("$");
        }
    }
}
