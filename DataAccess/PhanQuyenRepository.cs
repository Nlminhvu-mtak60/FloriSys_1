using System.Collections.Generic;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// PhanQuyen repository - inherits BaseRepository&lt;PhanQuyen&gt;.
    /// </summary>
    public class PhanQuyenRepository : BaseRepository<PhanQuyen>
    {
        public override string TableName => "PHAN_QUYEN";
        public override string IdColumn => "ChucVu";  // Composite key, but ChucVu is the main filter
        public override string IdPrefix => "";  // No auto-code for permissions

        public List<PhanQuyen> LayPhanQuyen(string chucVu)
        {
            string sql = "SELECT ChucVu, Module, Xem, Them, Sua, Xoa, Export FROM PHAN_QUYEN WHERE ChucVu = @ChucVu";
            return GetList(sql, new List<SqlParameter> { new SqlParameter("@ChucVu", chucVu) });
        }

        public void CapNhatQuyen(PhanQuyen pq)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM PHAN_QUYEN WHERE ChucVu=@ChucVu AND Module=@Module)
                          UPDATE PHAN_QUYEN SET Xem=@Xem, Them=@Them, Sua=@Sua, Xoa=@Xoa, Export=@Export WHERE ChucVu=@ChucVu AND Module=@Module
                          ELSE
                          INSERT INTO PHAN_QUYEN (ChucVu, Module, Xem, Them, Sua, Xoa, Export) VALUES (@ChucVu, @Module, @Xem, @Them, @Sua, @Xoa, @Export)";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@ChucVu", pq.ChucVu),
                new SqlParameter("@Module", pq.Module),
                new SqlParameter("@Xem", pq.Xem),
                new SqlParameter("@Them", pq.Them),
                new SqlParameter("@Sua", pq.Sua),
                new SqlParameter("@Xoa", pq.Xoa),
                new SqlParameter("@Export", pq.Export)
            });
        }
    }
}
