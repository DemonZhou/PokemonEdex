using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Entity;
using ExcelClass;
namespace PokemonEdex
{
    public delegate void RefreshEventHandler();
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string url;
        
        public void Refresh()
        {
            this.IsEnabled = true;
            list.Dispatcher.BeginInvoke(
                new Action(() => {
                    list.ItemsSource = ExcelApi.GetPokemonList();
                    list.Focus();
                    list.SelectedIndex = 0;
                    
                    
                }),
                null);
        }

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            string exeurl = Environment.CurrentDirectory;
            string debugurl = exeurl.Substring(0,exeurl.LastIndexOf('\\'));
            url = exeurl.Substring(0, debugurl.LastIndexOf('\\'));
            if (!System.IO.Directory.Exists(url + @"\Picture"))
            {
                System.IO.Directory.CreateDirectory(url+ @"\Picture");
            }
            if (!System.IO.Directory.Exists(url+@"\Excel"))
            {
                System.IO.Directory.CreateDirectory(url+@"\Excel");
            }
            
            
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog filedialog = new Microsoft.Win32.OpenFileDialog();
            filedialog.Filter = "Excel文件| *.xlsx;*.xls";
            filedialog.InitialDirectory = url + @"\Excel";
            
            filedialog.ShowDialog();
            string filename = filedialog.FileName;
            if(filename != String.Empty)
            {
                Progress prog = new Progress(filename,new RefreshEventHandler(Refresh));
                this.IsEnabled = false;
                prog.Show();
            }
            
           

        }

        
        private void SetShowPanel(Pokemon pk)
        {
            this.img.Source = new BitmapImage(new Uri(pk.Imagepath));
            this.Hp.Text = pk.Hp.ToString();
            this.Spattack.Text = pk.Spattack.ToString();
            this.Spdefense.Text = pk.Spdefense.ToString();
            this.Attack.Text = pk.Phattack.ToString();
            this.Defense.Text = pk.Phdefense.ToString();
            this.Speed.Text = pk.Speed.ToString();
            this.Sum.Text = pk.Sum.ToString();
            this.Attr.Text = pk.Attr.ToString();
            this.Id.Text = pk.Id;
            this.Name.Text = pk.Name;
        }


        private void Load(object sender, RoutedEventArgs e)
        {

        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pokemon pk = list.SelectedItem as Pokemon;
            SetShowPanel(pk);
        }
    }
}