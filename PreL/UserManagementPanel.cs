using System;
using System.Drawing;
using System.Windows.Forms;
using DAL.Entity.Sys;
using DAL.Enum.Sys;

namespace PreL
{

    public partial class UserManagementPanel : UserControl
    {
        private readonly User _currentUser;

        // Customizable properties
        public Color PrimaryColor { get; set; } = Color.FromArgb(0, 123, 255);
        public Color SecondaryColor { get; set; } = Color.FromArgb(108, 117, 125);
        public Font TitleFont { get; set; } = new Font("Segoe UI", 18, FontStyle.Bold);

        // Made event nullable
        public event Action? LogoutRequested;

        public UserManagementPanel(User user)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            InitializeComponent();
            SetupPanel();
        }

        private void SetupPanel()
        {
            this.BackColor = Color.White;
            this.Size = new Size(800, 600);

            // Header Panel
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = PrimaryColor
            };

            var lblWelcome = new Label
            {
                Text = $"Welcome, {_currentUser.Name}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = TitleFont,
                ForeColor = Color.White,
                Padding = new Padding(20, 0, 0, 0)
            };

            var lblRole = new Label
            {
                Text = $"({_currentUser.Role})",
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.White,
                Padding = new Padding(0, 0, 20, 0),
                Width = 150
            };

            headerPanel.Controls.Add(lblWelcome);
            headerPanel.Controls.Add(lblRole);

            // User Details Panel
            var detailsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                BackColor = Color.White
            };

            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons,
                ItemSize = new Size(0, 1),
                SizeMode = TabSizeMode.Fixed
            };

            // Profile Tab
            var profileTab = new TabPage();
            SetupProfileTab(profileTab);
            tabControl.TabPages.Add(profileTab);

            // Settings Tab (only for admin)
            if (_currentUser.Role == UserRole.Admin)
            {
                var settingsTab = new TabPage();
                SetupSettingsTab(settingsTab);
                tabControl.TabPages.Add(settingsTab);
            }

            detailsPanel.Controls.Add(tabControl);

            // Footer Panel
            var footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            var btnLogout = new Button
            {
                Text = "LOGOUT",
                BackColor = SecondaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 40),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Location = new Point(footerPanel.Width - btnLogout.Width - 20,
                                         (footerPanel.Height - btnLogout.Height) / 2);
            btnLogout.Click += (sender, e) => LogoutRequested?.Invoke();

            footerPanel.Controls.Add(btnLogout);

            // Add all panels
            this.Controls.Add(detailsPanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(footerPanel);
        }

        private void SetupProfileTab(TabPage tab)
        {
            tab.BackColor = Color.White;
            tab.Padding = new Padding(20);

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            var userDetails = new Label
            {
                Text = _currentUser.ToString(),
                Font = new Font("Consolas", 10),
                AutoSize = true,
                MaximumSize = new Size(panel.Width - 40, 0)
            };

            panel.Controls.Add(userDetails);
            tab.Controls.Add(panel);
        }

        private void SetupSettingsTab(TabPage tab)
        {
            tab.BackColor = Color.White;
            tab.Padding = new Padding(20);

            var lblAdmin = new Label
            {
                Text = "Admin Settings Panel",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Height = 50
            };

            // Add admin-specific controls here
            var btnManageUsers = new Button
            {
                Text = "Manage Users",
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Size = new Size(200, 40),
                Cursor = Cursors.Hand,
                Location = new Point(20, 70)
            };
            btnManageUsers.FlatAppearance.BorderSize = 0;
            btnManageUsers.Click += (s, e) => MessageBox.Show("User management feature would go here");

            tab.Controls.Add(lblAdmin);
            tab.Controls.Add(btnManageUsers);
        }
    }
}