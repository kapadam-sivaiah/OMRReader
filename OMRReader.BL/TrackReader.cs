/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OMRReader.BL
{
    public class TrackReader
    {
        public static void ReadTrack(int trackLeft, int trackTop, Bitmap previewBitmap, int templateLeft, int templateTop, out int templateDiffLeft, out int templateDiffTop)
        {
            ArrayList indexValues = new ArrayList();

          // string trackindex = string.Empty;
            if (trackLeft != 0 && trackTop != 0)
            {
                ExtBitmap.ImageLeft = trackLeft;
                ExtBitmap.ImageTop = trackTop;
            }
            else
            {
                ExtBitmap.ImageLeft =templateLeft;   // e.X;
                ExtBitmap.ImageTop = templateTop; //e.Y;
            }

           // ApplyFilter(true);
            Bitmap  bitmapResult = ExtBitmap.PrewittFilter(previewBitmap, false);
            int leftDiff = 0;
            string trackindex = null;
           // indexValues1 = new ArrayList();
            //  indexValues2 = new ArrayList();
            int N = 0;
            try
            {
                for (int i = 0; i < ExtBitmap.a.Length; i++)
                {

                    if (i != 0)
                    {
                        string[] y1 = ExtBitmap.a[i].ToString().Split(',');
                        string[] y2 = ExtBitmap.a[i - 1].ToString().Split(',');
                        int n = int.Parse(y1[0].ToString()) - int.Parse(indexValues[indexValues.Count-1].ToString().Split(',')[0]);
                        if (n >=20)
                        {
                            indexValues.Add(ExtBitmap.a[i].ToString());
                            trackindex = trackindex + "," + y1[1];
                            if (indexValues.Count == 2)
                            {
                                
                               leftDiff = int.Parse(y1[1]);
                                
                            }

                        }
                    }
                    else
                    {
                        string[] y1 = ExtBitmap.a[0].ToString().Split(',');
                        indexValues.Add(ExtBitmap.a[0].ToString());
                        trackindex = y1[1];
                    }
                }

            }

            catch
            {


            }
          
            if (leftDiff > trackLeft)
            {
                templateDiffLeft =leftDiff- trackLeft;
            }
            else
            {
                templateDiffLeft = trackLeft - leftDiff;
            }
            if (leftDiff == 0)
            {
                templateDiffLeft = 0;
            }
            templateDiffTop = int.Parse(indexValues[0].ToString().Split(',')[0]) - (trackTop);
           // templateDiffTop 
                if (templateDiffTop <= 5 && templateDiffTop > 0)
                {
                    templateDiffTop = 0;
                } 
            
                if (templateDiffTop > 5)
                {
                    templateDiffTop = templateDiffTop - 5;
                }
           

        }
    }
}
