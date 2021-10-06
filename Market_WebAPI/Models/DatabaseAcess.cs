using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public static class DatabaseAcess
    {
        //private static string connectionString = @"Data Source=DESKTOP-3884TB7;Initial Catalog=EnergyDB;Integrated Security=True";
        private static string connectionString = @"Data Source=localhost;Initial Catalog=guilherme2109300258_tcc;User Id=guilherme2109300258_tcc_user;Password=!tcc123!;";

        #region Usuario
        public static void CadastrarUsuario(Usuario usuario)
        {
            string procedure = "in_Usuario_InserirUsuario";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@email", usuario.Email);
                    comando.Parameters.AddWithValue("@senha", usuario.Senha);
                    comando.Parameters.AddWithValue("@nome", usuario.Nome);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return;
                }
            }
        }

        public static bool ValidarEmailCliente(string email)
        {
            bool ret = false;

            string procedure = "sl_Usuario_SelectUsuarioPorEmail";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@email", email);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                    conexao.Close();
                }
            }
            return ret;
        }

        public static Usuario BuscarUsuarioPorID(int id)
        {
            string procedure = "sl_Usuario_SelectUsuarioPorID";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Usuario usuario = new Usuario();
                        usuario.ID = id;
                        usuario.Email = reader["Email"].ToString();
                        usuario.Senha = reader["Senha"].ToString();
                        usuario.Nome = reader["Nome"].ToString();
                        conexao.Close();
                        return usuario;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }

        public static Usuario BuscarUsuarioPorEmail(string email)
        {
            string procedure = "sl_Usuario_SelectUsuarioPorEmail";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@email", email);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Usuario usuario = new Usuario();
                        usuario.ID = Convert.ToInt32(reader["Codigo"]);
                        usuario.Email = email;
                        usuario.Senha = reader["Senha"].ToString();
                        usuario.Nome = reader["Nome"].ToString();
                        conexao.Close();
                        return usuario;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }
        #endregion

        #region Ponto
        public static void CadastrarPonto(Ponto ponto)
        {
            string procedure = "in_Ponto_InserirPonto";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nome", ponto.Nome);
                    comando.Parameters.AddWithValue("@codigoUsuario", ponto.CodigoUsuario);
                    comando.Parameters.AddWithValue("@descricao", ponto.Descricao);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return;
                }
            }
        }

        public static void AtualizarPonto(Ponto ponto)
        {
            string procedure = "up_Ponto_UpdatePonto";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nome", ponto.Nome);
                    comando.Parameters.AddWithValue("@codigoUsuario", ponto.CodigoUsuario);
                    comando.Parameters.AddWithValue("@descricao", ponto.Descricao);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return;
                }
            }
        }

        public static void DeletarPonto(Ponto ponto)
        {
            string procedure = "del_Ponto_DeletePonto";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@codigoPonto", ponto.Codigo);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return;
                }
            }
        }

        public static Ponto BuscarPontoPorCodigo(int codigo)
        {
            string procedure = "sl_Ponto_SelectPontoPorCodigo";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@codigo", codigo);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Ponto ponto = new Ponto();
                        ponto.Codigo = codigo;
                        ponto.Nome = reader["Nome"].ToString();
                        ponto.Descricao = reader["Descricao"].ToString();
                        ponto.CodigoUsuario = Convert.ToInt32(reader["CodigoUsuario"]);
                        conexao.Close();
                        return ponto;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }

        public static List<Ponto> BuscarPontosPorCodigoUsuario(int codigoUsuario)
        {
            string procedure = "sl_Ponto_SelectPontosPorCodigoUsuario";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@codigoUsuario", codigoUsuario);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<Ponto> pontos = new List<Ponto>();
                        while (reader.Read())
                        {
                            Ponto ponto = new Ponto();
                            ponto.Codigo = Convert.ToInt32(reader["Codigo"]);
                            ponto.Nome = reader["Nome"].ToString();
                            ponto.Descricao = reader["Descricao"].ToString();
                            ponto.CodigoUsuario = codigoUsuario;
                            pontos.Add(ponto);
                        }
                        conexao.Close();
                        return pontos;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }
        #endregion

        #region Medicao
        public static void CadastrarMedicao(Medicao medicao)
        {
            string procedure = "in_Medicao_InserirMedicao";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@horario", medicao.Horario);
                    comando.Parameters.AddWithValue("@potenciaTotal", medicao.PotenciaTotal);
                    comando.Parameters.AddWithValue("@potenciaAtiva", medicao.PotenciaAtiva);
                    comando.Parameters.AddWithValue("@potenciaReativa", medicao.PotenciaReativa);
                    comando.Parameters.AddWithValue("@fatorPotencia", medicao.FatorPotencia);
                    comando.Parameters.AddWithValue("@corrente", medicao.Corrente);
                    comando.Parameters.AddWithValue("@tensao", medicao.Tensao);
                    comando.Parameters.AddWithValue("@frequencia", medicao.Frequencia);
                    comando.Parameters.AddWithValue("@codigoPonto", medicao.CodigoPonto);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return;
                }
            }
        }

        public static List<Medicao> BuscarMedicao(int codigoPonto, 
            DateTime? horarioInicial = null,
            DateTime? horarioFinal = null,
            double? potenciaTotalInicial = null,
            double? potenciaTotalFinal = null,
            double? potenciaAtivaInicial = null,
            double? potenciaAtivaFinal = null,
            double? potenciaReativaInicial = null,
            double? potenciaReativaFinal = null,
            double? fatorPotenciaInicial = null,
            double? fatorPotenciaFinal = null,
            double? correnteInicial = null,
            double? correnteFinal = null,
            double? tensaoInicial = null,
            double? tensaoFinal = null,
            double? frequenciaInicial = null,
            double? frequenciaFinal = null)
        {
            string procedure = "sl_Medicao_SelectMedicaoComTodosParametros";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@codigoPonto", codigoPonto);
                    if (horarioInicial.HasValue) comando.Parameters.AddWithValue("@horarioInicial", horarioInicial.Value);
                    if (horarioFinal.HasValue) comando.Parameters.AddWithValue("@horarioFinal", horarioFinal.Value);
                    if (potenciaTotalInicial.HasValue) comando.Parameters.AddWithValue("@potenciaTotalInicial", potenciaTotalInicial.Value);
                    if (potenciaTotalFinal.HasValue) comando.Parameters.AddWithValue("@potenciaTotalFinal", potenciaTotalFinal.Value);
                    if (potenciaAtivaInicial.HasValue) comando.Parameters.AddWithValue("@potenciaAtivaInicial", potenciaAtivaInicial.Value);
                    if (potenciaAtivaFinal.HasValue) comando.Parameters.AddWithValue("@potenciaAtivaFinal", potenciaAtivaFinal.Value);
                    if (potenciaReativaInicial.HasValue) comando.Parameters.AddWithValue("@potenciaReativaInicial", potenciaReativaInicial.Value);
                    if (potenciaReativaFinal.HasValue) comando.Parameters.AddWithValue("@potenciaReativaFinal", potenciaReativaFinal.Value);
                    if (fatorPotenciaInicial.HasValue) comando.Parameters.AddWithValue("@fatorPotenciaInicial", fatorPotenciaInicial.Value);
                    if (fatorPotenciaFinal.HasValue) comando.Parameters.AddWithValue("@fatorPotenciaFinal", fatorPotenciaFinal.Value);
                    if (correnteInicial.HasValue) comando.Parameters.AddWithValue("@correnteInicial", correnteInicial.Value);
                    if (correnteFinal.HasValue) comando.Parameters.AddWithValue("@correnteFinal", correnteFinal.Value);
                    if (tensaoInicial.HasValue) comando.Parameters.AddWithValue("@tensaoInicial", tensaoInicial.Value);
                    if (tensaoFinal.HasValue) comando.Parameters.AddWithValue("@tensaoFinal", tensaoFinal.Value);
                    if (frequenciaInicial.HasValue) comando.Parameters.AddWithValue("@frequenciaInicial", frequenciaInicial.Value);
                    if (frequenciaFinal.HasValue) comando.Parameters.AddWithValue("@frequenciaFinal", frequenciaFinal.Value);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<Medicao> medicoes = new List<Medicao>();
                        while (reader.Read())
                        {
                            Medicao medicao = new Medicao();
                            medicao.Codigo = Convert.ToInt32(reader["Codigo"]);
                            medicao.Horario = Convert.ToDateTime(reader["Horario"]);
                            medicao.PotenciaTotal = Convert.ToDouble(reader["PotenciaTotal"]);
                            medicao.PotenciaAtiva = Convert.ToDouble(reader["PotenciaAtiva"]);
                            medicao.PotenciaReativa = Convert.ToDouble(reader["PotenciaReativa"]);
                            medicao.FatorPotencia = Convert.ToDouble(reader["FatorPotencia"]);
                            medicao.Corrente = Convert.ToDouble(reader["Corrente"]);
                            medicao.Tensao = Convert.ToDouble(reader["Tensao"]);
                            medicao.Frequencia = Convert.ToDouble(reader["Frequencia"]);
                            medicao.CodigoPonto = codigoPonto;
                            medicoes.Add(medicao);
                        }
                        conexao.Close();
                        return medicoes;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }
        #endregion

        #region Transaction
        private static void BeginTransaction(SqlConnection conexao)
        {
            using (SqlCommand comando = new SqlCommand("BEGIN TRANSACTION", conexao))
            {
                comando.ExecuteNonQuery();
            }
        }
        private static void CommitTransaction(SqlConnection conexao)
        {
            using (SqlCommand comando = new SqlCommand("COMMIT TRANSACTION", conexao))
            {
                comando.ExecuteNonQuery();
            }
        }
        private static void RollbackTransaction(SqlConnection conexao)
        {
            using (SqlCommand comando = new SqlCommand("ROLLBACK TRANSACTION", conexao))
            {
                comando.ExecuteNonQuery();
            }
        }
        #endregion
    }
}