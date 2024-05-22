﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace SacLauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
        }


        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Handle the window content rendered event if needed
        }

        private void Nickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Handle nickname text changed event if needed
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

        }

        private void Detect_Resolution_Checked(object sender, RoutedEventArgs e)
        {
            Choose_Resolution.IsChecked = false;
        }

        private void Choose_Resolution_Checked(object sender, RoutedEventArgs e)
        {
            Detect_Resolution.IsChecked = false;
        }

        private void Resolution_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle resolution selection change event if needed
        }

        private void Fullscreen_Checked(object sender, RoutedEventArgs e)
        {
            // Handle fullscreen checked event if needed
        }

        private void Fullscreen_UnChecked(object sender, RoutedEventArgs e)
        {
            // Handle fullscreen unchecked event if needed
        }

        private void Widgets_Checked(object sender, RoutedEventArgs e)
        {
            // Handle widgets checked event if needed
        }

        private void Widgets_UnChecked(object sender, RoutedEventArgs e)
        {
            // Handle widgets unchecked event if needed
        }

        private void Fog_Checked(object sender, RoutedEventArgs e)
        {
            // Handle fog checked event if needed
        }

        private void Fog_UnChecked(object sender, RoutedEventArgs e)
        {
            // Handle fog unchecked event if needed
        }

        private void MapBottom_Checked(object sender, RoutedEventArgs e)
        {
            // Handle map bottom checked event if needed
        }

        private void MapBottom_UnChecked(object sender, RoutedEventArgs e)
        {
            // Handle map bottom unchecked event if needed
        }

        private void Glow_Checked(object sender, RoutedEventArgs e)
        {
            // Handle glow checked event if needed
        }

        private void Glow_UnChecked(object sender, RoutedEventArgs e)
        {
            // Handle glow unchecked event if needed
        }

        private void Glow_brightness_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Glow_brightness_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Glow_brightness_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Glow1.Text = Glow_brightness_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Antialiasing_Checked(object sender, RoutedEventArgs e)
        {
            // Handle antialiasing checked event if needed
        }

        private void Antialiasing_UnChecked(object sender, RoutedEventArgs e)
        {
            // Handle antialiasing unchecked event if needed
        }

        private void Sun_strength_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Sun_strength_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Sun_strength_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Sun.Text = Sun_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Ambient_strength_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Ambient_strength_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Ambient_strength_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Ambient.Text = Ambient_strength_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Master_volume_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Master_volume_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Master_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Master.Text = Master_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Music_volume_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Music_volume_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Music_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Music.Text = Music_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void Sound_volume_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Sound_volume_UnChecked(object sender, RoutedEventArgs e)
        {

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
            P2.Visibility = Visibility.Visible;
            P3.Visibility = Visibility.Visible;
            P4.Visibility = Visibility.Visible;
            P5.Visibility = Visibility.Visible;
            P6.Visibility = Visibility.Visible;
            P7.Visibility = Visibility.Visible;
            P8.Visibility = Visibility.Visible;
            Game_mode_list.IsEnabled = true;
            Random_gods.IsChecked = true;
            Random_gods.IsEnabled = true;
            Shuffle_sides.IsChecked = true;
            Shuffle_sides.IsEnabled = true;
            Stutter_on_desync.IsChecked = true;
            Stutter_on_desync.IsEnabled = true;
            No_particles.IsChecked = true;
            No_particles.IsEnabled = true;
            Slaughter.IsEnabled = true;
            Map_list.IsEnabled = true;
            Map_list1.IsEnabled = true;
            Map_selection.IsEnabled = true;
            Map_select.IsEnabled = true;
        }

        private void Guest_Checked(object sender, RoutedEventArgs e)
        {
            P2.Visibility = Visibility.Hidden;
            P3.Visibility = Visibility.Hidden;
            P4.Visibility = Visibility.Hidden;
            P5.Visibility = Visibility.Hidden;
            P6.Visibility = Visibility.Hidden;
            P7.Visibility = Visibility.Hidden;
            P8.Visibility = Visibility.Hidden;
            P2.IsChecked = false;
            P3.IsChecked = false;
            P4.IsChecked = false;
            P5.IsChecked = false;
            P6.IsChecked = false;
            P7.IsChecked = false;
            P8.IsChecked = false;
            Game_mode_list.IsEnabled = false;
            Random_gods.IsChecked = false;
            Random_gods.IsEnabled = false;
            Shuffle_sides.IsChecked = false;
            Shuffle_sides.IsEnabled = false;
            Stutter_on_desync.IsChecked = false;
            Stutter_on_desync.IsEnabled = false;
            No_particles.IsChecked = false;
            No_particles.IsEnabled = false;
            Map_list.IsEnabled = false;
            Map_list.IsChecked = false;
            Map_list1.IsEnabled = false;
            Map_selection.IsEnabled = false;
            Map_select.IsEnabled = false;
            Map_select.IsChecked = false;
            Slaughter.IsEnabled = false;
        }

        private void Singleplayer_Checked(object sender, RoutedEventArgs e)
        {
            P2.Visibility = Visibility.Hidden;
            P3.Visibility = Visibility.Hidden;
            P4.Visibility = Visibility.Hidden;
            P5.Visibility = Visibility.Hidden;
            P6.Visibility = Visibility.Hidden;
            P7.Visibility = Visibility.Hidden;
            P8.Visibility = Visibility.Hidden;
            P2.IsChecked = false;
            P3.IsChecked = false;
            P4.IsChecked = false;
            P5.IsChecked = false;
            P6.IsChecked = false;
            P7.IsChecked = false;
            P8.IsChecked = false;
            Game_mode_list.IsEnabled = false;
            Random_gods.IsChecked = false;
            Shuffle_sides.IsChecked = false;
            Shuffle_sides.IsEnabled = false;
            Stutter_on_desync.IsChecked = false;
            Stutter_on_desync.IsEnabled = false;
            No_particles.IsChecked = false;
            No_particles.IsEnabled = false;
            Slaughter.IsEnabled = true;
            Map_list.IsEnabled = true;
            Map_select.IsEnabled= true;
        }

        private void P2_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("1v1");
            Game_mode_list.SelectedIndex = 0;
            Map_list1.SelectedIndex = 4;
            No_particles.IsChecked = false;
        }

        private void P2_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void P3_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("FFA");
            Game_mode_list.SelectedIndex = 0;
            No_particles.IsChecked = false;
        }

        private void P3_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void P4_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("2v2");
            Game_mode_list.Items.Add("FFA");
            Game_mode_list.SelectedIndex = 0;
            Map_list1.SelectedIndex = 5;
            No_particles.IsChecked = false;
        }

        private void P4_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void P5_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("FFA");
            Game_mode_list.SelectedIndex = 0;
            No_particles.IsChecked = false;
        }
        private void P5_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void P6_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("3v3");
            Game_mode_list.Items.Add("FFA");
            Game_mode_list.SelectedIndex = 0;
            Map_list1.SelectedIndex = 6;
            No_particles.IsChecked = true;
        }
        private void P6_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void P7_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("FFA");
            Game_mode_list.SelectedIndex = 0;
            No_particles.IsChecked = true;
        }
        private void P7_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void P8_Checked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Add("4v4");
            Game_mode_list.Items.Add("FFA");
            Game_mode_list.SelectedIndex = 0;
            Map_list1.SelectedIndex = 7;
            No_particles.IsChecked = true;
        }

        private void P8_Unchecked(object sender, RoutedEventArgs e)
        {
            Game_mode_list.Items.Clear();
        }

        private void Game_mode_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Start_souls_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Start_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Min_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Max_level_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Random_gods_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Random_gods_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Shuffle_sides_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Shuffle_sides_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Stutter_on_desynch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Stutter_on_desynch_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void No_particles_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void No_particles_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Slaughter_Checked(object sender, RoutedEventArgs e)
        {
            Slaugher_target_kills.IsEnabled = true;
        }

        private void Slaughter_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void Slaugher_target_kills_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Map_list_Checked(object sender, RoutedEventArgs e)
        {
            Map_list1.IsEnabled = true;
        }

        private void Map_list_UnChecked(object sender, RoutedEventArgs e)
        {
            Map_list1.IsEnabled = false;
        }

        private void Map_select_Checked(object sender, RoutedEventArgs e)
        {
            Map_selection.IsEnabled = true;
        }

        private void Map_select_UnChecked(object sender, RoutedEventArgs e)
        {
            Map_selection.IsEnabled = false;
        }

        private void Savereplays_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
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
            StringBuilder sb = new StringBuilder();
            sb.Append("--hotkeys=hotkeys.txt").Append(Environment.NewLine);
            sb.AppendFormat("--name={0}", Nickname.Text).Append(Environment.NewLine);
            sb.AppendFormat("--wizard={0}", (Wizard_list.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            if (Randomgod.IsChecked == true) sb.AppendFormat("# --god={0}", (God_list.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            else sb.AppendFormat("--god={0}", (God_list.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            if (Detect_Resolution.IsChecked == true)
            {
                sb.Append("--detect-resolution").Append(Environment.NewLine);
            }
            else if (Choose_Resolution.IsChecked == true)
            {
                sb.AppendFormat("--resolution={0}", (Resolution_list.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            }
            if (Fullscreen.IsChecked == true) sb.Append("--fullscreen").Append(Environment.NewLine);
            else sb.Append("# --fullscreen").Append(Environment.NewLine);
            if (Widgets.IsChecked == true) sb.Append("--no-widgets").Append(Environment.NewLine);
            else sb.Append("# --no-widgets").Append(Environment.NewLine);
            if (Fog.IsChecked == true) sb.Append("# --fog").Append(Environment.NewLine);
            else sb.Append("--fog").Append(Environment.NewLine);
            if (MapBottom.IsChecked == true) sb.Append("# --no-map-bottom").Append(Environment.NewLine);
            else sb.Append("--no-map-bottom").Append(Environment.NewLine);
            if (Glow.IsChecked == true) sb.Append("# --no-glow").Append(Environment.NewLine);
            else sb.Append("--no-glow").Append(Environment.NewLine);
            if (Glow_brightness.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--glow-brightness={0:F1}", Glow_brightness_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --glow-brightness={0}", Glow_brightness_slider.Value).Append(Environment.NewLine);
            if (Antialiasing.IsChecked == true) sb.Append("--antialiasing").Append(Environment.NewLine);
            else sb.Append("# --antialiasing").Append(Environment.NewLine);
            if (Sun_strength.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--sun-factor={0:F1}", Sun_strength_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --sun-factor={0}", Sun_strength_slider.Value).Append(Environment.NewLine);
            if (Ambient_strength.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--ambient-factor={0:F1}", Ambient_strength_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --ambient-factor={0}", Ambient_strength_slider.Value).Append(Environment.NewLine);
            if (Master_volume.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--volume={0:F1}", Master_volume_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --volume={0}", Master_volume_slider.Value).Append(Environment.NewLine);
            if (Music_volume.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--music-volume={0:F1}", Music_volume_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --music-volume={0}", Music_volume_slider.Value).Append(Environment.NewLine);
            if (Sound_volume.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--sound-volume={0:F1}", Sound_volume_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --sound-volume={0}", Sound_volume_slider.Value).Append(Environment.NewLine);
            if (No_advisor_help_speech.IsChecked == true) sb.Append("# --no-advisor-help-speech").Append(Environment.NewLine);
            else sb.Append("--no-advisor-help-speech").Append(Environment.NewLine);
            if (Host.IsChecked == true) sb.Append("--zerotier-network=6ab565387ab194c6").Append(Environment.NewLine);
            if (Guest.IsChecked == true)
            {
                sb.Append("--zerotier-network=6ab565387ab194c6").Append(Environment.NewLine);
                sb.Append("--join").Append(Environment.NewLine);
            }
            if (Singleplayer.IsChecked == true) sb.Append("# --join").Append(Environment.NewLine);
            if (P2.IsChecked == true) sb.Append("--host=2").Append(Environment.NewLine);
            if (P3.IsChecked == true) sb.Append("--host=3").Append(Environment.NewLine);
            if (P4.IsChecked == true) sb.Append("--host=4").Append(Environment.NewLine);
            if (P5.IsChecked == true) sb.Append("--host=5").Append(Environment.NewLine);
            if (P6.IsChecked == true) sb.Append("--host=6").Append(Environment.NewLine);
            if (P7.IsChecked == true) sb.Append("--host=7").Append(Environment.NewLine);
            if (P8.IsChecked == true) sb.Append("--host=8").Append(Environment.NewLine);
            if (Host.IsChecked == true)
            {
                // Append selected game mode if available
                if (Game_mode_list.SelectedItem != null)
                {
                    string selectedMode = Game_mode_list.SelectedItem.ToString();
                    sb.AppendFormat("--{0}", selectedMode).Append(Environment.NewLine);
                }
            }
            sb.AppendFormat("--souls={0}", Start_souls_slider.Value).Append(Environment.NewLine);
            sb.AppendFormat("--level={0}", Start_level_slider.Value).Append(Environment.NewLine);
            sb.AppendFormat("--min-level={0}", Min_level_slider.Value).Append(Environment.NewLine);
            sb.AppendFormat("--max-level={0}", Max_level_slider.Value).Append(Environment.NewLine);
            if (Random_gods.IsChecked == true) sb.Append("--random-gods").Append(Environment.NewLine);
            if (Shuffle_sides.IsChecked == true) sb.Append("--shuffle-sides").Append(Environment.NewLine);
            if (Stutter_on_desync.IsChecked == true) sb.Append("--stutter-on-desynch").Append(Environment.NewLine);
            if (No_particles.IsChecked == true) sb.Append("--no-particles").Append(Environment.NewLine);
            if (Slaughter.IsChecked == true) sb.AppendFormat("--slaughter={0}", Slaugher_target_kills.Value).Append(Environment.NewLine);
            else if (Slaughter.IsChecked == false) sb.AppendFormat("# --slaughter={0}", Slaugher_target_kills.Value).Append(Environment.NewLine);
            if (Map_list.IsChecked == true) sb.AppendFormat("--map-list={0}", (Map_list1.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            else if (Map_list.IsChecked == false) sb.AppendFormat("# --map-list={0}", (Map_list1.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            if (Map_select.IsChecked == true) sb.AppendFormat("maps/{0}", Map_selection.Text).Append(Environment.NewLine);
            else if (Map_select.IsChecked == false ) sb.AppendFormat("# maps/{0}", Map_selection.Text).Append(Environment.NewLine);
            if (Observer.IsChecked == true) sb.Append("--observer").Append(Environment.NewLine);
            if (Savereplays.IsChecked == true) sb.Append("--record-folder=replays").Append(Environment.NewLine);

            File.WriteAllText("settings.txt", sb.ToString());
            MessageBox.Show("Settings saved successfully!", "Save Settings", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        key = setting.Substring(0, index + 1).Trim(); // include the '/' in the key
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
                            break;
                        case "# --join":
                            Singleplayer.IsChecked = true;
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
                        case "--shuffle-sides":
                            Shuffle_sides.IsChecked = true;
                            break;
                        case "--stutter-on-desynch":
                            Stutter_on_desync.IsChecked = true;
                            break;
                        case "--no-particles":
                            No_particles.IsChecked = true;
                            break;
                        case "--slaughter":
                            Slaughter.IsChecked = true;
                            Slaugher_target_kills.Value = double.Parse(value);
                            break;
                    case "# --slaughter":
                        Slaughter.IsChecked = false;
                        Slaugher_target_kills.Value = double.Parse(value);
                        break;
                    case "--map-list":
                            if (value != null)
                            {
                                foreach (ComboBoxItem item in Map_list1.Items)
                                {
                                    if (item.Content.ToString() == value)
                                    {
                                        Map_list1.SelectedItem = item;
                                        Map_list.IsChecked = true;
                                        break;
                                    }
                                }
                            }
                            break;
                    case "# --map-list":
                        if (value != null)
                        {
                            foreach (ComboBoxItem item in Map_list1.Items)
                            {
                                if (item.Content.ToString() == value)
                                {
                                    Map_list1.SelectedItem = item;
                                    Map_list.IsChecked = false;
                                    break;
                                }
                            }
                        }
                        break;
                    case "maps/":
                            if (value != null)
                            {
                                Map_selection.Text = value;
                                Map_select.IsChecked = true;
                            }
                            break;

                    case "# maps/":
                        if (value != null)
                        {
                            Map_selection.Text = value;
                            Map_select.IsChecked = false;
                        }
                        break;
                    case "--observer":
                            Observer.IsChecked = true;
                            break;
                        case "--record-folder":
                            Savereplays.IsChecked = true;
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
