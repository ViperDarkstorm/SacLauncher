using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace SacLauncher
{


    public partial class MainWindow : Window
    {
        private readonly string folderPath;

        public MainWindow()
        {
            InitializeComponent();
            folderPath = AppDomain.CurrentDomain.BaseDirectory;
            LoadReplays();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Player_details_grid.Visibility = Visibility.Hidden;
            Settings_grid.Visibility = Visibility.Hidden;
            Save_button.Visibility = Visibility.Hidden;
            Glow_brightness_slider.Value = 0.5;
            Sun_strength_slider.Value = 1;
            Ambient_strength_slider.Value = 1;
            Master_volume_slider.Value = 0.5;  
            Sound_volume_slider.Value = 0.5;   
            Music_volume_slider.Value = 0.5;   
            Start_souls_slider.Value = 12;
            Max_level_slider.Value = 9;
            Min_level_slider.Value = 5;
            xp_rate_slider.Value = 1.00;
            Slaughter_target_kills.Value = 999;
            LoadSettings();
        }

        private void SetSingleplayerMode()
        {
            Settings_grid.Visibility = Visibility.Hidden;
            Player_details_grid.Visibility = Visibility.Visible;
            Save_button.Visibility = Visibility.Visible;
            Players.Visibility = Visibility.Visible;
            EnterIP.Visibility = Visibility.Hidden;
            IP.Visibility = Visibility.Hidden;
            Game_mode.Visibility = Visibility.Visible;
            Game_mode_list.Visibility = Visibility.Visible;
            Random_gods.Visibility = Visibility.Visible;
            Custom_souls.Visibility = Visibility.Visible;
            Custom_xp_rate.Visibility = Visibility.Visible;
            Custom_level.Visibility = Visibility.Visible;
            Custom_level_bounds.Visibility = Visibility.Visible;
            Shuffle_sides.Visibility = Visibility.Visible;
            Stutter_on_desync.Visibility = Visibility.Visible;
            No_particles.Visibility = Visibility.Visible;
            Slaughter.Visibility = Visibility.Visible;
            Slaughter_target_kills.Visibility = Visibility.Visible;
            Slaughter_kills_number.Visibility = Visibility.Visible;
            Reroll_button.Visibility = Visibility.Visible;
            Map_list.Visibility = Visibility.Visible;
            Map_list1.Visibility = Visibility.Visible;
            Map_select.Visibility = Visibility.Visible;
            Map_selection.Visibility = Visibility.Visible;
            Restart_game.Visibility = Visibility.Hidden;
            Watch_replay.Visibility = Visibility.Visible;
            ReplaysComboBox.Visibility = Visibility.Visible;
            Continue_at.Visibility = Visibility.Hidden;
            Continue_at2.Visibility = Visibility.Visible;
            Frame.Visibility = Visibility.Visible;
            LaunchButton1.Visibility = Visibility.Visible;
            SetPlayerVisibility(Visibility.Visible);
            SetPlayerEnabled(true);
            SetControlsEnabled(true);
            P6.IsChecked = true;
            UpdateNoParticlesCheckbox();
            Singleplayer.IsChecked = true;
        }

        private void SetHostMode()
        {
            Settings_grid.Visibility = Visibility.Hidden;
            Player_details_grid.Visibility = Visibility.Visible;
            Save_button.Visibility = Visibility.Visible;
            Players.Visibility = Visibility.Visible;
            EnterIP.Visibility = Visibility.Hidden;
            IP.Visibility = Visibility.Hidden;
            Game_mode.Visibility = Visibility.Visible;
            Game_mode_list.Visibility = Visibility.Visible;
            Random_gods.Visibility = Visibility.Visible;
            Custom_souls.Visibility = Visibility.Visible;
            Custom_xp_rate.Visibility = Visibility.Visible;
            Custom_level.Visibility = Visibility.Visible;
            Custom_level_bounds.Visibility = Visibility.Visible;
            Shuffle_sides.Visibility = Visibility.Visible;
            Stutter_on_desync.Visibility = Visibility.Visible;
            No_particles.Visibility = Visibility.Visible;
            Slaughter.Visibility = Visibility.Visible;
            Slaughter_target_kills.Visibility = Visibility.Visible;
            Slaughter_kills_number.Visibility = Visibility.Visible;
            Reroll_button.Visibility = Visibility.Visible;
            Map_list.Visibility = Visibility.Visible;
            Map_list1.Visibility = Visibility.Visible;
            Map_select.Visibility = Visibility.Visible;
            Map_selection.Visibility = Visibility.Visible;
            Restart_game.Visibility = Visibility.Visible;
            Watch_replay.Visibility = Visibility.Hidden;
            ReplaysComboBox.Visibility = Visibility.Visible;
            Continue_at.Visibility = Visibility.Visible;
            Continue_at2.Visibility = Visibility.Hidden;
            Frame.Visibility = Visibility.Visible;
            LaunchButton1.Visibility = Visibility.Visible;
            SetPlayerVisibility(Visibility.Visible);
            SetPlayerEnabled(true);
            SetControlsEnabled(true);
            P6.IsChecked = true;
            UpdateNoParticlesCheckbox();
            Host_game.IsChecked = true;
        }

        private void SetJoinMode()
        {
            Settings_grid.Visibility = Visibility.Hidden;
            Player_details_grid.Visibility = Visibility.Visible;
            Save_button.Visibility = Visibility.Visible;
            Players.Visibility = Visibility.Hidden;
            Game_mode.Visibility = Visibility.Hidden;
            Game_mode_list.Visibility = Visibility.Hidden;
            EnterIP.Visibility = Visibility.Visible;
            IP.Visibility = Visibility.Visible;
            Random_gods.Visibility = Visibility.Hidden;
            Custom_souls.Visibility = Visibility.Hidden;
            Custom_xp_rate.Visibility = Visibility.Hidden;
            Custom_level.Visibility = Visibility.Hidden;
            Custom_level_bounds.Visibility = Visibility.Hidden;
            Shuffle_sides.Visibility = Visibility.Hidden;
            Stutter_on_desync.Visibility = Visibility.Hidden;
            No_particles.Visibility = Visibility.Hidden;
            Slaughter.Visibility = Visibility.Hidden;
            Slaughter_target_kills.Visibility = Visibility.Hidden;
            Slaughter_kills_number.Visibility = Visibility.Hidden;
            Reroll_button.Visibility = Visibility.Hidden;
            Map_list.Visibility = Visibility.Hidden;
            Map_list1.Visibility = Visibility.Hidden;
            Map_select.Visibility = Visibility.Hidden;
            Map_selection.Visibility = Visibility.Hidden;
            Restart_game.Visibility = Visibility.Hidden;
            Watch_replay.Visibility = Visibility.Hidden;
            ReplaysComboBox.Visibility = Visibility.Hidden;
            Continue_at.Visibility = Visibility.Hidden;
            Continue_at2.Visibility = Visibility.Hidden;
            Frame.Visibility = Visibility.Hidden;
            LaunchButton1.Visibility = Visibility.Visible;
            SetPlayerVisibility(Visibility.Hidden);
            SetPlayerCheckStates(false);
            SetControlsEnabled(false);
            Join_game.IsChecked = true;
        }

        private void SetSettingsMode()
        {
            Settings_grid.Visibility = Visibility.Visible;
            Player_details_grid.Visibility = Visibility.Hidden;
            Save_button.Visibility = Visibility.Visible;
            Settings.IsChecked = true;
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
                MessageBox.Show("Couldn't locate a maplist file pattern: " + pattern);
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
            UpdateNoParticlesCheckbox();
        }

        private void UpdateNoParticlesCheckbox()
        {
            if (P2.IsChecked == true || P3.IsChecked == true || P4.IsChecked == true || P5.IsChecked == true)
            {
                Random_gods.IsChecked = true;
                Shuffle_sides.IsChecked = true;
                Stutter_on_desync.IsChecked = true;
                No_particles.IsChecked = false;
            }
            else if (P6.IsChecked == true || P7.IsChecked == true || P8.IsChecked == true)
            {
                Random_gods.IsChecked = true;
                Shuffle_sides.IsChecked = true;
                Stutter_on_desync.IsChecked = true;
                No_particles.IsChecked = true;
            }
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
                "1v1" => @"maps-.*-1v1.txt",
                "2v2" => @"maps-.*-2v2.txt",
                "3v3" => @"maps-.*-3v3.txt",
                "4v4" => @"maps-.*-4v4.txt",
                "ffa" when P3.IsChecked == true => @"maps-.*-3ffa.txt",
                "ffa" when P4.IsChecked == true => @"maps-.*-4ffa.txt",
                "ffa" when P5.IsChecked == true => @"maps-.*-5ffa.txt",
                "ffa" when P6.IsChecked == true => @"maps-.*-6ffa.txt",
                "ffa" when P7.IsChecked == true => @"maps-.*-7ffa.txt",
                "ffa" when P8.IsChecked == true => @"maps-.*-8ffa.txt",
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

        private void Singleplayer1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Host_game_button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Join_game_button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void LoadReplays()
        {
            string replaysFolder = Path.Combine(Directory.GetCurrentDirectory(), "replays");

            if (Directory.Exists(replaysFolder))
            {
                var rcpFiles = Directory.GetFiles(replaysFolder, "*.rcp")
                                        .Select(Path.GetFileName)
                                        .ToList();

                ReplaysComboBox.ItemsSource = rcpFiles;
            }
            else
            {
                MessageBox.Show("Replays folder not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReplaysComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFile = ReplaysComboBox.SelectedItem as string;
            if (selectedFile != null)
            {
                MessageBox.Show($"Selected file: {selectedFile}", "File Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            if (Choose_Resolution != null) 
            {
                Choose_Resolution.IsChecked = false;
                Resolution_list.Visibility = Visibility.Hidden;
            }
        }


        private void Choose_Resolution_Checked(object sender, RoutedEventArgs e)
        {
            Detect_Resolution.IsChecked = false;
            Resolution_list.Visibility = Visibility.Visible;
        }

        private void Enable_green_souls_Checked(object sender, RoutedEventArgs e)
        {
            if (Refuse_green_souls != null)
            {
                Refuse_green_souls.IsChecked = false;
            }
        }

        private void Refuse_green_souls_Checked(object sender, RoutedEventArgs e)
        {
            if (Enable_green_souls != null)
            {
                Enable_green_souls.IsChecked = false;
            }
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
            if (Glow_brightness_slider != null)
            {
                Glow_brightness_slider.Value = 0.5;
                Glow_brightness_slider.IsEnabled = true;
            }
        }

        private void Glow_brightness_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Glow_brightness_slider != null)
            {
                Glow_brightness_slider.Value = 0.5;
                Glow_brightness_slider.IsEnabled = false;
            }
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
            if (Sun_strength_slider != null)
            {
                Sun_strength_slider.Value = 1;
                Sun_strength_slider.IsEnabled = true;
            }
        }

        private void Sun_strength_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Sun_strength_slider != null)
            {
                Sun_strength_slider.Value = 1;
                Sun_strength_slider.IsEnabled = false;
            }
        }

        private void Sun_strength_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Sun1.Text = Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Ambient_strength_Checked(object sender, RoutedEventArgs e)
        {
            if (Ambient_strength_slider != null)
            {
                Ambient_strength_slider.Value = 1;
                Ambient_strength_slider.IsEnabled = true;
            }
        }

        private void Ambient_strength_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Ambient_strength_slider != null)
            {
                Ambient_strength_slider.Value = 1;
                Ambient_strength_slider.IsEnabled = false;
            }
        }

        private void Ambient_strength_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Ambient1.Text = Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Master_volume_Checked(object sender, RoutedEventArgs e)
        {
            if (Master_volume_slider != null)
            {
                Master_volume_slider.Value = 0.5;  // Set the slider to a default value when the checkbox is checked
                Master_volume_slider.IsEnabled = true;  // Enable the slider
            }
        }

        private void Master_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Master_volume_slider != null)
            {
                Master_volume_slider.Value = 0;  // Set the slider value to 0 when the checkbox is unchecked
                Master_volume_slider.IsEnabled = true;  // Keep the slider enabled
            }
        }

        private void Master_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Master_volume_slider != null)
            {
                Master1.Text = Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);

                // If the slider value is 0, uncheck the checkbox
                if (Master_volume_slider.Value == 0)
                {
                    Master_volume.IsChecked = false;
                }
                else if (Master_volume_slider.Value > 0 && Master_volume.IsChecked == false)
                {
                    // If the slider is moved above 0, recheck the checkbox if it was unchecked
                    Master_volume.IsChecked = true;
                }
            }
        }


        private void Music_volume_Checked(object sender, RoutedEventArgs e)
        {
            if (Music_volume_slider != null)
            {
                // Set the slider to a default value (0.5) when the checkbox is checked
                Music_volume_slider.Value = 0.5;
            }
        }

        private void Music_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Music_volume_slider != null)
            {
                // Set the slider value to 0 when the checkbox is unchecked
                Music_volume_slider.Value = 0;
            }
        }

        private void Music_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Music_volume_slider != null)
            {
                // Update the label with the current slider value
                Music1.Text = Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);

                // If the slider value is 0, uncheck the checkbox
                if (Music_volume_slider.Value == 0)
                {
                    Music_volume.IsChecked = false;
                }
                else if (Music_volume_slider.Value > 0 && Music_volume.IsChecked == false)
                {
                    // If the slider is moved above 0, recheck the checkbox if it was unchecked
                    Music_volume.IsChecked = true;
                }
            }
        }

        private void Sound_volume_Checked(object sender, RoutedEventArgs e)
        {
            if (Sound_volume_slider != null)
            {
                Sound_volume_slider.Value = 0.5;  // Set the slider to a default value when the checkbox is checked
                Sound_volume_slider.IsEnabled = true;  // Enable the slider
            }
        }

        private void Sound_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Sound_volume_slider != null)
            {
                Sound_volume_slider.Value = 0;  // Set the slider value to 0 when the checkbox is unchecked
                Sound_volume_slider.IsEnabled = true;  // Keep the slider enabled
            }
        }

        private void Sound_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Sound_volume_slider != null)
            {
                Sound1.Text = Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);

                // If the slider value is 0, uncheck the checkbox
                if (Sound_volume_slider.Value == 0)
                {
                    Sound_volume.IsChecked = false;
                }
                else if (Sound_volume_slider.Value > 0 && Sound_volume.IsChecked == false)
                {
                    // If the slider is moved above 0, recheck the checkbox if it was unchecked
                    Sound_volume.IsChecked = true;
                }
            }
        }


        private void No_advisor_help_speech_Checked(object sender, RoutedEventArgs e)
        {
            // Handle no advisor help speech checked event if needed
        }

        /*private void Host_game_Checked(object sender, RoutedEventArgs e)
        {
            Settings_grid.Visibility = Visibility.Hidden;
            Player_details_grid.Visibility = Visibility.Visible;
            Save_button.Visibility = Visibility.Visible;
            Players.Visibility = Visibility.Visible;
            EnterIP.Visibility = Visibility.Hidden;
            IP.Visibility = Visibility.Hidden;
            Game_mode.Visibility = Visibility.Visible;
            Game_mode_list.Visibility = Visibility.Visible;
            Random_gods.Visibility = Visibility.Visible;
            Custom_souls.Visibility = Visibility.Visible;
            Custom_level.Visibility = Visibility.Visible;
            Custom_level_bounds.Visibility = Visibility.Visible;
            Shuffle_sides.Visibility = Visibility.Visible;
            Stutter_on_desync.Visibility = Visibility.Visible;
            No_particles.Visibility = Visibility.Visible;
            Slaughter.Visibility = Visibility.Visible;
            Slaughter_target_kills.Visibility = Visibility.Visible;
            Slaughter_kills_number.Visibility = Visibility.Visible;
            Reroll_button.Visibility = Visibility.Visible;
            Map_list.Visibility = Visibility.Visible;
            Map_list1.Visibility = Visibility.Visible;
            Map_select.Visibility = Visibility.Visible;
            Map_selection.Visibility = Visibility.Visible;
            Restart_game.Visibility = Visibility.Visible;
            Watch_replay.Visibility = Visibility.Hidden;
            ReplaysComboBox.Visibility = Visibility.Visible;
            Continue_at.Visibility = Visibility.Visible;
            Continue_at2.Visibility = Visibility.Hidden;
            Frame.Visibility = Visibility.Visible;
            LaunchButton1.Visibility = Visibility.Visible;
            SetPlayerVisibility(Visibility.Visible);
            SetPlayerEnabled(true);
            SetControlsEnabled(true);
            P6.IsChecked = true;
            UpdateNoParticlesCheckbox();
            Singleplayer1.IsChecked = false;
            Join_game.IsChecked = false;
            Settings.IsChecked = false;
        }
        

        private void Join_game_Checked(object sender, RoutedEventArgs e)
        {
            Settings_grid.Visibility = Visibility.Hidden;
            Player_details_grid.Visibility = Visibility.Visible;
            Save_button.Visibility = Visibility.Visible;
            Players.Visibility = Visibility.Hidden;
            Game_mode.Visibility = Visibility.Hidden;
            Game_mode_list.Visibility = Visibility.Hidden;
            EnterIP.Visibility = Visibility.Visible;
            IP.Visibility = Visibility.Visible;
            Random_gods.Visibility = Visibility.Hidden;
            Custom_souls.Visibility = Visibility.Hidden;
            Custom_level.Visibility = Visibility.Hidden;
            Custom_level_bounds.Visibility = Visibility.Hidden;
            Shuffle_sides.Visibility = Visibility.Hidden;
            Stutter_on_desync.Visibility = Visibility.Hidden;
            No_particles.Visibility = Visibility.Hidden;
            Slaughter.Visibility = Visibility.Hidden;
            Slaughter_target_kills.Visibility = Visibility.Hidden;
            Slaughter_kills_number.Visibility = Visibility.Hidden;
            Reroll_button.Visibility = Visibility.Hidden;
            Map_list.Visibility = Visibility.Hidden;
            Map_list1.Visibility = Visibility.Hidden;
            Map_select.Visibility = Visibility.Hidden;
            Map_selection.Visibility = Visibility.Hidden;
            Restart_game.Visibility = Visibility.Hidden;
            Watch_replay.Visibility = Visibility.Hidden;
            ReplaysComboBox.Visibility = Visibility.Hidden;
            Continue_at.Visibility = Visibility.Hidden;
            Continue_at2.Visibility = Visibility.Hidden;
            Frame.Visibility = Visibility.Hidden;
            LaunchButton1.Visibility = Visibility.Visible;
            SetPlayerVisibility(Visibility.Hidden);
            SetPlayerCheckStates(false);
            SetControlsEnabled(false);
            Singleplayer.IsChecked = false;
            Host_game.IsChecked = false;
            Settings.IsChecked = false;
        }

        private void Singleplayer_Checked(object sender, RoutedEventArgs e)
        {
            { 
                Settings_grid.Visibility = Visibility.Hidden;
                Player_details_grid.Visibility = Visibility.Visible;
                Save_button.Visibility = Visibility.Visible;
                Players.Visibility = Visibility.Visible;
                EnterIP.Visibility = Visibility.Hidden;
                IP.Visibility = Visibility.Hidden;
                Game_mode.Visibility = Visibility.Visible;
                Game_mode_list.Visibility = Visibility.Visible;
                Random_gods.Visibility = Visibility.Visible;
                Custom_souls.Visibility = Visibility.Visible;
                Custom_level.Visibility = Visibility.Visible;
                Custom_level_bounds.Visibility = Visibility.Visible;
                Shuffle_sides.Visibility = Visibility.Visible;
                Stutter_on_desync.Visibility = Visibility.Visible;
                No_particles.Visibility = Visibility.Visible;
                Slaughter.Visibility = Visibility.Visible;
                Slaughter_target_kills.Visibility = Visibility.Visible;
                Slaughter_kills_number.Visibility = Visibility.Visible;
                Reroll_button.Visibility = Visibility.Visible;
                Map_list.Visibility = Visibility.Visible;
                Map_list1.Visibility = Visibility.Visible;
                Map_select.Visibility = Visibility.Visible;
                Map_selection.Visibility = Visibility.Visible;
                Restart_game.Visibility = Visibility.Hidden;
                Watch_replay.Visibility = Visibility.Visible;
                ReplaysComboBox.Visibility = Visibility.Visible;
                Continue_at.Visibility = Visibility.Hidden;
                Continue_at2.Visibility = Visibility.Visible;
                Frame.Visibility = Visibility.Visible;
                LaunchButton1.Visibility = Visibility.Visible;
                SetPlayerVisibility(Visibility.Visible);
                SetPlayerEnabled(true);
                SetControlsEnabled(true);
                P6.IsChecked = true;
                UpdateNoParticlesCheckbox();
                Host_game.IsChecked = false;
                Join_game.IsChecked = false;
                Settings.IsChecked = false; 
            }
        }

        private void Settings_Checked(object sender, RoutedEventArgs e)
        {
            Player_details_grid.Visibility = Visibility.Hidden;
            Settings_grid.Visibility = Visibility.Visible;
            Singleplayer.IsChecked = false;
            Host_game.IsChecked = false;
            Join_game.IsChecked = false;
            Settings.IsChecked = true;
        }*/



        private void Start_souls_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void xp_rate_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Xprate1.Text = xp_rate_slider.Value.ToString("F2", CultureInfo.InvariantCulture);
        }

        private void Min_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Min_level_slider == null || Max_level_slider == null)
                return;

            // Ensure the minimum level does not exceed the start level
            if (Min_level_slider.Value > Max_level_slider.Value)
            {
                Min_level_slider.Value = Max_level_slider.Value;
            }
        }

        private void Max_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Min_level_slider == null || Max_level_slider == null)
                return;

            // Ensure the maximum level is not below the start level
            if (Max_level_slider.Value < Min_level_slider.Value)
            {
                Max_level_slider.Value = Min_level_slider.Value;
            }
        }

        private void Random_gods_Checked(object sender, RoutedEventArgs e)
        {
            // Handle random gods checked event if needed
        }

        private void Custom_souls_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Custom_XP_rate_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Custom_level_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Custom_level_bounds_Checked(object sender, RoutedEventArgs e)
        {

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

        private void Restart_game_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Watch_replay_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Resetbutton_Click(object sender, RoutedEventArgs e)
        {
            network.Text = "6ab565387ab194c6";
            Host_game.IsChecked = true;
            P6.IsChecked = true;
            Start_souls_slider.Value = 12;
            Max_level_slider.Value = 9;
            Min_level_slider.Value = 5;
            xp_rate_slider.Value = 1.00;
            Randomgod.IsChecked = true;
            Random_gods.IsChecked = true;
            Custom_souls.IsChecked = false;
            Custom_xp_rate.IsChecked = false;
            Custom_level.IsChecked = false;
            Custom_level_bounds.IsChecked = false;
            Shuffle_sides.IsChecked = true;
            Stutter_on_desync.IsChecked = true;
            No_particles.IsChecked = true;
            Slaughter.IsChecked = false;
            Slaughter_target_kills.Value = 999; 
            Observer.IsChecked = false;
            Watch_replay.IsChecked = false;
            Restart_game.IsChecked = false;
            Frame.Clear();
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
            Random_gods.IsEnabled = isEnabled;
            Shuffle_sides.IsEnabled = isEnabled;
            Stutter_on_desync.IsEnabled = isEnabled;
            No_particles.IsEnabled = isEnabled;
            Slaughter.IsEnabled = isEnabled;
        }


        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void Menus_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton checkedToggleButton)
            {
                return;
            }

            foreach (ToggleButton toggleButton in Menus.Children.OfType<ToggleButton>())
            {
                // Set IsChecked to true only for the clicked button, false for others
                toggleButton.IsChecked = toggleButton == checkedToggleButton;

                // Make other buttons non-interactive, only the active one can be clicked again
                toggleButton.IsHitTestVisible = toggleButton != checkedToggleButton;
            }
            // Apply specific logic based on which ToggleButton was checked
            switch (checkedToggleButton.Name)
            {
                case "Singleplayer":
                    // Logic for ToggleButton1
                    SetSingleplayerMode();
                    break;
                case "Host_game":
                    // Logic for ToggleButton2
                    SetHostMode();
                    break;
                case "Join_game":
                    // Logic for ToggleButton3
                    SetJoinMode();
                    break;
                case "Settings":
                    // Logic for ToggleButton4
                    SetSettingsMode();
                    break;
            }
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Settings saved successfully!", "Save Settings", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Quit_button_Click(object sender, RoutedEventArgs e)
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
            Restart_game.IsChecked = false;
            Watch_replay.IsChecked = false;
            Frame.Clear();
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
            sb.AppendLine("--hotkeys=hotkeys.txt");
            sb.AppendLine($"--name={Nickname.Text}");
            sb.AppendLine($"--wizard={(Wizard_list.SelectedItem as ComboBoxItem)?.Content}");
            if (Randomgod.IsChecked == true)
                sb.AppendLine("# --god=" + (God_list.SelectedItem as ComboBoxItem)?.Content);
            else
                sb.AppendLine($"--god={(God_list.SelectedItem as ComboBoxItem)?.Content}");
            if (Detect_Resolution.IsChecked == true)
                sb.AppendLine("--detect-resolution");
            else if (Choose_Resolution.IsChecked == true)
                sb.AppendLine($"--resolution={(Resolution_list.SelectedItem as ComboBoxItem)?.Content}");
            sb.AppendLine(Fullscreen.IsChecked == true ? "--fullscreen" : "# --fullscreen");
            sb.AppendLine(Widgets.IsChecked == true ? "--no-widgets" : "# --no-widgets");
            sb.AppendLine(Fog.IsChecked == true ? "# --fog" : "--fog");
            sb.AppendLine(MapBottom.IsChecked == true ? "# --no-map-bottom" : "--no-map-bottom");
            sb.AppendLine(Glow.IsChecked == true ? "# --no-glow" : "--no-glow");
            sb.AppendLine(Glow_brightness.IsChecked == true ?
                $"--glow-brightness={Glow_brightness_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --glow-brightness={Glow_brightness_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");
            sb.AppendLine(Antialiasing.IsChecked == true ? "--antialiasing" : "# --antialiasing");
            sb.AppendLine(Sun_strength.IsChecked == true ?
                $"--sun-factor={Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --sun-factor={Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");
            sb.AppendLine(Ambient_strength.IsChecked == true ?
                $"--ambient-factor={Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --ambient-factor={Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");
            sb.AppendLine(Master_volume.IsChecked == true ?
                $"--volume={Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --volume={Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");
            sb.AppendLine(Music_volume.IsChecked == true ?
                $"--music-volume={Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --music-volume={Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");
            sb.AppendLine(Sound_volume.IsChecked == true ?
                $"--sound-volume={Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}" :
                $"# --sound-volume={Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture)}");
            sb.AppendLine(No_advisor_help_speech.IsChecked == true ? "# --no-advisor-help-speech" : "--no-advisor-help-speech");
            if (Host_game.IsChecked == true && !string.IsNullOrWhiteSpace(network.Text))
                sb.AppendLine($"--zerotier-network={network.Text}");
            if (Join_game.IsChecked == true && !string.IsNullOrWhiteSpace(network.Text))
                sb.AppendLine($"--zerotier-network={network.Text}");
            if (Join_game.IsChecked == true)
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
            if (Host_game.IsChecked == true && P2.IsChecked == true) sb.Append("--host=2").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && P3.IsChecked == true) sb.Append("--host=3").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && P4.IsChecked == true) sb.Append("--host=4").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && P5.IsChecked == true) sb.Append("--host=5").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && P6.IsChecked == true) sb.Append("--host=6").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && P7.IsChecked == true) sb.Append("--host=7").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && P8.IsChecked == true) sb.Append("--host=8").Append(Environment.NewLine);
            if (Host_game.IsChecked == true && Game_mode_list.SelectedItem != null)
            {
                sb.AppendLine($"--{Game_mode_list.SelectedItem}");
            }
            sb.AppendLine($"--souls={Start_souls_slider.Value}");
            sb.AppendLine($"--level={Min_level_slider.Value}");
            sb.AppendLine($"--min-level={Min_level_slider.Value}");
            sb.AppendLine($"--max-level={Max_level_slider.Value}");
            sb.AppendLine($"--xp-rate={xp_rate_slider.Value}");
            sb.AppendLine(Random_gods.IsChecked == true ? "--random-gods" : "# --random-gods");
            sb.AppendLine(Custom_souls.IsChecked == true ? "--no-synchronize-souls" : "# --no-synchronize-souls");
            sb.AppendLine(Custom_xp_rate.IsChecked == true ? "--no-synchronize-xp-rate" : "# --no-synchronize-xp-rate");
            sb.AppendLine(Custom_level.IsChecked == true ? "--no-synchronize-level" : "# --no-synchronize-level");
            sb.AppendLine(Custom_level_bounds.IsChecked == true ? "--no-synchronize-level-bounds" : "# --no-synchronize-level-bounds");
            sb.AppendLine(Shuffle_sides.IsChecked == true ? "--shuffle-sides" : "# --shuffle-sides");
            sb.AppendLine(Stutter_on_desync.IsChecked == true ? "--stutter-on-desynch" : "# --stutter-on-desynch");
            sb.AppendLine(No_particles.IsChecked == true ? "--no-particles" : "# --no-particles");
            sb.AppendLine(Slaughter.IsChecked == true ? $"--slaughter={Slaughter_target_kills.Value}" : $"# --slaughter={Slaughter_target_kills.Value}");

            // Append map list and selection settings
            if (string.IsNullOrWhiteSpace(Map_selection.Text) && !string.IsNullOrWhiteSpace(Map_list1.Text)) sb.AppendLine($"--map-list={Map_list1.Text}");
            else if (!string.IsNullOrWhiteSpace(Map_list1.Text)) sb.AppendLine($"# --map-list={Map_list1.Text}");
            if (Host_game.IsChecked == true && !string.IsNullOrWhiteSpace(Map_selection.Text))
                sb.AppendLine($"maps/{Map_selection.Text}");
            if (Singleplayer.IsChecked == true && !string.IsNullOrWhiteSpace(Map_selection.Text))
            {
                sb.AppendLine($"maps/{Map_selection.Text}");
            }

            // Append observer and save replays settings
            sb.AppendLine(Observer.IsChecked == true ? "--observer" : "# --observer");
            if (Enable_green_souls.IsChecked == true) sb.AppendLine("--green-ally-souls");
            if (Refuse_green_souls.IsChecked == true) sb.AppendLine("--refuse-green-souls");
            sb.AppendLine(Savereplays.IsChecked == true ? "--record-folder=replays" : "# --record-folder=replays");
            if (Host_game.IsChecked == true && Restart_game.IsChecked == true)
            {
                sb.AppendLine($"--continue=replays/{ReplaysComboBox.Text}");
                if (!string.IsNullOrWhiteSpace(Frame.Text))sb.AppendLine($"--continue-at={Frame.Text}");
            }
            if (Singleplayer.IsChecked == true && Watch_replay.IsChecked == true)
            {
                sb.AppendLine($"--play=replays/{ReplaysComboBox.Text}");
                if (!string.IsNullOrWhiteSpace(Frame.Text)) sb.AppendLine($"--continue-at={Frame.Text}");
            }
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
                    case "# --glow-brightness":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double glowBrightness1))
                        {
                            Glow_brightness_slider.Value = glowBrightness1;
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
                    case "# --sun-factor":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double sunStrength1))
                        {
                            Sun_strength_slider.Value = sunStrength1;
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
                    case "# --ambient-factor":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double ambientStrength1))
                        {
                            Ambient_strength_slider.Value = ambientStrength1;
                            Ambient_strength.IsChecked = true;
                        }
                        break;
                    case "--volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double masterVolume))
                        {
                            if (masterVolume == 0)
                            {
                                Master_volume_slider.Value = 0;  // Ensure slider is set to 0 first
                                Master_volume.IsChecked = false; // Then uncheck the checkbox
                            }
                            else
                            {
                                Master_volume.IsChecked = true;
                                Master_volume_slider.Value = masterVolume;
                            }
                        }
                        break;

                    case "# --volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double masterVolume1))
                        {
                            if (masterVolume1 == 0)
                            {
                                Master_volume_slider.Value = 0;  // Ensure slider is set to 0 first
                                Master_volume.IsChecked = false; // Then uncheck the checkbox
                            }
                            else
                            {
                                Master_volume.IsChecked = true;
                                Master_volume_slider.Value = masterVolume1;
                            }
                        }
                        break;
                    case "--music-volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double musicVolume))
                        {
                            if (musicVolume == 0)
                            {
                                Music_volume_slider.Value = 0;  // Ensure slider is set to 0 first
                                Music_volume.IsChecked = false; // Then uncheck the checkbox
                            }
                            else
                            {
                                Music_volume.IsChecked = true;
                                Music_volume_slider.Value = musicVolume;
                            }
                        }
                        break;
                    case "# --music-volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double musicVolume1))
                        {
                            if (musicVolume1 == 0)
                            {
                                Music_volume_slider.Value = 0;  // Ensure slider is set to 0 first
                                Music_volume.IsChecked = false; // Then uncheck the checkbox
                            }
                            else
                            {
                                Music_volume.IsChecked = true;
                                Music_volume_slider.Value = musicVolume1;
                            }
                        }
                        break;
                    case "--sound-volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double soundVolume))
                        {
                            if (soundVolume == 0)
                            {
                                Sound_volume_slider.Value = 0;  // Ensure slider is set to 0 first
                                Sound_volume.IsChecked = false; // Then uncheck the checkbox
                            }
                            else
                            {
                                Sound_volume.IsChecked = true;
                                Sound_volume_slider.Value = soundVolume;
                            }
                        }
                        break;
                    case "# --sound-volume":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double soundVolume1))
                        {
                            if (soundVolume1 == 0)
                            {
                                Sound_volume_slider.Value = 0;  // Ensure slider is set to 0 first
                                Sound_volume.IsChecked = false; // Then uncheck the checkbox
                            }
                            else
                            {
                                Sound_volume.IsChecked = true;
                                Sound_volume_slider.Value = soundVolume1;
                            }
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
                                    Host_game.IsChecked = true;
                                    P2.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 4;
                                    break;
                                case 3:
                                    Host_game.IsChecked = true;
                                    P3.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 4:
                                    Host_game.IsChecked = true;
                                    P4.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 5;
                                    break;
                                case 5:
                                    Host_game.IsChecked = true;
                                    P5.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 6:
                                    Host_game.IsChecked = true;
                                    P6.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 6;
                                    break;
                                case 7:
                                    Host_game.IsChecked = true;
                                    P7.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 8:
                                    Host_game.IsChecked = true;
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
                        Join_game.IsChecked = true;
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
                    case "--xp-rate":
                        if (value != null && double.TryParse(value, out double xprate))
                        {
                            xp_rate_slider.Value = xprate;
                        }
                        break;
                    case "--random-gods":
                        Random_gods.IsChecked = true;
                        break;
                    case "# --random-gods":
                        Random_gods.IsChecked = false;
                        break;
                    case "--no-synchronize-souls":
                        Custom_souls.IsChecked = true;
                        break;
                    case "# --no-synchronize-souls":
                        Custom_souls.IsChecked = false;
                        break;
                    case "--no-synchronize-xp-rate":
                        Custom_xp_rate.IsChecked = true;
                        break;
                    case "# --no-synchronize-xp-rate":
                        Custom_xp_rate.IsChecked = false;
                        break;
                    case "--no-synchronize-level":
                        Custom_level.IsChecked = true;
                        break;
                    case "# --no-synchronize-level":
                        Custom_level.IsChecked = false;
                        break;
                    case "--no-synchronize-level-bounds":
                        Custom_level_bounds.IsChecked = true;
                        break;
                    case "# --no-synchronize-level-bounds":
                        Custom_level_bounds.IsChecked = false;
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
                        if(value != null && double.TryParse(value, out double slaughter))
                        {
                            Slaughter_target_kills.Value = slaughter;
                        }
                        break;
                    case "# --slaughter":
                        Slaughter.IsChecked = false;
                        if (value != null && double.TryParse(value, out double slaughter1))
                        {
                            Slaughter_target_kills.Value = slaughter1;
                        }
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
                    case "--green-ally-souls":
                        Enable_green_souls.IsChecked = true;
                        break;
                    case "--refuse-green-souls":
                        Refuse_green_souls.IsChecked = true;
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
    }
}
