using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using System;

namespace SacLauncher
{
    public partial class MainWindow : Window
    {
        private readonly string folderPath;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTunnel();
            AddTubes();
            AnimateCamera();
            folderPath = AppDomain.CurrentDomain.BaseDirectory;
            LoadReplays();
            InitializeDefaultImage();
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
            Start_souls_slider.Value = 16;
            Max_level_slider.Value = 9;
            Min_level_slider.Value = 5;
            xp_rate_slider.Value = 1.00;
            Slaughter_target_kills.Value = 999;
            LoadSettings();
        }

        private void InitializeTunnel()
        {
            // Generate the tunnel mesh
            MeshGeometry3D tunnelMesh = TunnelMeshGenerator.CreateTunnelMesh(32, 5, 50);

            // Apply the generated mesh to the GeometryModel3D in XAML
            tunnelModel.Geometry = tunnelMesh;
        }

        private void AnimateCamera()
        {
            // Create a Transform3DGroup to combine rotation and translation
            Transform3DGroup cameraTransformGroup = new Transform3DGroup();

            // Rotation: Rotate around the Z-axis
            AxisAngleRotation3D rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);
            RotateTransform3D rotateTransform = new RotateTransform3D(rotation);

            // Translation: Move along the Z-axis
            TranslateTransform3D translateTransform = new TranslateTransform3D(0, 0, 0);

            // Add transforms to the group
            cameraTransformGroup.Children.Add(rotateTransform);
            cameraTransformGroup.Children.Add(translateTransform);

            // Apply the transformation group to the camera
            camera.Transform = cameraTransformGroup;

