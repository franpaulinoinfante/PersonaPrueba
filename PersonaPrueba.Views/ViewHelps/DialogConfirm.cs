using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonaPrueba.Views.ViewHelps
{
    public class DialogConfirm
    {
        public static DialogResult GetResult(string message, string caption)
        {
            return MessageBox.Show($"{message}", $"{caption}", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}
