using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMRManagement.Entities;
using OMRReader.BL;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
//using Inlite.ClearImageNet;
//using Inlite.ClearImageNet;
//using OMRManagement.BL;
namespace OMRManagement
{
    public partial class OMRTemplateManager : Form
    {
        Template Template;
        ProjectDetails Project;
        public int CurrentFieldID = 0;
        //public int projectID = 0;
        public string currentFieldName = string.Empty;
        //TemplateField TemplateField;
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;
        private Bitmap templateBitmap = null;
        public int v = 0;
        public int TrackLeft = 0, TrackTop = 0;
        private ContextMenu mnu;
        public int ProcessType = 0;
        public int CurrentFrontTemplate = 0;
        public OMRTemplateManager()
        {
            InitializeComponent();
        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTemplateCode.Text) || (!string.IsNullOrEmpty(txtTemplateName.Text)))
            {
                //txtTemplateCode.BackColor = Color.White;
                //txtTemplateName.BackColor = Color.White;
                lblTrackCount.Text = "";
                if (rdFront.Checked || rdBack.Checked)
                {

                    Template.Page = rdFront.Checked ? PageType.FRONT : PageType.BACK;
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Select an image file.";
                    ofd.Filter = "Png Images(*.jpg)|*.jpg|Jpeg Images(*.jpg)|*.jpg";
                    ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";
                    // listBox1.Items.Clear();
                    // listBox2.Items.Clear();
                    picPreview.Image = null;
                    originalBitmap = null;
                    previewBitmap = null;
                    picPreview.Controls.Clear();
                    //clear();
                    resultBitmap = null;
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        CreateBitmap(ofd.FileName);

                        MessageBox.Show("Please Double Click On Top of the Image Track");
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Any Page Type", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //MessageBox.Show("Please check TemplateName or TemplateCode");
                }
            }
            else
            {
                // MessageBox.Show("Please Select Any Page Type");
                MessageBox.Show("Please check TemplateName or TemplateCode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //if (string.IsNullOrEmpty(txtTemplateName.Text))
                //{
                //    txtTemplateName.BackColor = Color.Red;
                //}
                //if (string.IsNullOrEmpty(txtTemplateCode.Text))
                //{
                //    txtTemplateCode.BackColor = Color.Red;
                //}
            }
        }
      
        private void CreateBitmap(string fileName)
        {
         ///  originalBitmap = biotionalconvertion(fileName);
            StreamReader streamReader = new StreamReader(fileName);
            originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            streamReader.Close();
            label1.Text = fileName;
            label1.Refresh();
            Bitmap bit = originalBitmap;
            picPreview.Width = originalBitmap.Width;
            previewBitmap = originalBitmap;
            picPreview.Image = previewBitmap;
            Image _img = previewBitmap;
            templateBitmap = new Bitmap(_img.Width, _img.Height);
            ImageAttributes ia = new ImageAttributes();
            ColorMatrix cm = new ColorMatrix();
            #region Old Code

            #endregion
            //Replace the Old Code as followings
            #region New Code
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = -5f;
            cm.Matrix30 = cm.Matrix31 = cm.Matrix32 = 5f;
            #endregion
            ia.SetColorMatrix(cm);
            Graphics g = Graphics.FromImage(templateBitmap);
            g.DrawImage(_img, new Rectangle(0, 0, _img.Width, _img.Height), 0, 0, _img.Width, _img.Height, GraphicsUnit.Pixel, ia);
            //ApplyFilter(true);
            //templateBitmap.Save("d:\\123.bmp");
        }

