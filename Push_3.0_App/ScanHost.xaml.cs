﻿using System;
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
using ScanHostLib;

namespace ScanHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ScanHostWindow : Window
    {
        public ScanHostWindow()
        {
            InitializeComponent();

            ComputerInfo host = ScanHostHelper.Scan("localhost");   //TODO: Remember to change this
            PIInfoBox.Text = host.CPU.ToString();
            OSInfoBox.Text = host.OS.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
