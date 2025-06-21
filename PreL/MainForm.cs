using System;
using System.Drawing;
using System.Windows.Forms;
using DAL.Entity.Sys;

namespace PreL
{
    public enum FormMode { Login, Register }
    public partial class MainForm : Form
    {
        private User? _currentUser; // Made nullable
        private readonly Color _primaryColor = Color.FromArgb(0, 123, 255);
        private readonly Color _secondaryColor = Color.FromArgb(108, 117, 125);
        private readonly Font _titleFont = new Font("Segoe UI", 18, FontStyle.Bold);

        public MainForm()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            // Form properties
            this.Text = "User Management System";
            this.Width = 1000;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Commented out the icon line since it was causing errors
            // this.Icon = Properties.Resources.AppIcon;

            // Show login form by default
            ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            var loginForm = new LoginRegisterForm(FormMode.Login)
            {
                PrimaryColor = _primaryColor,
                SecondaryColor = _secondaryColor,
                TitleFont = _titleFont
            };

            loginForm.SuccessfulLogin += (user) =>
            {
                _currentUser = user;
                this.Text = $"User Management System - Welcome {user.Name} ({user.Role})";
                ShowUserManagementPanel(user);
            };

            loginForm.ShowRegisterInstead += () => ShowRegisterForm();
            SetMainContent(loginForm);
        }

        private void ShowRegisterForm()
        {
            var registerForm = new LoginRegisterForm(FormMode.Register)
            {
                PrimaryColor = _primaryColor,
                SecondaryColor = _secondaryColor,
                TitleFont = _titleFont
            };

            registerForm.SuccessfulRegistration += (user) =>
            {
                MessageBox.Show($"Registration successful! Welcome {user.Name}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowLoginForm();
            };

            registerForm.ShowLoginInstead += () => ShowLoginForm();
            SetMainContent(registerForm);
        }

        private void ShowUserManagementPanel(User user)
        {
            var userPanel = new UserManagementPanel(user)
            {
                PrimaryColor = _primaryColor,
                SecondaryColor = _secondaryColor,
                TitleFont = _titleFont
            };

            userPanel.LogoutRequested += () =>
            {
                _currentUser = null;
                this.Text = "User Management System";
                ShowLoginForm();
            };

            SetMainContent(userPanel);
        }

        private void SetMainContent(Control control)
        {
            this.SuspendLayout();
            this.Controls.Clear();

            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
            this.ResumeLayout();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}