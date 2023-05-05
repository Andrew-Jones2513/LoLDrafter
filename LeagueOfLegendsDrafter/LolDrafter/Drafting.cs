using System.Collections;
using System.Globalization;
using System.Resources;
using System.Runtime.Versioning;
using System.Windows.Forms;
using LolDrafter.Properties;
using System.Drawing;
using LolDrafter.Migrations;

namespace LolDrafter
{
    public partial class Drafting : Form
    {
        // Global Variables 
        ChampionContext championData;
        List<Champion> champions = new List<Champion>();
        Dictionary<string, Image> searchedImages = new Dictionary<string, Image>();
        bool bluePick = true;
        int bPosition = 0;
        int rPosition = 0;
        private const int cGrip = 16;
        private const int cCaption = 32;

        public Drafting()
        {
            championData = new ChampionContext();
            InitializeComponent();
            champions = championData.Champions.Select(c => c).ToList();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            GetImages();
        }

        // Gets Images from Resources
        public void GetImages()
        {
            searchedImages.Clear();
            ResourceManager rm = Properties.Resources.ResourceManager;
            ResourceSet rs = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            List<string> imageNames = new List<string>();

            // Loop through resources and add image names to list
            foreach (DictionaryEntry resource in rs)
            {
                if (resource.Value is Image)
                {
                    imageNames.Add(resource.Key.ToString());
                }
            }

            // Sort the image names list alphabetically then adds it to my image dictionary
            imageNames.Sort();
            foreach (string imageName in imageNames)
            {
                searchedImages.Add(imageName, (Image)rm.GetObject(imageName));
            }

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


        // Gets all images for each posion
        public void AddImagesByPosition(string position)
        {
            paneMain.Controls.Clear();
            foreach (Champion champ in champions)
            {
                if (champ.Positions.Contains(position))
                {
                    searchedImages.TryGetValue(champ.ChampName, out Image value);
                    Image image = value;
                    PictureBox pic1 = new PictureBox();
                    pic1.Width = 100;
                    pic1.Height = 100;
                    pic1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic1.Image = image;
                    pic1.Margin = new Padding(9);
                    paneMain.Controls.Add(pic1);
                    Label label1 = new Label();
                    label1.Text = champ.ChampName;
                    label1.AutoSize = false;
                    label1.Dock = DockStyle.Bottom;
                    label1.TextAlign = ContentAlignment.TopCenter;
                    label1.ForeColor = Color.White;
                    pic1.Controls.Add(label1);
                    pic1.Click += new EventHandler(champSelect_Click);
                }
            }
        }


        // Goes through the picking and banning
        public void Picking(string champName, Image champImage)
        {
            if (bluePick == true)
            {
                if (bPosition == 0)
                {
                    pbBlueBan1.Image = champImage;
                    disBackBB1.Visible = false;
                    disBackRB1.Visible= true;
                    lblSidePick.Text = "Red Side Turn To Ban!!";
                }
                else if (bPosition == 1)
                {
                    pbBlueBan2.Image = champImage;
                    disBackBB2.Visible = false;
                    disBackRB2.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Ban!!";
                }
                else if (bPosition == 2)
                {
                    pbBlueBan3.Image = champImage;
                    disBackBB3.Visible = false;
                    disBackRB3.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Ban!!";
                }
                else if (bPosition == 3)
                {
                    pbBlueOne.Image = champImage;
                    lblBlueOne.Text = champName;
                    disBackBP1.Visible = false;
                    disBackRP1.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Pick!!";
                }
                else if (bPosition == 4)
                {
                    pbBlueTwo.Image = champImage;
                    lblBlueTwo.Text = champName;
                    disBackBP2.Visible = false;
                    disBackRP2.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Pick!!";
                }
                else if (bPosition == 5)
                {
                    pbBlueThree.Image = champImage;
                    lblBlueThree.Text = champName;
                    disBackBP3.Visible = false;
                    disBackRP3.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Pick!!";
                }
                else if (bPosition == 6)
                {
                    pbBlueBan4.Image = champImage;
                    disBackBB4.Visible = false;
                    disBackRB4.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Ban!!";
                }
                else if (bPosition == 7)
                {
                    pbBlueBan5.Image = champImage;
                    disBackBB5.Visible = false;
                    disBackRB5.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Ban!!";
                }
                else if (bPosition == 8)
                {
                    pbBlueFour.Image = champImage;
                    lblBlueFour.Text = champName;
                    disBackBP4.Visible = false;
                    disBackRP4.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Pick!!";
                }
                else if (bPosition == 9)
                {
                    pbBlueFive.Image = champImage;
                    lblBlueFive.Text = champName;
                    disBackBP5.Visible = false;
                    disBackRP5.Visible = true;
                    lblSidePick.Text = "Red Side Turn To Pick!!";
                }

                bluePick = false;
                lblSidePick.BackColor = Color.Red;
                if (bPosition == 9)
                {
                    bPosition = 0;
                }
                else
                {
                    bPosition++;
                }
            }
            else
            {
                if (rPosition == 0)
                {
                    pbRedBan1.Image = champImage;
                    disBackRB1.Visible = false;
                    disBackBB2.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Ban!!";
                }
                else if (rPosition == 1)
                {
                    pbRedBan2.Image = champImage;
                    disBackRB2.Visible = false;
                    disBackBB3.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Ban!!";
                }
                else if (rPosition == 2)
                {
                    pbRedBan3.Image = champImage;
                    disBackRB3.Visible = false;
                    disBackBP1.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Pick!!";
                }
                else if (rPosition == 3)
                {
                    pbRedOne.Image = champImage;
                    lblRedOne.Text = champName;
                    disBackRP1.Visible = false;
                    disBackBP2.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Pick!!";
                }
                else if (rPosition == 4)
                {
                    pbRedTwo.Image = champImage;
                    lblRedTwo.Text = champName;
                    disBackRP2.Visible = false;
                    disBackBP3.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Pick!!";
                }
                else if (rPosition == 5)
                {
                    pbRedThree.Image = champImage;
                    lblRedThree.Text = champName;
                    disBackRP3.Visible = false;
                    disBackBB4.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Ban!!";
                }
                else if (rPosition == 6)
                {
                    pbRedBan4.Image = champImage;
                    disBackRB4.Visible = false;
                    disBackBB5.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Ban!!";
                }
                else if (rPosition == 7)
                {
                    pbRedBan5.Image = champImage;
                    disBackRB5.Visible = false;
                    disBackBP4.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Pick!!";
                }
                else if (rPosition == 8)
                {
                    pbRedFour.Image = champImage;
                    lblRedFour.Text = champName;
                    disBackRP4.Visible = false;
                    disBackBP5.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Pick!!";
                }
                else if (rPosition == 9)
                {
                    pbRedFive.Image = champImage;
                    lblRedFive.Text = champName;
                    disBackRP5.Visible = false;
                    disBackBB1.Visible = true;
                    lblSidePick.Text = "Blue Side Turn To Ban!!";
                }

                bluePick = true;
                lblSidePick.BackColor = Color.Blue;
                if (rPosition == 9)
                {
                    rPosition = 0;
                }
                else
                {
                    rPosition++;
                }
            }
        }


        // Selects all champs for display
        private void btnAll_Click(object sender, EventArgs e)
        {
            GetImages();
        }


        // Selects all top champs for display
        private void btnTop_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Top");
        }


