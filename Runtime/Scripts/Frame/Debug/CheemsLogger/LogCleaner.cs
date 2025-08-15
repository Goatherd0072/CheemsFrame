using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace Cheems.Debug
{
    public class LogCleaner
    {
        private const string LAST_RUN_KEY     = "LastRunTime";
        private const uint   MAX_INTERVAL_DAY = 7; // 清理时间

        public void StartCleaner(string filePath)
        {
            CleanOldLogs(filePath);
            PlayerPrefs.SetString(LAST_RUN_KEY, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture));
            PlayerPrefs.Save();
        }

        private void CleanOldLogs(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                CLog.Warning("日志目录不存在，无法清理日志文件。");
                return;
            }

            string lastRunTimeStr = PlayerPrefs.GetString(LAST_RUN_KEY, string.Empty);

            if (!string.IsNullOrEmpty(lastRunTimeStr) && DateTime.TryParse(lastRunTimeStr, out var lastRunTime))
            {
                if ((DateTime.Now - lastRunTime).TotalDays > MAX_INTERVAL_DAY)
                {
                    DeleteLogFiles(filePath);
                }
            }
            else
            {
                DateTime oldestFileTime = GetOldestFileCreationTime(filePath);
                if (oldestFileTime != DateTime.MinValue && (DateTime.Now - oldestFileTime).TotalDays > 7)
                {
                    DeleteLogFiles(filePath);
                }
            }
        }

        /// <summary>
        /// 获取日志文件夹下最早的文件创建时间
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private DateTime GetOldestFileCreationTime(string filePath)
        {
            var logFiles = Directory.GetFiles(filePath);
            DateTime oldestFileTime = DateTime.MaxValue;

            foreach (var file in logFiles)
            {
                DateTime creationTime = File.GetCreationTime(file);
                if (creationTime < oldestFileTime)
                {
                    oldestFileTime = creationTime;
                }
            }

            return oldestFileTime == DateTime.MaxValue ? DateTime.MinValue : oldestFileTime;
        }

        /// <summary>
        /// 清空文件夹下的所有日志文件
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteLogFiles(string filePath)
        {
            try
            {
                var logFiles = Directory.GetFiles(filePath);
                foreach (var file in logFiles)
                {
                    if (!IsFileInUse(file))
                    {
                        File.Delete(file);
                        CLog.Debug($"已删除文件: {file}");
                    }
                    else
                    {
                        CLog.Warning($"文件正在使用，跳过删除: {file}");
                    }
                }
            }
            catch (Exception ex)
            {
                CLog.Error($"删除日志文件时出错: {ex.Message}");
            }
        }

        private bool IsFileInUse(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // 文件可以被打开，表示没有被占用
                }
            }
            catch (IOException)
            {
                // 文件被占用
                return true;
            }

            return false;
        }
    }
}