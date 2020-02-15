using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonaPrueba.Views.ViewHelps
{
    public class MessageResult
    {
        public static void LogErrors(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
        }

        public static void ShowResults(string message)
        {
            MessageBox.Show(message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
