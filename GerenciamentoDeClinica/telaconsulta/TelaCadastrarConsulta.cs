﻿using GerenciamentoDeClinica.localhost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciamentoDeClinica.telaconsulta
{
    public partial class TelaCadastrarConsulta : Form
    {
        private List<Medico> medicos;
        private List<Paciente> pacientes;
        private List<Secretaria> secretarias;
        private int? selectedRowPaciente, selectedRowMedico, selectedRowSecretaria;

        public TelaCadastrarConsulta()
        {
            InitializeComponent();
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta consulta = new Consulta
                {
                    Horario = dateTimePickerDia.Value,
                    Duracao = Convert.ToInt32(txtDuracao.Text),
                    Medico = new Medico {ID_Medico = Convert.ToInt32(txtIDMedico.Text)},
                    Paciente = new Paciente {ID_Paciente = Convert.ToInt32(txtIDPaciente.Text)},
                    Secretaria = new Secretaria {ID_Secretaria = Convert.ToInt32(txtIDSecretaria.Text)},
                    Estado = ((BindingList<Estado>) comboEstado.DataSource).ElementAt(comboEstado.SelectedIndex)
                };


                ClinicaService service = new ClinicaService();
                service.CadastrarConsulta(consulta);
                MessageBox.Show("Consulta marcada com sucesso!");

                txtIDMedico.Clear();
                txtNomeMedico.Clear();
                txtIDPaciente.Clear();
                txtNomePaciente.Clear();
                txtIDSecretaria.Clear();
                txtNomeSecretaria.Clear();
                txtDuracao.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnPesquisarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                listViewPacientes.Items.Clear();
                ClinicaService service = new ClinicaService();
                pacientes = new List<Paciente>(service.ListarPaciente(new Paciente
                {
                    Nome = txtNomePaciente.Text
                }));
                foreach (Paciente paciente in pacientes)
                {
                    ListViewItem linha = listViewPacientes.Items.Add(paciente.Nome);
                    linha.SubItems.Add(paciente.CPF);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btnPesquisarMedico_Click(object sender, EventArgs e)
        {
            try
            {
                listViewMedicos.Items.Clear();
                ClinicaService service = new ClinicaService();
                medicos = new List<Medico>(service.ListarMedico(new Medico
                {
                    Nome = txtNomeMedico.Text
                }));
                foreach (Medico medico in medicos)
                {
                    ListViewItem linha = listViewMedicos.Items.Add(medico.Nome);
                    linha.SubItems.Add(medico.CRM);
                    linha.SubItems.Add(medico.Especialidade.Descricao);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void listViewMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMedicos.SelectedItems.Count > 0)
            {
                selectedRowMedico = listViewMedicos.SelectedItems.Cast<ListViewItem>().ToList().ElementAt(0).Index;
                txtIDMedico.Text = medicos[selectedRowMedico.Value].ID_Medico.ToString();
            }
        }

        private void TelaCadastrarConsulta_Load(object sender, EventArgs e)
        {
            ClinicaService service = new ClinicaService();
            comboEstado.DataSource = new BindingList<Estado>(service.ListarEstado(new Estado()));
            comboEstado.DisplayMember = "Descricao";
        }

        private void btnPesquisarSecretaria_Click(object sender, EventArgs e)
        {
            try
            {
                listViewSecretarias.Items.Clear();
                ClinicaService service = new ClinicaService();
                secretarias = new List<Secretaria>(service.ListarSecretaria(new Secretaria
                {
                    Nome = txtNomeSecretaria.Text
                }));
                foreach (Secretaria secretaria in secretarias)
                {
                    ListViewItem linha = listViewSecretarias.Items.Add(secretaria.Nome);
                    linha.SubItems.Add(secretaria.CPF);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void listViewSecretarias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSecretarias.SelectedItems.Count > 0)
            {
                selectedRowSecretaria = listViewSecretarias.SelectedItems.Cast<ListViewItem>().ToList().ElementAt(0).Index;
                txtIDSecretaria.Text = secretarias[selectedRowSecretaria.Value].ID_Secretaria.ToString();
            }
        }

        private void listViewPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPacientes.SelectedItems.Count > 0)
            {
                selectedRowPaciente = listViewPacientes.SelectedItems.Cast<ListViewItem>().ToList().ElementAt(0).Index;
                txtIDPaciente.Text = pacientes[selectedRowPaciente.Value].ID_Paciente.ToString();
            }
        }
    }
}