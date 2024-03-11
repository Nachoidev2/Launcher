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
using craftersmine.SteamGridDBNet;
using craftersmine.SteamGridDBNet.Exceptions;
using System.Net;

namespace Launcher
{
    public partial class Form1 : Form
    {
        private ToolStripDropDownMenu toolStripDropDownMenu;
        private Timer fadeTimer;
        private bool fadeIn = true;
        private SteamGridDb sgdb = new SteamGridDb("b31e5f6ab66df37f2d0c8613d70413f9");
        public Form1()
        {
            InitializeComponent();

            //Set Size
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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

            tableLayoutPanel2.MouseDown += Window_MouseDown;
            tableLayoutPanel2.MouseMove += Window_MouseMove;
            tableLayoutPanel2.MouseUp += Window_MouseUp;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            this.BackgroundImageLayout = ImageLayout.Stretch;

            fadeTimer = new Timer
            {
                Interval = 50, // Ajusta para controlar la velocidad del fade
            };
            fadeTimer.Tick += FadeTimer_Tick;
            //fadeTimer.Start();

        }

        private async void Add_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var Reference_Game = new Game
                    {
                        Path = fileDialog.FileName,
                        //Name = System.IO.Path.GetFileNameWithoutExtension(fileDialog.FileName)
                    };

                    string enteredName = OpenPromptDialog("Name Game", "Diálogo de entrada");

