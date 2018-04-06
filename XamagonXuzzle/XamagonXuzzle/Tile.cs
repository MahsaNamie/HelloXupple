using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamagonXuzzle
{
    public class Tile
    {
        //const string UrlPrefix = "http://xamarin.github.io/xamarin-forms-book-samples/XamagonXuzzle/";
        //const string UrlPrefix = "file://E:/MyExperience/XamarinDemos/XamagonXuzzleLP/XamagonXuzzle/XamagonXuzzle.Android/Resources/drawable/photo_2018";
        const string UrlPrefix = "https://mahsanamie.github.io/HelloXupple/";
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
                    Source = ImageSource.FromUri(new Uri(UrlPrefix + "Bitmap" + row + col + ".jpg"))
                }
            };

            // Add TileView to dictionary for obtaining Tile from TileView
            Dictionary.Add(TileView, this);
        }

        public static Dictionary<View, Tile> Dictionary { get; } = new Dictionary<View, Tile>();

        public int Row { set; get; }

        public int Col { set; get; }

        public View TileView { private set; get; }
    }
}