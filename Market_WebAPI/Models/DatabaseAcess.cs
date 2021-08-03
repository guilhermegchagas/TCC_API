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
        private static string connectionString = @"Data Source=DESKTOP-3021GD2\MSSQLSERVER01;Initial Catalog=MarketAPI;Integrated Security=True";
        //private static string connectionString = @"Data Source=SQL5053.site4now.net;Initial Catalog=DB_A620FF_recicladoDBv2;User Id=DB_A620FF_recicladoDBv2_admin;Password=senha12345;";

        public static void CadastrarCliente(Cliente cliente)
        {
            string procedure = "in_Cliente_InserirClienteArg";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@cpf", cliente.CPF);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@senha", cliente.Senha);
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return;
                }
            }
        }

        public static bool ValidarCPFCliente(string cpf)
        {
            bool ret = false;

            string procedure = "sl_Cliente_SelectClientePorCPF";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@cpf", cpf);
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
        public static bool ValidarEmailCliente(string email)
        {
            bool ret = false;

            string procedure = "sl_Cliente_SelectClientePorEmail";
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

        public static Cliente BuscarClientePorCPF(string cpf)
        {
            string procedure = "sl_Cliente_SelectClientePorCPF";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Cliente cliente = new Cliente();
                        cliente.CPF = reader["CPF"].ToString();
                        cliente.Nome = reader["Nome"].ToString();
                        cliente.Sobrenome = reader["Sobrenome"].ToString();
                        cliente.Email = reader["Email"].ToString();
                        cliente.Senha = reader["Senha"].ToString();
                        cliente.Creditos = Convert.ToDouble(reader["Creditos"]);
                        conexao.Close();
                        return cliente;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }

        public static bool CadastrarReciclagem(string cpf, List<ItemReciclagem> listaItems)
        {
            string procedure;
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                BeginTransaction(conexao);
                try
                {
                    procedure = "in_Reciclagem_CadastrarReciclagem";
                    using (SqlCommand comando = new SqlCommand(procedure, conexao))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@cpf", cpf);
                        SqlParameter codigoRetornado = comando.Parameters.Add("@CodigoReciclagem", SqlDbType.Int);
                        codigoRetornado.Direction = ParameterDirection.Output;
                        comando.ExecuteNonQuery();
                        int codigoReciclagem = Convert.ToInt32(codigoRetornado.Value);

                        procedure = "in_ItemReciclagem_CadastrarItemReciclagem";
                        foreach (ItemReciclagem item in listaItems)
                        {
                            using (SqlCommand comandoItem = new SqlCommand(procedure, conexao))
                            {
                                comandoItem.CommandType = CommandType.StoredProcedure;
                                comandoItem.Parameters.AddWithValue("@cpf", cpf);
                                comandoItem.Parameters.AddWithValue("@codigoQR", item.codigo_qr);
                                comandoItem.Parameters.AddWithValue("@valorItem", item.vl_item_reciclagem);
                                comandoItem.Parameters.AddWithValue("@codigoReciclagem", codigoReciclagem);
                                comandoItem.ExecuteNonQuery();
                            }
                        }
                    }
                    CommitTransaction(conexao);
                    conexao.Close();
                    return true;
                }
                catch
                {
                    RollbackTransaction(conexao);
                    conexao.Close();
                    return false;
                }
            }
        }

        public static ItemReciclagem CarregarItemReciclagem(string qr_code)
        {
            string procedure = "sl_CodigoQR_Produto_CarregarItemReciclagem";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@codigo_qr", qr_code);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ItemReciclagem reciclavel = new ItemReciclagem();
                        reciclavel.codigo_qr = qr_code;
                        reciclavel.nome_produto = reader["NomeProduto"].ToString();
                        reciclavel.codigo_barra_produto = reader["CodigoBarra"].ToString();
                        reciclavel.vl_item_reciclagem = Convert.ToDouble(reader["ValorReciclagem"].ToString());
                        conexao.Close();
                        return reciclavel;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }

        public static List<Cupom> BuscarCuponsDisponiveis()
        {
            string procedure = "sl_Cupom_CarregarCuponsDisponiveis";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<Cupom> cuponsDisponiveis = new List<Cupom>();
                        while(reader.Read())
                        {
                            Cupom cupom = new Cupom();
                            cupom.Codigo = Convert.ToInt32(reader["Codigo"]);
                            cupom.Nome = reader["Nome"].ToString();
                            cupom.Descricao = reader["Descricao"].ToString();
                            cupom.Vencimento = reader["Vencimento"].ToString();
                            cupom.CustoCreditos = Convert.ToDouble(reader["CustoCreditos"]);
                            cupom.CorHex = reader["CorHex"].ToString();
                            cuponsDisponiveis.Add(cupom);
                        }
                        conexao.Close();
                        return cuponsDisponiveis;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }

        public static List<CupomResgatado> BuscarMeusCupons(string cpf)
        {
            string procedure = "sl_Cupom_CarregarMeusCupons";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(procedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    conexao.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<CupomResgatado> meusCupons = new List<CupomResgatado>();
                        while (reader.Read())
                        {
                            CupomResgatado cupom = new CupomResgatado();
                            cupom.Codigo = reader["Codigo"].ToString();
                            cupom.Nome = reader["Nome"].ToString();
                            cupom.Descricao = reader["Descricao"].ToString();
                            cupom.Vencimento = reader["Vencimento"].ToString();
                            cupom.CorHex = reader["CorHex"].ToString();
                            meusCupons.Add(cupom);
                        }
                        conexao.Close();
                        return meusCupons;
                    }
                    else
                    {
                        conexao.Close();
                        return null;
                    }
                }
            }
        }

        public static bool ResgatarCupom(string cpf, string codigo_cupom)
        {
            string procedure;
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                try
                {
                    procedure = "in_CupomResgatado_ResgatarCupom";
                    using (SqlCommand comando = new SqlCommand(procedure, conexao))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@cpf", cpf);
                        comando.Parameters.AddWithValue("@codigo_cupom", codigo_cupom);
                        comando.ExecuteNonQuery();
                    }
                    conexao.Close();
                    return true;
                }
                catch
                {
                    conexao.Close();
                    return false;
                }
            }
        }

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