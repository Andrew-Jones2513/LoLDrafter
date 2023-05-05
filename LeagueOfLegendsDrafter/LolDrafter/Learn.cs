using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
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
        ChampionContext championData;
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


        // Adds the positions to the combo box
        public void populatePositions(ComboBox cbPosAdd)
        {
            cbPosAdd.Items.Add("Top");
            cbPosAdd.Items.Add("Jungle");
            cbPosAdd.Items.Add("Mid");
            cbPosAdd.Items.Add("Adc");
            cbPosAdd.Items.Add("Support");
        }


        // Adds the champs to the combo box
        public void populateChamps(ComboBox cbChampAdd)
        {
            foreach (string name in searchedImages.Keys)
            {
                cbChampAdd.Items.Add(name);
            }
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


        // Selects the add page and populates the page
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tcLA.SelectedTab == tpLearn)
            {
                tcLA.SelectedTab = tpAdd;
                populatePositions(cbFirstPos);
                populatePositions(cbSecondPos);
                populateChamps(cbGoodChampOne);
                populateChamps(cbGoodChampTwo);
                populateChamps(cbBadChampOne);
                populateChamps(cbBadChampTwo);
            }
        }


        // Selects the learn page and populates the page
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

                Image image = Image.FromFile(filePath);

                try
                {
                    // Save the image to the resources folder with the same file name
                    Properties.Resources.ResourceManager.GetObject("Images").GetType().GetMethod("AddOrUpdate", new Type[] { typeof(string), typeof(object) }).Invoke(Properties.Resources.ResourceManager, new object[] { fileName, Image.FromFile(filePath) });
                }
                catch (Exception ex)
                {

                    pbAdd.Image = Properties.Resources.Wizard;
                }
            }
        }


        // Addes a new champ
        private void btnAddChamp_Click(object sender, EventArgs e)
        {
            bool newChamp = true;
            bool addnewChamp = false;
            foreach (string champ in searchedImages.Keys)
            {
                if (txtAddChamp.Text == champ && txtAddChamp.Text != "Wizard")
                {
                    newChamp = false;
                    break;
                }
            }

            if (newChamp == true)
            {
                string newChampName = "", newChampAbilities = "", newChampItems = "", newChampPos = "", newChampGood = "", newChampBad = "";
                if (pbAdd.Image != null)
                {
                    if (txtAddChamp.Text != "".Trim())
                    {
                        newChampName = txtAddChamp.Text;
                        if (txtChampA.Text != "".Trim())
                        {
                            newChampAbilities = txtAddChamp.Text;
                            if (txtChampItem.Text != "".Trim())
                            {
                                newChampItems = txtChampItem.Text;
                                if (cbFirstPos.SelectedItem != "".Trim() || cbSecondPos.SelectedItem != "".Trim())
                                {
                                    newChampPos = $"{cbFirstPos.SelectedItem}, {cbSecondPos.SelectedItem}";
                                    if (cbGoodChampOne.SelectedItem != "".Trim() || cbGoodChampTwo.SelectedItem != "".Trim())
                                    {
                                        newChampGood = $"{cbGoodChampOne.SelectedItem}, {cbGoodChampTwo.SelectedItem}";
                                        if (cbBadChampOne.SelectedItem != "".Trim() || cbBadChampTwo.SelectedItem != "".Trim())
                                        {
                                            newChampBad = $"{cbBadChampOne.SelectedItem}, {cbBadChampTwo.SelectedItem}";
                                            addnewChamp = true;
                                        }
                                        else
                                        {
                                            lblError.Text = "The Champ matchups need to be selected";
                                        }
                                    }
                                    else
                                    {
                                        lblError.Text = "The Champ matchups need to be selected";
                                    }
                                }
                                else
                                {
                                    lblError.Text = "The Champ positions need to be selected";
                                }
                            }
                            else
                            {
                                lblError.Text = "The Champ abilities is empty";
                            }
                        }
                        else
                        {
                            lblError.Text = "The Champ abilities is empty";
                        }
                    }
                    else
                    {
                        lblError.Text = "The Champ name is empty";
                    }
                }
                else
                {
                    lblError.Text = "The Champ image is empty";
                }

                if (addnewChamp == true)
                {
                    Champion addChamp = new Champion(newChampName, newChampAbilities, newChampGood, newChampBad, newChampItems, newChampPos);
                    try
                    {
                        championData.Champions.Add(addChamp);
                        championData.SaveChanges();
                        champions = championData.Champions.Select(c => c).ToList();
                        GetImages();
                        Display();
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Could not add champion!";
                    }
                }
            }
            else
            {
                lblError.Text = "This champ exists already!";
            }

        }


        // Updates an existing champ
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool newChamp = true;
            bool updateChamp = false;
            foreach (string champ in searchedImages.Keys)
            {
                if (txtAddChamp.Text == champ)
                {
                    newChamp = false;
                    break;
                }
            }

            if (newChamp == false)
            {
                string newChampName = "", newChampAbilities = "", newChampItems = "", newChampPos = "", newChampGood = "", newChampBad = "";
                if (pbAdd.Image != null)
                {
                    if (txtAddChamp.Text != "".Trim())
                    {
                        newChampName = txtAddChamp.Text;
                        if (txtChampA.Text != "".Trim())
                        {
                            newChampAbilities = txtAddChamp.Text;
                            if (txtChampItem.Text != "".Trim())
                            {
                                newChampItems = txtChampItem.Text;
                                if (cbFirstPos.SelectedItem != "".Trim() || cbSecondPos.SelectedItem != "".Trim())
                                {
                                    newChampPos = $"{cbFirstPos.SelectedItem}, {cbSecondPos.SelectedItem}";
                                    if (cbGoodChampOne.SelectedItem != "".Trim() || cbGoodChampTwo.SelectedItem != "".Trim())
                                    {
                                        newChampGood = $"{cbGoodChampOne.SelectedItem}, {cbGoodChampTwo.SelectedItem}";
                                        if (cbBadChampOne.SelectedItem != "".Trim() || cbBadChampTwo.SelectedItem != "".Trim())
                                        {
                                            newChampBad = $"{cbBadChampOne.SelectedItem}, {cbBadChampTwo.SelectedItem}";
                                            updateChamp = true;
                                        }
                                        else
                                        {
                                            lblError.Text = "The Champ matchups need to be selected";
                                        }
                                    }
                                    else
                                    {
                                        lblError.Text = "The Champ matchups need to be selected";
                                    }
                                }
                                else
                                {
                                    lblError.Text = "The Champ positions need to be selected";
                                }
                            }
                            else
                            {
                                lblError.Text = "The Champ abilities is empty";
                            }
                        }
                        else
                        {
                            lblError.Text = "The Champ abilities is empty";
                        }
                    }
                    else
                    {
                        lblError.Text = "The Champ name is empty";
                    }
                }
                else
                {
                    lblError.Text = "The Champ image is empty";
                }

                if (updateChamp == true)
                {
                    
                    Champion updated = champions.FirstOrDefault(c => c.ChampName == newChampName);
                    if (updated != null)
                    {
                        updated.ChampName = newChampName;
                        updated.Abilities = newChampAbilities;
                        updated.GoodMatchup = newChampGood;
                        updated.BadMatchup = newChampBad;
                        updated.Build = newChampItems;
                        updated.Positions = newChampPos;
                        try
                        {
                            championData.Champions.Update(updated);
                            championData.SaveChanges();
                            champions = championData.Champions.Select(c => c).ToList();
                            GetImages();
                            Display();
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "Could not add champion!";
                        }
                    }
                    else
                    {
                        lblError.Text = "Champion not found.";
                    }
                    
                }
            }
            else
            {
                lblError.Text = "This champ doesn't exist!";
            }
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
            Display();
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


        // Adds champs
        public void Display()
        {
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


        // Adds all champs to the search flow panel
        private void btnAll_Click(object sender, EventArgs e)
        {
            Display();
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


        // Adds Top champs to the search flow panel
        private void btnTop_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Top");
        }


        // Adds Jungle champs to the search flow panel
        private void btnJungle_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Jungle");
        }


        // Adds Mid champs to the search flow panel
        private void btnMiddle_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Mid");
        }


        // Adds Adc champs to the search flow panel
        private void btnADC_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Adc");
        }


        // Adds Support champs to the search flow panel
        private void btnSupport_Click(object sender, EventArgs e)
        {
            AddImagesByPosition("Support");
        }


        // Populates the search flow panel with champs containing text
        private void txtSearch_TextChanged(object sender, EventArgs e)
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


        // Removes the champ
        private void btnRemove_Click(object sender, EventArgs e)
        {
            ResourceManager rm = Properties.Resources.ResourceManager;
            string champName = lblChampName.Text;
            Champion? deleteChamp = championData.Champions.Find(champName);

            if (deleteChamp != null)
            {
                try
                {
                    championData.Champions.Remove(deleteChamp);
                    championData.SaveChanges();
                    champions = championData.Champions.Select(c => c).ToList();
                    if (Properties.Resources.ResourceManager.GetObject(champName) != null)
                    {
                        System.IO.File.Delete(champName);
                    }
                    Display();
                }
                catch (Exception ex)
                {
                    lblErrorLearn.Text = "Could not remove champion!";
                }
            }
            else
            {
                lblErrorLearn.Text = "Please select a champion";
            }

        }

        private void cbFirstPos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
