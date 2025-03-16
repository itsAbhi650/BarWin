using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;

namespace BarWin
{
    public partial class Form1 : Krypton.Toolkit.KryptonForm
    {
        ZXing.BarcodeWriter barcodeWriter;
        public Form1()
        {
            InitializeComponent();
            barcodeWriter = new ZXing.BarcodeWriter()
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new EncodingOptions() { Height = Convert.ToInt32(numHeight.Value), Width = Convert.ToInt32(numWidth.Value), Margin = Convert.ToInt32(numMargin.Value) },
                Renderer = new ZXing.Rendering.BitmapRenderer() { Background = Color.White, Foreground = Color.Black }
            };
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ActiveTabTag = Convert.ToString(tabControl1.SelectedTab.Tag);
            string CurrentString = "";
            switch (ActiveTabTag)
            {
                case "text":
                    CurrentString = textBox1.Text;
                    break;
                case "wifi":
                    CurrentString = QRCodeContentFormatter.GenerateWiFi(txtSSID.Text, txtPass.Text, hidden: chkHidden.Checked);
                    break;
                case "vcard":
                    CurrentString = QRCodeContentFormatter.GenerateVCard(txtName.Text, txtPhone.Text, txtVEmail.Text, txtAddress.Text, txtOrg.Text, txtVUrl.Text);
                    break;
                case "bizcard":
                    CurrentString = QRCodeContentFormatter.GenerateBizCard(txtFirstName.Text, txtLastName.Text, txtBizPhone.Text, txtBizEmail.Text, txtCompany.Text);
                    break;
                case "telephone":
                    CurrentString = QRCodeContentFormatter.GenerateTelephone(txtTel.Text);
                    break;
                case "url":
                    CurrentString = QRCodeContentFormatter.GenerateURL(txtURL.Text);
                    break;
                case "email":
                    CurrentString = QRCodeContentFormatter.GenerateEmail(txtEmail.Text);
                    break;
                case "playstore":
                    CurrentString = QRCodeContentFormatter.GeneratePlayStoreLink(txtPlayLink.Text);
                    break;
                case "sms":
                    CurrentString = QRCodeContentFormatter.GenerateSMS(txtSMSPhone.Text, txtSMSMsg.Text);
                    break;
                case "geo":
                    CurrentString = QRCodeContentFormatter.GenerateGeoLocation(Convert.ToDouble(txtLatitude.Text), Convert.ToDouble(txtLongitude.Text), Convert.ToDouble(nudZoom.Value));
                    break;
                case "calendar":
                    CurrentString = QRCodeContentFormatter.GenerateCalendarEvent(txtEvent.Text, dtpEventStart.Value, dtpEventEnd.Value, txtLocation.Text, txtDesc.Text);
                    break;
                case "whatsapp":
                    CurrentString = QRCodeContentFormatter.GenerateWhatsApp(txtWhatsappPhone.Text, txtWhatsappMessage.Text);
                    break;
            }
            GenerateCode(CurrentString);
        }

        private void GenerateCode(string Text)
        {
            try
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    outBox.Image = barcodeWriter.Write(Text);
                    AddHistory();
                }
                //textBox2.Text = string.Empty;
            }
            catch (Exception ex)
            {

                //textBox2.Text = ex.Message;
            }
        }

        private void AddHistory()
        {
            PictureBox HistoryItem = new PictureBox()
            {
                Margin = new Padding(10),
                Height = 80,
                Width = 80,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            HistoryItem.DoubleClick += HistoryItem_DoubleClick;
            MemoryStream ms = new MemoryStream();
            outBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            HistoryItem.Image = Image.FromStream(ms);
            historyPanel.Controls.Add(HistoryItem);
        }

        private void HistoryItem_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pBox = sender as PictureBox;
            pBox.Image.Dispose();
            pBox.Dispose();
            historyPanel.Controls.Remove(pBox);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GenerateCode(textBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Visible = false;
                textBox1.TextChanged += textBox1_TextChanged;
            }
            else
            {
                button1.Visible = true;
                textBox1.TextChanged -= textBox1_TextChanged;
            }
        }

        private void lblColorBox_DoubleClick(object sender, EventArgs e)
        {
            using (ColorDialog cDiag = new ColorDialog())
            {
                if (cDiag.ShowDialog() == DialogResult.OK)
                {
                    ((Label)sender).BackColor = cDiag.Color;
                    barcodeWriter.Renderer = new ZXing.Rendering.BitmapRenderer() { Background = lblBackColorBox.BackColor, Foreground = lblForeColorBox.BackColor };
                }
            }
        }

        private void numMargin_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Options.Margin = Convert.ToInt32(numMargin.Value);
            //GenerateCode();
        }

        private void numHeight_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Options.Height = Convert.ToInt32(numHeight.Value);
            barcodeWriter.Options.Width = Convert.ToInt32(numWidth.Value);
            //GenerateCode();
        }

        private void btnGenWiFi_Click(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateWiFi(txtSSID.Text, txtPass.Text, hidden: true));
        }

        private void btnGenVCard_Click(object sender, EventArgs e)
        {
            GenerateCode(QRCodeContentFormatter.GenerateVCard(txtName.Text, txtPhone.Text, txtVEmail.Text, txtAddress.Text, txtOrg.Text, txtVUrl.Text));
        }

        private void tabText_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new BarWinMain().Show();
        }
    }
}
