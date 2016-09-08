using System;
using System.Windows;
using ExcelClass;
using System.ComponentModel;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace PokemonEdex
{
    /// <summary>
    /// Progress.xaml 的交互逻辑
    /// </summary>
    public partial class Progress : Window
    {
        RefreshEventHandler RefreshDelg;
        BackgroundWorker bgWorker ;
        int count;
        int rows;
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, UInt32 bRevert);
        [DllImport("USER32.DLL ", CharSet = CharSet.Unicode)]
        private static extern UInt32 RemoveMenu(IntPtr hMenu, UInt32 nPosition, UInt32 wFlags);
        private const UInt32 SC_CLOSE = 0x0000F060;
        private const UInt32 MF_BYCOMMAND = 0x00000000;
        public Progress()
        {
            InitializeComponent();
            this.
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(Exec);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkCompleted);
            
        }

       
        public Progress(string filename, RefreshEventHandler delg)
        {
            InitializeComponent();
            RefreshDelg = delg;
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = false;
            bgWorker.DoWork += new DoWorkEventHandler(Exec);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkCompleted);
            ExcelApi.Init(filename);
            count = 3;
            rows = ExcelApi.GetRows();
        }
        
        private void Exec(object sender, DoWorkEventArgs e)
        {
            count = 3;
            while(count < rows)
            {
                
                int val = ExcelApi.readone(count);
                bgWorker.ReportProgress(100*val/rows);
                count++;
            }

        }
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            Percent.Text = e.ProgressPercentage.ToString() + "%";
        }
        void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ExcelApi.Dispose();
            bgWorker.Dispose();
            this.Close();
        }
        private void StartExcel(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
            
        }

        
        private void ClosedWindow(object sender, EventArgs e)
        {
            RefreshDelg();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle; 
            IntPtr hMenu = GetSystemMenu(hwnd, 0);
            RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
        }
    }
}
