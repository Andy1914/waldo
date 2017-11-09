using System;
using ImageCompare.lib;

namespace ImageCompare
{
    class Program
    {
        public static void Main(string[] args)
        {
            IImageCompare _imageComparison = new ImageComparison("SampleImages");

            //If you want to crop an image from image then you can call below function
            //Crop image
            //_imageComparison.CropImage("cat_1.jpg", 10, 20, 200, 200);

            
            //Comparison of same image
            Console.WriteLine("1. Comparison of same images");
            _imageComparison.CompareImages("cat_1.jpg", "cat_1.jpg");

            Console.WriteLine("-------------------------------------------------------------");

            //Comparison of crop image
            Console.WriteLine("2. Comparison of crop images");
            _imageComparison.CompareImages("cat_1.jpg", "cat_2.jpg");
            _imageComparison.CompareImages("cat_1.jpg", "cat_3.jpg");
            _imageComparison.CompareImages("cat_1.jpg", "cat_4.jpg");
            _imageComparison.CompareImages("cat_1.jpg", "cat_6.jpg");

            Console.WriteLine("-------------------------------------------------------------");

            //Comparison of big and short sizes images
            Console.WriteLine("3. Comparison of big and short sizes images");
            _imageComparison.CompareImages("cat_1.jpg", "cat_5.jpg");

            Console.WriteLine("-------------------------------------------------------------");



            //Comparison of mostly common images
            Console.WriteLine("4. Comparison of mostly common images");

            _imageComparison.CompareImages("img_one.png", "img_two.png");
            _imageComparison.CompareImages("lab200.jpg", "lab100.jpg");

            Console.WriteLine("-------------------------------------------------------------");

            

            Console.WriteLine("Any key to end...");
            Console.ReadKey();
        }
        
    }
}
