using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD
{
    public partial class Form1 : Form
    {
        UI ui = new UI();
        public Form1()
        {
            InitializeComponent();

        }
        SqlConnection conexao = null;
        private string cx = "Data Source=DANIELOLIVEIRA\\DANIELOLIVEIRA;" +
            "Initial Catalog=Condominio; user id=sa; password=123456";
        private string sql = "";


        private SqlCommand CriaParametros()
        {
            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@CEP", SqlDbType.VarChar).Value = mskCEP.Text;
            comando.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.Add("@UF", SqlDbType.VarChar).Value = txtUF.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = mskTelefone.Text;
            
            return comando;
        }
        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool erro = false;
            try
            {
                if (Convert.ToInt32(txtId.Text) <= 0)
                {
                    errorProvider1.SetError(txtId, "O Id deve ser maior que zero!");
                    erro = true;
                }
            }
            catch
            {
                errorProvider1.SetError(txtId, "O Id deve ser um número interio!");
                erro = true;
            }

            if (txtNome.Text.Length == 0)
            {
                errorProvider1.SetError(txtNome, "O nome é obrigatório!");
                erro = true;
            }
            if (txtEndereco.Text.Length == 0)
            {
                errorProvider1.SetError(txtEndereco, "O endereço é obrigatório!");
                erro = true;
            }
            if (txtBairro.Text.Length == 0)
            {
                errorProvider1.SetError(txtBairro, "O bairro é obrigatório!");
                erro = true;
            }
            if (txtCidade.Text.Length == 0)
            {
                errorProvider1.SetError(txtCidade, "A cidade é obrigatório!");
                erro = true;
            }
            if (txtUF.Text.Length == 0)
            {
                errorProvider1.SetError(txtUF, "O UF é obrigatório!");
                erro = true;
            }
            if (txtUF.Text.Length > 2)
            {
                errorProvider1.SetError(txtUF, "O UF deve possuir apenas 2 letras!");
                erro = true;
            }

            if (mskCEP.Text.Length != 9)
            {
                errorProvider1.SetError(mskCEP, "O CEP deve possuir apenas 8 números!");
                erro = true;
            }
            if (mskCEP.Text.Length == 0)
            {
                errorProvider1.SetError(mskCEP, "O CEP é obrigatório!");

                erro = true;
            }
            if (mskTelefone.Text.Length != 14)
            {
                errorProvider1.SetError(mskTelefone, "O Telfone deve possuir apenas 11 números!");
                erro = true;
            }
            if (mskTelefone.Text.Length == 0)
            {
                errorProvider1.SetError(mskTelefone, "O Telefone é obrigatório!");
                erro = true;
            }

            if (erro == false)
            {
                sql = "insert into funcionarios (Id, Nome,Endereco,CEP," +
                    "Bairro,Cidade,Uf,Telefone) values" +
                    "(@Id, @Nome, @Endereco, @CEP, @Bairro," +
                    "@Cidade, @Uf, @Telefone) ";
                conexao = new SqlConnection(cx);
                CriaParametros();
                try
                {
                    conexao.Open();
                    CriaParametros().ExecuteNonQuery();
                    MessageBox.Show("Funcionário cadastrado com sucesso");

                    ui.LimpaCampos(panel1.Controls);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        private void tsbDeletar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo escluir este funcionário?", "Cuidado",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
                MessageBox.Show("Operação cancelada!");
            else
            {
                sql = "delete from funcionarios where Id=@Id";
                conexao = new SqlConnection(cx);
                SqlCommand comando = new SqlCommand(sql, conexao);
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = tstIdBuscar.Text;
                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionário excluído com sucesso");
                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.Message);
                }
                finally
                {
                    conexao.Close();
                }
                tsbNovo.Enabled = true; tsbSalvar.Enabled = false;
                tsbAlterar.Enabled = false; tsbCancelar.Enabled = false;
                tsbDeletar.Enabled = false; tstIdBuscar.Enabled = true;
                tsbPesquisar.Enabled = true; txtId.Enabled = false;
                txtNome.Enabled = false; txtEndereco.Enabled = false;
                txtBairro.Enabled = false; txtCidade.Enabled = false;
                txtUF.Enabled = false; mskCEP.Enabled = false;
                mskTelefone.Enabled = false;
            }
        }

        private void tsbAlterar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool erro = false;
            try
            {
                if (Convert.ToInt32(txtId.Text) <= 0)
                {
                    errorProvider1.SetError(txtId, "O Id deve ser maior que zero!");
                    erro = true;
                }
            }
            catch
            {
                errorProvider1.SetError(txtId, "O Id deve ser um número inteiro!");
                erro = true;
            }

            if (txtNome.Text.Length == 0)
            {
                errorProvider1.SetError(txtNome, "O nome é obrigatório!");
                erro = true;
            }
            if (txtEndereco.Text.Length == 0)
            {
                errorProvider1.SetError(txtEndereco, "O endereço é obrigatório!");
                erro = true;
            }
            if (txtBairro.Text.Length == 0)
            {
                errorProvider1.SetError(txtBairro, "O bairro é obrigatório!");
                erro = true;
            }
            if (txtCidade.Text.Length == 0)
            {
                errorProvider1.SetError(txtCidade, "A cidade é obrigatório!");
                erro = true;
            }
            if (txtUF.Text.Length == 0)
            {
                errorProvider1.SetError(txtUF, "O UF é obrigatório!");
                erro = true;
            }
            if (txtUF.Text.Length > 2)
            {
                errorProvider1.SetError(txtUF, "O UF deve possuir apenas 2 letras!");
                erro = true;
            }

            if (mskCEP.Text.Length != 9)
            {
                errorProvider1.SetError(mskCEP, "O CEP deve possuir apenas 8 números!");
                erro = true;
            }
            if (mskCEP.Text.Length == 0)
            {
                errorProvider1.SetError(mskCEP, "O CEP é obrigatório!");

                erro = true;
            }
            if (mskTelefone.Text.Length != 14)
            {
                errorProvider1.SetError(mskTelefone, "O Telfone deve possuir apenas 11 números!");
                erro = true;
            }
            if (mskTelefone.Text.Length == 0)
            {
                errorProvider1.SetError(mskTelefone, "O Telefone é obrigatório!");
                erro = true;
            }

            if (erro == false)
            {
                sql = "update funcionarios set Nome=@Nome, Endereco=@Endereco, " +
                    "CEP=@CEP, Bairro=@Bairro, Cidade=@Cidade, Uf=@Uf, Telefone=@Telefone " +
                    "where Id= @Id";
                conexao = new SqlConnection(cx);


                CriaParametros().Parameters.Add("@IdBuscar", SqlDbType.Int).Value = tstIdBuscar.Text;
                try
                {
                    conexao.Open();
                    CriaParametros().ExecuteNonQuery();
                    MessageBox.Show("Funcionário alterado com sucesso");
                    ui.LimpaCampos(panel1.Controls);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
                tsbNovo.Enabled = true; tsbSalvar.Enabled = false;
                tsbAlterar.Enabled = false; tsbCancelar.Enabled = false;
                tsbDeletar.Enabled = false; tstIdBuscar.Enabled = true;
                tsbPesquisar.Enabled = true; txtId.Enabled = false;
                txtNome.Enabled = false; txtEndereco.Enabled = false;
                txtBairro.Enabled = false; txtCidade.Enabled = false;
                txtUF.Enabled = false; mskCEP.Enabled = false;
                mskTelefone.Enabled = false;
            }
        }

        private void tsbPesquisar_Click(object sender, EventArgs e)
        {
            

            sql = "select * from funcionarios where id=@Id order by 1";
            conexao = new SqlConnection(cx);
            SqlCommand comando = new SqlCommand(sql, conexao);

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = tstIdBuscar.Text;
            try
            {
                if(string.IsNullOrEmpty(tstIdBuscar.Text))
                {
                    throw new Exception("Digite um Id para buscar");
                }
                conexao.Open();
                SqlDataReader reader = comando.ExecuteReader();

                if(reader.HasRows==false)
                {
                    throw new Exception("Id não cadastrado");
                }
                reader.Read();
                txtId.Text = Convert.ToString(reader["Id"]);
                txtNome.Text= Convert.ToString(reader["Nome"]);
                txtEndereco.Text = Convert.ToString(reader["Endereco"]);

                mskCEP.Text = Convert.ToString(reader["CEP"]);
                txtBairro.Text = Convert.ToString(reader["Bairro"]);
                txtCidade.Text = Convert.ToString(reader["Cidade"]);

                txtUF.Text = Convert.ToString(reader["UF"]);
                mskTelefone.Text = Convert.ToString(reader["Telefone"]);
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            finally
            {
                conexao.Close();
            }
            tsbNovo.Enabled = false; tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = true; tsbCancelar.Enabled = true;
            tsbDeletar.Enabled = true; tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true; txtId.Enabled = true;
            txtNome.Enabled = true; txtEndereco.Enabled = true;
            txtBairro.Enabled = true; txtCidade.Enabled = true;
            txtUF.Enabled = true; mskCEP.Enabled = true;
            mskTelefone.Enabled = true; txtNome.Focus();
        }

        private void lblBairro_Click(object sender, EventArgs e)
        {

        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = false; tsbSalvar.Enabled = true;
            tsbAlterar.Enabled = false; tsbCancelar.Enabled = true;
            tsbDeletar.Enabled = false; tstIdBuscar.Enabled = false;
            tsbPesquisar.Enabled = false; txtId.Enabled = true; 
            txtNome.Enabled = true; txtEndereco.Enabled = true;
            txtBairro.Enabled = true; txtCidade.Enabled = true;
            txtUF.Enabled = true; mskCEP.Enabled = true;
            mskTelefone.Enabled = true;

            ui.LimpaCampos(panel1.Controls);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true; tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false; tsbCancelar.Enabled = false;
            tsbDeletar.Enabled = false; tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true; txtId.Enabled = false;
            txtNome.Enabled = false; txtEndereco.Enabled = false;
            txtBairro.Enabled = false; txtCidade.Enabled = false;
            txtUF.Enabled = false; mskCEP.Enabled = false;
            mskTelefone.Enabled = false;
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true; tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false; tsbCancelar.Enabled = false;
            tsbDeletar.Enabled = false; tstIdBuscar.Enabled = true;
            tsbPesquisar.Enabled = true; txtId.Enabled = false;
            txtNome.Enabled = false; txtEndereco.Enabled = false;
            txtBairro.Enabled = false; txtCidade.Enabled = false;
            txtUF.Enabled = false; mskCEP.Enabled = false;
            mskTelefone.Enabled = false;
        }

        private void tsbListar_Click(object sender, EventArgs e)
        {
            frmLista formLista = new frmLista();
            formLista.ShowDialog();
        }
    }
}
