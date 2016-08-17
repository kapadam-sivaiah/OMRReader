/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using OMRManagement.Entities;
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
    public class TemplateReader
    {

        public static bool IsMatch(Bitmap image, ArrayList trackArray, int templateTrackCount, int templateDiffLeft, int templateDiffTop, int templateTop, int templateLeft)
        {
            int imageTrackCount = 0;
            //templateLeft=templateLeft+templateDiffLeft;
            for (int t = 0; t < templateTrackCount; t++)
            {
                try
                {
                    
                    imageTrackCount = GetImagePoints(image,templateLeft+templateDiffLeft , templateTop, imageTrackCount);
                    templateTop = int.Parse(trackArray[t].ToString()) + templateDiffTop;
                }
                catch
                {
                }
            }
           // templateTop = 535;
           return (templateTrackCount==imageTrackCount || templateTrackCount == imageTrackCount + 1 || templateTrackCount == imageTrackCount + 2 || templateTrackCount == imageTrackCount - 2 || templateTrackCount == imageTrackCount - 1) ? true : false;

         //  return (templateTrackCount == imageTrackCount);
        }



        private static int GetImagePoints(Bitmap b, int templateLeft, int templateTop, int imageTrackCount)
        {
            Color tt;
            int counter = 0;
            for (int y = templateTop; y < (templateTop + 10); ++y)
            {
                for (int x = templateLeft; x < (templateLeft + 10); ++x)
                {
                    tt = b.GetPixel(x, y);
                    if (tt.Name == "ffffffff")
                    {
                        counter++;

                    }
                }

            }
            if (counter >= 10)
            {
                imageTrackCount++;
            }
            return imageTrackCount;
        }



        public static string GetSheetValues(Bitmap image, Template template, int templateDiffTop, int templateDiffLeft, ArrayList trackArray, int jobThreshold, out bool hasError)
        {

            string sheetValues = string.Empty;
            string values = string.Empty;
            bool hasFieldError = false;
            string FieldValues = string.Empty;
            //templateDiffLeft = 0;
           
            template.FieldList.ForEach(field =>
                {
                    int fieldTop =int.Parse( template.TrackIndex.Split(',')[field.TrackRowNo-1])+templateDiffTop;
                    int fieldLeft = field.FieldLeftPosition+templateDiffLeft ;
                    char[] fieldIndexValues = field.FieldIndex.ToCharArray();
                    int bubbleThreashold = (field.BubbleWidth * field.BubbleHeight * jobThreshold) / 100;
                    int fieldStartIndex = field.StartIndex;
                    FieldValues = string.Empty;
                    
                    if (field.TemplateFieldType == FieldType.HOR)
                    {

                        for (int r = 0; r < field.FieldRowNo; r++)
                        {
                            fieldLeft = field.FieldLeftPosition + templateDiffLeft; 
                            for (int col = 0; col < field.FieldColumnNo; col++)
                            {


                                string bubbleShadeValue = ReadBubble(fieldLeft, fieldTop, field.BubbleWidth, field.BubbleHeight, fieldIndexValues[col].ToString(), bubbleThreashold, image);
                                if (col % 4 ==0&&col!=0)
                                {
                                    fieldLeft += (field.BubbleColumnGap +2);
                                }
                                else
                                {
                                    fieldLeft += field.BubbleColumnGap;
                                }

                                values = values + bubbleShadeValue;
                            }

                            if (r % 4==0&&r!=0)
                            {
                                fieldTop = fieldTop + field.BubbleRowGap +2;
                            }
                            else
                            {

                                fieldTop = fieldTop + field.BubbleRowGap;

                            }
                           

                            if (string.IsNullOrEmpty(values))
                            {
                                values = "*";
                            }
                            if (values.Length > 1)
                            {
                               values = ">";
                            }

                            //if (hasFieldError == false)
                            //{
                            //    hasFieldError = HasFieldValueError(sheetValues, template.AllowedErrorCharCount);
                            //}

                            FieldValues += values;
                            sheetValues += values;
                            //   fieldLeft = 0;
                            values = string.Empty;
                        }
                    }
                    else if (field.TemplateFieldType == FieldType.VER)
                    {

                        fieldLeft = field.FieldLeftPosition + templateDiffLeft;

                        for (int c = 0; c < field.FieldColumnNo; c++)
                        {

                            fieldTop = int.Parse(template.TrackIndex.Split(',')[field.TrackRowNo - 1])+templateDiffTop;

                            for (int row = 0; row < field.FieldRowNo; row++)
                            {

                                string bubbleShadeValue = ReadBubble(fieldLeft, fieldTop, field.BubbleWidth, field.BubbleHeight, fieldIndexValues[row].ToString(), bubbleThreashold, image);
                                if (row % 4 == 0 && row != 0)
                                {
                                    fieldTop = fieldTop + field.BubbleRowGap + 2;
                                }
                                else
                                {

                                    fieldTop = fieldTop + field.BubbleRowGap;
                                }
                                values = values + bubbleShadeValue;
                            }

                            if (c % 4== 0&&c!=0)
                            {
                                fieldLeft += (field.BubbleColumnGap + 2);
                            }
                            else
                            {
                                fieldLeft += field.BubbleColumnGap;
                            }

                            if (string.IsNullOrEmpty(values))
                            {
                                values = "*";
                            }
                            if (values.Length > 1)
                            {
                               values = ">";
                            }



                            FieldValues += values;
                            sheetValues += values;
                           
                            // fieldLeft = 0;
                            values = string.Empty;
                        }
                    }
                    if (hasFieldError == false)
                    {
                        hasFieldError = HasFieldValueError(FieldValues, template.AllowedErrorCharCount);
                    }

                   // labelResult.Text = FieldValues;

                });

            hasError = hasFieldError;

            return sheetValues;
        }
        public static bool HasFieldValueError(string value, int allowedCharCount)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            if (value.Contains(">"))
                return true;
            value = value.TrimEnd('*');
            if (allowedCharCount < 0)
                return false;
            string searchPattern = new string('*', allowedCharCount) + "*";

            int ocuuranceCount = GetSubstringOccuranceCount(value, searchPattern);
            if (ocuuranceCount > 0)
                return true;
            return false;

        }

        public static int GetSubstringOccuranceCount(string value, string searchPattern)
        {
            int num = 0;
            int pos = 0;

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(searchPattern))
            {
                while ((pos = value.IndexOf(searchPattern, pos)) > -1)
                {
                    num++;
                    pos += searchPattern.Length;
                }
            }

            return num;

        }


        public static string ReadBubble(int fieldLeft, int fieldTop, int fieldBubbleWidth, int fieldBubbleHeight, string BubbleValue, int bubbleThreashold, Bitmap image)
        {
            int bubble = 0;
            string shadeBubbleValue = string.Empty;
            for (int bubbleTop = fieldTop; bubbleTop < fieldTop + fieldBubbleHeight; bubbleTop++)
            {
                for (int bubbleLeft = fieldLeft; bubbleLeft < fieldLeft + fieldBubbleWidth-2; bubbleLeft++)
                {
                    Color bubbleShadeColor = image.GetPixel(bubbleLeft, bubbleTop);
                    //image.Save("E:\\3.bmp"); if (bubbleShadeColor.Name == "ff000000")
                    if (bubbleShadeColor.Name == "ffffffff")
                    {
                        bubble++;

                    }
                }
            }
            if (bubble > bubbleThreashold)
            {
                shadeBubbleValue += BubbleValue;
            }
            return shadeBubbleValue;
        }


    }
}
