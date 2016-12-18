using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using bookworm.Entities;

namespace bookworm.Helpers
{
  public  class GuiHelper
    {
        private static GuiHelper guiHelper;
        public static GuiHelper GetGuiHelper()
        {
            if (guiHelper == null)
            {
                guiHelper = new GuiHelper();
            }
            return guiHelper;
        }

        private GuiHelper() { }


        public ProgressBar ProgressBar = null;
        public DataGrid DataGrid = null;

        private Dictionary<string, bool> _progressBarStatues = new Dictionary<string, bool>();


    }
}
