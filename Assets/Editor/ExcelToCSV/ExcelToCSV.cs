using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEditor;
using UnityEngine;

public class ExcelToCSV {
    //EXcel文件路径
    private static string excelPath = Path.Combine(Application.dataPath, ".Excel");
    //CSV文件路径
    private static string csvPath = Path.Combine(Application.dataPath, "Bundles/CSV/");
    //排除的行数，从0开始
    private static int[] exclusiveRows = {1, 2, 3, 4};
    
    [MenuItem("Tools/ExcelToCSV")]
    public static void Convert() {
        //删除csv文件夹下面的所有文件
        Directory.Delete(csvPath, true);
        DirectoryInfo root = new DirectoryInfo(excelPath);
        FileInfo[] fileDic = root.GetFiles();
        foreach (var file in fileDic)
        {
            //查找xlsx，并且开头不是~(打开的文件)
            if (file.FullName.EndsWith("xlsx") && !file.Name.StartsWith("~") && !file.Name.StartsWith("_"))
            {
                FileStream stream = file.Open(FileMode.Open, FileAccess.Read);
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelDataReader.AsDataSet();
                foreach (DataTable one in result.Tables)
                {
                    ReadOneTable(one);
                }
            }
        }
    }

    private static void ReadOneTable(DataTable dataTable) {
        //排除以下划线开头的子表
        if (dataTable.TableName.StartsWith("_"))
        {
            return;
        }

        string fullPath = csvPath + dataTable.TableName + ".csv";
        FileInfo fi = new FileInfo(fullPath);
        if (fi.Directory != null && !fi.Directory.Exists)
        {
            fi.Directory.Create();
        }
        FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

        //写入各行数据
        for (int i = 0; i < dataTable.Rows.Count; i++) {
            bool isContinue = true;
            foreach (var t in exclusiveRows) {
                if (t == i) {
                    isContinue = false;
                    break;
                }
            }
            if (!isContinue) {
                continue;
            }
            
            string data = "";
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                string str = dataTable.Rows[i][j].ToString();
                str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                if (str.Contains(",") || str.Contains("\"")
                                      || str.Contains("\r") || str.Contains("\n")) //含逗号 冒号 换行符的需要放到引号中
                {
                    str = string.Format("\"{0}\"", str);
                }

                data += str;
                if (j < dataTable.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
        }
        sw.Close();
        fs.Close();
        AssetDatabase.Refresh();
    }
}
