using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokemon_JSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pokemon pokemon;
        bool shouldShowFront = true;
        public MainWindow()
        {
            InitializeComponent();

            string url = "https://pokeapi.co/api/v2/pokemon?offset=0&limit=1400";
            PokemonAPI api = new PokemonAPI();
            using (var client = new HttpClient())
            {
                string json = client.GetStringAsync(url).Result;
                api = JsonConvert.DeserializeObject<PokemonAPI>(json);
            }

            foreach (var item in api.Results.OrderBy(p => p.Name))
            {
                pokemonCbo.Items.Add(item);
            }
        }

        private void pokemonCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Result selected = (Result)pokemonCbo.SelectedItem;
            string url = selected.Url;

            using (var client = new HttpClient())
            {
                string json = client.GetStringAsync(url).Result;
                pokemon = JsonConvert.DeserializeObject<Pokemon>(json);
            }

            nameTxtBox.Text = pokemon.Name;
            weightTxtBox.Text = pokemon.Weight.ToString();
            heightTxtBox.Text = pokemon.Height.ToString();

            pokeImage.Source = new BitmapImage(new Uri(pokemon.Sprites.FrontDefault));
            shouldShowFront = false;
            flipBtn.IsEnabled = true;
        }

        private void flipBtn_Click(object sender, RoutedEventArgs e)
        {
            string url = "";

            if (pokemon == null)
            {
                return;
            }

            if (shouldShowFront == true)
            {
                url = pokemon.Sprites.FrontDefault;
                shouldShowFront = false;
            }
            else
            {
                url = pokemon.Sprites.BackDefault;
                shouldShowFront = true;
            }
            pokeImage.Source = new BitmapImage(new Uri(url));
        }
    }
}
