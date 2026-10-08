namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        public static ApplicationContext AppContext { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            AppContext = new ApplicationContext(new FormLogin());
            Application.Run(AppContext);
        }
    }
}