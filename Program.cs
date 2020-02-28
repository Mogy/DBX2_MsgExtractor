using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DBX2_MsgExtractor
{
    class Program
    {
        const string DIR_EN_MSG = "CsEnMsg";
        const string DIR_JA_MSG = "CsJaMsg";
        const string DIR_TEMP = "tmp";
        const string MSG_TOOL = "Dragon_Ball_Xenoverse_2_MSG_Tool.exe";
        const string EN_MSG = "enMsg.txt";
        const string JA_MSG = "jaMsg.txt";
        static void Main(string[] args)
        {
            // フォルダ生成
            Directory.CreateDirectory(DIR_EN_MSG);
            Directory.CreateDirectory(DIR_JA_MSG);

            // msgTool存在チェック
            if (!File.Exists(MSG_TOOL))
            {
                Console.WriteLine($"{MSG_TOOL} is not found.");
                Console.WriteLine();
                Console.WriteLine("-- please push any key --");
                Console.ReadKey();
                Environment.Exit(1);
            }

            // tempフォルダを再生成
            if (Directory.Exists(DIR_TEMP))
            {
                Directory.Delete(DIR_TEMP, true);
                // 実行完了待ち
                while (Directory.Exists(DIR_TEMP))
                {
                    Thread.Sleep(1);
                }
            }
            Directory.CreateDirectory(DIR_TEMP);

            // 出力ファイルを削除
            if(File.Exists(EN_MSG))
            {
                File.Delete(EN_MSG);
            }
            if (File.Exists(JA_MSG))
            {
                File.Delete(JA_MSG);
            }

            // msgTool設定
            var msgTool = new ProcessStartInfo();
            msgTool.FileName = MSG_TOOL;
            msgTool.CreateNoWindow = true;
            msgTool.UseShellExecute = false;

            var jaFiles = Directory.GetFiles(DIR_JA_MSG, "*_ja.msg");
            var currents = 1;

            foreach (string jaPath in jaFiles)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{currents++}/{jaFiles.Length}");

                // 英語メッセージ存在チェック
                var jaMsg = Path.GetFileName(jaPath);
                var enMsg = jaMsg.Replace("_ja", "_en");
                var enPath = Path.Join(DIR_EN_MSG, enMsg);
                if (!File.Exists(enPath))
                {
                    continue;
                }

                // メッセージ出力
                var enTxtPath = Path.Join(DIR_TEMP, Path.ChangeExtension(enMsg, "txt"));
                var jaTxtPath = Path.Join(DIR_TEMP, Path.ChangeExtension(jaMsg, "txt"));
                exportMessage(msgTool, enMsg, enPath, enTxtPath, EN_MSG);
                exportMessage(msgTool, jaMsg, jaPath, jaTxtPath, JA_MSG);
            }

            // tempフォルダを削除
            Directory.Delete(DIR_TEMP, true);

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{jaFiles.Length} messages extracted.");
            Console.WriteLine();
            Console.WriteLine("-- please push any key --");
            Console.ReadKey();
        }

        static void exportMessage(ProcessStartInfo msgTool, string msg, string msgPath, string txtPath, string exportPath) {
            // msgTool実行
            msgTool.Arguments = String.Join(" ", "-e", msgPath, txtPath);
            Process.Start(msgTool).WaitForExit();

            // メッセージからNullを除外
            var data = "";
            using (var sr = new StreamReader(txtPath))
            {
                data = sr.ReadToEnd();
            }
            data = data.Replace("\0", "");
            using (var sw = new StreamWriter(txtPath))
            {
                sw.Write(data);
            }

            // メッセージを抽出
            using (var sw = new StreamWriter(exportPath, true))
            {
                sw.WriteLine($"■■■{msg}");
                sw.Write(data);
            }
        }
    }
}