        // Selects all jg champs for display
        private void btnJungle_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Jungle");
        }


        // Selects all mid champs for display
        private void btnMiddle_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Mid");
        }


        // Selects all adc champs for display
        private void btnADC_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Adc");
        }


        // Selects all sup champs for display
        private void btnSupport_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Support");
        }


        // Selects champs containing the search for display
        private void Searching(object sender, EventArgs e)
        {
            paneMain.Controls.Clear();
            string userSearch = txtSearch.Text.ToLower().Trim();
            foreach (KeyValuePair<string, Image> img in searchedImages)
            {
                if (img.Key.ToLower().Contains(userSearch))
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
        }


        // Selects the champ to put into the display
        private void champSelect_Click(object sender, EventArgs e)
        {
            Image champSelected = ((PictureBox)sender).Image;
            string champName = ((PictureBox)sender).Controls[0].Text;
            Picking(champName, champSelected);
            
            // Might do later
            //searchedImages.Remove(champName);
            //paneMain.Controls.Remove((PictureBox)sender);
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
            Close();
        }


        // Minimizes the program when the - is hit
        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        // Creates a border for the Red Side Label
        void DisRedSide_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, DisRedSide.DisplayRectangle, Color.Black, ButtonBorderStyle.Solid);
        }


        // Creates a border for the Blue Side Label
        void DisBlueSide_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, DisBlueSide.DisplayRectangle, Color.Black, ButtonBorderStyle.Solid);
        }


        // Opens the Learning Form
        private void btnLearn_Click(object sender, EventArgs e)
        {
            Learn learnform = new Learn(searchedImages, champions);
            learnform.Show();
        }
    }
}