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
using System.Security.Cryptography;

namespace Launcher
{
    public partial class Form1 : Form
    {
        private ToolStripDropDownMenu toolStripDropDownMenu;
        private Timer fadeTimer;
        private bool fadeIn = true;
        public Form1()
        {
            InitializeComponent();

            //Set Size
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Cover.Dock = DockStyle.Fill;
            listBox1.Dock = DockStyle.Fill;
            Add.Dock = DockStyle.Fill;
            Play.Dock = DockStyle.Fill;
            Cover.SizeMode = PictureBoxSizeMode.Zoom;

            //Set Fonts
            listBox1.Font = new Font(listBox1.Font.FontFamily, 12, FontStyle.Regular);
            Add.Font = new Font(Add.Font.FontFamily, 16, FontStyle.Bold);
            Play.Font = new Font(Play.Font.FontFamily, 16, FontStyle.Bold);

            //Disable Default Border Windows
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;

            tableLayoutPanel1.ColumnCount = 5;

            //Load list
            LoadGames();

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            CreateMenu();
            listBox1.MouseDown += ListBox1_MouseDown;

            GameSelect(false);

            tableLayoutPanel1.MouseDown += Window_MouseDown;
            tableLayoutPanel1.MouseMove += Window_MouseMove;
            tableLayoutPanel1.MouseUp += Window_MouseUp;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            fadeTimer = new Timer
            {
                Interval = 50, // Ajusta para controlar la velocidad del fade
            };
            fadeTimer.Tick += FadeTimer_Tick;
            //fadeTimer.Start();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var Reference_Game = new Game
                    {
                        Path = fileDialog.FileName,
                        Name = System.IO.Path.GetFileNameWithoutExtension(fileDialog.FileName)
                    };

                    // Dialog Cover
                    using (var imgDialog = new OpenFileDialog())
                    {
                        imgDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                        if (imgDialog.ShowDialog() == DialogResult.OK)
                        {
                            Reference_Game.Cover = imgDialog.FileName;
                        }
                    }

                    listBox1.Items.Add(Reference_Game);

                    GameSelect(false);
                    SaveGames();
                }
            }
        }

        //Play Game
        private void Play_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Game Reference_Game)
            {
                this.WindowState = FormWindowState.Minimized;
                Process.Start(Reference_Game.Path);
            }
        }

        // Update Picture of item select.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ChangeCover();
        }

        // Change Picture
        private void ChangeCover()
        {
            if (listBox1.SelectedItem is Game Reference_Game)
            {
                using (var imgDialog = new OpenFileDialog())
                {
                    imgDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                    if (imgDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Change picture of the game
                        Reference_Game.Cover = imgDialog.FileName;

                        // Update Picture
                        Cover.ImageLocation = Reference_Game.Cover;

                        SaveGames();
                    }
                }
            }
        }
        
        // Event if select item change
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem is Game juego)
            {
                GameSelect(true);
            }
        }

        // Event key pressed.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {   
            // delete
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
                GameSelect(false);
            }
            // Edit
            if (e.KeyCode == Keys.F2)
            {
                Edit();
            }
            // Finish edit on Enter key
            else if (e.KeyCode == Keys.Enter)
            {
                FinalizarEdicionNombre(listBox1.SelectedIndex, listBox1.Text);
            }
        }

        // Delete item from listbox
        private void Delete()
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);

                GameSelect(false);

                SaveGames();
            }
        }

        // submenu
        private void CreateMenu()
        {
            // Create Float Menu
            toolStripDropDownMenu = new ToolStripDropDownMenu();

            // Custom menu
            toolStripDropDownMenu.Renderer = new MyCustomRenderer();

            // Create elements
            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Rename");
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Change Cover");
            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("Delete");

            // Adds Events to Buttons
            menuItem1.Click += (sender, e) => MenuItem_Click(sender, e, "Rename");
            menuItem2.Click += (sender, e) => MenuItem_Click(sender, e, "Change Cover");
            menuItem3.Click += (sender, e) => MenuItem_Click(sender, e, "Delete");

            // add elements to the float menu
            toolStripDropDownMenu.Items.Add(menuItem1);
            toolStripDropDownMenu.Items.Add(menuItem2);
            toolStripDropDownMenu.Items.Add(menuItem3);
        }

        // Class Custom menu
        class MyCustomRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    // Background item selected
                    Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                    Color colorBackground = Color.FromArgb(50, 50, 50);
                    using (SolidBrush brush = new SolidBrush(colorBackground))
                        e.Graphics.FillRectangle(brush, rc);
                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                }
            }
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = Color.White;
                e.TextFont = new Font("Arial", 9, FontStyle.Bold);
                base.OnRenderItemText(e);
            }
        }

        // open submenu
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

        private void MenuItem_Click(object sender, EventArgs e, string action)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                // Get Index
                int selectedIndex = listBox1.SelectedIndex;

                switch (action)
                {
                    case "Rename":
                        Edit();
                        break;
                    case "Change Cover":
                        ChangeCover();
                        break;
                    case "Delete":
                        Delete();
                        GameSelect(false);
                        break;
                    default:
                        break;
                }
            }
        }


        //show and hide play button event
        private void GameSelect(bool IsSelect)
        {
            Play.Visible = IsSelect;
            if (IsSelect == true && listBox1.SelectedItem is Game Reference_Game)
            {
                Cover.ImageLocation = Reference_Game.Cover;
            }
            else
            {
                Cover.ImageLocation = null;
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
            SortList(true);
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
            SortList(true);
        }

        // Custom Window
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

        private bool dragging = false;
        private Point dragStartPoint;


        // Widnow borde move
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point location = this.PointToScreen(e.Location);
                this.Location = new Point(location.X - dragStartPoint.X, location.Y - dragStartPoint.Y);
            }
        }
        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

        // Sort
        private void SortList(bool ascending)
        {
            // Obtén los elementos de la ListBox y conviértelos a una lista
            List<object> items = listBox1.Items.Cast<object>().ToList();

            // Ordena la lista según la opción (ascendente o descendente)
            if (ascending)
            {
                items.Sort((a, b) => string.Compare(a.ToString(), b.ToString()));
            }
            else
            {
                items.Sort((a, b) => string.Compare(b.ToString(), a.ToString()));
            }

            // Limpia la ListBox y agrega los elementos ordenados
            listBox1.Items.Clear();
            listBox1.Items.AddRange(items.ToArray());
        }

        // edit
        private void Edit()
        {
            if (listBox1.SelectedItem != null)
            {
                // Obtener el índice y el nombre actual del item seleccionado
                int selectedIndex = listBox1.SelectedIndex;
                string currentName = listBox1.SelectedItem.ToString();

                // Activar el modo de edición en el ListBox para permitir la modificación directa
                listBox1.SetSelected(selectedIndex, false);
                listBox1.Enabled = false;

                // Iniciar la edición directa del nombre del item seleccionado en el ListBox
                listBox1.SelectedIndex = selectedIndex;
                listBox1.SelectionMode = SelectionMode.One;

                // Crear un TextBox temporal para editar el nombre
                TextBox textBox = new TextBox();
                textBox.Parent = listBox1;
                textBox.Location = listBox1.GetItemRectangle(selectedIndex).Location;
                textBox.Size = listBox1.GetItemRectangle(selectedIndex).Size;
                textBox.Text = currentName;
                textBox.SelectAll();
                textBox.Focus();
            }
        }
        private void FinalizarEdicionNombre(int selectedIndex, string newName)
        {
            // Actualizar el nombre del item en el ListBox y en la lista de juegos
            listBox1.Items[selectedIndex] = newName;

            // Finalizar la edición y volver a activar el ListBox
            listBox1.Enabled = true;
            listBox1.SelectionMode = SelectionMode.MultiSimple;
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (fadeIn)
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.05; // Aumenta la opacidad
                else
                    fadeIn = false; // Completa el fade in, puedes parar el timer o iniciar un fade out
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity -= 0.05; // Disminuye la opacidad
                else
                    fadeIn = true; // Completa el fade out, puedes parar el timer o reiniciar un fade in
            }
        }
    }
}
