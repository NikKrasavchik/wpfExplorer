using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            initDriveButtons(LeftTableDisks);
            initDriveButtons(RightTableDisks);

            initTable(LeftTable, @"C:\");
            initTable(RightTable, @"C:\");
        }

        public void initTable(DataGrid table, string path)
        {
            List<ComplexData> complexData = new List<ComplexData>();
            TableData tableData = new TableData();
            bool isError = tableData.determineFiles(path);

            if (!isError)
            {
                for (int i = 0; i < tableData.getDirCount(); i++)
                    complexData.Add(new ComplexData(tableData.getDir(i)));
                for (int i = 0; i < tableData.getFileCount(); i++)
                    complexData.Add(new ComplexData(tableData.getFile(i)));

                table.ItemsSource = complexData;

                if (table.Name[0] == 'L')
                    LeftTablePath.Text = path;
                else
                    RightTablePath.Text = path;
            }    
        }

        public void initDriveButtons(WrapPanel wrapPanel)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                Button diskButton = new Button() { Content = drives[i] };
                diskButton.Click += diskButton_Click;
                wrapPanel.Children.Add(diskButton);
            }
        }

        public void diskButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;
            string senderName = senderButton.Content.ToString();

            WrapPanel senderParent = senderButton.Parent as WrapPanel;
            string senderParentName = senderParent.Name;
            if (senderParentName[0] == 'L')
                initTable(LeftTable, senderName);
            else
                initTable(RightTable, senderName);
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            // Some operations with this row
        }
    }
}
