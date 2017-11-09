using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace ImageCompare.lib
{
    class ImageComparison : IImageCompare
    {
        DirectoryInfo exeFolder, projectFolder, sampleImagesFolder;

        public ImageComparison(string imageFolderName)
        {
            //find the image folder
            string codebase = Assembly.GetExecutingAssembly().Location;
            exeFolder = new DirectoryInfo(Path.GetDirectoryName(codebase));
            projectFolder = exeFolder.Parent.Parent;
            sampleImagesFolder = new DirectoryInfo(Path.Combine(projectFolder.FullName, imageFolderName));
        }

        public void CompareImages(string bmp1, string bmp2, byte threshold = 3)
        {
            //get the full path of the images
            string image1Path = Path.Combine(sampleImagesFolder.FullName, bmp1);
            string image2Path = Path.Combine(sampleImagesFolder.FullName, bmp2);

            //compare the two
            Console.WriteLine("Comparing images: " + bmp1 + " and " + bmp2 + ", with a threshold of " + threshold);
            Bitmap InputImage1 = (Bitmap)Image.FromFile(image1Path);
            Bitmap InputImage2 = (Bitmap)Image.FromFile(image2Path);

            int size = 128; //128;

            Bitmap Image1 = new Bitmap(InputImage1, new Size(size, size));
            Bitmap Image2 = new Bitmap(InputImage2, new Size(size, size));

            //int Image1Size = Image1.Width * Image1.Height;
            //int Image2Size = Image2.Width * Image2.Height;

            int Image1Size = InputImage1.Width * InputImage1.Height;
            int Image2Size = InputImage2.Width * InputImage2.Height;


            Bitmap Image3;
            if (Image1Size > Image2Size)
            {
                Image1 = new Bitmap(Image1, Image2.Size);
                Image3 = new Bitmap(Image2.Width, Image2.Height);
            }
            else if (Image1Size < Image2Size)
            {
                Image1 = new Bitmap(Image1, Image1.Size);
                Image3 = new Bitmap(Image2.Width, Image2.Height);
            }
            else
            {
                Image1 = new Bitmap(Image1, Image1.Size);
                Image3 = new Bitmap(Image2.Width, Image2.Height);
            }
            for (int x = 0; x < Image1.Width; x++)
            {
                for (int y = 0; y < Image1.Height; y++)
                {
                    Color Color1 = Image1.GetPixel(x, y);
                    Color Color2 = Image2.GetPixel(x, y);
                    int r = Color1.R > Color2.R ? Color1.R - Color2.R : Color2.R - Color1.R;
                    int g = Color1.G > Color2.G ? Color1.G - Color2.G : Color2.G - Color1.G;
                    int b = Color1.B > Color2.B ? Color1.B - Color2.B : Color2.B - Color1.B;
                    Image3.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            //Find first matched cordindates
            int xx = -1, yy = -1;

            int Difference = 0;
            for (int x = 0; x < Image1.Width; x++)
            {
                for (int y = 0; y < Image1.Height; y++)
                {
                    Color Color1 = Image3.GetPixel(x, y);
                    int Media = (Color1.R + Color1.G + Color1.B) / 3;
                    if (Media > threshold)
                    {
                        Difference++;
                    }else
                    {
                        //if match parameter are not set then set
                        if ((xx == -1 && yy == -1))
                        {
                            xx = x;
                            yy = y;
                        }
                    }
                }
            }


          
            //int width = Image1Size > Image2Size ? Image1.Width : Image2.Width;
            //int height = Image1Size > Image2Size ? Image1.Height : Image2.Height;

            //int width = InputImage1.Width;
            //int height = InputImage1.Height;

            //int width2 = InputImage2.Width;
            //int height2 = InputImage2.Height;


            //for (int x = 0; x < width; x++)
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        if (x < width2 && y < height2)
            //        {
            //            if ((xx == -1 && yy == -1))
            //            {
            //                if (InputImage1.GetPixel(x, y) == InputImage2.GetPixel(x, y))
            //                {
            //                    xx = x;
            //                    yy = y;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}


            double UsedSize = Image1Size > Image2Size ? Image2Size : Image1Size;
            if (Difference > UsedSize)
            {
                UsedSize = Image1Size > Image2Size ? Image1Size : Image2Size;
            }
            double result = Difference * 100 / UsedSize;

            double matched = 100 - result;

            Console.WriteLine(string.Format("Images Matched : {0:0.0} %, Difference: {1:0.0} %, Crop image match coordinates are x:{2}, y:{3}", matched, result, xx, yy));
            Console.WriteLine("");
        }

        public void CropImage(string bmp1, int x, int y, int width, int height)
        {
            string image1Path = Path.Combine(sampleImagesFolder.FullName, bmp1);

            Bitmap source = new Bitmap(image1Path);
            Bitmap CroppedImage = source.Clone(new System.Drawing.Rectangle(x, y, width, height), source.PixelFormat);

            string saveFileName = Path.Combine(sampleImagesFolder.FullName, "_cropImage.png");
            CroppedImage.Save(saveFileName);
        }
    }
}
