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
using System.Threading;

namespace FreedomJoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonRunMain_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() => // TODO: Figure out how to do this correctly... needs to die when I kill the program, need to track it somewhere, ...etc
            {
                Map map = new Map();
                map.Run();
            }).Start();

        }
    }
}