            // Animate the rotation angle (rotation around Z-axis)
            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                From = 0,
                To = -360,
                Duration = TimeSpan.FromSeconds(20),
                RepeatBehavior = RepeatBehavior.Forever
            };
            rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotationAnimation);

            // Animate the translation along the Z-axis (move through the tunnel)
            DoubleAnimation translationAnimation = new DoubleAnimation
            {
                From = 100,
                To = 300, // Match total tunnel length
                Duration = TimeSpan.FromSeconds(20), // Adjust for camera speed
                RepeatBehavior = RepeatBehavior.Forever
            };
            translateTransform.BeginAnimation(TranslateTransform3D.OffsetZProperty, translationAnimation);

            // Periodically reposition tubes to prevent gaps
            DispatcherTimer repositionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100) // Adjust frequency as needed
            };
            repositionTimer.Tick += (s, e) => UpdateTunnelPosition();
            repositionTimer.Start();
        }




        private void UpdateTunnelPosition()
        {
        // Move tubes that go out of view to the end of the tunnel
        foreach (ModelVisual3D visual in viewport3D.Children)
            {
            if (visual.Content is GeometryModel3D tubeModel)
                {
                if (tubeModel.Transform is TranslateTransform3D translate)
                    {
                    if (translate.OffsetZ < -50) // Tube goes out of view
                        {
                        translate.OffsetZ += 100; // Reposition to the end of the tunnel
                        }
                    }
                }
            }
        }

        private void AddTubes()
        {
        int tubeCount = 50; // Number of tubes
        double tubeLength = 50; // Length of each tube
        double tubeSpacing = tubeLength; // Spacing equal to tube length to remove gaps

        for (int i = 0; i < tubeCount; i++)
        {
            // Generate a new mesh for the tube
            MeshGeometry3D tunnelMesh = TunnelMeshGenerator.CreateTunnelMesh(32, 5, tubeLength);

            // Create a new GeometryModel3D for the tube
            GeometryModel3D tubeModel = new GeometryModel3D
            {
                Geometry = tunnelMesh,
                Material = new DiffuseMaterial
                {
                    Brush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tunnel.png"))
                    }
                },
                BackMaterial = new DiffuseMaterial
                {
                    Brush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tunnel.png"))
                    }
                }
            };

            // Apply transformation to position the tube
            TranslateTransform3D transform = new TranslateTransform3D(0, 0, i * tubeSpacing);
            tubeModel.Transform = transform;

            // Wrap the model in a ModelVisual3D and add to the Viewport3D
            ModelVisual3D visual = new ModelVisual3D { Content = tubeModel };
            viewport3D.Children.Add(visual);
            }
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
            Random_Spellbook.Visibility = Visibility.Visible;
            Reroll_button.Visibility = Visibility.Visible;
            Map_list.Visibility = Visibility.Visible;
            Map_list1.Visibility = Visibility.Visible;
            Map_preview.Visibility = Visibility.Visible;
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
            Random_Spellbook.Visibility = Visibility.Visible;
            Reroll_button.Visibility = Visibility.Visible;
            Map_list.Visibility = Visibility.Visible;
            Map_list1.Visibility = Visibility.Visible;
            Map_preview.Visibility = Visibility.Visible;
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
            Random_Spellbook.Visibility = Visibility.Hidden;
            Reroll_button.Visibility = Visibility.Hidden;
            Map_list.Visibility = Visibility.Hidden;
            Map_list1.Visibility = Visibility.Hidden;
            Map_preview.Visibility = Visibility.Hidden;
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

        private void Map_list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(Map_list1.SelectedItem is string selectedFileName))
            {
                Map_selection.Text = string.Empty;
                return;
            }

            // Resolve folder paths
            string mainFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string listsFolderPath = Path.Combine(mainFolderPath, "lists");

            // Determine the correct file path
            string selectedFilePath;
            if (File.Exists(Path.Combine(mainFolderPath, selectedFileName)))
            {
                selectedFilePath = Path.Combine(mainFolderPath, selectedFileName);
            }
            else if (File.Exists(Path.Combine(listsFolderPath, selectedFileName)))
            {
                selectedFilePath = Path.Combine(listsFolderPath, selectedFileName);
            }
            else
            {
                Map_selection.Text = "File not found.";
                return;
            }

            // Read a random entry from the file
            if (File.Exists(selectedFilePath))
            {
                string randomEntry = GetRandomEntry(selectedFilePath);

                // Process the entry if it starts with "maps/"
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

        private void UpdateMaplist1Dropdown()
        {
            string selectedType = Game_mode_list.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedType)) return;

            // Resolve the folder path where files are stored
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lists");
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show($"Folder does not exist: {folderPath}");
                return;
            }

            // Construct the regex pattern based on the game mode
            string filePattern = selectedType switch
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

            if (string.IsNullOrEmpty(filePattern))
            {
                MessageBox.Show("No valid file pattern found.");
                return;
            }

            // Populate the dropdown
            PopulateDropdown(folderPath, filePattern);
        }


        private void PopulateDropdown(string folderPath, string pattern)
        {
            // Clear the dropdown items
            Map_list1.Items.Clear();

            // Check if the pattern is valid
            if (string.IsNullOrEmpty(pattern))
            {
                MessageBox.Show("Invalid pattern. Unable to populate dropdown.");
                return;
            }

            Regex regex;
            try
            {
                // Compile the regex pattern
                regex = new Regex(pattern, RegexOptions.IgnoreCase);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Invalid regex pattern: " + ex.Message);
                return;
            }

            try
            {
                // Fetch all map files in the folder
                string[] mapFiles = Directory.GetFiles(folderPath, "maps*.txt");
                foreach (string filePath in mapFiles)
                {
                    string fileName = Path.GetFileName(filePath);

                    // Match files using the regex pattern
                    if (regex.IsMatch(fileName))
                    {
                        Map_list1.Items.Add(fileName);
                    }
                }

                // Automatically select the first item if files are found
                if (Map_list1.Items.Count > 0)
                {
                    Map_list1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show($"Couldn't locate a maplist file matching pattern: {pattern}");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error accessing directory or finding files: " + ex.Message);
            }
        }


        private Random random = new Random();

        private void ButtonRoll_Click(object sender, RoutedEventArgs e)
        {
            if (!(Map_list1.SelectedItem is string selectedFileName))
            {
                Map_selection.Text = string.Empty;
                return;
            }

            // Determine the folderPath dynamically based on the file location
            string mainFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string listsFolderPath = Path.Combine(mainFolderPath, "lists");

            string selectedFilePath;
            if (File.Exists(Path.Combine(listsFolderPath, selectedFileName)))
            {
                selectedFilePath = Path.Combine(listsFolderPath, selectedFileName);
            }
            else
            {
                MessageBox.Show($"File not found: {selectedFileName}");
                Map_selection.Text = string.Empty;
                return;
            }

            // Get a random entry from the file
            string randomEntry = GetRandomEntry(selectedFilePath);

            // Clean up the entry if it starts with "maps/"
            if (randomEntry?.StartsWith("maps/") == true)
            {
                randomEntry = randomEntry.Substring("maps/".Length);
            }

            // Display the result or a fallback message
            Map_selection.Text = randomEntry ?? "No entries found.";
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

        /*private void ButtonRoll_Click(object sender, RoutedEventArgs e)
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
        }*/
        
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
            
        }

        private void Master_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            
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
            
        }

        private void Music_volume_Unchecked(object sender, RoutedEventArgs e)
        {
            
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

        }

        private void Sound_volume_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Sound_volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Sound_volume_slider != null)
            {
                Sound1.Text = Sound_volume_slider.Value.ToString("F1", CultureInfo.InvariantCulture);
                if (Sound_volume_slider.Value == 0)
                {
                    Sound_volume.IsChecked = false;
                }
                else if (Sound_volume_slider.Value > 0 && Music_volume.IsChecked == false)
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

        public delegate int ExtractCallback(string name, int subfiles, int type, byte[] data);

        private void InitializeDefaultImage()
        {
            try
            {
                // Load the default image from resources
                BitmapImage defaultImage = new BitmapImage();
                defaultImage.BeginInit();
                defaultImage.UriSource = new Uri("pack://application:,,,/Images/defaultmap.png");
                defaultImage.CacheOption = BitmapCacheOption.OnLoad;
                defaultImage.EndInit();

                // Set the default image to ImagePreview
                ImagePreview.Source = defaultImage;
            }
            catch (Exception ex)
            {
                // Display a message if there's an issue loading the default image
                MessageBox.Show($"Error initializing default image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MapSelection_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Define the folder to scan
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "maps");

            // Get the raw input from the TextBox
            string fullInput = Map_selection.Text.Trim();

            // Determine the valid file name
            string fileName;
            if (fullInput.Contains(".scp"))
            {
                // Extract everything before and including ".scp"
                int index = fullInput.IndexOf(".scp") + 4; // ".scp" is 4 characters long
                fileName = fullInput.Substring(0, index).Trim();
            }
            else
            {
                // If no ".scp" is found, treat it as invalid
                fileName = string.Empty;
            }

            // Debugging output
            Console.WriteLine($"Input: {fullInput}, Extracted File Name: {fileName}");

            // Ensure a valid file name or reset to default image if the field is empty
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("No valid .scp file name found. Loading default image.");
                InitializeDefaultImage();
                return; // Exit if no valid file name is found
            }

            // Construct the full file path
            string filePath = Path.Combine(folderPath, fileName);

            Console.WriteLine($"Constructed File Path: {filePath}");

            // Check if the file exists in the "maps" folder
            if (File.Exists(filePath))
            {
                try
                {
                    // Load the file and process it
                    byte[] fileData = File.ReadAllBytes(filePath);
                    PreviewScpFile(fileData); // Call your file preview function
                }
                catch (Exception ex)
                {
                    // Display an error message if the file cannot be loaded
                    Console.WriteLine($"Error loading file: {ex.Message}");
                    MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Console.WriteLine($"File not found: {filePath}. Loading default image.");
                // Load the default image if the file is not found
                InitializeDefaultImage();
            }
        }

        public void PreviewScpFile(byte[] fileData)
        {
            // Clear previous content
            ImagePreview.Source = null;

            // Extract data from SCP file
            Extract(fileData, (name, subfiles, type, data) =>
            {
                // Ensure we always return an int (e.g., 0 here)
                Dispatcher.Invoke(() =>
                {
                    // Check if the current file is a .prev file
                    if (name.EndsWith(".PREV"))
                    {
                        if (data.Length == 32768)
                        {
                            // Convert the .prev file data into an image
                            var image = ConvertPrevToImage(data);

                            // Set the ImagePreview to the generated image
                            ImagePreview.Source = image;
                        }
                    }
                });

                // Return a value (assuming 0 means success)
                return 0;
            });
        }

        private WriteableBitmap ConvertPrevToImage(byte[] prevData)
        {
            // Create a WriteableBitmap to hold the image data
            WriteableBitmap writableBitmap = new WriteableBitmap(128, 128, 96, 96, PixelFormats.Bgr32, null);

            // Ensure the image is writable and we can set pixels
            writableBitmap.Lock();

            try
            {
                // The .prev file contains 128x128 pixels, each represented by a 16-bit value (RGB 5-6-5)
                int pixelIndex = 0;
                byte[] pixels = new byte[128 * 128 * 4]; // Buffer for RGBA data (4 bytes per pixel)

                for (int y = 0; y < 128; y++)
                {
                    for (int x = 0; x < 128; x++)
                    {
                        // Extract the 16-bit pixel value
                        ushort pixel = BitConverter.ToUInt16(prevData, pixelIndex);

                        // Extract RGB components from the 16-bit pixel (5-6-5 format)
                        byte b = (byte)(((pixel >> 11) & 0x1F) * 255 / 31);
                        byte g = (byte)(((pixel >> 5) & 0x3F) * 255 / 63);
                        byte r = (byte)((pixel & 0x1F) * 255 / 31);

                        // Assign the RGBA values to the pixel buffer (4 bytes per pixel: R, G, B, A)
                        int bufferIndex = (y * 128 + x) * 4;
                        pixels[bufferIndex] = r;    // Red
                        pixels[bufferIndex + 1] = g; // Green
                        pixels[bufferIndex + 2] = b; // Blue
                        pixels[bufferIndex + 3] = 255; // Alpha (fully opaque)

                        // Move to the next pixel
                        pixelIndex += 2;
                    }
                }

                // Write the pixel data to the WriteableBitmap
                writableBitmap.WritePixels(
                    new Int32Rect(0, 0, 128, 128),
                    pixels,
                    128 * 4, // stride (bytes per row)
                    0 // offset (no additional offset)
                );
            }
            catch (Exception ex)
            {
                writableBitmap.Unlock();
                return null;
            }

            // Unlock the bitmap after the operation is done
            writableBitmap.Unlock();

            return writableBitmap;
        }

        public static int Extract(byte[] input, ExtractCallback callback)
        {
            int ParseLE(byte[] data, int offset)
            {
                return BitConverter.ToInt32(data, offset);
            }

            int infoOff = ParseLE(input, 4);
            int infoSize = ParseLE(input, 8);
            int unknown1 = ParseLE(input, 12);
            int unknown2 = ParseLE(input, 16);
            int offset = 20;

            byte[] info = Decompress(input[infoOff..]);

            var names = new Dictionary<string, Dictionary<string, int>>();
            var dirSiz = new Stack<int>();

            int idx = 0;
            while (info.Length > idx)
            {
                string ReadString(int length)
                {
                    var tmp = new byte[length];
                    Array.Copy(info, idx, tmp, 0, length);
                    Array.Reverse(tmp);
                    idx += length;
                    return Encoding.UTF8.GetString(tmp);
                }

                string name = ReadString(4);
                string ext = ReadString(4);

                if (!names.ContainsKey(ext))
                {
                    names[ext] = new Dictionary<string, int>();
                }

                int zsize = ParseLE(info, idx); idx += 4;
                int size = ParseLE(info, idx); idx += 4;
                int subfiles = ParseLE(info, idx); idx += 4;
                int type = ParseLE(info, idx); idx += 4;
                int unknown = ParseLE(info, idx); idx += 4;

                switch (type)
                {
                    case 0: // uncompressed file
                        if (subfiles != 0 || size != zsize)
                            throw new Exception("Invalid uncompressed file.");

                        if (callback($"{name}.{ext}", subfiles, type, input[offset..(offset + size)]) != 0)
                            return 1;

                        offset += size;
                        break;

                    case 2: // compressed file
                        if (subfiles != 0)
                            throw new Exception("Invalid compressed file.");

                        byte[] data = zsize > 0 ? Decompress(input[offset..(offset + zsize)]) : Array.Empty<byte>();
                        if (callback($"{name}.{ext}", subfiles, type, data) != 0)
                            return 1;

                        offset += zsize;
                        break;

                    case 1: // folder
                        if (dirSiz.Count > 0)
                            dirSiz.Push(dirSiz.Pop() - 1);

                        if (callback($"{name}.{ext}", subfiles, type, Array.Empty<byte>()) != 0)
                            return 1;

                        dirSiz.Push(subfiles);
                        break;

                    default:
                        Console.Error.WriteLine($"Warning: unknown type {type}");

                        if (subfiles != 0)
                        {
                            goto case 1;
                        }
                        else if (zsize < size)
                        {
                            goto case 2;
                        }
                        else
                        {
                            goto case 0;
                        }
                }

                if (type != 1 && subfiles == 0 && dirSiz.Count > 0)
                    dirSiz.Push(dirSiz.Pop() - 1);

                while (dirSiz.Count > 0 && dirSiz.Peek() == 0)
                {
                    dirSiz.Pop();
                }
            }

            if (dirSiz.Count != 0)
                throw new Exception("Directory stack is not empty.");

            return 0;
        }

        private static byte[] Decompress(byte[] compressedData)
        {
            using (var inputStream = new MemoryStream(compressedData))
            using (var outputStream = new MemoryStream())
            using (var decompressor = new Ionic.Zlib.ZlibStream(inputStream, Ionic.Zlib.CompressionMode.Decompress))
            {
                decompressor.CopyTo(outputStream);
                return outputStream.ToArray();
            }
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
            Start_souls_slider.Value = 16;
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
            if (Singleplayer.IsChecked == true && P2.IsChecked == true) sb.Append("# --host=2").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && P3.IsChecked == true) sb.Append("# --host=3").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && P4.IsChecked == true) sb.Append("# --host=4").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && P5.IsChecked == true) sb.Append("# --host=5").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && P6.IsChecked == true) sb.Append("# --host=6").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && P7.IsChecked == true) sb.Append("# --host=7").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && P8.IsChecked == true) sb.Append("# --host=8").Append(Environment.NewLine);
            if (Singleplayer.IsChecked == true && Game_mode_list.SelectedItem != null)
            {
                sb.AppendLine($"# --{Game_mode_list.SelectedItem}");
            }
            sb.AppendLine($"--souls={Start_souls_slider.Value}");
            sb.AppendLine($"--level={Min_level_slider.Value}");
            sb.AppendLine($"--min-level={Min_level_slider.Value}");
            sb.AppendLine($"--max-level={Max_level_slider.Value}");
            sb.AppendLine($"--xp-rate={xp_rate_slider.Value.ToString("F2", CultureInfo.InvariantCulture)}");
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
            if (string.IsNullOrWhiteSpace(Map_selection.Text) && !string.IsNullOrWhiteSpace(Map_list1.Text)) sb.AppendLine($"--map-list=lists/{Map_list1.Text}");
            else if (!string.IsNullOrWhiteSpace(Map_list1.Text)) sb.AppendLine($"# --map-list=lists/{Map_list1.Text}");
            if (Host_game.IsChecked == true && !string.IsNullOrWhiteSpace(Map_selection.Text))
            {
                string mapText = Map_selection.Text;

                // Locate the map name by finding ".scp"
                int scpIndex = mapText.IndexOf(".scp");
                if (scpIndex > 0)
                {
                    // Include ".scp" in the map name
                    string mapName = mapText.Substring(0, scpIndex + 4);
                    sb.AppendLine($"maps/{mapName}");

                    // Extract the parameters (everything after the map name)
                    string parameters = mapText.Substring(scpIndex + 4).Trim();
                    if (!string.IsNullOrWhiteSpace(parameters))
                    {
                        string[] args = parameters.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        // Append only recognized parameters
                        foreach (string arg in args)
                        {
                            if (arg.StartsWith("--level=") ||
                                arg.StartsWith("--souls=") ||
                                arg.StartsWith("--min-level=") ||
                                arg.StartsWith("--max-level="))
                            {
                                sb.AppendLine(arg); // Append recognized arguments as-is
                            }
                            // Ignore unrecognized arguments
                        }
                    }
                }
                else
                {
                    // If no ".scp" is found, assume the entire text is the map name
                    sb.AppendLine($"maps/{mapText}");
                }
            }
            if (Singleplayer.IsChecked == true && !string.IsNullOrWhiteSpace(Map_selection.Text))
            {
                string mapText = Map_selection.Text;

                // Locate the map name by finding ".scp"
                int scpIndex = mapText.IndexOf(".scp");
                if (scpIndex > 0)
                {
                    // Include ".scp" in the map name
                    string mapName = mapText.Substring(0, scpIndex + 4);
                    sb.AppendLine($"maps/{mapName}");

                    // Extract the parameters (everything after the map name)
                    string parameters = mapText.Substring(scpIndex + 4).Trim();
                    if (!string.IsNullOrWhiteSpace(parameters))
                    {
                        string[] args = parameters.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        // Append only recognized parameters
                        foreach (string arg in args)
                        {
                            if (arg.StartsWith("--level=") ||
                                arg.StartsWith("--souls=") ||
                                arg.StartsWith("--min-level=") ||
                                arg.StartsWith("--max-level="))
                            {
                                sb.AppendLine(arg); // Append recognized arguments as-is
                            }
                            // Ignore unrecognized arguments
                        }
                    }
                }
                else
                {
                    // If no ".scp" is found, assume the entire text is the map name
                    sb.AppendLine($"maps/{mapText}");
                }
            }

            // Append observer and save replays settings
            sb.AppendLine(Observer.IsChecked == true ? "--observer" : "# --observer");
            if (Enable_green_souls.IsChecked == true) sb.AppendLine("--green-ally-souls");
            if (Refuse_green_souls.IsChecked == true) sb.AppendLine("--refuse-green-souls");
            if (Random_Spellbook.IsChecked == true) sb.AppendLine("--random-spellbooks");
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
            if (Skin_changer.IsChecked == true) sb.AppendLine("# tunnel background");
            // Process unhandled arguments from the TextBox
            if (!string.IsNullOrWhiteSpace(UnhandledArgumentsTextBox.Text))
            {
                // Split the text box content into lines
                string[] unhandledLines = UnhandledArgumentsTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in unhandledLines)
                {
                    sb.AppendLine(line.Trim()); // Append each unhandled argument
                }
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
                    case "--hotkeys":
                        break;
                    case "--1v1":
                        break;
                    case "--2v2":
                        break;
                    case "--3v3":
                        break;
                    case "--4v4":
                        break;
                    case "--ffa":
                        break;
                    case "# --1v1":
                        break;
                    case "# --2v2":
                        break;
                    case "# --3v3":
                        break;
                    case "# --4v4":
                        break;
                    case "# --ffa":
                        break;
                    case "--level":
                        break;
                    case "maps":
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
                    case "# --host":
                        if (value != null)
                        {
                            int hostValue = int.Parse(value);
                            switch (hostValue)
                            {
                                case 2:
                                    Singleplayer.IsChecked = true;
                                    P2.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 4;
                                    break;
                                case 3:
                                    Singleplayer.IsChecked = true;
                                    P3.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 4:
                                    Singleplayer.IsChecked = true;
                                    P4.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 5;
                                    break;
                                case 5:
                                    Singleplayer.IsChecked = true;
                                    P5.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 6:
                                    Singleplayer.IsChecked = true;
                                    P6.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    Map_list1.SelectedIndex = 6;
                                    break;
                                case 7:
                                    Singleplayer.IsChecked = true;
                                    P7.IsChecked = true;
                                    Game_mode_list.SelectedIndex = 0;
                                    break;
                                case 8:
                                    Singleplayer.IsChecked = true;
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
                    case "--map-list":
                        if (value != null)
                        {
                            // Extract the part after the last '/' in the value
                            int slashIndex = value.LastIndexOf('/');
                            if (slashIndex != -1)
                            {
                                // Extract the map filename (everything after the last '/')
                                string mapFilename = value.Substring(slashIndex + 1).Trim();

                                string directoryPath = "lists/";
                                if (Directory.Exists(directoryPath))
                                {
                                    // Don't clear existing items - we want to keep the existing dropdown population logic intact
                                    // However, we will add the map if it isn't already present
                                    if (!Map_list1.Items.Contains(mapFilename))
                                    {
                                        Map_list1.Items.Add(mapFilename);
                                    }

                                    // Try to select the item that matches the extracted filename
                                    if (Map_list1.Items.Contains(mapFilename))
                                    {
                                        Map_list1.SelectedItem = mapFilename;
                                    }
                                }
                            }
                        }
                        break;
                    case "# --map-list":
                        if (value != null)
                        {
                            // Extract the part after the last '/' in the value
                            int slashIndex = value.LastIndexOf('/');
                            if (slashIndex != -1)
                            {
                                // Extract the map filename (everything after the last '/')
                                string mapFilename = value.Substring(slashIndex + 1).Trim();

                                string directoryPath = "lists/";
                                if (Directory.Exists(directoryPath))
                                {
                                    // Clear ComboBox to avoid adding incorrect or partial entries
                                    Map_list1.Items.Clear();
                                    bool exactMatchFound = false;

                                    // Loop through all files in the directory
                                    foreach (var file in Directory.GetFiles(directoryPath, "*.txt"))
                                    {
                                        string fileName = Path.GetFileName(file);

                                        // Check if the file name exactly matches the target map filename
                                        if (fileName.Equals(mapFilename, StringComparison.OrdinalIgnoreCase))
                                        {
                                            // Add the exact match to the ComboBox and select it
                                            Map_list1.Items.Add(fileName);
                                            Map_list1.SelectedItem = fileName;
                                            exactMatchFound = true;
                                            break; // Exit the loop after finding the first exact match
                                        }
                                    }

                                    // If no exact match was found, do not add any invalid entries
                                    if (!exactMatchFound)
                                    {
                                        MessageBox.Show($"No exact match found for {mapFilename}.");
                                    }
                                }
                            }
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
                    case "--random-spellbooks":
                        Random_Spellbook.IsChecked = true;
                        break;
                    case "--record-folder":
                        Savereplays.IsChecked = true;
                        break;
                    case "# --record-folder":
                        Savereplays.IsChecked = false;
                        break;
                    case "# tunnel background":
                        Skin_changer.IsChecked = true;
                        break;

                    default:
                        if (!string.IsNullOrEmpty(key))
                        {
                            // Append key and value as is, without adding "=No value" if value is null
                            string entry = value != null ? $"{key}={value}" : key;
                            UnhandledArgumentsTextBox.Text += $"{entry}{Environment.NewLine}";
                        }
                        break;
                }
            }
        }

        private void Skin_changer_Checked(object sender, RoutedEventArgs e)
        {
            // Show Tunnel Background and hide Image Background
            Image_background.Visibility = Visibility.Collapsed;
            Tunnel_background.Visibility = Visibility.Visible;
        }

        // When the ToggleButton is Unchecked
        private void Skin_changer_Unchecked(object sender, RoutedEventArgs e)
        {
            // Show Image Background and hide Tunnel Background
            Image_background.Visibility = Visibility.Visible;
            Tunnel_background.Visibility = Visibility.Collapsed;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure the slider remains focused
            if (sender is Button button && button.Tag is Slider slider)
            {
                slider.Focus();

                // Decrease slider value by SmallChange
                slider.Value = Math.Max(slider.Minimum, slider.Value - slider.SmallChange);
            }
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure the slider remains focused
            if (sender is Button button && button.Tag is Slider slider)
            {
                slider.Focus();

                // Increase slider value by SmallChange
                slider.Value = Math.Min(slider.Maximum, slider.Value + slider.SmallChange);
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // Get the Thumb that triggered the event
            var thumb = sender as Thumb;
            if (thumb == null)
                return;

            // Get the Slider that contains this Thumb (parent of the Thumb in the visual tree)
            var slider = FindParent<Slider>(thumb);

            if (slider == null)
                return;

            // Calculate the new value based on the horizontal movement (e.HorizontalChange)
            double newValue = slider.Value + (e.HorizontalChange / slider.ActualWidth) * (slider.Maximum - slider.Minimum);

            // Ensure the new value is within the slider's minimum and maximum range
            slider.Value = Math.Max(slider.Minimum, Math.Min(slider.Maximum, newValue));
        }

        // Helper function to find the parent of a specific type in the visual tree
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }
    }

    public class TunnelMeshGenerator
    {
        public static MeshGeometry3D CreateTunnelMesh(int segments, double radius, double length)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            int divisionsAlongLength = 100;

            // Adjust the tunnel's center to the origin
            double centerX = 0;
            double centerY = 0;
            double centerZ = 0;

            // Generate vertices and texture coordinates
            for (int i = 0; i <= segments; i++)
            {
                double angle = i * 2 * Math.PI / segments;
                double x = Math.Cos(angle) * radius;
                double y = Math.Sin(angle) * radius;

                for (int j = 0; j <= divisionsAlongLength; j++)
                {
                    double z = j * (length / divisionsAlongLength);

                    // Add vertex positions, adjusting by the center offset
                    mesh.Positions.Add(new Point3D(x + centerX, y + centerY, z + centerZ));

                    // Texture coordinates: Map along the tunnel (U = around the circumference, V = along the length)
                    double u = (double)i / segments;      // Circumference
                    double v = (double)j / divisionsAlongLength; // Length
                    mesh.TextureCoordinates.Add(new Point(u, v));
                }
            }

            // Generate triangle indices
            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < divisionsAlongLength; j++)
                {
                    int baseIndex = i * (divisionsAlongLength + 1) + j;

                    // First triangle
                    mesh.TriangleIndices.Add(baseIndex);
                    mesh.TriangleIndices.Add(baseIndex + 1);
                    mesh.TriangleIndices.Add(baseIndex + divisionsAlongLength + 1);

                    // Second triangle
                    mesh.TriangleIndices.Add(baseIndex + 1);
                    mesh.TriangleIndices.Add(baseIndex + divisionsAlongLength + 2);
                    mesh.TriangleIndices.Add(baseIndex + divisionsAlongLength + 1);
                }
            }

            return mesh;
        }

    }
}
