using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        int tentsOfSecondsElapsed;
        int matchesFound;
        int nomatch;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tentsOfSecondsElapsed++;
            timeTextBlock.Text = (tentsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🦍", "🦍",
                "🦊", "🦊",
                "🐭", "🐭",
                "🐷", "🐷",
                "🐰", "🐰",
                "🦝", "🦝",
                "🐸", "🐸",
                "🙈", "🙈"
            };

            Random random = new Random();

            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextmEmoji = animalEmoji[index];
                    textblock.Text = nextmEmoji;
                    textblock.Foreground = textblock.Background;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tentsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private  void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
               
                lastTextBlockClicked = textBlock;
                //textBlock.Visibility = Visibility.Visible;
                textBlock.Foreground = Brushes.Black;
                findingMatch = true;
                nomatch = 0;
            }

            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                //textBlock.Visibility = Visibility.Visible;
                textBlock.Foreground = Brushes.Black;
                findingMatch = false;
                nomatch = 1;
            }
            else
            {
                textBlock.Foreground = Brushes.Black;
                lastTextBlockClicked.Foreground = Brushes.Black;
                nomatch = 1;
                //lastTextBlockClicked.Visibility = Visibility.Hidden;                
            }

            if (nomatch == 1)
            {
                nollaaAsync(textBlock);
            }
            
        }
        private async Task nollaaAsync(TextBlock textBlock)
        {
            await Task.Delay(500);
            lastTextBlockClicked.Foreground = lastTextBlockClicked.Background;
            textBlock.Foreground = textBlock.Background;
            findingMatch = false;
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
