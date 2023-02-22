/*
 * Created by SharpDevelop.
 * User: Hugo
 * Date: 21/10/2022
 * Time: 08:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using MySql.Data.MySqlClient;

namespace CRUD
{
	public class Campos
	{
		public int id;
		public string nome;
		public string ender;
		public decimal sal;
	}
	public class DAO
	{
		public DAO()
		{
		}
		public Campos campos = new Campos();
		public MySqlConnection minhaConexao;
		public string usuarioBD = "root";
		public string senhaBd = "root";
		public string servidor="localhost";
		string bancoDados;
		string tabela;
		
	public void conecte (string BancoDados, string Tabela)
	{
		bancoDados = BancoDados;
		tabela = Tabela;
		minhaConexao = new MySqlConnection("server="+servidor+"; database="+bancoDados+"; uid="+usuarioBD+"; password="+senhaBd);
		
		
	}
	
	void Abrir()
	{
		minhaConexao.Open();
	}
	
	void Fechar()
	{
		minhaConexao.Close();
	}
	
	public void PreencherTabela (System.Windows.Forms.DataGridView dataGridView)
	{
		Abrir();
		
		MySqlDataAdapter meuAdapter = new MySqlDataAdapter ("select * from  " + tabela, minhaConexao);
		System.Data.DataSet dataSet = new System.Data.DataSet();
		dataSet.Clear();
		meuAdapter.Fill(dataSet, tabela);
		dataGridView.DataSource = dataSet;
		dataGridView.DataMember = tabela;
		
		Fechar();
		
	}
	
	public void PreencherTabela (System.Windows.Forms.DataGridView dataGridView, string busca)
	{
		Abrir();
		MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela + " where nome like "+" '"+busca+"%' ; " , minhaConexao);
		System.Data.DataSet dataSet = new System.Data.DataSet();
		dataSet.Clear();
		meuAdapter.Fill(dataSet, tabela);
		dataGridView.DataSource = dataSet;
		dataGridView.DataMember = tabela;
		Fechar();
	}
	
	public void Consulta(string campoNome)
	{
		Abrir();
		MySqlCommand comando = new MySqlCommand("select * from " + tabela + " where nome = '" + campoNome + "' " , minhaConexao);
		MySqlDataReader dtReader = comando.ExecuteReader();
		if (dtReader.Read())
		{
			campos.id = int.Parse(dtReader["id"].ToString());
			campos.nome = dtReader["nome"].ToString();
			campos.ender = dtReader["endereco"].ToString();
			campos.sal = decimal.Parse(dtReader ["salario"].ToString());
		}
		
		Fechar();
	}
	
	public void Consulta (int id)
	{
		Abrir();
		
		MySqlCommand comando = new MySqlCommand("select * from " + tabela + " where id = ' " + id + " ' " ,minhaConexao);
		MySqlDataReader dtReader = comando.ExecuteReader();
		if (dtReader.Read())
		{
			campos.id = int.Parse(dtReader["id"].ToString());
			campos.nome = dtReader["nome"].ToString();
			campos.ender = dtReader["endereco"].ToString();
			campos.sal = decimal.Parse(dtReader["salario"].ToString());			                        
		}
		Fechar();
	}
	public void Insere (string campoNome, string campoEnder, decimal campoSalario)
	{
		Abrir();
		
		MySqlCommand comando = new MySqlCommand ("insert into " + tabela + "(nome, endereco, salario)"+
		                                         "values (@nome, @endereco, @salario)", minhaConexao);
		comando.Parameters.AddWithValue("@nome", 	campoNome);
		comando.Parameters.AddWithValue("@endereco", 	campoEnder);
		comando.Parameters.AddWithValue("@salario", 	campoSalario);
		comando.ExecuteNonQuery();
		
		Fechar();
	}
	public void Atualiza(string campoNome, string campoEnder, decimal campoSalario, int id)
	{
		Abrir();
		MySqlCommand comando = new MySqlCommand("update " + tabela + " set nome=@nome, endereco=@endereco , " + "salario=@salario where id=@id", minhaConexao);
		comando.Parameters.AddWithValue("@id", id);
		comando.Parameters.AddWithValue("@nome", campoNome);
		comando.Parameters.AddWithValue("@endereco", campoEnder);
		comando.Parameters.AddWithValue("@salario", campoSalario);
		comando.ExecuteNonQuery();
		
		Fechar();
	}
	public void Deleta(int id)
	{
		Abrir();
		
		MySqlCommand comando = new MySqlCommand("delete from  " + tabela + " where id = @id", minhaConexao);
		comando.Parameters.AddWithValue("@id", id);
		comando.ExecuteNonQuery();
		
		Fechar();
	}
	
	public int NumRegistro()
	{
		Abrir();
		
		//Parâmetro MAX retorna o número do último valor de id
		MySqlCommand comando = new MySqlCommand("select max(id) from "+tabela, minhaConexao);
		
		//ExecuteScalar retorna um dado do tipoobject. 
		string n = comando.ExecuteScalar().ToString();
		int num = int.Parse(n) +  1;
		Fechar();
		
		return num;
	}
	}
	


}
