using System.Windows.Forms;

namespace FloriSys.Shared
{
    /// <summary>
    /// Base UserControl with common UI functionality.
    /// Demonstrates: INHERITANCE (all UCs inherit this),
    /// POLYMORPHISM (virtual LoadData/FormatGrid that UCs override),
    /// ENCAPSULATION (IsLoading hides cursor logic, ShowError/ShowSuccess standardize messaging).
    /// Not abstract so WinForms Designer can render derived controls.
    /// </summary>
    public class BaseUserControl : UserControl
    {
        // ============================================================
        // ENCAPSULATION: Private field with controlled access
        // ============================================================

        private bool _isLoading = false;

        /// <summary>
        /// Encapsulated loading state - automatically changes cursor.
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            protected set
            {
                _isLoading = value;
                this.Cursor = value ? Cursors.WaitCursor : Cursors.Default;
            }
        }

        // ============================================================
        // ENCAPSULATION: Standardized message dialogs
        // ============================================================

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