        public DialogResult ShowDialog(ProjectDetails project)
        {
            this.Project = project;
            this.Project.ProjectID = this.Project.ProjectID;
            txtProjectName.Text = this.Project.ProjectName;
            txtProjectCode.Text = this.Project.ProjectCode;
            btnProjectSave.Visible = false;
            return this.ShowDialog();

        }
        List<ProjectDetails> projectList;
        private void LoadProjects()
        {
            projectList = ProjectBL.GetProjects();
            if (projectList != null)
            {
                dataGridViewProjectsList.AutoGenerateColumns = false;
                dataGridViewProjectsList.Columns[0].Name = "slno1";
                dataGridViewProjectsList.Columns[0].HeaderText = "Project ID";
                //dataGridViewProjectsList.Columns[0].DataPropertyName = "0";
                //dataGridViewProjectsList.Columns[0].Visible = false;
                dataGridViewProjectsList.Columns[1].Name = "ProjectnName";
                dataGridViewProjectsList.Columns[1].HeaderText = "Project Name";
                dataGridViewProjectsList.Columns[1].DataPropertyName = "ProjectName";
                dataGridViewProjectsList.Columns[2].Name = "ProjectCode";
                dataGridViewProjectsList.Columns[2].HeaderText = "Project Code";
                dataGridViewProjectsList.Columns[2].DataPropertyName = "ProjectCode";
                dataGridViewProjectsList.Columns[3].Name = "ProjectID1";
                dataGridViewProjectsList.Columns[3].HeaderText = "Project ID";
                dataGridViewProjectsList.Columns[3].DataPropertyName = "ProjectID";
                dataGridViewProjectsList.Columns[3].Visible = false;
                dataGridViewProjectsList.DataSource = projectList;
                for (int row = 0; row < projectList.Count; row++)
                {

                    dataGridViewProjectsList.Rows[row].Cells[0].Value = row + 1;

                }
            }
            //else
            //{
            //    MessageBox.Show("No Projects Found");
            //    return;
            //}
        }
        List<TabPage> savedTabPages;
        public int tab = 0;
        public string CurrentProjectName=null;
        private void OMRTemplateManager_Load(object sender, EventArgs e)
        {

            Home h = new Home();
            h.Hide();
                LoadProjects();
                if (projectList != null)
                {
                    lblProjectNmae.Text = "Project Name : " + this.CurrentProjectName;
                    lblProjectNmae.Refresh();
                    savedTabPages = new List<TabPage>();
                    foreach (TabPage tp in tabControl1.TabPages)
                    {
                        savedTabPages.Add(tp);
                    }

                    if (tab == 0)
                    {
                        tabControl1.TabPages.Remove(tabControl1.TabPages[2]);
                        tabControl1.TabPages.Remove(tabControl1.TabPages[1]);
                    }
                    else
                    {
                        tabControl1.TabPages.Remove(tabControl1.TabPages[1]);
                        tabControl1.TabPages.Remove(tabControl1.TabPages[0]);
                        label15.Visible = true;
                        txtThreshold.Visible = true;
                        //if (this.ShowDialog == DialogResult.OK)
                        //{
                        //}
                        //List<ProjectDetails >  projectList=
                    }
                    //TabPage page2 = tabControl1.TabPages[1];
                    //page2.Visible = false;

                    Template = new Template();
                    cmbFieldType.DataSource = Enum.GetValues(typeof(FieldType));

                    mnu = new ContextMenu();
                    MenuItem mnuCopy = new MenuItem("Copy");
                    MenuItem mnuCut = new MenuItem("Cut");
                    MenuItem mnuPaste = new MenuItem("Paste");
                    MenuItem mnudelte = new MenuItem("Delete");
                    MenuItem mnuMove = new MenuItem();
                    //mnuCopy.Click += new EventHandler(mnuCopy_Click);
                    mnuMove.Click += new EventHandler(mnuMove_Click);
                    mnuCut.Click += new EventHandler(mnuCut_Click);
                    mnuPaste.Click += new EventHandler(mnuPaste_Click);
                    mnudelte.Click += new EventHandler(mnudelte_Click);
                    //  lblCoordinates.Visible = false;
                    mnu.MenuItems.AddRange(new MenuItem[] { mnuCopy, mnuCut, mnuPaste, mnudelte, mnuMove });
                    picPreview.ContextMenu = mnu;
                }
                else
                {
                    MessageBox.Show("No Projects Found");
                    this.Close();
                }
        }
        private Control clipBoardRef; Panel panels0;
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            try
            {
                clipBoardRef = panels0;
                if (((Control)sender).Name != "Track Problem")
                {
                    Clipboard.SetData(txtFieldName.Text, "it doesn't matter");
                   
                }
            }
            catch
            {
            }
        }
        private void mnuMove_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    Clipboard.SetData(txtFieldName.Text, "it doesn't matter");
            //}
            //catch
            //{
            //}
        }
        private void mnuPaste_Click(object sender, EventArgs e)
        {
             string errorMsg = FieldsValidate();
             //FieldLeft = ControlMoverOrResizer.H;
             if (string.IsNullOrEmpty(errorMsg))
             {
                 if (cmbFieldType.Text == "HOR")
                 {
                     if (cmbFiledIndex.Text.Length != int.Parse(txtColumns.Text))
                     {
                         MessageBox.Show("Invalied Column NO");
                     }
                     else
                     {
                         Getvalues1();
                     }
                 }
                 if (cmbFieldType.Text == "VER")
                 {
                     if (cmbFiledIndex.Text.Length != int.Parse(txtRows.Text))
                     {
                         MessageBox.Show("Invalied Row NO");
                     }
                     else
                     {
                         Getvalues1();
                     }
                 }
             }
             else
             {
                 MessageBox.Show(errorMsg);


             }
          //  Getvalues1();
            panels0.ContextMenu = mnu;
        }
        private void mnuCut_Click(object sender, EventArgs e)
        {

        }
        private void mnudelte_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFieldName.Text == "")
                {
                    TrackLeft = 0; TrackTop = 0;
                    picPreview.Controls.RemoveByKey("Track");
                }
                else
                {

                    picPreview.Controls.RemoveByKey(txtFieldName.Text);
                    var itemToRemove = Template.FieldList.Single(r => r.FieldName == txtFieldName.Text);
                    var FieldID = Template.FieldList.Where(r => r.FieldName == txtFieldName.Text).Select(f => f.FieldID).SingleOrDefault();
                    Template.FieldList.Remove(itemToRemove);
                    if (FieldID != 0)
                    {
                        Template.Delete(FieldID);
                    }
                    LoadTemplateToGrid(Template);
                    if (TemplateGrid.Rows.Count >= 1)
                    {
                        TemplateGrid.Rows[0].Selected = true;
                    }
                    
                   // TemplateGrid.RowCount= Template.FieldList.Count;
                  //  TemplateGrid.Refresh();
                 
                }
            }
            catch
            {
            }


        }
        private Color SETTRASNFAERENCY(int p, Color color)
        {
            return Color.FromArgb(p, color.R, color.G, color.B);
        }
        int multi;
        private void CreateBubble(int x, int y, int w, int h, string chara, Panel panel)
        {

            // panel.Controls.Add(10);
            
                Rectangle r;
                Color CL1 = SETTRASNFAERENCY(255, Color.Red);
                if (multi == 2)
                {
                    CL1 = SETTRASNFAERENCY(255, Color.Red);
                }
                //panels0.BackColor = CL1;

                panel.Paint += (sender, e) =>
                {
                    if (cmbFieldType.SelectedItem != null)
                    {
                        Color clr = Color.Red;
                        Pen p = new Pen(Color.Red);

                        int PW = panel.Location.X;
                        int PH = panel.Location.Y;
                        r = new Rectangle(x, y, w, h);
                        v = 0;

                        if ((FieldType)cmbFieldType.SelectedItem == FieldType.HOR)
                        {
                            try
                            {
                                for (int c2 = PH + y; c2 <= PH + y + h; c2++)
                                {
                                    for (int c3 = PW + x; c3 <= PW + x + w; c3++)
                                    {
                                        if (c2 < templateBitmap.Height && c3 < templateBitmap.Width)
                                        {
                                            Color c1 = templateBitmap.GetPixel(c3, c2);
                                            // b.Save("d:\\23.bmp");

                                            if (c1.Name == "ffffffff")
                                            {
                                                v++;

                                            }
                                        }
                                    }
                                }
                                if (v > threshold)
                                {
                                    sb = new SolidBrush(CL1);
                                    //label1.Text = label1.Text + chara;
                                    multi++;

                                }
                                else
                                {
                                    sb = new SolidBrush(Color.Transparent);
                                }
                            }
                            catch
                            {
                            }
                            e.Graphics.DrawRectangle(p, r);
                            e.Graphics.FillRectangle(sb, r);
                            // txtBubbleHeight.Text = "20";
                            Font Font1 = new Font("Arial", h- 2, FontStyle.Regular);
                            sb = new SolidBrush(Color.Blue);
                            StringFormat StringFormat1 = new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            e.Graphics.DrawString(Convert.ToString(chara), Font1, sb, r, StringFormat1);
                            Font1.Dispose();
                            StringFormat1.Dispose();
                        }
                        //if (chara == "V")
                        //{
                        //}
                        if ((FieldType)cmbFieldType.SelectedItem == FieldType.VER)
                        {

                            for (int c2 = PH + y; c2 <= PH + y + h; c2++)
                            {
                                for (int c3 = PW + x; c3 <= PW + x + w; c3++)
                                {
                                    if (c2 < templateBitmap.Height && c3 < templateBitmap.Width)
                                    {
                                        Color c1 = templateBitmap.GetPixel(c3, c2);
                                        // b.Save("d:\\23.bmp");

                                        if (c1.Name == "ffffffff")
                                        {
                                            v++;

                                        }
                                    }
                                }
                            }
                            if (v > threshold)
                            {
                                sb = new SolidBrush(CL1);
                                //label1.Text = label1.Text + chara;

                            }
                            else
                            {
                                sb = new SolidBrush(Color.Transparent);
                            }
                            e.Graphics.DrawRectangle(p, r);
                            e.Graphics.FillRectangle(sb, r);

                            Font Font1 = new Font("Arial", h- 2, FontStyle.Regular);
                            sb = new SolidBrush(Color.Blue);
                            StringFormat StringFormat1 = new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            e.Graphics.DrawString(Convert.ToString(chara), Font1, sb, r, StringFormat1);
                            Font1.Dispose();
                            StringFormat1.Dispose();
                        }
                    }
                };
            

                     
        }
        public TemplateField GetAndAssignExistingField(Template template, string fieldName)
        {
            TemplateField templateField = template.FieldList.Where(f => f.FieldName == fieldName).Select(f => f).FirstOrDefault();
            if (templateField != null)
            {
                txtFieldName.Text = templateField.FieldName.Trim();
                cmbFiledIndex.Text = templateField.FieldIndex.Trim();
                cmbFieldType.Text = templateField.TemplateFieldType.ToString().Trim();
                txtFieldStartIndex.Text = templateField.TrackRowNo.ToString();
                txtColumns.Text = templateField.FieldColumnNo.ToString();
                txtRows.Text = templateField.FieldRowNo.ToString();
                txtBubblegaf.Text = templateField.BubbleColumnGap.ToString();
                txtBubbleRowGaf.Text = templateField.BubbleRowGap.ToString();
                txtBubbleHeight.Text = templateField.BubbleHeight.ToString();
                txtBubbleWidth.Text = templateField.BubbleWidth.ToString();
                txtannotationheight.Text = templateField.AnnotationHeight.ToString();
                txtannotationwidth.Text = templateField.AnnotationWidth.ToString();
                CurrentFieldID = templateField.FieldID;
            }
            return templateField;
        }



        // Bitmap b;
        string omrchar = null;
        SolidBrush sb;
        Color CL1;
        int y = 0, x = 0;
        //string[] VALUES;
        private void panels0_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                //label1.Text = "";

                if (((Control)sender).Name != "Track")
                {
                    txtFieldName.Text = ((Control)sender).Name;
                    currentFieldName = txtFieldName.Text;
                   
                    TemplateField templateField = GetAndAssignExistingField(Template, txtFieldName.Text.Trim());
                    if (templateField != null)
                    {
                        CurrentFieldID = templateField.FieldID;
                    }
                    else if (Template.FieldList.Count != 0)
                    {
                        CurrentFieldID = 0;
                    }
                    txtannotationwidth.Text = ((Control)sender).Width.ToString();
                    txtannotationheight.Text = ((Control)sender).Height.ToString();
                }
            }
            catch
            {
            }
        }
        private void picPreview_Click(object sender, EventArgs e)
        {

        }
       
        private void picPreview_MouseMove(object sender, MouseEventArgs e)
        {

            if (picPreview.Image != null)
            {

                lblCoordinates.Text = String.Format("Image Left Position: {0};       Image Top Position: {1}", e.X, e.Y);
                lblCoordinates.Refresh();
                  if (ControlMoverOrResizer.H > 0 )
                {
                    FieldLeft = ControlMoverOrResizer.H;
                }
                  ControlMoverOrResizer.H = 0;
               
            }
            else
            {
                lblCoordinates.Text = "";
                lblCoordinates.Refresh();
            }
        }
        private void CreateTrack(int x, int y1, int w, int h, int chara, Panel panel)
        {
            try
            {
                // panel.Controls.Add(10);
                Rectangle r;

                panel.Paint += (sender, e) =>
                {

                    Color clr = Color.Red;
                    Pen p = new Pen(Color.Red, 5);
                    SolidBrush sb = new SolidBrush(Color.Red);
                    r = new Rectangle(x, y1, w, h);
                    for (int c2 = y1; c2 <= y1; c2++)
                    {
                        for (int c3 = x; c3 <= x + w; c3++)
                        {
                            Color c1 = templateBitmap.GetPixel(c3, c2);
                            if (c1.Name == "ffffffff")
                            {
                                v++;

                            }
                        }
                    }
                    if (v >threshold)
                    {
                        sb = new SolidBrush(Color.Red);
                    }
                    else
                    {
                        sb = new SolidBrush(Color.Transparent);
                    }
                    e.Graphics.DrawRectangle(p, r);
                    e.Graphics.FillRectangle(sb, r);
                    Font Font1 = new Font("Arial", g, FontStyle.Regular);
                    sb = new SolidBrush(Color.Blue);
                    StringFormat StringFormat1 = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(Convert.ToString(chara), Font1, sb, r, StringFormat1);
                    Font1.Dispose();
                    StringFormat1.Dispose();

                };
            }
            catch
            {
            }

        }
        Char[] VALUES; int g = 0;
        string[] fieldtrack; string[] fieldtrack1;
        int rowno;
        private void txtFieldStartIndex_Leave(object sender, EventArgs e)
        {
            try
            {
                rowno = int.Parse(txtFieldStartIndex.Text);
            }
            catch
            {
                MessageBox.Show("Wrong TrackNumber Details");
            }
        }
        int k = 0; int trackok = 0;
        Color tt;
        private void IMAGEPOINTS(Bitmap b)
        {
            k = 0;
            for (int y1 = y; y1 < (y + 10); ++y1)
            {
                for (int x1 = x; x1 < (x + 10); ++x1)
                {
                    tt = b.GetPixel(x1, y1);
                    if (tt.Name == "ffffffff")
                    {
                        k++;

                    }
                }
            }
            if (k >= 10)
            {
                trackok++;
            }
        }
        private void cmbFiledIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                VALUES = cmbFiledIndex.SelectedItem.ToString().ToCharArray();

            }
            catch
            {

            }
        }

        private void picPreview_Click_1(object sender, EventArgs e)
        {
          //  FieldLeft = this.Left;
        }

        private void picPreview_MouseClick(object sender, MouseEventArgs e)
        {
           FieldLeft =e.X;
        }
        private void ApplyFilter(bool preview)
        {

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;
            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }

            if (selectedSource != null)
            {

                bitmapResult = ExtBitmap.PrewittFilter(originalBitmap, false);

            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                }
            }

        }

        public static int tlef = 0, ttop = 0;

        string trackindex;
        ArrayList indexValues1; int leftDiff = 0;
        
        private void picPreview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            trackindex = string.Empty;
           
            //if(MouseButtons.
            if (TrackLeft != 0 && TrackTop != 0)
            {
                ExtBitmap.ImageLeft = TrackLeft;
                ExtBitmap.ImageTop = TrackTop;
            }
            else
            {
                TrackLeft = 0;
                TrackTop=0;
                ExtBitmap.ImageLeft = TrackLeft = e.X;
                ExtBitmap.ImageTop = TrackTop = e.Y;
            }
            picPreview.Image = previewBitmap;
            ApplyFilter(true);
            leftDiff = 0;
            indexValues1 = new ArrayList();
            //  indexValues2 = new ArrayList();

            try
            {
                for (int i = 0; i < ExtBitmap.a.Length; i++)
                {

                    if (i != 0)
                    {
                        string[] y1 = ExtBitmap.a[i].ToString().Split(',');
                        
                        int n =int.Parse(y1[0].ToString())- int.Parse(indexValues1[indexValues1.Count-1].ToString().Split(',')[0]);
                        if (n >=20)
                        {
                            indexValues1.Add(ExtBitmap.a[i].ToString());
                            trackindex = trackindex + "," + y1[0];
                            if (indexValues1.Count == 1 && indexValues1.Count == 10)
                            {
                                leftDiff = int.Parse(y1[1]);
                            }
                          //i = i +45;
                        }
                    }
                    else
                    {
                        string[] y1 = ExtBitmap.a[0].ToString().Split(',');
                        indexValues1.Add(ExtBitmap.a[i].ToString());
                        trackindex = y1[0];
                      //  i = i + 10;
                    }
                }

            }

            catch
            {


            }

            //  picPreview.Image =templateBitmap;
           
            //button3_Click(sender, e);
            if (indexValues1.Count >5)
            {
                TrackCreate(indexValues1);
                MessageBox.Show(("Track Count :"+(indexValues1.Count-1).ToString()));
                Template.TrackCount = indexValues1.Count - 1;
                lblTrackCount.Text = "No of Tracks :"+Template.TrackCount.ToString();
                lblTrackCount.Refresh();
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Visible = true;
                groupBox4.Visible = true;
                groupBox5.Visible = true;
                btnAdd.Visible = true;
                btnSave.Visible = true;
                btnSaveTemplate.Visible = true;
                TemplateGrid.Visible = true;
                FOUND = true;
                button5 .Visible= true;
                diff1 = int.Parse(indexValues1[0].ToString().Split(',')[1]) - (TrackLeft);
                //fieldDif = leftDiff - TrackLeft;
            }
            else
            {
                MessageBox.Show("Track is Not Found");
                FOUND = false;
                return;
            }
            picPreview.Image = previewBitmap;
            textResult.Clear();
            textResult.Visible = false;
        }

        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtColumns.Text = "1";
            txtRows.Text = "1";
            if (cmbFieldType.SelectedItem.ToString() == "H")
            {
                txtColumns.Text = VALUES.Length.ToString();
            }
            if (cmbFieldType.SelectedItem.ToString() == "V")
            {
                txtRows.Text = VALUES.Length.ToString();
            }
        }
        private void TrackCreate(ArrayList indexValues1)
        {
            //string[] ARRAY = listBox2.Items[0].ToString().Split(',');
            // string[] ARRAY2 = listBox2.Items[1].ToString().Split(',');
            y = int.Parse(indexValues1[0].ToString().Split(',')[0]);
            x = int.Parse(indexValues1[0].ToString().Split(',')[1]);

            g = 8;
            // b = new Bitmap(picPreview.Image);
            txtTTGaf.Text = (int.Parse(indexValues1[1].ToString().Split(',')[0]) - int.Parse(indexValues1[0].ToString().Split(',')[0])).ToString();
            txtBubbleRowGaf.Text = txtTTGaf.Text;

            panels0 = new Panel();
            panels0.Width = 30;
            // string[] hi = listBox2.Items[listBox2.Items.Count-1].ToString().Split(',');
            panels0.Height = int.Parse(indexValues1[indexValues1.Count - 1].ToString().Split(',')[0]) + 10;
            panels0.Name = "Track";
            Color CL = SETTRASNFAERENCY(0, Color.Yellow);
            panels0.BackColor = CL;
            Graphics G;
            G = panels0.CreateGraphics();
            panels0.Location = new Point(x, y);
            int ty = 2;
            int j = 1;
            //string[] ARRAY1 = indexValues1.ToString().Split(',');
            for (int t = 1; t <= indexValues1.Count - 1; t++)
            {

                
                if (ty < picPreview.Image.Height)
                {
                    CreateTrack(0, ty, 20, g, j, panels0);
                }
                //if (t == 10 || t == 20 || t == 30 || t == 40 || t == 50 || t == 60 || t == 70 || t == 80 || t == 90)
                //{
                //    ty = ty + g + 3;
                //}
                //else
                //{
                // ARRAY = indexValues1[t - 1].ToString().Split(',');
                try
                {
                    //if ((t - 1) == 0)
                    //{
                    //    ty =int.Parse(txtTTGaf.Text);
                    //}
                    //else
                    //{

                    // ARRAY1 = indexValues1[t].ToString().Split(',');
                    // }
                }
                catch
                {
                    ty = ty + int.Parse(txtTTGaf.Text);
                }
                ty = ty + (int.Parse(indexValues1[t].ToString().Split(',')[0]) - int.Parse(indexValues1[t - 1].ToString().Split(',')[0]));
                //}
                j++;
            }


            picPreview.Controls.Add(panels0);
            ControlMoverOrResizer.Init(panels0);
            this.panels0.MouseClick += new MouseEventHandler(this.panels0_MouseClick);
            panels0.ContextMenu = mnu;


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMsg=FieldsValidate();
                if(string.IsNullOrEmpty(errorMsg))
                {
                    if (rdFront.Checked)
                    {
                        FieldLeft = TrackLeft + 40;
                        FieldTop = TrackTop + 40;
                    }
                    if (rdBack.Checked)
                    {
                         FieldLeft =100;
                        FieldTop =100;
                    }


                    threshold = ((int.Parse(txtBubbleWidth.Text)) * (int.Parse(txtBubbleHeight.Text)) * (int.Parse(txtThreshold.Text))) / 100;
                    if (VALUES.Length > 0)
                    {
                       
                        if (int.Parse(txtColumns.Text.ToString()) == VALUES.Length && cmbFieldType.Text=="HOR")
                        {
                           
                            Getvalues1();

                        }
                        else if (int.Parse(txtRows.Text.ToString()) == VALUES.Length && cmbFieldType.Text == "VER")
                        {
                           
                            Getvalues1();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Check FieldIndex");
                    }
                }
                else
                {
                    MessageBox.Show(errorMsg);
                }
                //}
            }
            catch
            {
                MessageBox.Show("Please Give Correct Input Values");
            }
        }
        int diff1;
        int FieldLeft, FieldTop;
        int threshold = 0;
        private void Getvalues1()
        {
            threshold = ((int.Parse(txtBubbleWidth.Text)) * (int.Parse(txtBubbleHeight.Text)) * (int.Parse(txtThreshold.Text))) / 100;
            // textBox2.Text = n.ToString();
            string errorMsg = FieldsValidate();
            if (string.IsNullOrEmpty(errorMsg))
            {
                try
                {
                    panels0 = new Panel();

                    panels0.Width = int.Parse(txtannotationwidth.Text);
                    if (txtFieldName.Text.Length < 1)
                    {
                        MessageBox.Show("Please Mention Field Name");

                        return;

                    }
                    else
                    {
                        panels0.Name = txtFieldName.Text;
                    }
                    // txtTTGaf.Text = "22";
                    panels0.Height = (int.Parse(txtannotationheight.Text));
                    // txtX.Text ="58";                txtY.Text = "200";
                    //  txtTTCount.Text = "47";
                    // rowno =int.Parse( txtFieldStartIndex.Text);
                    //X = X;
                    // string[] ARRAY = track[rowno - 1].Split(',');
                    // X = int.Parse(indexValues1[0].ToString().Split(',')[0]) + 3;
                    // txtX.Text = (diff).ToString();
                    // txtX.Text = (diff+diff1).ToString();
                    FieldTop = Convert.ToInt32(indexValues1[Convert.ToInt32(txtFieldStartIndex.Text) - 1].ToString().Split(',')[0]);
                   // fieldDif = -5;
                    panels0.Location = new Point(FieldLeft, FieldTop);
                    //panels0.sh
                    panels0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    Color CL = SETTRASNFAERENCY(110, Color.Yellow);
                    panels0.BackColor = CL;
                    Graphics G;
                    // picPreview.Image = b;
                    G = panels0.CreateGraphics();
                    picPreview.Image = previewBitmap;
                    int bx = int.Parse(txtBubblegaf.Text);
                    int by = int.Parse(txtBubbleRowGaf.Text);
                    VALUES = cmbFiledIndex.Text.ToCharArray();
                    if (VALUES == null)
                    {
                        MessageBox.Show("Please Select Field Index");
                        return;
                    }

                    y = 0;
                    x = 0;
                    if (txtRows.Text.Length < 1)
                    {
                        MessageBox.Show("Please Menssion No of Rows");
                        return;
                    }
                    //ans = null;

                    int i = 1;

                    if ((FieldType)cmbFieldType.SelectedItem == FieldType.HOR)
                    {
                        int dif = 1;
                        int tr = rowno;

                        for (int r = 0; r < int.Parse(txtRows.Text); r++)
                        {
                            multi = 1;
                            for (int col = 0; col < int.Parse(txtColumns.Text); col++)
                            {
                                if (VALUES[col].ToString() != " " && (int.Parse(txtBubbleHeight.Text) > 0 && int.Parse(txtBubbleWidth.Text) > 0))
                                {
                                    CreateBubble(x, y, int.Parse(txtBubbleWidth.Text), int.Parse(txtBubbleHeight.Text), VALUES[col].ToString(), panels0);
                                }
                                if (col % 4== 0 && col!=0)
                                {
                                    x = x + int.Parse(txtBubblegaf.Text) +2;
                                }
                                else
                                {
                                    x = x + int.Parse(txtBubblegaf.Text);
                                }



                            }
                            if (r % 4== 0 && r!=0)
                            {
                                y = y + int.Parse(txtBubbleRowGaf.Text)+2;
                            }
                            else
                            {
                                y = y + int.Parse(txtBubbleRowGaf.Text);
                            }
                            
                            tr++;

                            i++;
                            x = 0;
                        }
                    }

                    if ((FieldType)cmbFieldType.SelectedItem == FieldType.VER)
                    {
                        // y = 2;
                        for (int r = 0; r < int.Parse(txtColumns.Text); r++)
                        {
                            int j = 1;
                            multi = 1;
                            int dif = 1;
                            int tr = rowno;
                            for (int col = 0; col < int.Parse(txtRows.Text); col++)
                            {

                                if (VALUES[col].ToString() != " " && (int.Parse(txtBubbleWidth.Text) > 0 && int.Parse(txtBubbleHeight.Text) > 0))
                                {

                                    CreateBubble(x, y, int.Parse(txtBubbleWidth.Text), int.Parse(txtBubbleHeight.Text), VALUES[col].ToString(), panels0);

                                }
                                if (col % 10== 0&&col!=0)
                                {
                                    y = y + int.Parse(txtBubbleRowGaf.Text)+2 ;
                                }
                                else
                                {
                                    y = y + int.Parse(txtBubbleRowGaf.Text);
                                }
                                //try
                                //{

                                //    if (col == 0)
                                //    {
                                //        y = int.Parse(txtBubbleRowGaf.Text);
                                //    }
                                //    else
                                //    {
                                //        y = y + int.Parse(indexValues1[tr].ToString().Split(',')[0]) - int.Parse(indexValues1[tr - 1].ToString().Split(',')[0]);
                                //    }

                                //}
                                //catch
                                //{
                                //}



                                j++; tr++;
                                // label5.Text = "";
                            }

                            if (r % 4 == 0)
                            {
                                x = x + int.Parse(txtBubblegaf.Text) + 2;
                            }
                            else
                            {
                                x = x + int.Parse(txtBubblegaf.Text);
                            }
                            i++;
                            y = 0;

                        }
                    }
                    
                    picPreview.Controls.Add(panels0);
                    ControlMoverOrResizer.Init(panels0);
                    this.panels0.MouseClick += new MouseEventHandler(this.panels0_MouseClick);
                    panels0.ContextMenu = mnu;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void cmbFiledIndex_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                VALUES = cmbFiledIndex.SelectedItem.ToString().ToCharArray();

            }
            catch
            {

            }
        }

        private void cmbFieldType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtColumns.Text = "1";
            txtRows.Text = "1";
            if (VALUES != null && cmbFieldType.SelectedItem != null)
            {
                if ((FieldType)cmbFieldType.SelectedItem == FieldType.HOR)
                {
                    txtColumns.Text = VALUES.Length.ToString();
                }
                else if ((FieldType)cmbFieldType.SelectedItem == FieldType.VER)
                {
                    txtRows.Text = VALUES.Length.ToString();
                }
            }
        }

        public void SetTemplateField(TemplateField templateField)
        {

            templateField.AnnotationHeight = Convert.ToInt32(txtannotationheight.Text);
            templateField.AnnotationWidth = Convert.ToInt32(txtannotationwidth.Text);
            templateField.BubbleColumnGap = Convert.ToInt32(txtBubblegaf.Text);
            templateField.BubbleHeight = Convert.ToInt32(txtBubbleHeight.Text);
            templateField.BubbleRowGap = Convert.ToInt32(txtBubbleRowGaf.Text);
            templateField.BubbleWidth = Convert.ToInt32(txtBubbleWidth.Text);
            templateField.FieldIndex = cmbFiledIndex.Text.ToString().Trim();
            templateField.FieldLeftPosition = FieldLeft;
            templateField.FieldName = txtFieldName.Text.Trim();
            templateField.FieldTopPosition = FieldTop;
            templateField.StartIndex = Template.FieldList.Count + 1;
            templateField.TemplateFieldType = (FieldType)cmbFieldType.SelectedItem;
            templateField.FieldRowNo = Convert.ToInt32(txtRows.Text);
            templateField.FieldColumnNo = Convert.ToInt32(txtColumns.Text);
            templateField.TrackRowNo = Convert.ToInt32(txtFieldStartIndex.Text);

        }

        public void ClearFields()
        {
            txtFieldName.Clear();
            cmbFiledIndex.Text = null;
            cmbFieldType.Text = null;
            txtFieldStartIndex.Clear();
            txtColumns.Clear();
            txtRows.Clear();
            txtBubblegaf.Clear();
            txtBubbleRowGaf.Clear();
            txtBubbleHeight.Clear();
            txtBubbleWidth.Clear();
            txtannotationheight.Clear();
            txtannotationwidth.Clear();
        }
        // public bool fieldValied = true;
        public string FieldsValidate()
        {
            string errorMsg = string.Empty;
            if (string.IsNullOrEmpty(txtFieldName.Text))
            {
                errorMsg = "Please Check FieldName";
              //  txtFieldName.BackColor = Color.Red;
                //fieldValied = false;
            }
            //else
            //{
            //    txtFieldName.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(cmbFiledIndex.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check FiledIndex";
                //cmbFiledIndex.BackColor = Color.Red;
            }
            //else
            //{
            //    cmbFiledIndex.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(cmbFieldType.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check FieldType";
               // cmbFieldType.BackColor = Color.Red;

            }
            //else
            //{
            //    cmbFieldType.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtFieldStartIndex.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check FieldStartIndex";
               // txtFieldStartIndex.BackColor = Color.Red;

            }
            //else
            //{
            //    txtFieldStartIndex.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtRows.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check No.Rows";
               // txtRows.BackColor = Color.Red;

            }
            //else
            //{
            //    txtRows.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtColumns.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check No.Columns";
               // txtColumns.BackColor = Color.Red;

            }
            //else
            //{
            //    txtColumns.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtBubblegaf.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check BubbleColumnGap";
               // txtBubblegaf.BackColor = Color.Red;

            }
            //else
            //{
            //    txtBubblegaf.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtBubbleRowGaf.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check BubbleRowGap";
               // txtBubbleRowGaf.BackColor = Color.Red;

            }
            //else
            //{
            //    txtBubbleRowGaf.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtBubbleHeight.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check BubbleHeight";
                //txtBubbleHeight.BackColor = Color.Red;

            }
            //else
            //{
            //    txtBubbleHeight.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtBubbleWidth.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check BubbleWidth";
               // txtBubbleWidth.BackColor = Color.Red;

            }
            //else
            //{
            //    txtBubbleWidth.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtannotationheight.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check Annotationheight";
               // txtannotationheight.BackColor = Color.Red;

            }
            //else
            //{
            //    txtannotationheight.BackColor = Color.White;
            //}
            if (string.IsNullOrEmpty(txtannotationwidth.Text))
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check AnnotationWidth";
               // txtannotationwidth.BackColor = Color.Red;

            }
           
            if (picPreview.Image == null)
            {
                errorMsg = errorMsg + Environment.NewLine + "Please Check Image Not Loaded";
               // picPreview.BackColor = Color.Red;

            }
           
            return errorMsg;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMsg = FieldsValidate();
            if (string.IsNullOrEmpty(errorMsg))
            {
                if (CurrentFieldID == 0 && string.IsNullOrEmpty(currentFieldName))
                {
                    if (!Template.FieldList.Where(t => t.FieldName == txtFieldName.Text.Trim()).Any())
                    {
                        TemplateField templateField = new TemplateField();
                        FieldTop = Convert.ToInt32(indexValues1[Convert.ToInt32(txtFieldStartIndex.Text) - 1].ToString().Split(',')[0]);
                        //txtannotationwidth.Text = ((Control)sender).Width.ToString();
                        //txtannotationheight.Text = ((Control)sender).Height.ToString();
                        SetTemplateField(templateField);
                        Template.FieldList.Add(templateField);
                        MessageBox.Show("Field saved successfully");
                        //templateField.FieldID = Template.FieldList.Count;
                        ClearFields();
                       // txtFieldStartIndex.Text = "1";
                    }
                    else
                    {
                        picPreview.Controls.RemoveByKey(txtFieldName.Text);
                        //MessageBox.Show("Field Already Exists");
                        MessageBox.Show("Field already Exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFieldName.BackColor = Color.Red;
                    }
                }
                else
                {
                    FieldTop = Convert.ToInt32(indexValues1[Convert.ToInt32(txtFieldStartIndex.Text) - 1].ToString().Split(',')[0]);
                    TemplateField templateField = null;
                   if(!string.IsNullOrEmpty(txtFieldName.Text.Trim()))
                   {
                    templateField = Template.FieldList.Where(t => t.FieldName == currentFieldName).Select(f => f).FirstOrDefault();
                   }
                    if (templateField == null)
                    {
                        templateField = new TemplateField();
                        FieldTop = Convert.ToInt32(indexValues1[Convert.ToInt32(txtFieldStartIndex.Text) - 1].ToString().Split(',')[0]);
                        //txtannotationwidth.Text = ((Control)sender).Width.ToString();
                        //txtannotationheight.Text = ((Control)sender).Height.ToString();
                        SetTemplateField(templateField);
                        Template.FieldList.Add(templateField);
                        
                        MessageBox.Show("Field saved successfully");
                        //templateField.FieldID = Template.FieldList.Count;
                        ClearFields();
                       // txtFieldStartIndex.Text = "1";
                    }
                    else
                    {
                        //for (int row = 0; row < Template.FieldList.Count; row++)
                        //{

                        //    TemplateGrid.Rows[row].Cells[0].Value = Template.FieldList[row].FieldID;

                        //}
                        SetTemplateField(templateField);
                       
                        ClearFields();
                        Template.FieldList[0].StartIndex =1;
                        MessageBox.Show("Field updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // txtFieldStartIndex.Text = "1";
                    }
                }
                CurrentFieldID = 0;
                currentFieldName = string.Empty;
                TemplateGrid.DataSource = null;
                LoadTemplateToGrid(Template);
                
                //for (int i = 2; i <= TemplateGrid.ColumnCount - 1; i++)
                //{
                //    TemplateGrid.Columns[i].Visible = false;
                //    // TemplateGrid.Columns[3].Visible = false;
                //}
                //TemplateGrid.Refresh();
                TemplateGrid.Refresh();
                //ClearFields();
                //txtFieldStartIndex.Text = "1";
            }
            else
            {
                MessageBox.Show(errorMsg);
            }

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            txtTemplateName.Cursor.ToString();
        }
        public void LoadTemplateToGrid(Template template)
        {
            TemplateGrid.DataSource=null;
           // template
           // TemplateGrid.au
            
            TemplateGrid.AutoGenerateColumns = false;
            TemplateGrid.Columns[0].Name = "slno";
            TemplateGrid.Columns[0].HeaderText = "Field ID";
            TemplateGrid.Columns[0].DataPropertyName = "StartIndex";
            
            TemplateGrid.Columns[1].Name = "TemplateID";
            TemplateGrid.Columns[1].HeaderText = "TemplateID";
            TemplateGrid.Columns[1].DataPropertyName = "TemplateID";
            TemplateGrid.Columns[2].Name = "FieldName";
            TemplateGrid.Columns[2].HeaderText = "Field Name";
            TemplateGrid.Columns[2].DataPropertyName = "FieldName";
            TemplateGrid.Columns[3].Name = "StartRowNo";
            TemplateGrid.Columns[3].HeaderText = "StartRowNo";
            TemplateGrid.Columns[3].DataPropertyName = "StartRowNo";
            TemplateGrid.Columns[4].Name = "StartIndex";
            TemplateGrid.Columns[4].HeaderText = "StartIndex";
            TemplateGrid.Columns[4].DataPropertyName = "StartIndex";
            TemplateGrid.Columns[5].Name = "FieldType";
            TemplateGrid.Columns[5].HeaderText = "FieldType";
            TemplateGrid.Columns[5].DataPropertyName = "TemplateFieldType";
            TemplateGrid.Columns[6].Name = "FieldTop";
           // template
            TemplateGrid.Columns[6].HeaderText = "FieldTop";
            TemplateGrid.Columns[6].DataPropertyName = "FieldTopPosition";
            TemplateGrid.Columns[7].Name = "FieldLeft";
            TemplateGrid.Columns[7].HeaderText = "FieldLeft";
            TemplateGrid.Columns[7].DataPropertyName = "FieldLeftPosition";
            TemplateGrid.Columns[8].Name = "FieldIndex";
            TemplateGrid.Columns[8].HeaderText = "FieldIndex";
            TemplateGrid.Columns[8].DataPropertyName = "FieldIndex";
            TemplateGrid.Columns[9].Name = "NoRows";
            TemplateGrid.Columns[9].HeaderText = "NoRows";
            TemplateGrid.Columns[9].DataPropertyName = "FieldRowNo";
            TemplateGrid.Columns[10].Name = "NoColumns";
            TemplateGrid.Columns[10].HeaderText = "NoColumns";
            TemplateGrid.Columns[10].DataPropertyName = "FieldColumnNo";
            TemplateGrid.Columns[11].Name = "BubbleWidth";
            TemplateGrid.Columns[11].HeaderText = "BubbleWidth";
            TemplateGrid.Columns[11].DataPropertyName = "BubbleWidth";
            TemplateGrid.Columns[12].Name = "BubbleHeight";
            TemplateGrid.Columns[12].HeaderText = "BubbleHeight";
            TemplateGrid.Columns[12].DataPropertyName = "BubbleHeight";
            TemplateGrid.Columns[13].Name = "BubbleRowGap";
            TemplateGrid.Columns[13].HeaderText = "BubbleRowGap";
            TemplateGrid.Columns[13].DataPropertyName = "BubbleRowGap";
            TemplateGrid.Columns[14].Name = "BubbleColumnGap";
            TemplateGrid.Columns[14].HeaderText = "BubbleColumnGap";
            TemplateGrid.Columns[14].DataPropertyName = "BubbleColumnGap";
            TemplateGrid.Columns[15].Name = "annotationWidth";
            TemplateGrid.Columns[15].HeaderText = "annotationWidth";
            TemplateGrid.Columns[15].DataPropertyName = "annotationWidth";
            TemplateGrid.Columns[16].Name = "annotationHeight";
            TemplateGrid.Columns[16].HeaderText = "annotationHeight";
            TemplateGrid.Columns[16].DataPropertyName = "annotationHeight";
            TemplateGrid.Columns[17].Name = "FieldID";
            TemplateGrid.Columns[17].HeaderText = "FieldID";
            TemplateGrid.Columns[17].DataPropertyName = "FieldID";
           // TemplateGrid.Columns[16].vi
            TemplateGrid.DataSource =template.FieldList;
            for (int row = 0; row < Template.FieldList.Count; row++)
            {

                TemplateGrid.Rows[row].Cells[0].Value = Template.FieldList[row].StartIndex;

            }
           
            
           // TemplateGrid.Refresh();
        }
        private void btnProjectSave_Click(object sender, EventArgs e)
        {
            Project = new ProjectDetails();
            Project.ProjectName = txtProjectName.Text.Trim();
            Project.ProjectCode = txtProjectCode.Text.Trim();
            Project = ProjectBL.Add(Project);
            if (string.IsNullOrEmpty(Project.ErrorMessge))
            {
                // MessageBox.Show("Project saved successfully");
                MessageBox.Show("Project saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<ProjectDetails> projectList = ProjectBL.GetProjects();
                dataGridViewProjectsList.AutoGenerateColumns = false;
                    dataGridViewProjectsList.Columns[0].Name="ProjectID";
                    dataGridViewProjectsList.Columns[0].HeaderText = "ProjectID";
                    dataGridViewProjectsList.Columns[0].DataPropertyName = "ProjectID";
                    dataGridViewProjectsList.Columns[1].Name = "ProjectnName";
                    dataGridViewProjectsList.Columns[1].HeaderText = "ProjectName";
                    dataGridViewProjectsList.Columns[1].DataPropertyName = "ProjectName";
                    dataGridViewProjectsList.Columns[2].Name="ProjectCode";
                    dataGridViewProjectsList.Columns[2].HeaderText = "ProjectCode";
                    dataGridViewProjectsList.Columns[2].DataPropertyName = "ProjectCode";
                    dataGridViewProjectsList.DataSource = projectList;
                txtProjectName.Clear();
                txtProjectCode.Clear();
                
            }
            else
            {
                // MessageBox.Show(Project.ErrorMessge);
                MessageBox.Show(Project.ErrorMessge, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int fieldDif = 0;
        public void SetPanelField(Template template, string fieldName)
        {
            TemplateField templateField = GetAndAssignExistingField(template, fieldName);
            if (templateField != null)
            {
                FieldLeft = templateField.FieldLeftPosition;
                FieldTop = templateField.FieldTopPosition;
                cmbFieldType.SelectedItem = templateField.TemplateFieldType.ToString();
                picPreview.Controls.RemoveByKey(txtFieldName.Text);
                txtannotationheight.Text = templateField.AnnotationHeight.ToString().Trim();
                txtannotationwidth.Text = templateField.AnnotationWidth.ToString().Trim();
                if (templateField.FieldID == 0)
                {
                    currentFieldName = templateField.FieldName;
                }
                Getvalues1();
            }
        }
        //public void LoadTemplateGrid(string templateID)
        //{

        //    List<TemplateField> fieldList = Template.Field();
        //    dataGridViewTemplates.DataSource = null;
        //    dataGridViewTemplates.DataSource = fieldList;
        //}
        private void TemplateGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //indexValues1[]

            SetPanelField(Template, TemplateGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
        }


        public void LoadProjectTemplates()
        {
            List<Template> templateList = TemplateBL.GetProjectTemplates(Project.ProjectID);
            if (templateList.Count != 0)
            {
                currentTemplateID = Template.TemplateID;
                dataGridViewTemplates.Columns[0].Visible = false;
                dataGridViewTemplates.AutoGenerateColumns = false;
                dataGridViewTemplates.Visible = true;
                dataGridViewTemplates.Columns[0].Name = "TemplateID";
                dataGridViewTemplates.Columns[0].HeaderText = "TemplateID";
                dataGridViewTemplates.Columns[0].DataPropertyName = "TemplateID";
                dataGridViewTemplates.Columns[1].Name = "TemplateName";
                dataGridViewTemplates.Columns[1].HeaderText = "Template Name";
                dataGridViewTemplates.Columns[1].DataPropertyName = "TemplateName";
                dataGridViewTemplates.Columns[2].Name = "TemplateCode";
                dataGridViewTemplates.Columns[2].HeaderText = "Template Code";
                dataGridViewTemplates.Columns[2].DataPropertyName = "TemplateCode";
                dataGridViewTemplates.Columns[3].Name = "TrackCount";
                dataGridViewTemplates.Columns[3].HeaderText = "Track Count";
                dataGridViewTemplates.Columns[3].DataPropertyName = "TrackCount";
                dataGridViewTemplates.Columns[4].Name = "TemplateTop";
                dataGridViewTemplates.Columns[4].HeaderText = "Template Top";
                dataGridViewTemplates.Columns[4].DataPropertyName = "TrackTop";
                dataGridViewTemplates.Columns[5].Name = "TemplateLeft";
                dataGridViewTemplates.Columns[5].HeaderText = "Template Left";
                dataGridViewTemplates.Columns[5].DataPropertyName = "TrackLeft";
                dataGridViewTemplates.Columns[6].Name = "Page";
                dataGridViewTemplates.Columns[6].HeaderText = "Page Type";
                dataGridViewTemplates.Columns[6].DataPropertyName = "Page";
                dataGridViewTemplates.Columns[7].Name = "IsDuplex";
                dataGridViewTemplates.Columns[7].HeaderText = "IsDuplex";
                dataGridViewTemplates.Columns[7].DataPropertyName = "IsDuplex";
                dataGridViewTemplates.Columns[8].Name = "Template FileName";
                dataGridViewTemplates.Columns[8].HeaderText = "TemplateFileName";
                dataGridViewTemplates.Columns[8].DataPropertyName = "TemplateFileName";
                dataGridViewTemplates.DataSource = templateList;
            }
           
           
        }


        private void btnErrors_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewTemplates_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        bool FOUND;
        public int currentTemplateID = 0;
        private void dataGridViewTemplates_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            picPreview.Image = null;
            TemplateGrid.DataSource = null;
            TemplateGrid.AutoGenerateColumns = false;
            picPreview.Controls.Clear();
            textResult.Clear();
            textResult.Visible = false;
            //n = 0;
            //tabControl1.TabPages.Add(
            //txtTemplateName.Text = dataGridViewTemplates.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            //txtTemplateCode.Text = dataGridViewTemplates.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            //List<TemplateField> fieldList = TemplateBL.GetTemplateFields(Convert.ToInt32(dataGridViewTemplates.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()));
            button1_Click(sender, e);
            if (e != null)
            {
                currentTemplateID = Convert.ToInt32(dataGridViewTemplates.Rows[e.RowIndex].Cells["TemplateID"].Value.ToString().Trim());
            }
            else
            {
                foreach (DataGridViewRow Datarow in dataGridViewTemplates.Rows)
                {

                    if ((Datarow.Cells[1].Value.ToString() ==tempname) && (Datarow.Cells[2].Value.ToString() ==tempcode))
                    {
                        currentTemplateID = Convert.ToInt32(Datarow.Cells[0].Value.ToString());

                    }


                }
               
            }
            
            Template template = TemplateBL.GetTemplateByID(currentTemplateID);
            this.Template = template;

            if (template != null)
            {
                if (tabControl1.TabPages.Count == 1)
                {
                    tabControl1.TabPages.Add(savedTabPages[1]);
                }
                n = 0;
                //else
                //{
                //    tabControl1.TabPages.Remove(savedTabPages[1]);
                //}
              //  tabControl1.TabPages.Remove(savedTabPages[0]);
                label15.Visible = true;
                txtThreshold.Visible = true;
                txtTemplateName.Text = template.TemplateName;
                txtTemplateCode.Text = template.TemplateCode; //txtFieldStartIndex.Text = "1";
                lblTrackCount.Text = "No of Tracks :" + template.TrackCount.ToString();
                lblTrackCount.Refresh();
                if (template.Page == (PageType)Enum.Parse(typeof(PageType), "FRONT"))
                {
                    rdFront.Checked = true;
                }
                else
                {
                    rdBack.Checked = true;
                }
                TemplateGrid.DataSource = null;
                LoadTemplateToGrid(template);
                
                if (template.FrontBackTemplateMapperList != null & template.FrontBackTemplateMapperList.Count > 0)
                {
                    SetFrontPageMapping(template.FrontBackTemplateMapperList);
                }
                LoadTemplateImage(template);
                TrackLeft = template.TrackLeft;
                TrackTop = template.TrackTop;
                tabControl1.SelectedIndex = 1;
                picPreview_MouseDoubleClick(sender, e);
               
                if (FOUND)
                {
                    int i = 0;
                    template.FieldList.ForEach(f =>
                    {
                        //TemplateGrid.da
                        if (i == 0)
                        {
                            SetPanelField(template, f.FieldName);
                        }
                        //if (i != 0)
                        //{
                        //    TemplateGrid.Rows[i-1].Selected =false;
                        //}
                        //else
                        //{
                            
                        //}
                        i++;
                    }
                    );
                    if (TemplateGrid.Rows.Count >= 1)
                    {
                        TemplateGrid.Rows[0].Selected = true;
                    }
                 //   TemplateGrid.TabIndex = Template.FieldList.Count - 1;
                   // TemplateGrid.RowsDefaultCellStyle.
                   
                }

            }
            else
            {
                //    MessageBox.Show("No Fields Found");
                MessageBox.Show("No Fields Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        public void SetFrontPageMapping(List<FrontBackPageMapper> mapperList)
        {
            //for (int i = 0; i < checkedListBoxFrontPageTemplates.Items.Count; i++)
            //{
            //    int frontPageTemplateID = Convert.ToInt32(((System.Web.UI.WebControls.ListItem)checkedListBoxFrontPageTemplates.Items[i]).Value);
            //    if (mapperList.Where(m => m.FrontPageTemplateID == frontPageTemplateID).Any())
            //    {
            //        checkedListBoxFrontPageTemplates.SetItemChecked(i, true);
            //    }
            //    else
            //    {
            //        checkedListBoxFrontPageTemplates.SetItemChecked(i, false);
            //    }
            //}
        }

        public void LoadTemplateImage(Template template)
        {
            if (File.Exists(template.TemplateFilePath))
            {
                CreateBitmap(template.TemplateFilePath);
                // templateBitmap =new Bitmap( picPreview.Image);
            }
            else if (!string.IsNullOrEmpty(template.TemplateImage))
            {

            }


        }

        private void btnTemplateSave_Click(object sender, EventArgs e)
        {
            if (projectID!= 0)
            {
                string validationMessages = ValidateTemplate();
                if (string.IsNullOrEmpty(validationMessages))
                {
                    Template.Project = Project;
                    Template.Page = rdFront.Checked ? PageType.FRONT : PageType.BACK;
                    Template.TemplateName = txtTemplateName.Text.Trim();
                    Template.TemplateCode = txtTemplateCode.Text.Trim();
                    Template.TrackIndex = trackindex;
                    Template.TrackLeft = TrackLeft;
                    Template.TrackTop = TrackTop;
                    Template.AllowedErrorCharCount = int.Parse(textAllowedCount.Text.ToString().Trim());
                    string[] file=label1.Text.Split('\\');
                    Template.TemplateFileName =file[file.Length-1].Trim();
                    Template.TemplateFilePath = label1.Text.Trim();
                    if (rdBack.Checked)
                    {
                        
                        AssignFrontPageMapping(Template);
                    }

                    if (Template.TemplateID == 0)
                    {

                        //validate for already exisiting template name
                        List<Template> templateList = TemplateBL.GetProjectTemplates(Project.ProjectID);
                       // Template.Project.ProjectID = projectID;
                        if (!templateList.Where(t => t.TemplateName == Template.TemplateName).Any())
                        {
                            Template.Add();
                            // MessageBox.Show("Template saved successfully");
                            MessageBox.Show("Template saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            

                        }
                        else
                        {
                            MessageBox.Show("Template already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        
                    }
                    else
                    {
                        Template.Update();
                        // MessageBox.Show("Template Updated successfully");
                        MessageBox.Show("Template updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    
                    LoadProjectTemplates();
                    tempcode = txtTemplateCode.Text;
                    tempname = txtTemplateName.Text;
                    if (rdBack.Checked)
                    {
                        currentTemplateID = Template.FrontBackTemplateMapperList[0].FrontPageTemplateID;
                    }
                    //this.Template = template;
                   
                    
                    dataGridViewTemplates_CellMouseDoubleClick(sender, null);
                    //string 
                   
                    //loa

                }
                else
                {
                    MessageBox.Show(validationMessages);
                }


            }
            else
            {
                // MessageBox.Show("Please create/save the project");
                MessageBox.Show("Please create/save the project", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string tempname, tempcode;
        public void AssignFrontPageMapping(Template template)
        {
            template.FrontBackTemplateMapperList = new List<FrontBackPageMapper>();
            //for (int i = 0; i < checkedListBoxFrontPageTemplates.Items.Count; i++)
            //{
            //    if (checkedListBoxFrontPageTemplates.GetItemChecked(i))
            //    {
                    FrontBackPageMapper mapper = new FrontBackPageMapper();
                   // mapper.FrontPageTemplateID = Convert.ToInt32(((System.Web.UI.WebControls.ListItem)checkedListBoxFrontPageTemplates.Items[i]).Value);
                    mapper.FrontPageTemplateID = Convert.ToInt32(currentTemplateID);
                    mapper.BackPageTemplateID = currentTemplateID;
                    template.FrontBackTemplateMapperList.Add(mapper);
                    Template.IsDuplex = true;
            //    }
            //}
        }

        public string ValidateTemplate()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(textAllowedCount.Text))
            {
                errorMessage = errorMessage + Environment.NewLine + "Please check Allowed Character Count null not allowed";
            }
            if (string.IsNullOrEmpty(txtTemplateName.Text))
            {
                errorMessage = "Please enter the template name";
            }
            if (string.IsNullOrEmpty(txtTemplateCode.Text))
            {
                errorMessage = errorMessage + Environment.NewLine + "Please enter the template code";
            }
            if (string.IsNullOrEmpty(trackindex))
            {
                errorMessage = errorMessage + Environment.NewLine + "Please create the track index";
            }
            if (rdBack.Checked && string.IsNullOrEmpty(txtTemplateCode.Text))
            {
                errorMessage = errorMessage + Environment.NewLine + "Please select the front page tmplate(s)";
            }
            return errorMessage;
        }


        DirectoryInfo BaseFolder;
        private void OnGridDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["TemplateName"].Value = 0;
        }
        public void BindJobGrid()
        {
            DirectoryInfo[] subFolders = BaseFolder.GetDirectories();
            BaseFolderGrid.DataSource = null;
           
            BaseFolderGrid.DataSource = subFolders.Select(f => new { FolderName = f.Name.ToString() }).ToList();
           // BaseFolderGrid.Columns[0].Name = "Folder Name";
            BaseFolderGrid.Columns[0].HeaderText = "Folder Name";
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            
            col.DataSource = TemplateBL.GetProjectTemplates(Project.ProjectID).Where(t => t.Page == PageType.FRONT).Select(t => new { t.TemplateName, t.TemplateID }).ToList();
            col.Name= "TemplateName";
            col.HeaderText="Template Name";
            col.ValueMember = "TemplateID";
            col.DisplayMember = "TemplateName";
            col.DataPropertyName = "TemplateID";
           // col.
            BaseFolderGrid.Columns.Add(col);
           // col.DefaultCellStyle.f
            //OnGridDefaultValuesNeeded(sender, null);
            //BaseFolderGrid.Row.
           // this.BaseFolderGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(BaseFolderGrid_CellFormatting);
           
              
           

        }
        private void btnBrowsDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                //listBox1.Items.Clear();
               // BaseFolderGrid.AutoGenerateColumns = false;
                chekAll.Checked = false;
                FolderBrowserDialog fd = new FolderBrowserDialog();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    txtBaseFolder.Text = fd.SelectedPath;
                    BaseFolder = new DirectoryInfo(txtBaseFolder.Text);
                    BaseFolderGrid.Columns.Clear();
                    BindJobGrid();
                    //DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)BaseFolderGrid.Rows[0].Cells[0];
                    //cell.Items[0] = "Select";
                    
                   // ProjectName = projectList.Where(i => i.ProjectID == projectID).Select(j => new { j.ProjectName }).t;
                }
            }
            catch (Exception EX)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Template = new Template();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            btnAdd.Visible = false;
            btnSave.Visible = false;
            btnSaveTemplate.Visible = false;
            TemplateGrid.Visible =false;
            button5.Visible= false;
            FOUND = true;
           // button5.Visible = true;
            TrackLeft = 0;
            TrackTop = 0;
            lblTrackCount.Text = "";
            templateBitmap = null;
            CurrentFieldID = 0;
            currentFieldName = string.Empty;
            txtTemplateCode.Clear();
            txtTemplateName.Clear();
            TemplateGrid.DataSource = null;
            txtFieldStartIndex.Text = "1";
            rdBack.Checked = false;
            rdFront.Checked = false; 
            picPreview.Image = null;
            picPreview.Controls.Clear();
            ClearFields();
            groupBox1.Visible = false;
            TemplateGrid.Visible = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBaseFolder.Text))
            {
                JobDetails job = new JobDetails();
                job.BasePath = txtBaseFolder.Text.Trim();
                job.JobStartDate = DateTime.Now;
                job.Project = Project;
                job.Type = RunType.Normal;
                job.JobStatus = Status.STARTED;
                btnRun.Enabled = false;
                job.Threshold = Convert.ToInt32(txtThreshold.Text.Trim());
                bool status = false;
                AddTaskDetails(job, out status);
                if (status)
                {
                    Template.Project = Project;
                    dataGridSheetValues.DataSource = null;
                    Job outputJob = job.ValidateAndInsert();
                    if (outputJob != null)
                    {
                        if (string.IsNullOrEmpty(outputJob.ErrorMessge))
                        {
                            timerRefresh.Enabled = true;
                            timerRefresh.Start();

                            // ======================== ConsoleApplication Command ====================
                            //string pathToFile = null;
                            //pathToFile =  Application.StartupPath+ "\\OMRConsoleReader.exe";
                            //string cmdexePath = @"C:\Windows\System32\cmd.exe";
                            //string arg = outputJob.JobID.ToString();
                            //string cmdArguments = String.Format("/K {0}/hid", arg);
                            //ProcessStartInfo psi = new ProcessStartInfo(cmdexePath, cmdArguments);
                            //psi.WindowStyle = ProcessWindowStyle.Maximized;
                            //Process p = new Process();
                            //p.StartInfo = psi;
                            //p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                            //p.Start();
                            // =============================End===============================

                            //System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + "\\OMRConsoleReader.exe", outputJob.JobID.ToString());

                            //System.Diagnostics.Process p = new Process();
                            //p.StartInfo = info;
                            //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                            //p.Start();

                            RunProcess(outputJob.JobID);



                        }
                        else
                        {
                            // MessageBox.Show(outputJob.ErrorMessge);
                            // MessageBox.Show(outputJob.ErrorMessge, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Reprocess reprocess = new Reprocess();
                            reprocess.ShowDialog();
                            if (ProcessType != 0)
                            {
                                job = new JobDetails();
                                job.BasePath = txtBaseFolder.Text.Trim();
                                job.JobStartDate = DateTime.Now;
                                job.Project = Project;
                                job.JobStatus = Status.STARTED;
                                job.JobID = outputJob.JobID;
                                job.Threshold = Convert.ToInt32(txtThreshold.Text);

                                switch (ProcessType)
                                {
                                    //recover
                                    case 1:
                                        job.Type = RunType.Recovery;
                                        break;
                                    // reprocess
                                    case 2:
                                        job.Type = RunType.ForceOverwrite;
                                        break;
                                    //error run
                                    case 3:
                                        job.Type = RunType.ErrorOverwrite;
                                        break;
                                }
                                AddTaskDetails(job, out status);
                                job.UpdateJob();
                                timerRefresh.Enabled = true;
                                timerRefresh.Start();

                                RunProcess(outputJob.JobID);
                            }
                            else
                            {
                                MessageBox.Show("Process cancelled");
                            }


                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select BaseFolder");
            }

        }



        public void RunProcess(int jobID)
        {

            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + "\\OMRConsoleReader.exe";
            process.StartInfo.Arguments = jobID.ToString();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();

            ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(WaitForProc), process);
        }



        private void WaitForProc(object obj)
        {
            var proc = (Process)obj;
           
            proc.WaitForExit();
            LoadSheetValuesGrid();
            // timerRefresh_Tick(
            MessageBox.Show("Done");
            timerRefresh.Stop();
            btnRun.Enabled =true;
            timerRefresh.Enabled = false;
            
            // Do the file deletion here
           
        }

        public void AddTaskDetails(JobDetails job,out bool status)
        {
            status =true;
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
            int n = 1;
            foreach (DataGridViewRow row in BaseFolderGrid.Rows)
            {
                TaskDetails task = new TaskDetails();
                task.FolderPath = job.BasePath + "\\" + row.Cells[0].Value.ToString();
                task.TotalFileCount = Directory.GetFiles(task.FolderPath, "*.jpg").Count();   // implement filter only image files
               // BaseFolderGrid.dis = (cmb.Items[0] as DataRowView).Row[1].ToString();
                //if (!chekAll.Checked)
                //{
               // Combobox box = (Combobox)(row.FindControl("[TemplateName")
                if (!(string.IsNullOrEmpty(Convert.ToString(row.Cells[1].Value))))
                {
                    task.MatchingTemplate.TemplateID = Convert.ToInt32(row.Cells[1].Value);
                    task.RunType = job.Type;
                    job.TaskList.Add(task);
                    status=true;
                   
                }
                else
                {

                    MessageBox.Show("Row No "+n+" Template Not Selected.Please Select");
                    status=false;
                    return;
                }
                n++;
               // return status;
            }
           

        }

        private void lblCoordinates_Click(object sender, EventArgs e)
        {

        }
        private static bool IsAlreadyRunning()
        {
            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            //Mutex mutex = new Mutex(true, "Global\\" + OMRConsoleReader, out bCreatedNew);
            //Mutex mutex = new Mutex(true, "Global\\" + OMRConsoleReader, out bCreatedNew);
            string sExeName = "OMRConsoleReader";
            bool bCreatedNew;

            Mutex mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
            //if (bCreatedNew)
            //    mutex.ReleaseMutex();

            return !bCreatedNew;
        }
        private void timerRefresh_Tick(object sender, EventArgs e)
        {

            LoadSheetValuesGrid2();
            bool app = true;
            if (!app)
            {
                MessageBox.Show("Application Stoped");
            }

            
        }

        void LoadSheetValuesGrid()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(InnerLoadData));
        }
        public void LoadSheetValuesGrid2()
        {
            InnerLoadData();
        }
        void InnerLoadData()
        {

            if (dataGridSheetValues.DataSource == null)
            {
                dataGridSheetValues.DataSource = SheetValuesBL.GetSheetValues(this.Project.ProjectID).Tables[0];
                // dataGridSheetValues.Refresh();
                if (dataGridSheetValues.RowCount > 1)
                {
                    dataGridSheetValues.FirstDisplayedScrollingRowIndex = dataGridSheetValues.RowCount - 1;
                    dataGridSheetValues.Rows[dataGridSheetValues.RowCount - 1].Selected = true;
                }
                //
            }
            else
            {
                DataSet newDs = SheetValuesBL.GetSheetValues(this.Project.ProjectID);
                if (newDs != null && newDs.Tables.Count != 0)
                {
                    DataTable dt = dataGridSheetValues.DataSource as DataTable;
                    dt.Merge(newDs.Tables[0]);
                    dataGridSheetValues.DataSource = dt;
                    // dataGridSheetValues.Refresh();
                    if (dataGridSheetValues.RowCount > 1)
                    {
                        dataGridSheetValues.FirstDisplayedScrollingRowIndex = dataGridSheetValues.RowCount - 1;
                        dataGridSheetValues.Rows[dataGridSheetValues.RowCount - 1].Selected = true;
                    }
                    // dataGridSheetValues.CurrentRow.Selected = true;
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                threshold = ((int.Parse(txtBubbleWidth.Text)) * (int.Parse(txtBubbleHeight.Text)) * (int.Parse(txtThreshold.Text))) / 100;
                picPreview.Controls.Owner.Refresh();
            }
            catch
            {
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // int n=  this.tabControl1.SelectedIndex;
            //TabPage page = tabControl1.TabPages[n];
            //page.Name.b= Color.BlueViolet;
            //page.ForeColor = Color.White;
            //e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);

            //Rectangle paddedBounds = e.Bounds;
            //int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            //paddedBounds.Offset(1, yOffset);
            //TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, page.ForeColor);
        }
        int show = 1;
        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
            //if (show == 1)
            //{
            //    bool hasError = false;
            //    int templateDiffLeft = 0, templateDiffTop = 0;
            //    TrackReader.ReadTrack(Template.TrackLeft, Template.TrackTop, templateBitmap,0,0,out templateDiffLeft, out templateDiffTop);
            //    diff1 = templateDiffTop;
            //    leftDiff = templateDiffLeft;
            //    ArrayList trackArray = new ArrayList();
            //    indexValues1 =new ArrayList();
            //    //Template.TrackLeft=
            //    trackArray.AddRange(Template.TrackIndex.Split(','));
            //    indexValues1 = trackArray;
            //    if (TemplateReader.IsMatch(templateBitmap, trackArray, Template.TrackCount, templateDiffLeft, templateDiffTop, Template.TrackTop, Template.TrackLeft))
            //    {
            //        //Getvalues1();
            //        string OMRString = TemplateReader.GetSheetValues(templateBitmap, Template, templateDiffTop, templateDiffLeft, indexValues1, Convert.ToInt32(txtThreshold.Text), out hasError);
            //        textResult.Visible = true;
            //        int n1 = 0, n2 = 1;
            //        textResult.Text = "SheetValues is:" + System.Environment.NewLine + "==================" + System.Environment.NewLine;
            //        for (int i = 0; i < TemplateGrid.RowCount; i++)
            //        {
            //            if (TemplateGrid.Rows[i].Cells[5].Value.ToString() == "VER")
            //            {
            //                n2 = Convert.ToInt32(TemplateGrid.Rows[i].Cells[10].Value);
            //                textResult.Text = textResult.Text + TemplateGrid.Rows[i].Cells[2].Value+": " + OMRString.Substring(n1, n2) + System.Environment.NewLine;
            //                n1 = n1 + n2;
            //                textResult.Refresh();
            //            }
            //            else
            //            {
            //                n2 = Convert.ToInt32(TemplateGrid.Rows[i].Cells[9].Value);
            //                textResult.Text = textResult.Text + TemplateGrid.Rows[i].Cells[2].Value+": " + OMRString.Substring(n1, n2) + System.Environment.NewLine;
            //                n1 = n1 + n2;
            //                textResult.Refresh();
            //            }
            //        }
            //        show = 2;
            //    }
               
            //}
            //else
            //{
            //    textResult.Visible = false;
            //    show = 1;
            //}
        }

        private void OMRTemplateManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("OMRConsoleReader");
            foreach (var process in processes)
            {
                process.Kill();
            }
            Process[] processes1 = Process.GetProcessesByName("cmd");
            foreach (var process in processes1)
            {
                process.Kill();
            }
         Home h = new Home();
         h.Visible = true;

        }
        //public void LoadFrontPageTemplates()
        //{
        //    //int projectID = Convert.ToInt32(dataGridViewProjectsList.Rows[e.RowIndex].Cells[0].Value.ToString().Trim());
        //    List<Template> templateList = TemplateBL.GetFrontPageTemplates(projectID);
        //    if (templateList != null && templateList.Count > 0)
        //    {
        //        MessageBox.Show("Please select the Front page template now");
        //        templateList.ForEach(t =>
        //            {
        //                checkedListBoxFrontPageTemplates.Items.Add(new System.Web.UI.WebControls.ListItem(t.TemplateName, t.TemplateID.ToString()));
        //            });
               
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please create the Front page template(s) first");
        //    }
        //}
        private void rdBack_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (currentTemplateID != 0)
                {
                    if (n == 0)
                    {
                        n = 1;
                        ClearFields();
                        textResult.Clear();
                        textResult.Visible = false;
                        Template = LoadBackTemplate();
                        if (Template != null)
                        {
                            LoadTemplateImage(Template);
                            TrackLeft = Template.TrackLeft;
                            TrackTop = Template.TrackTop;
                            tabControl1.SelectedIndex = 1;
                            picPreview_MouseDoubleClick(sender, null);

                            if (FOUND)
                            {
                                int i = 0;
                                Template.FieldList.ForEach(f =>
                                {
                                    //TemplateGrid.da
                                    if (i == 0)
                                    {
                                        SetPanelField(Template, f.FieldName);
                                    }

                                    i++;
                                }
                                );
                                if (TemplateGrid.ColumnCount >= 1)
                                {
                                    TemplateGrid.Rows[0].Selected = true;
                                    // TemplateGrid.Columns[3].Visible = false;
                                }

                            }
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("Do you want to Create Back Template?", "Confirmation", MessageBoxButtons.YesNoCancel);
                            if (result == DialogResult.Yes)
                            {
                                button1_Click(sender, e);
                                rdBack.Checked = true;
                                // Template.Page = PageType.BACK;
                            }
                            if (result == DialogResult.Cancel)
                            {
                                return;
                            }
                            if (result == DialogResult.No)
                            {
                                rdFront.Checked = true;
                            }


                        }
                    }
                }
               if(rdBack.Checked&&currentTemplateID==0)
                {
                    MessageBox.Show("Front Template Not Exist.Please Create Front Template First");
                    rdFront.Checked = true;
                   // n = 1;
                }
            }
            catch
            {
            }
        }

        private Template LoadBackTemplate()
        {
            picPreview.Image = null;
            TemplateGrid.DataSource = null;
            picPreview.Controls.Clear();

            //tabControl1.TabPages.Add(
            //txtTemplateName.Text = dataGridViewTemplates.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            //txtTemplateCode.Text = dataGridViewTemplates.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            //List<TemplateField> fieldList = TemplateBL.GetTemplateFields(Convert.ToInt32(dataGridViewTemplates.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()));
            Template template = TemplateBL.GetBackTemplateByFrontTemplateID(currentTemplateID);
            this.Template = template;

            if (template != null)
            {
                //if (tabControl1.TabPages.Count ==1)
                //{
                //    tabControl1.TabPages.Add(savedTabPages[1]);
                //}
               
               // tabControl1.TabPages.Add(savedTabPages[1]);
                //  tabControl1.TabPages.Remove(savedTabPages[0]);
                label15.Visible = true;
                txtThreshold.Visible = true;
                txtTemplateName.Text = template.TemplateName;
                txtTemplateCode.Text = template.TemplateCode; //txtFieldStartIndex.Text = "1";
                if (template.Page == (PageType)Enum.Parse(typeof(PageType), "FRONT"))
                {
                    rdFront.Checked = true;
                }
                else
                {
                    rdBack.Checked = true;
                }
                TemplateGrid.DataSource = null;
                //  IBindingList ib = Template.FieldList;
                //BindingList<Customer> bindingList = new BindingList<Customer>(Template.FieldList);
                TemplateGrid.DataSource = template.FieldList;
                //for (int i = 2; i <= TemplateGrid.ColumnCount - 1; i++)
                //{
                //    TemplateGrid.Columns[i].Visible = false;
                //    // TemplateGrid.Columns[3].Visible = false;
                //}
                ////  TemplateGrid.Columns[0].Visible = false;
                // TemplateGrid.Rows[TemplateGrid.Rows.Count - 1].Cells[0].Selected= true;
                if (template.FrontBackTemplateMapperList != null & template.FrontBackTemplateMapperList.Count > 0)
                {
                    SetFrontPageMapping(template.FrontBackTemplateMapperList);
                }
                

            }
            //else
            //{
            //    //    MessageBox.Show("No Fields Found");
            //    MessageBox.Show("No Fields Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            return template;
        }

       

        private void btnStop_Click(object sender, EventArgs e)
        {           
            
                Process[] processes = Process.GetProcessesByName("OMRConsoleReader");
                foreach (var process in processes)
                {
                    process.Kill();
                }
                Process[] processes1 = Process.GetProcessesByName("cmd");
                foreach (var process in processes1)
                {
                    process.Kill();
                }
                btnRun.Enabled =true;
        }

       

        private void picPreview_LocationChanged(object sender, EventArgs e)
        {
            
        }
        Point MouseDownLocation;
        private void picPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }
        public int projectID=0;
        private void dataGridViewProjectsList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OMRTemplateManager templateManager = new OMRTemplateManager();
            projectID = Convert.ToInt32(dataGridViewProjectsList.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
            projectList = ProjectBL.GetProjectByID(projectID);
            Project = new ProjectDetails();
            Project.ProjectID = projectList[0].ProjectID;
            Project.ProjectName= projectList[0].ProjectName;
            Project.ProjectCode= projectList[0].ProjectCode;
            LoadTemplate(Project.ProjectID);
            lblProjectNmae.Text = "Project Name : " + dataGridViewProjectsList.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            lblProjectNmae.Refresh();
        }

        private void LoadTemplate(int projectID)
        {
            List<Template> templateList = TemplateBL.GetProjectTemplates(projectID);
            if (templateList.Count != 0)
            {
                dataGridViewTemplates.Columns[0].Visible = false;
                dataGridViewTemplates.AutoGenerateColumns = false;
                dataGridViewTemplates.Visible = true;
                dataGridViewTemplates.Columns[0].Name = "TemplateID";
                dataGridViewTemplates.Columns[0].HeaderText = "TemplateID";
                dataGridViewTemplates.Columns[0].DataPropertyName = "TemplateID";
                dataGridViewTemplates.Columns[1].Name = "TemplateName";
                dataGridViewTemplates.Columns[1].HeaderText = "Template Name";
                dataGridViewTemplates.Columns[1].DataPropertyName = "TemplateName";
                dataGridViewTemplates.Columns[2].Name = "TemplateCode";
                dataGridViewTemplates.Columns[2].HeaderText = "Template Code";
                dataGridViewTemplates.Columns[2].DataPropertyName = "TemplateCode";
                dataGridViewTemplates.Columns[3].Name = "TrackCount";
                dataGridViewTemplates.Columns[3].HeaderText = "Track Count";
                dataGridViewTemplates.Columns[3].DataPropertyName = "TrackCount";
                dataGridViewTemplates.Columns[4].Name = "TemplateTop";
                dataGridViewTemplates.Columns[4].HeaderText = "Template Top";
                dataGridViewTemplates.Columns[4].DataPropertyName = "TrackTop";
                dataGridViewTemplates.Columns[5].Name = "TemplateLeft";
                dataGridViewTemplates.Columns[5].HeaderText = "Template Left";
                dataGridViewTemplates.Columns[5].DataPropertyName = "TrackLeft";
                dataGridViewTemplates.Columns[6].Name = "Page";
                dataGridViewTemplates.Columns[6].HeaderText = "Page Type";
                dataGridViewTemplates.Columns[6].DataPropertyName = "Page";
                dataGridViewTemplates.Columns[7].Name = "IsDuplex";
                dataGridViewTemplates.Columns[7].HeaderText = "IsDuplex";
                dataGridViewTemplates.Columns[7].DataPropertyName = "IsDuplex";
                dataGridViewTemplates.Columns[8].Name = "TemplateFileName";
                dataGridViewTemplates.Columns[8].HeaderText = "Template FileName";
                dataGridViewTemplates.Columns[8].DataPropertyName = "TemplateFileName";
                dataGridViewTemplates.DataSource = templateList;
                for (int I = 0; I < dataGridViewTemplates.Rows.Count; I++)
                {
                    //dataGridViewTemplates.Columns[I].=
                }
                label16.Text = "Selected Project No Templates:" + templateList.Count;
                label16.ForeColor = Color.Green;
                label16.Refresh();
                linkLblCreateTemplate.Visible =true;

               // dataGridViewTemplates.AutoSizeColumnsMode=
            }
            else
            {
                dataGridViewTemplates.Visible = false;
                MessageBox.Show("No Templates");
                label16.Text = "In this Project Templates are Not Created";
                label16.ForeColor = Color.Red;
                label16.Refresh();
                linkLblCreateTemplate.Visible = true;

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(savedTabPages[1]);
         
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // tabControl1.TabPages.Remove(savedTabPages[1]);
        }

        private void tabPage1_MouseClick(object sender, MouseEventArgs e)
        {
            //tabControl1.TabPages.Remove(savedTabPages[1]);
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(savedTabPages[1]);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab != savedTabPages[1])
            {
               // tabControl1.TabPages.Remove(savedTabPages[1]);
                label15.Visible = false;
                txtThreshold.Visible = false;
                lblCoordinates.Visible = false;
            }
            else
            {
                //label15.Visible = false;
                if (tabControl1.SelectedTab.Text == "Template Details")
                {
                    label15.Visible = true;
                    txtThreshold.Visible = true;
                    lblCoordinates.Visible = true;
                }

            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewProjectsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewTemplates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
        int n = 0;
        private void rdFront_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFront.Checked)
            {
                if (n == 1)
                {
                    ClearFields();
                    dataGridViewTemplates_CellMouseDoubleClick(sender, null);
                    if (n == 1)
                    {
                        button1_Click(sender, e);
                    }
                    //n = 0;
                }
            }
        }

        private void chekAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chekAll.Checked&& !string.IsNullOrWhiteSpace(txtBaseFolder.Text))
            {
               DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
               foreach (DataGridViewRow row in BaseFolderGrid.Rows)
               {
                   row.Cells[1].Value = BaseFolderGrid.Rows[0].Cells[1].Value;
                   cmb.DisplayMember= BaseFolderGrid.Rows[0].Cells[1].FormattedValue.ToString();
               }
                
            }
           
           
        }

        private void BaseFolderGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void txtFieldStartIndex_Leave_1(object sender, EventArgs e)
        {
            try
            {
                if (Template.TrackCount > Convert.ToInt32(txtFieldStartIndex.Text))
                {
                }
                else
                {
                    MessageBox.Show("Start index is between 1 to" + (Template.TrackCount - 1).ToString()); txtFieldStartIndex.Focus();


                }
            }
            catch
            {
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tabControl1.TabPages.Count == 1)
            {
               
                tabControl1.TabPages.Add(savedTabPages[1]);
                Template = new Template();
                TrackLeft = 0;
                TrackTop = 0;
                templateBitmap = null;
                CurrentFieldID = 0;
                currentFieldName = string.Empty;
                txtTemplateCode.Clear();
                txtTemplateName.Clear();
                TemplateGrid.DataSource = null;
                txtFieldStartIndex.Text = "1";
                rdBack.Checked = false;
                rdFront.Checked = false; picPreview.Image = null;
                picPreview.Controls.Clear();
                ClearFields();
                groupBox1.Visible = false;
                TemplateGrid.Visible = false;
            }
            //else
            //{
            //    tabControl1.TabPages.Remove(savedTabPages[1]);
            //}
            //  tabControl1.TabPages.Remove(savedTabPages[0]);
            n =0;
            button1_Click(sender, e);
            label15.Visible = true;
            txtThreshold.Visible = true;
            tabControl1.SelectedIndex = 1;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtFieldName_TextChanged(object sender, EventArgs e)
        {
            
                currentFieldName = txtFieldName.Text;
                CurrentFieldID = 0;
        }

        private void txtThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Please provide number between 1 and 99 only");
                txtThreshold.Text ="10";
            }
        }

        //private void dataGridViewProjectsList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void dataGridViewProjectsList_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        //{

        //}

       
        

    }
}
