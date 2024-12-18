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
                if (path[path.LastIndexOf('\\') - 1] != ':')
                    complexData.Add(new ComplexData());
                for (int i = 0; i < tableData.getDirCount(); i++)
                    complexData.Add(new ComplexData(tableData.getDir(i)));
                for (int i = 0; i < tableData.getFileCount(); i++)
                    complexData.Add(new ComplexData(tableData.getFile(i)));

                table.ItemsSource = complexData;

                var r = table.Items[0];

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
            ComplexData rowData = row.Item as ComplexData;

            var leftSelect = LeftTable.SelectedItem;
            var rightSelect = RightTable.SelectedItem;
            if (rowData.type == null)
            {
                DataGrid currentTable = null;
                TextBlock currentTablePath = null;

                if (leftSelect != null && rightSelect != null)
                {
                    LeftTable.SelectedItem = null;
                    RightTable.SelectedItem = null;
                    return;
                }

                if (leftSelect != null)
                {
                    currentTable = LeftTable;
                    currentTablePath = LeftTablePath;
                }
                else if (rightSelect != null)
                {
                    currentTable = RightTable;
                    currentTablePath = RightTablePath;
                }

                if (leftSelect != null || rightSelect != null)
                {
                    LeftTable.SelectedItem = null;
                    RightTable.SelectedItem = null;

                    if (rowData.name == "...")
                    {
                        currentTablePath.Text = currentTablePath.Text.Remove(currentTablePath.Text.Length - 1);
                        int indLastSlash = currentTablePath.Text.LastIndexOf('\\');
                        string path = "";
                        for (int i = 0; i < indLastSlash + 1; i++)
                            path += currentTablePath.Text[i];
                        initTable(currentTable, path);
                    }
                    else
                        initTable(currentTable, currentTablePath.Text + rowData.name + "\\");
                }

            }
            else
            {
                string fullFileName = "";
                if (leftSelect != null)
                {
                    LeftTable.SelectedItem = null;
                    fullFileName += LeftTablePath.Text;
                }
                else if (rightSelect != null)
                {
                    RightTable.SelectedItem = null;
                    fullFileName += RightTablePath.Text;
                }
                fullFileName += rowData.name + "." + rowData.type;
                System.Diagnostics.Process.Start(fullFileName);
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
            }

            if (e.Key == Key.F4)
            {
            }

            if (e.Key == Key.F5)
            {
            }

            if (e.Key == Key.F6)
            {
            }

            if (e.Key == Key.F7)
            {
            }

            if (e.Key == Key.F8)
            {
            }

            if (e.Key == Key.F9)
            {
            }

            if (e.SystemKey == Key.F10)
                Close();
        }

    }
}
