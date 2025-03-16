using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BarWin
{
    public partial class BarWinMain : Form
    {
        ZXing.BarcodeWriter barcodeWriter;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public BarWinMain()
        {
            InitializeComponent();
            barcodeWriter = new ZXing.BarcodeWriter()
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions() { Height = Convert.ToInt32(kryptonPictureBox1.Height), Width = Convert.ToInt32(kryptonPictureBox1.Width), Margin = Convert.ToInt32(1) },
                Renderer = new ZXing.Rendering.BitmapRenderer() { Background = Color.White, Foreground = Color.Black }
            };
            pnlBWHeader.MouseMove += PnlBWHeader_MouseMove;
            pnlBWHeader.MouseUp += PnlBWHeader_MouseUp;
            pnlBWHeader.MouseDown += PnlBWHeader_MouseDown;
        }

        private void PnlBWHeader_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void PnlBWHeader_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void PnlBWHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            kryptonPanel2.Visible = !kryptonPanel2.Visible;
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLinear_Click(object sender, EventArgs e)
        {
            navBar.SelectedPage = pageLinear;
        }

        private void btnMatrix_Click(object sender, EventArgs e)
        {
            navBar.SelectedPage = pageMatrix;
        }

        private void GenerateCode(string Text)
        {
            try
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    kryptonPictureBox1.Image = barcodeWriter.Write(Text);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnShowHidePass_Click(object sender, EventArgs e)
        {
            btnShowHidePass.Text = btnShowHidePass.Text == "Hide" ? "Show" : "Hide";
            tbPass.UseSystemPasswordChar = !tbPass.UseSystemPasswordChar;
        }

        private void tbMtxText_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(tbMtxText.Text);
        }

        private void tbWiFi_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateWiFi(tbSSID.Text, tbPass.Text, hidden: kryptonCheckBox1.Checked));
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateWiFi(tbSSID.Text, tbPass.Text, hidden: kryptonCheckBox1.Checked));
        }

        private void tbvCard_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateVCard(tbName.Text, tbPhone.Text, tbvEmail.Text, tbAddress.Text, tbOrg.Text));
        }

        private void tbBizCard_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateBizCard(tbFName.Text, tbLName.Text, tbBPhone.Text, tbBEmail.Text, tbCompany.Text));
        }

        private void tbTele_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateTelephone(tbTele.Text));
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateEmail(tbEmailAddr.Text, tbSubject.Text, tbBody.Text));
        }

        private void tbPlayURL_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GeneratePlayStoreLink(tbPlayURL.Text));
        }

        private void tbSMS_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateSMS(tbNum.Text, tbMsg.Text));
            
        }

        private void tbCal_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateCalendarEvent(tbEventName.Text, tbEventStart.Value, tbEventEnd.Value, tbEventLoc.Text, tbEventDesc.Text));
        }

        private void tbWA_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateWhatsApp(tbWAPhone.Text, tbWAMsg.Text));
        }

        private void tbURL_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateURL(tbURL.Text));
        }

        private void tbGeo_ValueChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateGeoLocation(Convert.ToDouble(nudLat.Value), Convert.ToDouble(nudLong.Value), Convert.ToInt32(nudZoom.Value)));
        }

        private void tbCalV_ValueChanged(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateCalendarEvent(tbEventName.Text, tbEventStart.Value, tbEventEnd.Value, tbEventLoc.Text, tbEventDesc.Text));
        }
    }
}
