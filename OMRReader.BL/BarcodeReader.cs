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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZXing;

namespace OMRReader.BL
{
    public class BarcodeReader
    {
        public static string ReadBarcodeDetails(string filePath)
        {
            string barcodeValue = string.Empty;
            try
            {

                ZXing.BarcodeReader reader = new ZXing.BarcodeReader();
                reader.Options.PossibleFormats = new List<BarcodeFormat>();
                reader.Options.PossibleFormats.Add(BarcodeFormat.CODE_128);
                reader.Options.TryHarder = true;
                Bitmap image = new Bitmap(filePath);
                barcodeValue = Convert.ToString(reader.Decode(image));
                var regexItem = new Regex("^[-:&()]*$");
                if (regexItem.IsMatch(barcodeValue) && barcodeValue != "")
                {
                    barcodeValue = string.Empty;
                }
            }
            catch (Exception )
            {

            }
            return barcodeValue;
        }

    }
}
