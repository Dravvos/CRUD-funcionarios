using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public class UI
    {
        public void LimpaCampos(Control.ControlCollection controles)
        {
            foreach (Control item in controles)
            {
                if (item.GetType() == typeof(TextBox)||item.GetType()==typeof(MaskedTextBox))
                {
                    item.Text = string.Empty; //limpa todos os controles do tipo TextBox
                }
            }
        }
    }
}