                    if (!string.IsNullOrEmpty(enteredName))
                    {
                        // Asigna el nombre al juego
                        Reference_Game.Name = enteredName;

                        (SteamGridDbGrid[] grids, SteamGridDbHero[] heroes) = await SearchGame(enteredName);

                        if (grids != null)
                        {
                            //Download Image
                            using (WebClient webClient = new WebClient())
                            {
                                string imageUrl = grids[0].FullImageUrl;
                                byte[] imageData = await webClient.DownloadDataTaskAsync(new Uri(imageUrl));

                                // Check Extension
                                string imageExtension = Path.GetExtension(imageUrl);
                                string imageFileName = $"{enteredName}_cover{imageExtension}";

                                // Rename File
                                string dataFolderPath = "Data";
                                string coversFolderPath = Path.Combine(dataFolderPath, "Covers");
                                string imagePath = Path.Combine(coversFolderPath, imageFileName);

                                // Save local
                                File.WriteAllBytes(imagePath, imageData);

                                Reference_Game.Cover = imagePath;

                            }
                        }

                        if (heroes != null)
                        {
                            //Download Image
                            using (WebClient webClient = new WebClient())
                            {
                                string imageUrl = heroes[0].FullImageUrl;
                                byte[] imageData = await webClient.DownloadDataTaskAsync(new Uri(imageUrl));

                                // Check Extension
                                string imageExtension = Path.GetExtension(imageUrl);
                                string imageFileName = $"{enteredName}_Background{imageExtension}";

                                // Rename File
                                string dataFolderPath = "Data";
                                string coversFolderPath = Path.Combine(dataFolderPath, "Backgrounds");
                                string imagePath = Path.Combine(coversFolderPath, imageFileName);

                                // Save local
                                File.WriteAllBytes(imagePath, imageData);

                                Reference_Game.Background = imagePath;

                            }
                        }

                        listBox1.Items.Add(Reference_Game);

                        GameSelect(false);
                        SaveGames();
                    }
                }
            }
        }

        private string OpenPromptDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                // Retorna el texto ingresado por el usuario
                return textBox.Text;
            }

            // En caso de cancelar, retorna una cadena vacía
            return string.Empty;
        }

        private async Task<(SteamGridDbGrid[], SteamGridDbHero[])> SearchGame(string searchTerm)
        {
            SteamGridDbGrid[] grids = null;
            SteamGridDbHero[] heroes = null;

            try
            {
                SteamGridDbGame[] games = await sgdb.SearchForGamesAsync(searchTerm);


                if (games != null && games.Length > 0)
                {
                    // Manejar los juegos encontrados, por ejemplo, seleccionar el primero o permitir que el usuario elija
                    var firstGame = games[0]; // Solo un ejemplo, ajusta según sea necesario
                    var ID = firstGame.Id;

                    grids = await sgdb.GetGridsByGameIdAsync(ID);
                    heroes = await sgdb.GetHeroesByGameIdAsync(ID);

                    if (grids != null && grids.Length > 0)
                    {
                        return (grids, heroes);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron grids para el juego seleccionado.");
                    }
                }
                else
                {
                    // No se encontraron juegos
                    MessageBox.Show("No se encontraron juegos para el término de búsqueda proporcionado.");
                }
            }
            catch (SteamGridDbBadRequestException ex)
            {
                // Manejar una solicitud mal formada, potencialmente un error de API o un bug en la biblioteca
            }
            catch (SteamGridDbUnauthorizedException ex)
            {
                // Manejar error de autenticación, como una clave de API faltante, inválida o expirada
            }
            catch (SteamGridDbNotFoundException ex)
            {
                // Manejar el caso en que no se encuentran juegos con los parámetros especificados
            }
            catch (SteamGridDbForbiddenException ex)
            {
                // Manejar el acceso prohibido, por ejemplo, intentar eliminar un objeto que no posees
            }
            catch (SteamGridDbImageException ex)
            {
                // Manejar errores relacionados con el acceso a imágenes
            }
            catch (SteamGridDbException ex)
            {
                // Manejar cualquier otro error genérico al acceder a la API de SteamGridDB
            }
            return (new SteamGridDbGrid[0], new SteamGridDbHero[0]);
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
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Change Path");
            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("Change Cover");
            ToolStripMenuItem menuItem4 = new ToolStripMenuItem("Delete");

            // Adds Events to Buttons
            menuItem1.Click += (sender, e) => MenuItem_Click(sender, e, "Rename");
            menuItem2.Click += (sender, e) => MenuItem_Click(sender, e, "Change Path");
            menuItem3.Click += (sender, e) => MenuItem_Click(sender, e, "Change Cover");
            menuItem4.Click += (sender, e) => MenuItem_Click(sender, e, "Delete");

            // add elements to the float menu
            toolStripDropDownMenu.Items.Add(menuItem1);
            toolStripDropDownMenu.Items.Add(menuItem2);
            toolStripDropDownMenu.Items.Add(menuItem3);
            toolStripDropDownMenu.Items.Add(menuItem4);
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
                    case "Change Path":

                        break;
                    default:
                        break;
                }
            }
        }


        //show and hide play button event
        private void GameSelect(bool IsSelect)
        {
            Play.Visible = Description.Visible = IsSelect;
            if (IsSelect == true && listBox1.SelectedItem is Game Reference_Game)
            {
                Cover.ImageLocation = Reference_Game.Cover;
                this.BackgroundImage = Image.FromFile(Reference_Game.Background);
            }
            else
            {
                Cover.ImageLocation = null;
                this.BackgroundImage = null;
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
            File.WriteAllText("./Data/games.json", json);
            SortList(true);
        }

        private void LoadGames()
        {
            // Check Exist json file
            if (File.Exists("./Data/games.json"))
            {
                // Read content of file and deserialize to a list of games
                string json = File.ReadAllText("./Data/games.json");
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
            if (listBox1.SelectedItem != null && listBox1.SelectedItem is Game Reference_Game)
            {
                // Activar el modo de edición en el ListBox para permitir la modificación directa
                string enteredName = OpenPromptDialog("Name Game", "Rename");
                if (!string.IsNullOrEmpty(enteredName))
                {
                    // Asigna el nombre al juego
                    Reference_Game.Name = enteredName;

                    //GameSelect(false);
                    SaveGames();
                }
            }
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
