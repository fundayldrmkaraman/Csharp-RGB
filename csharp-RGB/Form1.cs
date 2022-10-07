using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace final_ödevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void openImage()
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
        }

        void saveImage()
        {
            if (opened)
            {
                SaveFileDialog sfd = new SaveFileDialog(); 
                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName);
                    switch (ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pictureBox1.Image.Save(sfd.FileName, format);
                }
            }
            else { MessageBox.Show("No image loaded, first upload image "); }

        }

        Image file;
        Boolean opened = false;

        
        void hue()
        {
            float changered = redbar.Value * 0.1f;
            float changegreen = greenbar.Value * 0.1f;
            float changeblue = bluebar.Value * 0.1f;

         
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox1.Image;                             
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   
                                                                        

                ImageAttributes ia = new ImageAttributes();                 
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       
                {
                    new float[]{1+changered, 0, 0, 0, 0},
                    new float[]{0, 1+changegreen, 0, 0, 0},
                    new float[]{0, 0, 1+changeblue, 0, 0},
                   new float[]{0, 0, 0, 1, 0},
                   new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           
                Graphics g = Graphics.FromImage(bmpInverted);   

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

                g.Dispose();                            
                pictureBox1.Image = bmpInverted;

            }
        }

        Bitmap bitmap1;
        private void InitializeBitmap()
        {
            try
            {
                bitmap1 = (Bitmap)Bitmap.FromFile(@"C:\Documents and Settings\" +
                    @"All Users\Documents\My Music\music.bmp");
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.Image = bitmap1;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the bitmap.");
            }

            if (bitmap1 != null)
            {
                bitmap1.RotateFlip(RotateFlipType.Rotate90FlipY);
                pictureBox1.Image = bitmap1;
            }
        }

        void grayscale()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                Image img = pictureBox1.Image;                             
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);  

                ImageAttributes ia = new ImageAttributes();                
                ColorMatrix cmPicture = new ColorMatrix(new float[][]      
                {
                    new float[]{0.299f, 0.299f, 0.299f, 0, 0},
                    new float[]{0.587f, 0.587f, 0.587f, 0, 0},
                    new float[]{0.114f, 0.114f, 0.114f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 0}
                });
                ia.SetColorMatrix(cmPicture);           
                Graphics g = Graphics.FromImage(bmpInverted);   
                                                            

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                g.Dispose();                            
                pictureBox1.Image = bmpInverted;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void redbar_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void greenbar_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void bluebar_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grayscale();
        }
    }
}
