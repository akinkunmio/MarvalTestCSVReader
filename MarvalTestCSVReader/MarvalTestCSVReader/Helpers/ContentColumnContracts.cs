using MarvalTestCSVReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Helpers
{
    public static class ContentColumnContracts
    {
        public static ColumnContract[] Person()
        {
            return new[]
           {
                new ColumnContract{ ColumnName="Identity", DataType="integer", Max=9, Required = true, ValidateCell = true },
                new ColumnContract{ ColumnName="FirstName", DataType="string", Max=256, Required = true, ValidateCell = true },
                new ColumnContract{ ColumnName="Surname", DataType="string", Max=256, Required=true, ValidateCell = true },
                new ColumnContract{ ColumnName="Age", DataType="integer", Max=3, Required=false, ValidateCell = true },
                new ColumnContract{ ColumnName="Sex", DataType="string", Max=1, Required=false, ValidateCell = true },
                new ColumnContract{ ColumnName="Mobile", DataType="decimal", Max=15, Required=false, ValidateCell = true },
                new ColumnContract{ ColumnName="Active", DataType="bool", Max=100, Required=false, ValidateCell = true },
            };
        }

    }
}

