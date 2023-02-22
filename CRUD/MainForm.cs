/*
 * Created by SharpDevelop.
 * User: Hugo
 * Date: 21/10/2022
 * Time: 08:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CRUD
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			// Escalação do time do flamengo para a libertadores 2022 final contra o athletico
			//Santos, Rodinei, Filipe Luis, Leo Pereira e Davi luiz, e Matheuzinho
			//João Gomes, Everton ribeiro e Arrascaeta
			//Pedro e Gabigol.
			
		}
		DAO dao = new DAO();
		
		void LimparCampos()
		{
			textBox1.Clear();
			textBox2.Clear();
			textBox3.Clear();
			label2.Text = dao.NumRegistro().ToString();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			dao.conecte("bdteste", "tbl_teste");
			dao.PreencherTabela(dataGridView1);
			LimparCampos();
		}
		void Button1Click(object sender, EventArgs e)
		{
			if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text =="" )
			{	
				MessageBox.Show("Campos em Branco");
			}
			else
			{
				dao.Insere(textBox1.Text, textBox2.Text, decimal.Parse(textBox3.Text));
				MessageBox.Show("Registro salvo com sucesso");
				LimparCampos();
				dao.PreencherTabela(dataGridView1);
				button1.Enabled = false;
		
			}
		}
		
		void ExibirDados()
		{
			label2.Text = dao.campos.id.ToString();
			textBox1.Text = dao.campos.nome;
			textBox2.Text = dao.campos.ender;
			textBox3.Text = dao.campos.sal.ToString();
			button3.Enabled = true;
			button4.Enabled = true;
			button1.Enabled = false;
			
		}
		void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int numLinha = e.RowIndex;
			if (numLinha >=0)
			{
				int id = int.Parse(dataGridView1.Rows[numLinha].Cells[0].Value.ToString());
				label2.Visible = true;
				dao.Consulta(id);
				ExibirDados();
			
			}
		}
		void Button5Click(object sender, EventArgs e)
		{
				if (textBox1.Text != "")
			{
//					dao.Consulta(textBox1.Text);
//					ExibirDados();
//			
					dao.PreencherTabela(dataGridView1, textBox1.Text);
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			LimparCampos();
			button1.Enabled = true;
			button3.Enabled = false;
			button4.Enabled = false;
			dao.PreencherTabela(dataGridView1);
		}
		void Button3Click(object sender, EventArgs e)
		{
			dao.Atualiza(textBox1.Text, textBox2.Text, decimal.Parse(textBox3.Text),
			             int.Parse(label2.Text));
			dao.PreencherTabela(dataGridView1);
			MessageBox.Show("Registro alteraod com sucesso!", "AVISO");
		}
		void Button4Click(object sender, EventArgs e)
		{
			if(MessageBox.Show("Deseja Mesmo Excluir?", "AVISO DE EXCLUSÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				dao.Deleta(int.Parse(label2.Text));
				MessageBox.Show("Deletado Com Sucesso");
				dao.PreencherTabela(dataGridView1);
				LimparCampos();
				button3.Enabled = false;
				button4.Enabled = false;
			}
			else
			{
				MessageBox.Show("Registro Mantido");
			}
		}
		
	}
}
