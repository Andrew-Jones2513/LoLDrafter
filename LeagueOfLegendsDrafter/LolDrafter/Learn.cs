using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LolDrafter
{
    public partial class Learn : Form
    {
        Dictionary<string, Image> searchedImages = new Dictionary<string, Image>();
        List<Champion> champions = new List<Champion>();
        private const int cGrip = 16;
        private const int cCaption = 32;
        string fileName;

        public Learn()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public Learn(Dictionary<string, Image> champImages,List<Champion> champs) 
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            searchedImages = champImages;
            champions = champs;
            AddImages();
        }


        // Adds images to the search panel
        public void AddImages()
        {
            // Loop through sorted list and add images to the flowLayoutPanel
            paneMain.Controls.Clear();
            foreach (KeyValuePair<string, Image> img in searchedImages)
            {
                Image image = img.Value;
                PictureBox pic1 = new PictureBox();
                pic1.Width = 100;
                pic1.Height = 100;
                pic1.SizeMode = PictureBoxSizeMode.StretchImage;
                pic1.Image = image;
                pic1.Margin = new Padding(9);
                paneMain.Controls.Add(pic1);
                Label label1 = new Label();
                label1.Text = img.Key;
                label1.AutoSize = false;
                label1.Dock = DockStyle.Bottom;
                label1.TextAlign = ContentAlignment.TopCenter;
                label1.ForeColor = Color.White;
                pic1.Controls.Add(label1);
                pic1.Click += new EventHandler(champSelect_Click);
            }
        }


        // Selects the champ to put into the display
        private void champSelect_Click(object sender, EventArgs e)
        {
            Image champSelected = ((PictureBox)sender).Image;
            string champName = ((PictureBox)sender).Controls[0].Text;
            LearnMore(champName, champSelected);

            // Might do later
            //searchedImages.Remove(champName);
            //paneMain.Controls.Remove((PictureBox)sender);
        }


        // Selects a champ to learn more information about
        public void LearnMore(string champName, Image champImage)
        {
            foreach (Champion champ in champions)
            {
                if (champName == champ.ChampName) 
                {
                    if (tcLA.SelectedTab == tpLearn)
                    {
                        pbChampImage.Image = champImage;
                        lblChampName.Text = champName;
                        lblAbilities.Text = $"Abilities: {champ.Abilities}";
                        lblItems.Text = $"Item Build: {champ.Build}";
                        lblPositions.Text = $"Positions: {champ.Positions}";
                        List<string> check = champ.GoodMatchup.Split(", ").ToList();
                        List<string> check2 = champ.BadMatchup.Split(", ").ToList();
                        foreach (KeyValuePair<string, Image> img in searchedImages)
                        {
                            if (check[0] == img.Key)
                            {
                                lblGMatchOne.Text = img.Key;
                                pbGood1.Image = img.Value;
                            }
                            else if (check[1] == img.Key)
                            {
                                lblGMatchTwo.Text = img.Key;
                                pbGood2.Image = img.Value;
                            }

                            if (check2[0] == img.Key)
                            {
                                lblBMatchOne.Text = img.Key;
                                pbBad1.Image = img.Value;
                            }
                            else if (check2[1] == img.Key)
                            {
                                lblBMatchTwo.Text = img.Key;
                                pbBad2.Image = img.Value;
                            }
                        }
                    }
                    else
                    {
                        pbAdd.Image = champImage;
                        txtAddChamp.Text = champName;
                        txtChampA.Text = $"{champ.Abilities}";
                        txtChampItem.Text = $"{champ.Build}";
                        List<string> champPos = champ.Positions.Split(", ").ToList();
                        cbFirstPos.Text = champPos[0];
                        cbSecondPos.Text = champPos[1];
                        List<string> check = champ.GoodMatchup.Split(", ").ToList();
                        List<string> check2 = champ.BadMatchup.Split(", ").ToList();
                        foreach (KeyValuePair<string, Image> img in searchedImages)
                        {
                            if (check[0] == img.Key)
                            {
                                cbGoodChampOne.Text = img.Key;
                            }
                            else if (check[1] == img.Key)
                            {
                                cbGoodChampTwo.Text = img.Key;
                            }

                            if (check2[0] == img.Key)
                            {
                                cbBadChampOne.Text = img.Key;
                            }
                            else if (check2[1] == img.Key)
                            {
                                cbBadChampTwo.Text = img.Key;
                            }
                        }
                    }
                }
            }
        }


        // Selects the add page
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tcLA.SelectedTab == tpLearn)
            {
                tcLA.SelectedTab = tpAdd;
            }
        }


        // Selects the learn page
        private void btnLearn_Click(object sender, EventArgs e)
        {
            if (tcLA.SelectedTab == tpAdd)
            {
                tcLA.SelectedTab = tpLearn;
            }
        }


        // Gets an image from the user
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();

            // Set filter options and filter index
            FileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            FileDialog.FilterIndex = 1;

            // Show the dialog and get result
            DialogResult result = FileDialog.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                string filePath = FileDialog.FileName;

                // Get the file name without path and extension
                fileName = Path.GetFileNameWithoutExtension(filePath);

                // Save the image to the resources folder with the same file name
                Properties.Resources.ResourceManager.GetObject("Images").GetType().GetMethod("AddOrUpdate", new Type[] { typeof(string), typeof(object) }).Invoke(Properties.Resources.ResourceManager, new object[] { fileName, Image.FromFile(filePath) });
            }
        }


        // Addes a new champ
        private void btnAddChamp_Click(object sender, EventArgs e)
        {

        }


        // Updates an existing champ
        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }


        // Creates a new program border
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }


        // Gets rid of the existing border
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }


        // Closes the program when the x button is hit
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Minimizes the program when the - is hit
        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
