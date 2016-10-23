using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    public class TiffHelper
    {
        public static void MergeTiffImages(string distFile, params string[] tiffFiles)
        {
            var codecInfo = ImageCodecInfo.GetImageEncoders()[3];
            var saveEncoder = Encoder.SaveFlag;
            var compressionEncoder = Encoder.Compression;
            var saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
            var compressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionLZW);
            var encoderParams = new EncoderParameters(2)
            {
                Param =
                    {
                        [0] = saveEncodeParam,
                        [1] = compressionEncodeParam
                    }
            };


            Bitmap pages = null;
            var frame = 0;
            foreach (var strImageFile in tiffFiles)
            {
                if (frame == 0)
                {
                    pages = (Bitmap)Image.FromFile(strImageFile);
                    pages.Save(distFile, codecInfo, encoderParams);
                }
                else
                {
                    encoderParams.Param[0] = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    var bm = (Bitmap)Image.FromFile(strImageFile);
                    pages.SaveAdd(bm, encoderParams);
                    bm.Dispose();
                }

                if (frame == tiffFiles.Length - 1)
                {
                    encoderParams.Param[0] = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                    pages.SaveAdd(encoderParams);
                }
                frame++;
            }

            pages.Dispose(); 
        }
    }
}
