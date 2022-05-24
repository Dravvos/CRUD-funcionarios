using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class frmLista : Form
    {
        public frmLista()
        {
            InitializeComponent();
            lvLista.View = View.Details;
            lvLista.GridLines = true;
            lvLista.Columns.Add("Id", 40, HorizontalAlignment.Left);
            lvLista.Columns.Add("Nome", 160, HorizontalAlignment.Left);
            lvLista.Columns.Add("Endereco", 200, HorizontalAlignment.Left);
            lvLista.Columns.Add("CEP", 63, HorizontalAlignment.Left);
            lvLista.Columns.Add("Bairro", 150, HorizontalAlignment.Left);
            lvLista.Columns.Add("Cidade", 180, HorizontalAlignment.Left);
            lvLista.Columns.Add("UF", 30, HorizontalAlignment.Left);
            lvLista.Columns.Add("Telefone", 90, HorizontalAlignment.Left);
        }
        SqlConnection conexao = null;
        private string cx = "Data Source=DANIELOLIVEIRA\\DANIELOLIVEIRA;" +
            "Initial Catalog=Condominio; user id=sa; password=123456";
        private string sql = "";
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLista_Load(object sender, EventArgs e)
        {
            lvLista.Items.Clear();
            sql = "select * from funcionarios";
            conexao = new SqlConnection(cx);
            SqlCommand comando = new SqlCommand(sql, conexao);
            try
            {
                conexao.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetInt32(0).ToString(),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                    };
                    var linha_listview = new ListViewItem(row);
                    lvLista.Items.Add(linha_listview);
                }

                lvLista.Items.Add(reader.ToString());
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
