using Android.Content;
using Android.Graphics;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace HelloXupple
{
    public class Tile
    {
        //const string UrlPrefix = "@drawble/utube.png";
        // const string UrlPrefix = @"file://E:\MyExperience\XamarinDemos\XamagonXuzzleLP\XamagonXuzzle\XamagonXuzzle.Android\Resources";
        const string UrlPrefix = "http://xamarin.github.io/xamarin-forms-book-samples/XamagonXuzzle/";

        public Tile(int row, int col)
        {
            Row = row;
            Col = col;

            TileView = new ContentView
            {
                Padding = new Thickness(1),

                // Get the bitmap for each tile 
                Content = new Image
                {
                   // Source = ImageSource.FromUri(new Uri(UrlPrefix + "Bitmap" + row + col + ".png"))

                }
            };

            // Add TileView to dictionary for obtaining Tile from TileView
            Dictionary.Add(TileView, this);
        }


        public static Dictionary<View, Tile> Dictionary { get; } = new Dictionary<View, Tile>();
        public View TileView { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }

}
