using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Menta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
           /* e.Graphics.FillRectangle(Brushes.Green, Top);
            e.Graphics.FillRectangle(Brushes.Green, Left);
            e.Graphics.FillRectangle(Brushes.Green, Right);
            e.Graphics.FillRectangle(Brushes.Green, Bottom); */
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 4; // you can rename this variable if you like

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }

        private void Resize_Tick(object sender, EventArgs e)
        {
            // 4, 85, 216

            int margin = 2;
            this.BasePanel.Location = new Point(GapPanel.Left + margin, GapPanel.Top + GapPanel.Height);
            this.BasePanel.Width = this.Width - (margin * 2);
            this.BasePanel.Height = this.Height - (TopPanel.Height + GapPanel.Height + margin);
        }

        private void QuickSurvey()
        {
            int sFactor = 20;

            ExtendedPanel surveyPanel = new ExtendedPanel();
            surveyPanel.Width = 500;
            surveyPanel.Height = 300;
            surveyPanel.Location = new Point((BasePanel.Width / 2) - surveyPanel.Width / 2, BasePanel.Height / 2 - surveyPanel.Height / 2);
            surveyPanel.BackColor = Color.Tomato;
            surveyPanel.Opacity = 50;

            int factor = -35;

            Bunifu.Framework.UI.BunifuImageButton smile = new Bunifu.Framework.UI.BunifuImageButton();
            smile.Image = Image.FromFile(Environment.CurrentDirectory + @"\Icons\happy-480.png");
            smile.BackColor = Color.Transparent;
            smile.SizeMode = PictureBoxSizeMode.Zoom;
            smile.Zoom = 0;
            smile.Width = 100;
            smile.Height = 100;
            smile.Location = new Point((surveyPanel.Width / 3) - (smile.Width - factor), surveyPanel.Top + 10);

            Bunifu.Framework.UI.BunifuImageButton medium = new Bunifu.Framework.UI.BunifuImageButton();
            medium.Image = Image.FromFile(Environment.CurrentDirectory + @"\Icons\happy-480.png");
            medium.BackColor = Color.Transparent;
            medium.SizeMode = PictureBoxSizeMode.Zoom;
            medium.Zoom = 0;
            medium.Width = 100;
            medium.Height = 100;
            medium.Location = new Point((surveyPanel.Width / 3)*2 - (medium.Width - factor), surveyPanel.Top + 10);

            Bunifu.Framework.UI.BunifuImageButton sad = new Bunifu.Framework.UI.BunifuImageButton();
            sad.Image = Image.FromFile(Environment.CurrentDirectory + @"\Icons\happy-480.png");
            sad.BackColor = Color.Transparent;
            sad.SizeMode = PictureBoxSizeMode.Zoom;
            sad.Zoom = 0;
            sad.Width = 100;
            sad.Height = 100;
            sad.Location = new Point((surveyPanel.Width / 3)*3 - (sad.Width - factor), surveyPanel.Top + 10);


            surveyPanel.Controls.Add(smile);
            surveyPanel.Controls.Add(medium);
            surveyPanel.Controls.Add(sad);

            BasePanel.Controls.Add(surveyPanel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BasePanel.Dock = DockStyle.None;
            Resize.Start();
            QuickSurvey();
        }

        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}
