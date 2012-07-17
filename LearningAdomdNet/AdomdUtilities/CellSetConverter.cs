using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace AdomdUtilities
{
    public class CellSetConverter
    {
        public CellSetConverter(CellSet cellSet)
        {
            this.CellSet = cellSet;
        }

        public CellSet CellSet { get; set; }

        public DataTable ToDataTable()
        {
            throw new NotImplementedException();
        }


    }
}
