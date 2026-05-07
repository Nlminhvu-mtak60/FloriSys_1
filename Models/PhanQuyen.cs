namespace FloriSys.Models
{
    /// <summary>
    /// PhanQuyen model - inherits BaseEntity.
    /// Demonstrates: INHERITANCE, ENCAPSULATION (permission check methods).
    /// </summary>
    public class PhanQuyen : BaseEntity
    {
        public string ChucVu { get; set; }
        public string Module { get; set; }
        public bool Xem { get; set; }
        public bool Them { get; set; }
        public bool Sua { get; set; }
        public bool Xoa { get; set; }
        public bool Export { get; set; }

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => ChucVu + " - " + Module;
        public override string Id => ChucVu + "_" + Module;

        public override bool IsValid => !string.IsNullOrEmpty(ChucVu) && !string.IsNullOrEmpty(Module);

        // ENCAPSULATION: Permission check methods
        public bool HasFullAccess => Xem && Them && Sua && Xoa && Export;
        public bool CanOnlyView => Xem && !Them && !Sua && !Xoa;
    }
}
