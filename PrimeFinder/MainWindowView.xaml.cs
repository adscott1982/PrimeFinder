namespace PrimeFinder
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        private void SelectAllText(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            var tb = sender as TextBox;
            tb.SelectAll();
        }
    }
}
