using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbL.DatabaseHandler
{
    public static class DataBaseManager
    {


    }
}


/*
 * private static string FormatSize(long bytes)
{
    string[] sizes = { "B", "KB", "MB", "GB", "TB" };
    double len = bytes;
    int order = 0;
    while (len >= 1024 && order < sizes.Length - 1)
    {
        order++;
        len /= 1024;
    }
    return $"{len:0.##} {sizes[order]}";
}

 */