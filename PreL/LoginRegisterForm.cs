using BLL.Sys.Managers;
using DAL.Entity.Sys;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PreL
{

    public partial class LoginRegisterForm : UserControl
    {
        private readonly FormMode _mode;

        // Customizable properties
        public Color PrimaryColor { get; set; } = Color.FromArgb(0, 123, 255);
        public Color SecondaryColor { get; set; } = Color.FromArgb(108, 117, 125);
        public Font TitleFont { get; set; } = new Font("Segoe UI", 18, FontStyle.Bold);

        // Made events nullable
        public event Action<User>? SuccessfulLogin;
        public event Action<User>? SuccessfulRegistration;
        public event Action? ShowLoginInstead;
        public event Action? ShowRegisterInstead;

        public LoginRegisterForm(FormMode mode)
        {
            _mode = mode;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Size = new Size(450, _mode == FormMode.Login ? 350 : 400);
            this.BackColor = Color.White;

            // Title Label
            var lblTitle = new Label
            {
                Text = _mode == FormMode.Login ? "LOGIN" : "REGISTER",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = TitleFont,
                ForeColor = PrimaryColor,
                Height = 60
            };

            // Email Field
            var pnlEmail = CreateInputPanel("Email:", 70, "user@example.com");
            var txtEmail = (TextBox)pnlEmail.Controls[1];

            // Password Field
            var pnlPassword = CreateInputPanel("Password:", 120, "", true);
            var txtPassword = (TextBox)pnlPassword.Controls[1];

            // Name Field (only for register)
            Panel? pnlName = null;
            TextBox? txtName = null;

            if (_mode == FormMode.Register)
            {
                pnlName = CreateInputPanel("Full Name:", 170, "John Doe");
                txtName = (TextBox)pnlName.Controls[1];
            }

            // Action Button
            var btnAction = new Button
            {
                Text = _mode == FormMode.Login ? "LOGIN" : "REGISTER",
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Height = 40,
                Width = 200,
                Cursor = Cursors.Hand
            };
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Location = new Point((this.Width - btnAction.Width) / 2,
                                         _mode == FormMode.Login ? 220 : 270);

            // Switch Mode Button
            var btnSwitch = new LinkLabel
            {
                Text = _mode == FormMode.Login ? "Create an account" : "Already have an account?",
                AutoSize = true,
                LinkColor = SecondaryColor,
                ActiveLinkColor = PrimaryColor,
                VisitedLinkColor = SecondaryColor,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnSwitch.Location = new Point((this.Width - btnSwitch.Width) / 2, btnAction.Bottom + 15);

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(pnlEmail);
            this.Controls.Add(pnlPassword);
            if (_mode == FormMode.Register && pnlName != null) this.Controls.Add(pnlName);
            this.Controls.Add(btnAction);
            this.Controls.Add(btnSwitch);

            // Event handlers
            btnAction.Click += (sender, e) => ProcessFormSubmission(txtEmail, txtPassword, txtName);
            btnSwitch.Click += (sender, e) => SwitchFormMode();

            // Enter key submission
            txtPassword.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter) ProcessFormSubmission(txtEmail, txtPassword, txtName);
            };
        }

        private Panel CreateInputPanel(string labelText, int top, string placeholder, bool isPassword = false)
        {
            var panel = new Panel
            {
                Size = new Size(350, 60),
                Location = new Point((this.Width - 350) / 2, top)
            };

            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(73, 80, 87),
                Location = new Point(0, 0),
                AutoSize = true
            };

            var textBox = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(350, 30),
                Location = new Point(0, 25),
                Tag = placeholder
            };

            if (isPassword)
            {
                textBox.PasswordChar = '•';
                textBox.UseSystemPasswordChar = true;
            }

            // Placeholder functionality
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == (string?)textBox.Tag)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (isPassword) textBox.UseSystemPasswordChar = true;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = (string?)textBox.Tag;
                    textBox.ForeColor = SystemColors.GrayText;
                    if (isPassword) textBox.UseSystemPasswordChar = false;
                }
            };

            // Initialize placeholder
            textBox.Text = placeholder;
            textBox.ForeColor = SystemColors.GrayText;
            if (isPassword) textBox.UseSystemPasswordChar = false;

            panel.Controls.Add(label);
            panel.Controls.Add(textBox);

            return panel;
        }

        private void ProcessFormSubmission(TextBox txtEmail, TextBox txtPassword, TextBox? txtName)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Validate fields
            if (email == (string?)txtEmail.Tag || string.IsNullOrWhiteSpace(email))
            {
                ShowValidationError("Please enter your email address");
                return;
            }

            if (password == (string?)txtPassword.Tag || string.IsNullOrWhiteSpace(password))
            {
                ShowValidationError("Please enter your password");
                return;
            }

            if (_mode == FormMode.Register)
            {
                if (txtName == null || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    ShowValidationError("Please enter your full name");
                    return;
                }

                string name = txtName.Text;
                if (name == (string?)txtName.Tag || string.IsNullOrWhiteSpace(name))
                {
                    ShowValidationError("Please enter your full name");
                    return;
                }

                if (!IsValidEmail(email))
                {
                    ShowValidationError("Please enter a valid email address");
                    return;
                }

                if (password.Length < 8)
                {
                    ShowValidationError("Password must be at least 8 characters");
                    return;
                }
            }

            // Process login/registration
            if (_mode == FormMode.Login)
            {
                var user = LoginManager.Authenticate(email, password);
                if (user != null)
                {
                    SuccessfulLogin?.Invoke(user);
                }
                else
                {
                    ShowValidationError("Invalid email or password");
                }
            }
            else
            {
                if (txtName != null)
                {
                    var user = RegisterManager.Register(txtName.Text, email, password);
                    if (user != null)
                    {
                        SuccessfulRegistration?.Invoke(user);
                    }
                    else
                    {
                        ShowValidationError("Registration failed. Email may already be in use.");
                    }
                }
            }
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void SwitchFormMode()
        {
            if (_mode == FormMode.Login)
            {
                ShowRegisterInstead?.Invoke();
            }
            else
            {
                ShowLoginInstead?.Invoke();
            }
        }
    }
}