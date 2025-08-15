using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cheems
{
    public static class FileIOUtility
    {
        /// <summary>
        /// 去除文件中不合法的字符
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string SanitizeFileName(string fileName)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            StringBuilder sanitizedPath = new StringBuilder(fileName.Length);

            foreach (char c in fileName)
            {
                if (Array.Exists(invalidFileNameChars, invalidChar => invalidChar == c))
                {
                    sanitizedPath.Append('_'); // 用短横线替换
                }
                else
                {
                    sanitizedPath.Append(c);
                }
            }

            return sanitizedPath.ToString();
        }


        #region CSV Handle //来自CKCell

        public static void WriteCSV(string filePathName, List<String[]> ls)
        {
            WriteCSV(filePathName, false, ls);
        }

        //write a file, existed file will be overwritten if append = false
        public static void WriteCSV(string filePathName, bool append, List<String[]> ls)
        {
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
            foreach (String[] strArr in ls)
            {
                fileWriter.WriteLine(String.Join(",", strArr));
            }

            fileWriter.Flush();
            fileWriter.Close();
        }

        public static void WriteCSV(string filePathName, List<List<string>> ls)
        {
            WriteCSV(filePathName, false, ls);
        }

        //write a file, existed file will be overwritten if append = false
        public static void WriteCSV(string filePathName, bool append, List<List<string>> ls)
        {
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.UTF8);
            foreach (List<string> strArr in ls)
            {
                fileWriter.WriteLine(String.Join(",", strArr.ToArray()));
            }

            fileWriter.Flush();
            fileWriter.Close();
        }

        /// <summary>
        /// 解析并返回CSV文件的内容，每一行内容是一行list
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<List<string>> CSVDecode(string text)
        {
            if (text == null)
                return null;

            List<List<string>> result = new List<List<string>>();
            List<string> line = new List<string>();
            StringBuilder fieldBuilder = new StringBuilder();
            bool isInQuotation = false;
            bool isInField = true;
            int i = 0;
            while (i < text.Length)
            {
                char ch = text[i];
                if (isInQuotation)
                {
                    if (ch == '"')
                    {
                        if (i < text.Length - 1 && text[i + 1] == '"')
                        {
                            fieldBuilder.Append('"');
                            i++;
                        }
                        else
                        {
                            isInQuotation = false;
                        }
                    }
                    else
                    {
                        fieldBuilder.Append(ch);
                    }
                }
                else
                {
                    switch (ch)
                    {
                        case ',':
                            line.Add(fieldBuilder.ToString());
                            fieldBuilder.Clear();
                            isInField = true;
                            break;
                        case '"':
                            if (isInField)
                                isInQuotation = true;
                            else
                                fieldBuilder.Append(ch);
                            break;
                        case '\n':
                        case '\r':
                        {
                            if (fieldBuilder.Length > 0 || isInField)
                            {
                                line.Add(fieldBuilder.ToString());
                                fieldBuilder.Clear();
                            }

                            if (line.Count > 0)
                            {
                                result.Add(new List<string>(line));
                                line.Clear();
                            }

                            isInField = true;
                            if (i < text.Length - 1 && text[i + 1] == '\n')
                                i++;


                            break;
                        }
                        default:
                            isInField = false;
                            fieldBuilder.Append(ch);
                            break;
                    }
                }

                i++;
            }

            if (fieldBuilder.Length > 0 || (isInField && line.Count > 0))
                line.Add(fieldBuilder.ToString());

            if (line.Count > 0)
                result.Add(line);

            return result;
        }

        #endregion
    }
}