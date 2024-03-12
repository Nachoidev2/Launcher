using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public partial class ProgramList : Form
    {
        public string SelectedProgram { get; private set; }
        public ProgramList(List<string> programs)
        {
            InitializeComponent();
            // Rellenar la lista de programas instalados
            foreach (var program in programs)
            {
                listBoxPrograms.Items.Add(program);
            }
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    SelectedProgram = fileDialog.FileName;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Obtener el programa seleccionado de la lista
            SelectedProgram = listBoxPrograms.SelectedItem?.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
