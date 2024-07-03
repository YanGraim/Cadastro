using System.Data.SqlClient;

namespace Cadastro {
    public class Conexao {
        public SqlConnection conn = new SqlConnection("Server=localhost,1433;Database=JovemProgramador(Katia);User Id=sa;Password=Y@n1997.");

        public void Conectar() {
            conn.Open();
        }

        public void Desconectar() {
            conn.Close();
        }
    }
}

