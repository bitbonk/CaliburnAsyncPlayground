namespace CaliburnAsyncPlayground
{
    using System;
    using System.Windows;

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string FirstScreen = "First Screen";
        public const string SecondScreen = "Second Screen";
        public static readonly TimeSpan TaskDelay = TimeSpan.FromSeconds(3);
    }
}