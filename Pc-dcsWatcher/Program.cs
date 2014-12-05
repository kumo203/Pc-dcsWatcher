﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_dcsWatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //UI例外イベントハンドラ
            Application.ThreadException +=
                new System.Threading.ThreadExceptionEventHandler(
                    Application_ThreadException);

            string progName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            if (Process.GetProcessesByName(progName).Count() > 1)
            {
                MessageBox.Show(string.Format("{0} is already running.", progName), 
                        "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //UnhandledExceptionイベントハンドラを追加
            System.AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PcdcsWatcher());
        }

        //UI例外イベントハンドラ
        private static void Application_ThreadException(object sender,
            System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                ShowErrorMessage(e.Exception, "Exception rose from main thread.");
            }
            finally
            {
                //アプリケーションを終了する
                Application.Exit();
            }
        }

        //UnhandledExceptionイベントハンドラ
        private static void CurrentDomain_UnhandledException(object sender,
            UnhandledExceptionEventArgs e)
        {
            try
            {
                //エラーメッセージを表示する
                ShowErrorMessage(e.ExceptionObject as Exception, "Exception rose from sub threads.");
            }
            finally
            {
                //アプリケーションを終了する
                Environment.Exit(1);
            }
        }

        // ユーザー・フレンドリなダイアログを表示するメソッド
        public static void ShowErrorMessage(Exception ex, string extraMessage)
        {
            MessageBox.Show(extraMessage + " \n――――――――\n\n" +
              "Error Happened\n\n" +
              "[Error Message]\n" + ex.Message + "\n\n" +
              "[Error Information]\n" + ex.StackTrace, 
              "Exception Handling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
