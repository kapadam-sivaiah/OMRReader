/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

//using Inlite.ClearImageNet;
//using Inlite.ClearImageNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRReader.BL
{
 public  class BitmapConvertion
    {
     public static Bitmap originalBitmap;
     public JobDetails job { get; set; }
    //public static Bitmap previewBitmap ;
    public static Bitmap resultBitmap;
    private Bitmap cbitmap = null;// 

    ////ImageEditor rp = new ImageEditor();
    //public static Bitmap biotionalconvertion(string image)
    //{
    //    Bitmap distkewImage;
    //    System.Drawing.Image _img = null;
    //    try
    //    {
    //        // JobDetails.imgIO = new ImageIO();
    //        //  image = @"D:\CUCT_2016\IMAGES\PG_RP_NO\PG_RP_NO_SERIES\0200019.jpg";
    //        Bitmap b = new Bitmap(Bitmap.FromFile(image));
    //        JobDetails.ImageRepair.Image.Open(b);
    //        JobDetails.ImageRepair.AutoDeskew();
    //        //JobDetails.ImageRepair.ToBitonal();
    //        //  JobDetails.ImageRepair.AutoInvert(100);
    //        //JobDetails.ImageRepair.CleanNoise(5);
    //        //JobDetails.ImageRepair.t
    //        //JobDetails.ImageRepair.
    //        //JobDetails.ImageRepair.Invert();

    //        JobDetails.ImageRepair.Image.SaveToBitmap();
    //        distkewImage = JobDetails.ImageRepair.Bitmap;
    //        ImageAttributes imageAttr = new ImageAttributes();
    //         imageAttr.SetThreshold(70);

    //        ////  System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(pictureBox3.Image);
    //         Graphics g = System.Drawing.Graphics.FromImage(distkewImage);
    //         g.DrawImage(distkewImage, new Rectangle(0, 0, distkewImage.Width, distkewImage.Height), 0, 0,
    //                          distkewImage.Width, distkewImage.Height, GraphicsUnit.Pixel, imageAttr);

    //       //distkewImage.Save("E:\\1.BMP");
    //    }
        //catch (Exception e3)
        //{
        //    _img = System.Drawing.Image.FromFile(image);
        //    distkewImage = (Bitmap)_img;
        //}
        //return distkewImage;
        ////  throw new NotImplementedException();
   // }
     public Bitmap Prewit(string image)
     {
         //image = @"D:\CUCT_2016\IMAGES\PG_RP_PARTS_ONLY\PG_RP_PARTS_ONLY1\0500283.jpg";
         StreamReader streamReader = new StreamReader(image);
         originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
         streamReader.Close();
         //previewBitmap = originalBitmap;// ;
        // originalBitmap = biotionalconvertion(image);
         Image _img = originalBitmap;
         Bitmap b1 = new Bitmap(_img.Width, _img.Height);
         ImageAttributes ia = new ImageAttributes();
         ColorMatrix cm = new ColorMatrix();
         #region Old Code
         #endregion
         #region New Code
         cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = -5f;
         cm.Matrix30 = cm.Matrix31 = cm.Matrix32 = 5f;
         #endregion
         ia.SetColorMatrix(cm);
         Graphics g = Graphics.FromImage(b1);
         g.DrawImage(_img, new Rectangle(0, 0, _img.Width, _img.Height), 0, 0, _img.Width, _img.Height, GraphicsUnit.Pixel, ia);
         originalBitmap = b1;
        //  b1.Save("E:\\4.bmp");
         return originalBitmap;
     }
    }
}
