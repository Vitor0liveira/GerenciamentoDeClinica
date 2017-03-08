﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GerenciamentoDeClinica.classesBasicas;

namespace GerenciamentoDeClinica.Telas
{
    public partial class TelaTesteConexaoSql : Form
    {
        public TelaTesteConexaoSql()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConexaoSql con = new ConexaoSql();
                con.abrirConexao();
                MessageBox.Show("Conexão efetuada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
