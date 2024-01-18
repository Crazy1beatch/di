using System;
using System.Windows.Forms;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();
                container.Bind<MainForm>().ToSelf();
                container.Bind(x =>
                    x.FromThisAssembly().SelectAllClasses().InheritedFrom<IUiAction>().BindAllInterfaces());
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<ImageSettings>().ToMethod(imageSettings => container.Get<AppSettings>().ImageSettings)
                    .InSingletonScope();
                container.Bind<AppSettings>().ToMethod(c => c.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
                container.Bind<IDragonPainterFactory>().ToFactory();
                
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}