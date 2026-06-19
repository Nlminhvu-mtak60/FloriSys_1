using System.Windows.Forms;
using FloriSys.Services;

namespace FloriSys.Shared
{
  
    public class BaseUserControl : UserControl
    {
        

        private bool _isLoading = false;

       
        public bool IsLoading
        {
            get => _isLoading;
            protected set
            {
                _isLoading = value;
                this.Cursor = value ? Cursors.WaitCursor : Cursors.Default;
            }
        }

        protected void ShowError(string message, string title = "Loi")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void ShowWarning(string message)
        {
            MessageBox.Show(message, "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected bool Confirm(string message)
        {
            return MessageBox.Show(message, "Xac nhan",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Check if user has permission and show error if not.
        /// </summary>
        protected bool CheckPermission(string module, string action)
        {
            if (SessionManager.Instance.HasPermission(module, action))
                return true;

            ShowWarning($"Bạn không có quyền thực hiện thao tác '{action}' trên hệ thống này.");
            return false;
        }

        // ============================================================
        // POLYMORPHISM: Every UC should override data loading
        // ============================================================

        /// <summary>
        /// Load or refresh data for this UserControl.
        /// POLYMORPHISM: virtual so each subclass overrides this.
        /// </summary>
        public virtual void LoadData() { }

        // ============================================================
        // POLYMORPHISM: Optional grid formatting hook
        // ============================================================

        /// <summary>
        /// Format DataGridView after data load.
        /// POLYMORPHISM: virtual so UCs can optionally override.
        /// </summary>
        public virtual void FormatGrid() { }
    }
}
