using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SunamoTray.Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUserControlClosing userControlClosing = null;
        public bool CancelClosing { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ThisApp.Name = "SunamoTray.Tests";

            #region 7) Notify icon
            SetCancelClosing(true);
            var cm = forms.ContextMenuHelper.Get("Quit", WpfApp.Shutdown);

            // .ico must be set up to Resource
            Dictionary<string, Action> contextSuMenuItems = new Dictionary<string, Action>();
            NotifyIconHelper.Create( SetCancelClosing, ResourcesH.ci.GetStream(ThisApp.Name + ".ico"), delegate (object sen, EventArgs args)
            {
                this.Show();
                this.BringIntoView();
                var beforeTopMost = this.Topmost;
                this.Topmost = true;
                this.Topmost = beforeTopMost;

                // WindowState should be loaded from configuration
                //this.WindowState = WindowState.Normal;
            }, cm, null);
            #endregion
        }

        #region MyRegion
        // Only for working with notify, but always insert block with userControlClosing
        protected override void OnClosing(CancelEventArgs e)
        {
//#if !DEBUG
                e.Cancel = GetCancelClosing();
                WindowState = WindowState.Minimized;
//#endif
            //Must check before - during shutdowning down is miAlwaysOnTop null
        //if (!e.Cancel)
        //    {
        //        CheckSuMenuItemTopMost();
        //    }
            if (userControlClosing != null)
            {
                userControlClosing.OnClosing();
            }

            base.OnClosing(e);
        }
        #endregion

        public bool GetCancelClosing()
        {
            if (CancelClosing)
            {
                Hide();
            }
            return CancelClosing;
        }

        public void SetCancelClosing(bool b)
        {
            CancelClosing = b;
        }
    }
}
