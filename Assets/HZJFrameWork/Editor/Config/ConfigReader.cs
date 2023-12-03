//=====================================================
// - FileName:      ConfigReader.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/09/23 16:40:30
// - Description:   Excel配置表读取类
//======================================================
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Runtime.InteropServices;
using System.Data;
using UnityEditor;
using Newtonsoft.Json;
using System.Text;

namespace HZJFrameWork
{
    public class ConfigReader
    {
        private static string configPath = Application.dataPath + "/../Config/";
        private static string configJsonPath = Application.dataPath + "/Bundles/Config/";
        private static string configCodePath = Application.dataPath + "/Script/Config/";


        private static DataTable table = new DataTable();
        private static List<ExcelField> excelFields = new List<ExcelField>();

        public static void ReadAllConfig()
        {
            List<string> allConfigFiles = GetAllConfigFile();
            if (allConfigFiles == null)
            {
                return;
            }

            for (int i = 0; i < allConfigFiles.Count; i++)
            {
                string fileName = allConfigFiles[i];
                FileInfo fileInfo = new FileInfo(fileName);
                try
                {
                    using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheets worksheets = excelPackage.Workbook.Worksheets;
                        foreach (ExcelWorksheet worksheet in worksheets)
                        {
                            if (worksheet == null)
                            {
                                continue;
                            }


                            string[] excelNameInfo = worksheet.Name.Split("#");
                            
                            if (excelNameInfo == null || excelNameInfo.Length < 2)
                            {
                                continue;
                            }
                            string excelName = excelNameInfo[0];
                            string excelDesc = excelNameInfo[1];
                            ReadConfig(worksheet, excelName);
                        }

                    }
                }
                catch (IOException ex)
                {
                	//if (IsFileLocked(ex))
                	//{
                 //       HZJLog.LogWithRed($"[{fileName}]：另一个进程正在使用此文件 或者 文件已经被另一个进程打开");
                 //   }
                }

                
            }
        }


        private static bool IsFileLocked(IOException ex)
        {
            int errorCode = Marshal.GetHRForException(ex) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }

        private static List<string> GetAllConfigFile()
        {
            if (!Directory.Exists(configPath))
            {
                HZJLog.LogWithRed($"不存在{configPath}文件夹");
                return null;
            }

            string[] allFiles = Directory.GetFiles(configPath);
            List<string> configFiles = new List<string>();
            for (int i = 0; i < allFiles.Length; i++)
            {
                if (allFiles[i].EndsWith(".xlsx"))
                {
                    configFiles.Add(allFiles[i]);
                }
            }

            return configFiles;
        }

        private static void ReadConfig(ExcelWorksheet excelWorksheet,string excelName)
        {

            table.Clear();
            excelFields.Clear();
            for (int i = 4; i < excelWorksheet.Dimension.Rows; ++i)
            {
                DataRow newRow = table.NewRow();
                string rowInfo = string.Empty;
                int columnsNum = 0;
                for (int j = 1; j < excelWorksheet.Dimension.Columns; j++)
                {
                    string title = excelWorksheet.Cells[1, j].Value.ToString();
                    string type = excelWorksheet.Cells[2, j].Value.ToString();
                    string ColName = excelWorksheet.Cells[3, j].Value.ToString();
                    object value = excelWorksheet.Cells[i, j].Value.ToString();

                    if (string.IsNullOrEmpty(type))
                    {
                        continue;
                    }

                    if (!table.Columns.Contains(ColName))
                    {
                        table.Columns.Add(ColName, ConfigUtils.GetStringType(type));
                        excelFields.Add(new ExcelField(ColName, type, title));
                    }
                    value = ConfigUtils.GetObjectValue(type, (string)value);
                    newRow[columnsNum] = value;
                    columnsNum++;
                }
                table.Rows.Add(newRow);
                
            }
            CreateJsonFile(table, excelName);
            CreateConfigCodeFile(excelFields, excelName);
            AssetDatabase.Refresh();
        }

        private static void CreateConfigCodeFile(List<ExcelField> excelFields,string configName)
        {
            if (excelFields == null)
            {
                HZJLog.LogError($"表{configName}的字段为空！");
                return;
            }

            StringBuilder codeStr = new StringBuilder();
            codeStr.Append("using System.Collections;\r\nusing System.Collections.Generic;\r\n\r\n/// <summary>\r\n/// 工具生成，请不要修改\r\n/// </summary>\r\n public class ");
            codeStr.Append($"{configName}\r\n");
            codeStr.AppendLine("{");
            for (int i = 0; i < excelFields.Count; i++)
            {
                ExcelField filedInfo = excelFields[i];
                codeStr.Append($"    /// <summary>\r\n    /// {filedInfo.fieldDesc}\r\n    /// </summary>\n");
                codeStr.Append($"    public {ConfigUtils.GetCSStringType(filedInfo.fieldType)} {filedInfo.fieldName};\n");
            }
            codeStr.AppendLine("}");
            string saveStr = codeStr.ToString();
            string fileName = configName + "Config.cs";
            string savePath = configCodePath + fileName;

            if (!Directory.Exists(configCodePath))
            {
                Directory.CreateDirectory(configCodePath);
            }

            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            File.WriteAllText(savePath, saveStr);
            HZJLog.LogWithGreen($"表[{configName}]生成实体类文件成功！");
        }

        private static void CreateJsonFile(DataTable table, string configName)
        {
            string saveStr = JsonConvert.SerializeObject(table);
            string fileName = configName + "Config.bytes";
            string savePath = configJsonPath + fileName;
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            File.WriteAllText(savePath, saveStr);
            ToolsMenu.ReNameABPackage(configJsonPath);
            HZJLog.LogWithGreen($"表[{configName}]生成Json文件成功！");
        }

    }
}

