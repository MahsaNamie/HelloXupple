using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloXupple
{

    public partial class MainPage : ContentPage
    {
        Tile[,] TileArry = new Tile[4, 4];
        double tileSize;
        int EmptyCol = 3;
        int EmptyRow = 3;
        public MainPage()
        {
            InitializeComponent();

            for (int row = 0; row < 4; row++)

            {
                for (int col = 0; col < 4; col++)
                {

                    Tile tile = new Tile(row, col);

                    //تعریف tapRecognizer
                    TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += OnTileTapped;
                    tile.TileView.GestureRecognizers.Add(tapGesture);

                    TileArry[row, col] = tile;
                    absoluteLayout.Children.Add(tile.TileView);
                }
            }
        }

        async void OnTileTapped(object sender, EventArgs e)
        {
            View view = (View)sender;
            Tile Tptile = Tile.Dictionary[view];
            await ShiftToEmpty(Tptile.Row, Tptile.Col);
        }

        async Task AnimateTile(int row, int col, int NewRow, int NewCol, uint lenght)
        {

            Tile tile = TileArry[row, col];


            View TileView = tile.TileView;


            Rectangle rect = new Rectangle(EmptyCol * tileSize, EmptyRow * tileSize, tileSize, tileSize);

            await TileView.LayoutTo(rect, lenght);
            AbsoluteLayout.SetLayoutBounds(TileView, rect);
            TileArry[NewRow, NewCol] = tile;
            tile.Col = NewCol;
            tile.Row = NewRow;
            TileArry[row, col] = null;
            EmptyRow = row;
            EmptyCol = col;

        }

        async Task ShiftToEmpty(int Tprow, int Tpcol, uint lenght = 100)
        {
            //جا به جایی ستونها
            if (Tprow == EmptyRow && Tpcol != EmptyCol)
            {
                int diff = Math.Sign(Tpcol - EmptyCol);
                int BeginPointCol = EmptyCol + diff;
                int EndPointCol = Tpcol + diff;
                for (int i = BeginPointCol; i != EndPointCol; i += diff)
                {
                    await AnimateTile(EmptyRow, i, EmptyRow, EmptyCol, lenght);
                }
            }
            //جابه جایی سطرها
            if (Tpcol == EmptyCol && Tprow != EmptyRow)
            {
                int diff = Math.Sign(Tprow - EmptyRow);
                int BeginPointRow = EmptyRow + diff;
                int EndPointRow = Tprow + diff;

                for (int i = BeginPointRow; i != EndPointRow; i += diff)
                {
                    await AnimateTile(i, EmptyCol, EmptyRow, EmptyCol, lenght);
                }
            }

        }

        async void RandomizeBtn(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.IsEnabled = false;
            Random random = new Random();
            int Rand = random.Next(4);
            //int RandRow = random.Next(4);

            for (int i = 0; i < 100; i++)
            {
                await ShiftToEmpty(Rand, EmptyCol, 25);
                await ShiftToEmpty(EmptyRow, Rand, 25);
            }
            btn.IsEnabled = true;

        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            View view = (View)sender;
            double width = view.Width;
            double Height = view.Height;
            tileSize = Math.Min(width, Height) / 4;
            foreach (View item in absoluteLayout.Children)
            {
                Tile tile = Tile.Dictionary[item];

                absoluteLayout.WidthRequest = 4 * tileSize;
                absoluteLayout.HeightRequest = 4 * tileSize;
                AbsoluteLayout.SetLayoutBounds(item, new Rectangle(tile.Col * tileSize,
                                                                   tile.Row * tileSize,
                                                                   tileSize, tileSize));
            }
        }
    }
}
