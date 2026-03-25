using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class PhanQuyenDAO
    {
        public static DataTable LayPhanQuyen(string chucVu)
        {
            string sql = "SELECT Module, Xem, Them, Sua, Xoa, Export FROM PHAN_QUYEN WHERE ChucVu = @ChucVu";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@ChucVu", chucVu) });
        }

        public static void CapNhatQuyen(string chucVu, string module, bool xem, bool them, bool sua, bool xoa, bool export)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM PHAN_QUYEN WHERE ChucVu=@ChucVu AND Module=@Module)
                          UPDATE PHAN_QUYEN SET Xem=@Xem, Them=@Them, Sua=@Sua, Xoa=@Xoa, Export=@Export WHERE ChucVu=@ChucVu AND Module=@Module
                          ELSE
                          INSERT INTO PHAN_QUYEN (ChucVu, Module, Xem, Them, Sua, Xoa, Export) VALUES (@ChucVu, @Module, @Xem, @Them, @Sua, @Xoa, @Export)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@Module", module),
                new SqlParameter("@Xem", xem),
                new SqlParameter("@Them", them),
                new SqlParameter("@Sua", sua),
                new SqlParameter("@Xoa", xoa),
                new SqlParameter("@Export", export)
            });
        }
    }
}
