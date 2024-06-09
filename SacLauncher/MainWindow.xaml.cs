using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SacLauncher
{
    public partial class MainWindow : Window
    {
        private readonly string folderPath;

        public MainWindow()
        {
            InitializeComponent();
            folderPath = AppDomain.CurrentDomain.BaseDirectory;
            LoadSettings();
        }

        private void PopulateDropdown(string pattern)
        {
            Map_list1.Items.Clear();

            if (string.IsNullOrEmpty(pattern))
            {
            MessageBox.Show("Invalid pattern. Unable to populate dropdown.");
            return;
            }

            Regex regex;
            try
            {
                regex = new Regex(pattern);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Invalid regex pattern: " + ex.Message);
                return;
            }

            try
            {
                string[] mapFiles = Directory.GetFiles(folderPath, "maps*.txt");
                foreach (string filePath in mapFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    if (regex.IsMatch(fileName))
                    {
                        Map_list1.Items.Add(fileName);
                    }
                }

                if (Map_list1.Items.Count > 0)
                {
                    Map_list1.SelectedIndex = 0;
                }
                else
                {
                MessageBox.Show("No matching files found for pattern: " + pattern);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error accessing directory or finding files: " + ex.Message);
            }
        }

        private bool IsFileNameMatchingPattern(string fileName, string pattern)
        {
            // Implement your custom logic to check if the file name matches the pattern
            // For example, you can use string manipulation methods like StartsWith, Contains, EndsWith, etc.
            // Here's a simple example assuming the pattern starts with "maps-" and ends with ".txt":
            return fileName.StartsWith("maps-") && fileName.EndsWith(".txt");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateGame_mode_listDropdown();
            UpdateMaplist1Dropdown();
        }

        private void Game_mode_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaplist1Dropdown();
        }

        private void UpdateGame_mode_listDropdown()
        {
            Game_mode_list.Items.Clear();

            if (P2.IsChecked == true)
            {
                Game_mode_list.Items.Add("1v1");
            }
            else if (P3.IsChecked == true)
            {
                Game_mode_list.Items.Add("ffa");
            }
            else if (P4.IsChecked == true)
            {
                Game_mode_list.Items.Add("2v2");
                Game_mode_list.Items.Add("ffa");
            }
            else if (P5.IsChecked == true)
            {
                Game_mode_list.Items.Add("ffa");
            }
            else if (P6.IsChecked == true)
            {
                Game_mode_list.Items.Add("3v3");
                Game_mode_list.Items.Add("ffa");
            }
            else if (P7.IsChecked == true)
            {
                Game_mode_list.Items.Add("ffa");
            }
            else if (P8.IsChecked == true)
            {
                Game_mode_list.Items.Add("4v4");
                Game_mode_list.Items.Add("ffa");
            }

            if (Game_mode_list.Items.Count > 0)
            {
                Game_mode_list.SelectedIndex = 0;
            }
        }

        private void UpdateMaplist1Dropdown()
        {
            string selectedType = Game_mode_list.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedType)) return;

            string pattern = selectedType switch
            {
                "1v1" => @"maps-.*-1v1\.txt",
                "2v2" => @"maps-.*-2v2\.txt",
                "3v3" => @"maps-.*-3v3\.txt",
                "4v4" => @"maps-.*-4v4\.txt",
                "ffa" when P3.IsChecked == true => @"maps-.*-3ffa\.txt",
                "ffa" when P4.IsChecked == true => @"maps-.*-4ffa\.txt",
                "ffa" when P5.IsChecked == true => @"maps-.*-5ffa\.txt",
                "ffa" when P6.IsChecked == true => @"maps-.*-6ffa\.txt",
                "ffa" when P7.IsChecked == true => @"maps-.*-7ffa\.txt",
                "ffa" when P8.IsChecked == true => @"maps-.*-8ffa\.txt",
                _ => string.Empty
            };

            PopulateDropdown(pattern);
        }


        private Random random = new Random();

        private void Map_list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(folderPath) || !(Map_list1.SelectedItem is string selectedFileName))
            {
                Map_selection.Text = string.Empty;
                return;
            }

            string selectedFilePath = Path.Combine(folderPath, selectedFileName);

            if (File.Exists(selectedFilePath))
            {
                string randomEntry = GetRandomEntry(selectedFilePath);

                if (randomEntry?.StartsWith("maps/") == true)
                {
                    randomEntry = randomEntry.Substring("maps/".Length);
                }

                Map_selection.Text = randomEntry ?? "No entries found.";
            }
            else
            {
                Map_selection.Text = "File not found.";
            }
        }

        private string GetRandomEntry(string filePath)
        {
            var entries = new List<string>();

            try
            {
                entries.AddRange(File.ReadAllLines(filePath));
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading the file: " + ex.Message);
                return null;
            }

            return entries.Count > 0 ? entries[random.Next(entries.Count)] : null;
        }

        private void ButtonRoll_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(folderPath) || !(Map_list1.SelectedItem is string selectedFileName))
            {
                Map_selection.Text = string.Empty;
                return;
            }

            string selectedFilePath = Path.Combine(folderPath, selectedFileName);
            string randomEntry = GetRandomEntry(selectedFilePath);

            if (randomEntry?.StartsWith("maps/") == true)
            {
                randomEntry = randomEntry.Substring("maps/".Length);
            }

            Map_selection.Text = randomEntry ?? "No entries found.";
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Handle window content rendered event if needed
        }

        private void Nickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Handle nickname text changed event if needed
        }

        private void Nickname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void Nickname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
            }
        }

        private void Wizard_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle wizard selection change event if needed
        }

        private void God_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle god selection change event if needed
        }

        private void Randomgod_Checked(object sender, RoutedEventArgs e)
        {
            // Handle random god checked event if needed
        }

        private void Detect_Resolution_Checked(object sender, RoutedEventArgs e)
        {
            Choose_Resolution.IsChecked = false;
            Resolution_list.Visibility = Visibility.Hidden;
        }

        private void Choose_Resolution_Checked(object sender, RoutedEventArgs e)
        {
            Detect_Resolution.IsChecked = false;
            Resolution_list.Visibility = Visibility.Visible;
        }

        private void Resolution_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle resolution selection change event if needed
        }

        private void Fullscreen_Checked(object sender, RoutedEventArgs e)
        {
            // Handle fullscreen checked event if needed
        }

        private void Fullscreen_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle fullscreen unchecked event if needed
        }

        private void Widgets_Checked(object sender, RoutedEventArgs e)
        {
            // Handle widgets checked event if needed
        }

        private void Widgets_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle widgets unchecked event if needed
        }

        private void Fog_Checked(object sender, RoutedEventArgs e)
        {
            // Handle fog checked event if needed
        }

        private void Fog_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle fog unchecked event if needed
        }

        private void MapBottom_Checked(object sender, RoutedEventArgs e)
        {
            // Handle map bottom checked event if needed
        }

        private void MapBottom_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle map bottom unchecked event if needed
        }

        private void Glow_Checked(object sender, RoutedEventArgs e)
        {
            // Handle glow checked event if needed
        }

        private void Glow_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle glow unchecked event if needed
        }

        private void Glow_brightness_Checked(object sender, RoutedEventArgs e)
        {
            // Handle glow brightness checked event if needed
        }

        private void Glow_brightness_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle glow brightness unchecked event if needed
        }

        private void Glow_brightness_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Glow1.Text = Glow_brightness_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Antialiasing_Checked(object sender, RoutedEventArgs e)
        {
            // Handle antialiasing checked event if needed
        }

        private void Antialiasing_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle antialiasing unchecked event if needed
        }

        private void Sun_strength_Checked(object sender, RoutedEventArgs e)
        {
            // Handle sun strength checked event if needed
        }

        private void Sun_strength_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle sun strength unchecked event if needed
        }

        private void Sun_strength_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Sun.Text = Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Ambient_strength_Checked(object sender, RoutedEventArgs e)
        {
            // Handle ambient strength checked event if needed
        }

        private void Ambient_strength_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle ambient strength unchecked event if needed
        }

        private void Ambient_strength_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Ambient.Text = Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Master_volume_Checked(object sender, RoutedEventArgs e)
        {
            // Handle master volume checked event if needed
        }

        private void Master_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle master volume unchecked event if needed
        }

        private void Master_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Master.Text = Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Music_volume_Checked(object sender, RoutedEventArgs e)
        {
            // Handle music volume checked event if needed
        }

        private void Music_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle music volume unchecked event if needed
        }

        private void Music_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Music.Text = Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Sound_volume_Checked(object sender, RoutedEventArgs e)
        {
            // Handle sound volume checked event if needed
        }

        private void Sound_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle sound volume unchecked event if needed
        }

        private void Sound_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Sound.Text = Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void No_advisor_help_speech_Checked(object sender, RoutedEventArgs e)
        {
            // Handle no advisor help speech checked event if needed
        }

        private void Host_Checked(object sender, RoutedEventArgs e)
        {
            SetPlayerVisibility(Visibility.Visible);
            SetPlayerEnabled(true);
            P6.IsChecked = true;
            SetControlsEnabled(true);
        }


        private void Guest_Checked(object sender, RoutedEventArgs e)
        {
            SetPlayerVisibility(Visibility.Hidden);
            SetPlayerCheckStates(false);
            SetControlsEnabled(false);
        }

        private void Singleplayer_Checked(object sender, RoutedEventArgs e)
        {
            SetPlayerVisibility(Visibility.Hidden);
            SetPlayerCheckStates(false);
            SetControlsEnabled(true);
        }

        private void Start_souls_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Handle start souls slider value changed event if needed
        }

        private void Start_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Handle start level slider value changed event if needed
        }

        private void Min_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Handle min level slider value changed event if needed
        }

        private void Max_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Handle max level slider value changed event if needed
        }

        private void Random_gods_Checked(object sender, RoutedEventArgs e)
        {
            // Handle random gods checked event if needed
        }

        private void Random_gods_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle random gods unchecked event if needed
        }

        private void Shuffle_sides_Checked(object sender, RoutedEventArgs e)
        {
            // Handle shuffle sides checked event if needed
        }

        private void Shuffle_sides_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle shuffle sides unchecked event if needed
        }

        private void Stutter_on_desync_Checked(object sender, RoutedEventArgs e)
        {
            // Handle stutter on desync checked event if needed
        }

        private void Stutter_on_desync_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle stutter on desync unchecked event if needed
        }

        private void No_particles_Checked(object sender, RoutedEventArgs e)
        {
            // Handle no particles checked event if needed
        }

        private void No_particles_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle no particles unchecked event if needed
        }

        private void TextBoxEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void TextBoxEntry_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true; // Prevent other event handlers from deselecting the text
                textBox.Focus();
            }
        }

        private void Slaughter_Checked(object sender, RoutedEventArgs e)
        {
            // Handle slaughter checked event if needed
        }

        private void Slaughter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle slaughter unchecked event if needed
        }

        private void Slaughter_target_kills_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Handle slaughter target kills value changed event if needed
        }

        private void Map_list_Checked(object sender, RoutedEventArgs e)
        {
            // Handle map list checked event if needed
        }

        private void Map_list_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle map list unchecked event if needed
        }

        private void Map_select_Checked(object sender, RoutedEventArgs e)
        {
            // Handle map select checked event if needed
        }

        private void Map_select_Unchecked(object sender, RoutedEventArgs e)
        {
            // Handle map select unchecked event if needed
        }

        private void Selection_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void Selection_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true; // Prevent other event handlers from deselecting the text
                textBox.Focus();
            }
        }

        private void network_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void network_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void network_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
            }
        }
        private void IP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void IP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void IP_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
            }
        }

        private void SetPlayerVisibility(Visibility visibility)
        {
            P2.Visibility = visibility;
            P3.Visibility = visibility;
            P4.Visibility = visibility;
            P5.Visibility = visibility;
            P6.Visibility = visibility;
            P7.Visibility = visibility;
            P8.Visibility = visibility;
        }

        private void SetPlayerEnabled(bool isEnabled)
        {
            P2.IsEnabled = isEnabled;
            P3.IsEnabled = isEnabled;
            P4.IsEnabled = isEnabled;
            P5.IsEnabled = isEnabled;
            P6.IsEnabled = isEnabled;
            P7.IsEnabled = isEnabled;
            P8.IsEnabled = isEnabled;
        }

        private void SetPlayerCheckStates(bool isChecked)
        {
            P2.IsChecked = isChecked;
            P3.IsChecked = isChecked;
            P4.IsChecked = isChecked;
            P5.IsChecked = isChecked;
            P6.IsChecked = isChecked;
            P7.IsChecked = isChecked;
            P8.IsChecked = isChecked;
        }

        private void SetControlsEnabled(bool isEnabled)
        {
            Game_mode_list.IsEnabled = isEnabled;
            Random_gods.IsChecked = isEnabled;
            Random_gods.IsEnabled = isEnabled;
            Shuffle_sides.IsChecked = isEnabled;
            Shuffle_sides.IsEnabled = isEnabled;
            Stutter_on_desync.IsChecked = isEnabled;
            Stutter_on_desync.IsEnabled = isEnabled;
            No_particles.IsChecked = isEnabled;
            No_particles.IsEnabled = isEnabled;
            Slaughter.IsEnabled = isEnabled;
        }


        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void Savereplays_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseApplication();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            try
            {
                string executablePath = FindLatestBuildExecutable();
                if (!string.IsNullOrEmpty(executablePath))
                {
                    Process.Start(executablePath);
                }
                else
                {
                    MessageBox.Show("Executable not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string FindLatestBuildExecutable()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory; // Get the directory of the running application
            string filePattern = "SacEngine-*.exe";
            string[] files = Directory.GetFiles(directoryPath, filePattern);

            DateTime latestDate = DateTime.MinValue;
            string latestFile = null;

            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string timestamp = fileName.Substring("SacEngine-".Length);
                if (DateTime.TryParseExact(timestamp, "yyyy-MM-dd-HH-mm-ss", null, System.Globalization.DateTimeStyles.None, out DateTime buildDate))
                {
                    if (buildDate > latestDate)
                    {
                        latestDate = buildDate;
                        latestFile = file;
                    }
                }
            }

            return latestFile;
        }

        private void SaveSettings()
        {
            try
            {
                string config = GenerateConfiguration();
                File.WriteAllText("settings.txt", config);
                //MessageBox.Show("Settings saved successfully!", "Save Settings", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateConfiguration()
        {
            StringBuilder sb = new StringBuilder();

            // Append hotkeys
            sb.AppendLine("--hotkeys=hotkeys.txt");

            // Append nickname
            sb.AppendLine($"--name={Nickname.Text}");

            // Append wizard
            sb.AppendLine($"--wizard={(Wizard_list.SelectedItem as ComboBoxItem)?.Content}");

            // Append god
            if (Randomgod.IsChecked == true)
                sb.AppendLine("# --god=" + (God_list.SelectedItem as ComboBoxItem)?.Content);
            else
                sb.AppendLine($"--god={(God_list.SelectedItem as ComboBoxItem)?.Content}");

            // Append resolution
            if (Detect_Resolution.IsChecked == true)
                sb.AppendLine("--detect-resolution");
            else if (Choose_Resolution.IsChecked == true)
                sb.AppendLine($"--resolution={(Resolution_list.SelectedItem as ComboBoxItem)?.Content}");

            // Append fullscreen
            sb.AppendLine(Fullscreen.IsChecked == true ? "--fullscreen" : "# --fullscreen");

            // Append widgets
            sb.AppendLine(Widgets.IsChecked == true ? "--no-widgets" : "# --no-widgets");

            // Append fog
            sb.AppendLine(Fog.IsChecked == true ? "# --fog" : "--fog");

            // Append map bottom
            sb.AppendLine(MapBottom.IsChecked == true ? "# --no-map-bottom" : "--no-map-bottom");

            // Append glow
            sb.AppendLine(Glow.IsChecked == true ? "# --no-glow" : "--no-glow");

            // Append glow brightness
            sb.AppendLine(Glow_brightness.IsChecked == true ?
                $"--glow-brightness={Glow_brightness_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --glow-brightness={Glow_brightness_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");

            // Append antialiasing
            sb.AppendLine(Antialiasing.IsChecked == true ? "--antialiasing" : "# --antialiasing");

            // Append sun strength
            sb.AppendLine(Sun_strength.IsChecked == true ?
                $"--sun-factor={Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --sun-factor={Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");

            // Append ambient strength
            sb.AppendLine(Ambient_strength.IsChecked == true ?
                $"--ambient-factor={Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --ambient-factor={Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");

            // Append master volume
            sb.AppendLine(Master_volume.IsChecked == true ?
                $"--volume={Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --volume={Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");

            // Append music volume
            sb.AppendLine(Music_volume.IsChecked == true ?
                $"--music-volume={Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --music-volume={Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");

            // Append sound volume
            sb.AppendLine(Sound_volume.IsChecked == true ?
                $"--sound-volume={Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --sound-volume={Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");

            // Append no advisor help speech
            sb.AppendLine(No_advisor_help_speech.IsChecked == true ? "# --no-advisor-help-speech" : "--no-advisor-help-speech");

            // Append network configuration based on selected mode
            if (Host.IsChecked == true && !string.IsNullOrWhiteSpace(network.Text))
                sb.AppendLine($"--zerotier-network={network.Text}");
            if (Guest.IsChecked == true && !string.IsNullOrWhiteSpace(network.Text))
                sb.AppendLine($"--zerotier-network={network.Text}");
            if (Guest.IsChecked == true)
            {
                if (!string.IsNullOrWhiteSpace(IP.Text))
                {
                    sb.AppendLine($"--join={IP.Text}");
                }
                else
                {
                    sb.AppendLine("--join");
                }
            }
            else if (Singleplayer.IsChecked == true)
            {
                sb.AppendLine("# --join");
            }
            // Append player count for hosting or single player mode
            if (P2.IsChecked == true) sb.Append("--host=2").Append(Environment.NewLine);
            if (P3.IsChecked == true) sb.Append("--host=3").Append(Environment.NewLine);
            if (P4.IsChecked == true) sb.Append("--host=4").Append(Environment.NewLine);
            if (P5.IsChecked == true) sb.Append("--host=5").Append(Environment.NewLine);
            if (P6.IsChecked == true) sb.Append("--host=6").Append(Environment.NewLine);
            if (P7.IsChecked == true) sb.Append("--host=7").Append(Environment.NewLine);
            if (P8.IsChecked == true) sb.Append("--host=8").Append(Environment.NewLine);

            // Append selected game mode if available
            if (Host.IsChecked == true && Game_mode_list.SelectedItem != null)
                sb.AppendLine($"--{Game_mode_list.SelectedItem}");

            // Append soul, level, min-level, max-level settings
            sb.AppendLine($"--souls={Start_souls_slider.Value}")
                .AppendLine($"--level={Start_level_slider.Value}")
                .AppendLine($"--min-level={Min_level_slider.Value}")
                .AppendLine($"--max-level={Max_level_slider.Value}");

            // Append random gods, shuffle sides, stutter on desync, no particles, and slaughter settings
            if (Random_gods.IsChecked == true) sb.AppendLine("--random-gods");
            if (Random_gods.IsChecked == false) sb.AppendLine("# --random-gods");
            if (Shuffle_sides.IsChecked == true) sb.AppendLine("--shuffle-sides");
            if (Shuffle_sides.IsChecked == false) sb.AppendLine("# --shuffle-sides");
            if (Stutter_on_desync.IsChecked == true) sb.AppendLine("--stutter-on-desynch");
            if (Stutter_on_desync.IsChecked == false) sb.AppendLine("# --stutter-on-desynch");
            if (No_particles.IsChecked == true) sb.AppendLine("--no-particles");
            if (No_particles.IsChecked == false) sb.AppendLine("# --no-particles");
            if (Slaughter.IsChecked == true) sb.AppendLine($"--slaughter={Slaughter_target_kills.Value}");
            else if (Slaughter.IsChecked == false) sb.AppendLine($"# --slaughter={Slaughter_target_kills.Value}");

            // Append map list and selection settings
            if (string.IsNullOrWhiteSpace(Map_selection.Text)) sb.AppendLine($"--map-list={Map_list1.Text}");
            else sb.AppendLine($"# --map-list={Map_list1.Text}");
            if (Host.IsChecked == true && !string.IsNullOrWhiteSpace(Map_selection.Text))
                sb.AppendLine($"maps/{Map_selection.Text}");
            if (Singleplayer.IsChecked == true && !string.IsNullOrWhiteSpace(Map_selection.Text))
            {
                sb.AppendLine($"maps/{Map_selection.Text}");
            }

            // Append observer and save replays settings
            sb.AppendLine(Observer.IsChecked == true ? "--observer" : "# --observer");
            sb.AppendLine(Savereplays.IsChecked == true ? "--record-folder=replays" : "# --record-folder=replays");

            return sb.ToString();
        }



        private void LoadSettings()
        {
            if (!File.Exists("settings.txt")) return;

            string[] settings = File.ReadAllLines("settings.txt");
            foreach (var setting in settings)
            {
                if (string.IsNullOrWhiteSpace(setting)) continue;

                string key = null;
                string value = null;

                if (setting.Contains('='))
                {
                    // Split by '='
                    string[] parts = setting.Split(new[] { '=' }, 2);
                    key = parts[0].Trim();
                    if (parts.Length > 1)
                    {
                        value = parts[1]?.Trim();
                    }
                }
                else if (setting.Contains('/'))
                {
                    // Split by the first occurrence of '/'
                    int index = setting.IndexOf('/');
                    key = setting.Substring(0, index).Trim(); // Exclude the '/'
                    value = setting.Substring(index + 1).Trim();
                }
                else
                {
                    key = setting.Trim();
                }

                if (string.IsNullOrWhiteSpace(key))
                {
                    continue; // Skip invalid lines
                }

                switch (key)
                {
                    case "--name":
                        if (value != null)
                        {
                            Nickname.Text = value;
                        }
                        break;
                    case "--wizard":
                        if (value != null)
                        {
                            foreach (ComboBoxItem item in Wizard_list.Items)
                            {
                                if (item.Content.ToString() == value)
                                {
                                    Wizard_list.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                        break;
                    case "--god":
                        Randomgod.IsChecked = false;
                        if (value != null)
                        {
                            foreach (ComboBoxItem item in God_list.Items)
                            {
                                if (item.Content.ToString() == value)
                                {
                                    God_list.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                        break;
                    case "# --god":
                        Randomgod.IsChecked = true;
                        if (value != null)
                        {
                            foreach (ComboBoxItem item in God_list.Items)
                            {
                                if (item.Content.ToString() == value)
                                {
                                    God_list.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                        break;
                    case "--resolution":
                        if (value != null)
                        {
                            foreach (ComboBoxItem item in Resolution_list.Items)
                            {
                                if (item.Content.ToString() == value)
                                {
                                    Resolution_list.SelectedItem = item;
                                    Choose_Resolution.IsChecked = true;
                                    break;
                                }
                            }
                        }
                        break;
                    case "--detect-resolution":
                        Detect_Resolution.IsChecked = true;
                        break;
                    case "--fullscreen":
                        Fullscreen.IsChecked = true;
                        break;
                    case "# --fullscreen":
                        Fullscreen.IsChecked = false;
                        break;
                    case "--no-widgets":
                        Widgets.IsChecked = true;
                        break;
                    case "# --no-widgets":
                        Widgets.IsChecked = false;
                        break;
                    case "# --fog":
                        Fog.IsChecked = true;
                        break;
                    case "--fog":
                        Fog.IsChecked = false;
                        break;
                    case "# --no-map-bottom":
                        MapBottom.IsChecked = true;
                        break;
                    case "--no-map-bottom":
                        MapBottom.IsChecked = false;
                        break;
                    case "# --no-glow":
                        Glow.IsChecked = true;
                        break;
                    case "--no-glow":
                        Glow.IsChecked = false;
                        break;
                    case "--glow-brightness":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double glowBrightness))
                        {
                            Glow_brightness_slider.Value = glowBrightness;
                            Glow_brightness.IsChecked = true;
                        }
                        break;
                    case "--antialiasing":
                        Antialiasing.IsChecked = true;
                        break;
                    case "# --antialiasing":
                        Antialiasing.IsChecked = false;
                        break;
                    case "--sun-factor":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double sunStrength))
                        {
                            Sun_strength_slider.Value = sunStrength;
                            Sun_strength.IsChecked = true;
                        }
                        break;
                    case "--ambient-factor":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double ambientStrength))
                        {
                            Ambient_strength_slider.Value = ambientStrength;
                            Ambient_strength.IsChecked = true;
                        }
                        break;
                    case "--volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double masterVolume))
                        {
                            Master_volume_slider.Value = masterVolume;
                            Master_volume.IsChecked = true;
                        }
                        break;
                    case "--music-volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double musicVolume))
                        {
                            Music_volume_slider.Value = musicVolume;
                            Music_volume.IsChecked = true;
                        }
                        break;
                    case "--sound-volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double soundVolume))
                        {
                            Sound_volume_slider.Value = soundVolume;
                            Sound_volume.IsChecked = true;
                        }
                        break;
                    case "--no-advisor-help-speech":
                        No_advisor_help_speech.IsChecked = false;
                        break;
                    case "# --no-advisor-help-speech":
                        No_advisor_help_speech.IsChecked = true;
                        break;
                    case "--host":
                        if (value != null)
                        {
                            int hostValue = int.Parse(value);
                            switch (hostValue)
                            {
                                case 2:
                                    Host.IsChecked = true;
                                    P2.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 4;
                                    break;
                                case 3:
                                    Host.IsChecked = true;
                                    P3.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 4:
                                    Host.IsChecked = true;
                                    P4.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 5;
                                    break;
                                case 5:
                                    Host.IsChecked = true;
                                    P5.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 6:
                                    Host.IsChecked = true;
                                    P6.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 6;
                                    break;
                                case 7:
                                    Host.IsChecked = true;
                                    P7.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 8:
                                    Host.IsChecked = true;
                                    P8.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 7;
                                    break;
                                default:
                                    // Handle invalid host value
                                    break;
                            }
                        }
                        break;
                    case "--join":
                        Guest.IsChecked = true;
                        if (value != null)
                        {
                            IP.Text = value;
                        }
                        break;
                    case "# --join":
                        Singleplayer.IsChecked = true;
                        break;
                    case "--zerotier-network":
                        if (value != null)
                        {
                            network.Text = value;
                        }
                        else
                        {
                            network.Text = "6ab565387ab194c6";
                        }
                            break;
                    case "--souls":
                        if (value != null && double.TryParse(value, out double souls))
                        {
                            Start_souls_slider.Value = souls;
                        }
                        break;
                    case "--level":
                        if (value != null && double.TryParse(value, out double level))
                        {
                            Start_level_slider.Value = level;
                        }
                        break;
                    case "--min-level":
                        if (value != null && double.TryParse(value, out double minlvl))
                        {
                            Min_level_slider.Value = minlvl;
                        }
                        break;
                    case "--max-level":
                        if (value != null && double.TryParse(value, out double maxlvl))
                        {
                            Max_level_slider.Value = maxlvl;
                        }
                        break;
                    case "--random-gods":
                        Random_gods.IsChecked = true;
                        break;
                    case "# --random-gods":
                        Random_gods.IsChecked = false;
                        break;
                    case "--shuffle-sides":
                        Shuffle_sides.IsChecked = true;
                        break;
                    case "# --shuffle-sides":
                        Shuffle_sides.IsChecked = false;
                        break;
                    case "--stutter-on-desynch":
                        Stutter_on_desync.IsChecked = true;
                        break;
                    case "# --stutter-on-desynch":
                        Stutter_on_desync.IsChecked = false;
                        break;
                    case "--no-particles":
                        No_particles.IsChecked = true;
                        break;
                    case "# --no-particles":
                        No_particles.IsChecked = false;
                        break;
                    case "--slaughter":
                        Slaughter.IsChecked = true;
                        Slaughter_target_kills.Value = double.Parse(value);
                        break;
                    case "# --slaughter":
                        Slaughter.IsChecked = false;
                        Slaughter_target_kills.Value = double.Parse(value);
                        break;
                    case "# --map-list":
                        if (value != null)
                        {
                            // Clear existing items in Map_list1 before populating
                            Map_list1.Items.Clear();
                            // Assuming value is a single item, add it to Map_list1
                            Map_list1.Items.Add(value);
                            // Optionally select the item if needed
                            Map_list1.SelectedItem = value;
                        }
                        break;
                    case "maps/":
                        if (value != null)
                        {
                            Map_selection.Text = value;
                        }
                        break;
                    case "--observer":
                        Observer.IsChecked = true;
                        break;
                    case "# --observer":
                        Observer.IsChecked = false;
                        break;
                    case "--record-folder":
                        Savereplays.IsChecked = true;
                        break;
                    case "# --record-folder":
                        Savereplays.IsChecked = false;
                        break;
                    default:
                        Console.WriteLine("Unhandled key: " + key);
                        break;
                }
            }
        }
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MainWindow());
        }
    }
}
