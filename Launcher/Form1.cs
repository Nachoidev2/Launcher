using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Launcher
{
    public partial class Form1 : Form
    {
        private ToolStripDropDownMenu toolStripDropDownMenu;
        public Form1()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Dock = DockStyle.Fill;
            listBox1.Dock = DockStyle.Fill;
            button1.Dock = DockStyle.Fill;
            button2.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            listBox1.Font = new Font(listBox1.Font.FontFamily, 12, FontStyle.Regular);
            button1.Font = new Font(button1.Font.FontFamily, 16, FontStyle.Bold);
            button2.Font = new Font(button2.Font.FontFamily, 16, FontStyle.Bold);

            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;

            tableLayoutPanel1.ColumnCount = 5;

            LoadGames();

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            CreateMenu();
            listBox1.MouseDown += ListBox1_MouseDown;

            GameSelect(false);

        }

        private void Add_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var juego = new Game
                    {
                        Path = fileDialog.FileName,
                        Name = System.IO.Path.GetFileNameWithoutExtension(fileDialog.FileName)
                    };

                    // Dialog Caratula
                    using (var imgDialog = new OpenFileDialog())
                    {
                        imgDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                        if (imgDialog.ShowDialog() == DialogResult.OK)
                        {
                            juego.Caratula = imgDialog.FileName;
                        }
                    }

                    listBox1.Items.Add(juego);
                    //pictureBox1.ImageLocation = juego.Caratula;

                    GameSelect(false);
                    SaveGames();
                }
            }
        }

        //Play Game
        private void Play_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Game juego)
            {
                this.WindowState = FormWindowState.Minimized;
                Process.Start(juego.Path);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Game juego)
            {
                using (var imgDialog = new OpenFileDialog())
                {
                    imgDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                    if (imgDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Change picture of the game
                        juego.Caratula = imgDialog.FileName;

                        // Actualizar el PictureBox adicional (pictureBoxCaratulaSeleccionada)
                        pictureBox1.ImageLocation = juego.Caratula;

                        SaveGames();
                    }
                }
            }
        }
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem is Game juego)
            {
                GameSelect(true);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
        }

        private void Delete()
        {
            if (listBox1.SelectedItem != null)
            {
                // Delete item from listbox
                listBox1.Items.Remove(listBox1.SelectedItem);

                GameSelect(false);

                SaveGames();
            }
        }

        private void CreateMenu()
        {
            // Create Float Menu
            toolStripDropDownMenu = new ToolStripDropDownMenu();

            // Create elements
            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Rename");
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Delete");

            // Agregar manejadores de eventos a los elementos del menú flotante
            menuItem1.Click += MenuItem1_Click;
            menuItem2.Click += MenuItem2_Click;

            // add elements to the float menu
            toolStripDropDownMenu.Items.Add(menuItem1);
            toolStripDropDownMenu.Items.Add(menuItem2);
        }

        private void ListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get index of element the element over point
                int index = listBox1.IndexFromPoint(e.Location);

                // Select the element if click over here
                if (index >= 0)
                {
                    listBox1.SelectedIndex = index;

                    // Show Float Menu in the mouse position
                    toolStripDropDownMenu.Show(listBox1, e.Location);
                }
            }
        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            
            //MessageBox.Show("Opción 1 seleccionada");
        }

        private void MenuItem2_Click(object sender, EventArgs e)
        {
            Delete();
            GameSelect(false);
            //MessageBox.Show("Opción 2 seleccionada");
        }

        //show and hide play button event
        private void GameSelect(bool IsSelect)
        {
            button2.Visible = IsSelect;
            if (IsSelect == true && listBox1.SelectedItem is Game juego)
            {
                pictureBox1.ImageLocation = juego.Caratula;
            }
            else
            {
                pictureBox1.ImageLocation = null;
            }
        }

        //System Save and Load
        private void SaveGames()
        {
            // Get list games of listBox1
            List<Game> games = new List<Game>();
            foreach (Game game in listBox1.Items)
            {
                games.Add(game);
            }

            // deserialize the list to json and save in a file
            string json = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText("games.json", json);
        }

        private void LoadGames()
        {
            // Check Exist json file
            if (File.Exists("games.json"))
            {
                // Read content of file and deserialize to a list of games
                string json = File.ReadAllText("games.json");
                List<Game> games = JsonConvert.DeserializeObject<List<Game>>(json);

                // add games to listBox1
                listBox1.Items.Clear();
                foreach (Game game in games)
                {
                    listBox1.Items.Add(game);
                }
            }
        }

        //windows
        private void Quit(object sender, EventArgs e)
        {
            Close();
        }
        private void Minimized(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Maximize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
