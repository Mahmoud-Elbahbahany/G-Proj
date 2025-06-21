using DAL.Enum.Sys;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity.Sys
{
    public class User : ICloneable, IDisposable
    {
        #region Properties: +8

        #region Core: +2
        public const bool IS_DISPOSABLE = true;
        private volatile bool _disposed = false;
        #endregion

        #region ID_Constraints:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID = default;
        #endregion

        #region Name_Constraints:
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name = string.Empty;
        #endregion

        #region Email_Constraints:
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email = string.Empty;
        #endregion

        #region HashedPassword_Constraints:
        [Required(ErrorMessage = "Password hash is required.")]
        public string HashedPassword = string.Empty;
        #endregion

        #region Role_Contraints:
        [Required(ErrorMessage = "Role is required.")]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role.")]
        public UserRole Role = default;
        #endregion

        #region Timestamps: +2
        public string CreatedAt = string.Empty;
        public string UpdatedAt = string.Empty;
        #endregion

        #endregion

        #region Ctors: +2
        private User() { }

        public User(int id, string name, string email, string hashedPassword, UserRole role)
        {
            ID = id;
            Name = name;
            Email = email;
            HashedPassword = hashedPassword;
            Role = role;
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            UpdatedAt = CreatedAt;
        }
        #endregion

        #region Methods: +5

        #region Testing: +1
        public override string ToString()
        {
            return $@"
            User Details:
            ----------------------
            ID        : {this.ID}
            Name      : {this.Name}
            Email     : {this.Email}
            Role      : {this.Role}
            CreatedAt : {this.CreatedAt}
            UpdatedAt : {this.UpdatedAt}
            ";
        }
        #endregion

        #region Disposition: +2
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                Name = string.Empty;
                Email = string.Empty;
                HashedPassword = string.Empty;
                CreatedAt = string.Empty;
                UpdatedAt = string.Empty;
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Cloning: +2
        public User Clone()
        {
            return new User(
                ID,
                Name,
                Email,
                HashedPassword,
                Role)
            {
                CreatedAt = CreatedAt,
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
        object ICloneable.Clone() => Clone();
        #endregion

        #endregion

        #region DeCtor: +1
        ~User() => Dispose(false);
        #endregion
    }
}