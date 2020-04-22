using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CaliburnAsyncPlayground
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly TimeSpan TaskDelay = TimeSpan.FromSeconds(3);
        public const string FirstScreen = "First Screen";
        public const string SecondScreen = "Second Screen";
    }
}