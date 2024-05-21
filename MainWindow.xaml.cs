using System;
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
            Guest.IsChecked = false;
            Singleplayer.IsChecked = false;
            Game_mode_list.IsEnabled = true;
            Start_souls_slider.IsEnabled = true;
            Start_level_slider.IsEnabled = true;
            Min_level_slider.IsEnabled = true;
            Max_level_slider.IsEnabled = true;
            Random_gods.IsEnabled = true;
            Random_gods.IsChecked = true;
            Shuffle_sides.IsEnabled = true;
            Shuffle_sides.IsChecked = true;
            Stutter_on_desync.IsEnabled = true;
            Stutter_on_desync.IsChecked = true;
            No_particles.IsEnabled = true;
            No_particles.IsChecked = true;
            Slaughter.IsEnabled = true;
            Map_list.IsEnabled = true;
            Map_select.IsEnabled = true;
        }

        private void Guest_Checked(object sender, RoutedEventArgs e)
        {
            Host.IsChecked = false;
            Singleplayer.IsChecked = false;
            Game_mode_list.IsEnabled = false;
            Start_souls_slider.IsEnabled = false;
            Start_level_slider.IsEnabled = false;
            Min_level_slider.IsEnabled = false;
            Max_level_slider.IsEnabled = false;
            Random_gods.IsChecked = false;
            Random_gods.IsEnabled = false;
            Shuffle_sides.IsChecked = false;
            Shuffle_sides.IsEnabled = false;
            Stutter_on_desync.IsChecked = false;
            Stutter_on_desync.IsEnabled = false;
            No_particles.IsChecked = false;
            No_particles.IsEnabled = false;
            Slaughter.IsChecked = false;
            Slaughter.IsEnabled = false;
            Map_list.IsEnabled = false;
            Map_list.IsChecked = false;
            Map_list1.IsEnabled= false;
            Map_select.IsEnabled = false;
            Map_select.IsChecked = false;
            Map_selection.IsEnabled = false;
        }

        private void Singleplayer_Checked(object sender, RoutedEventArgs e)
        {
            Host.IsChecked = false;
            Guest.IsChecked = false;
            Game_mode_list.IsEnabled = false;
            Start_souls_slider.IsEnabled = true;
            Start_level_slider.IsEnabled = true;
            Min_level_slider.IsEnabled = true;
            Max_level_slider.IsEnabled = true;
            Random_gods.IsEnabled = true;
            Shuffle_sides.IsEnabled = false;
            Shuffle_sides.IsChecked = false;
            Stutter_on_desync.IsEnabled = false;
            Stutter_on_desync.IsChecked = false;
            No_particles.IsEnabled = false;
            No_particles.IsChecked = false;
            Slaughter.IsEnabled = true;
            Map_list.IsEnabled = true;
            Map_list.IsChecked = false;
            Map_select.IsEnabled = true;
            Map_select.IsChecked = false;
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
            Map_select.IsChecked = false;
            Map_selection.IsEnabled = false;
            Map_list1.IsEnabled = true;
        }

        private void Map_select_Checked(object sender, RoutedEventArgs e)
        {
            Map_list.IsChecked = false;
            Map_list1.IsEnabled = false;
            Map_selection.IsEnabled = true;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--name={0}", Nickname.Text).Append(Environment.NewLine);
            sb.AppendFormat("--wizard={0}", (Wizard_list.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);

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
            if (Sun_strength.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--sun-strength={0:F1}", Sun_strength_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --sun-strength={0}", Sun_strength_slider.Value).Append(Environment.NewLine);
            if (Ambient_strength.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--ambient-strength={0:F1}", Ambient_strength_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --ambient-strength={0}", Ambient_strength_slider.Value).Append(Environment.NewLine);
            if (Master_volume.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--volume={0:F1}", Master_volume_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --volume={0}", Master_volume_slider.Value).Append(Environment.NewLine);
            if (Music_volume.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--music-volume={0:F1}", Music_volume_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --music-volume={0}", Music_volume_slider.Value).Append(Environment.NewLine);
            if (Sound_volume.IsChecked == true) sb.AppendFormat(CultureInfo.InvariantCulture, "--sound-volume={0:F1}", Sound_volume_slider.Value).Append(Environment.NewLine);
            else sb.AppendFormat("# --sound-volume={0}", Sound_volume_slider.Value).Append(Environment.NewLine);
            if (No_advisor_help_speech.IsChecked == true) sb.Append("# --no-advisor-help-speech").Append(Environment.NewLine);
            else sb.Append("--no-advisor-help-speech").Append(Environment.NewLine);
            if (Host.IsChecked == true)
            {
                sb.Append("--zerotier-network=6ab565387ab194c6").Append(Environment.NewLine);
                sb.Append("--host").Append(Environment.NewLine);
            }
            else if (Guest.IsChecked == true)
            {
                sb.Append("--zerotier-network=6ab565387ab194c6").Append(Environment.NewLine);
                sb.Append("--join").Append(Environment.NewLine);
            }
            else if (Singleplayer.IsChecked == true)
            {
                sb.Append("# --join").Append(Environment.NewLine);
            }
            if (Host.IsChecked == true) sb.AppendFormat("--{0}", (Game_mode_list.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            sb.AppendFormat("--souls={0}", Start_souls_slider.Value).Append(Environment.NewLine);
            sb.AppendFormat("--level={0}", Start_level_slider.Value).Append(Environment.NewLine);
            sb.AppendFormat("--min-level={0}", Min_level_slider.Value).Append(Environment.NewLine);
            sb.AppendFormat("--max-level={0}", Max_level_slider.Value).Append(Environment.NewLine);
            if (Random_gods.IsChecked == true) sb.Append("--random-gods").Append(Environment.NewLine);
            if (Shuffle_sides.IsChecked == true) sb.Append("--shuffle-sides").Append(Environment.NewLine); 
            if (Stutter_on_desync.IsChecked == true) sb.Append("--stutter-on-desynch").Append(Environment.NewLine);
            if (No_particles.IsChecked == true) sb.Append("--no-particles").Append(Environment.NewLine);
            if (Slaughter.IsChecked == true) sb.AppendFormat("--slaughter={0}", Slaugher_target_kills.Value).Append(Environment.NewLine);
            if (Map_list.IsChecked == true) 
            {
                sb.AppendFormat("--map-list={0}", (Map_list1.SelectedItem as ComboBoxItem)?.Content).Append(Environment.NewLine);
            }
            else if (Map_select.IsChecked == true)
            {
                sb.AppendFormat("maps/{0}", Map_selection.Text).Append(Environment.NewLine);
            }
            if (Observer.IsChecked == true) sb.Append("--observer").Append(Environment.NewLine);

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
                    case "--sun-strength":
                        if (value != null && double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double sunStrength))
                        {
                            Sun_strength_slider.Value = sunStrength;
                            Sun_strength.IsChecked = true;
                        }
                        break;
                    case "--ambient-strength":
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
                        Host.IsChecked = true;
                        break;
                    case "--join":
                        Guest.IsChecked = true;
                        break;
                    case "# --join":
                        Singleplayer.IsChecked = true;
                        break;
                    case "--1v1":
                        if (Game_mode_list.Items.Count > 0)
                        {
                            Game_mode_list.SelectedIndex = 0;
                        }
                        break;
                    case "--2v2":
                        if (Game_mode_list.Items.Count > 1)
                        {
                            Game_mode_list.SelectedIndex = 1;
                        }
                        break;
                    case "--3v3":
                        if (Game_mode_list.Items.Count > 2)
                        {
                            Game_mode_list.SelectedIndex = 2;
                        }
                        break;
                    case "--4v4":
                        if (Game_mode_list.Items.Count > 3)
                        {
                            Game_mode_list.SelectedIndex = 3;
                        }
                        break;
                    case "--FFA":
                        if (Game_mode_list.Items.Count > 4)
                        {
                            Game_mode_list.SelectedIndex = 4;
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
                    case "maps/":
                        if(value != null)
                        {
                            Map_selection.Text = value;
                            Map_select.IsChecked= true;
                        }
                        break;
                    case "--observer":
                        Observer.IsChecked = true;
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
