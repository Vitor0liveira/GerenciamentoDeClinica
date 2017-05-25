﻿using Biblioteca.medico;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Biblioteca.utils
{
    public class ClinicaXMLUtils
    {
        private const string ROOT = "data";
        private const string ROOT_XPATH = "/" + ROOT;
        private const string CADASTRAR_MEDICO = "cadastrarmedico";
        private const string CADASTRAR_MEDICO_XPATH = "/" + CADASTRAR_MEDICO;
        private const string ID_MEDICO_XPATH = "/ID_Medico";
        private const string CRM_XPATH = "/CRM";
        private const string NOME_XPATH = "/Nome";
        private const string RG_XPATH = "/RG";
        private const string CPF_XPATH = "/CPF";
        private const string CONTATO_XPATH = "/Contato";
        private const string EMAIL_XPATH = "/Email";
        private const string ESTADO_CIVIL_XPATH = "/Estado_Civil";
        private const string SAVELOCATION = "data.xml";
        private static XmlDocument document;

        private ClinicaXMLUtils()
        {

        }

        public static void Create()
        {
            document = new XmlDocument();
            CheckExists();
            document.Load(SAVELOCATION);
        }

        private static void CheckExists()
        {
            if (!File.Exists(SAVELOCATION))
            {
                XmlNode rootNode = document.CreateElement(ROOT);
                document.AppendChild(rootNode);
                document.Save(SAVELOCATION);
            }
        }

        private static void CheckXmlLoad()
        {
            if (document == null)
                throw new Exception("XML não carregado.");
        }

        public static void SetCadastrarMedico(Medico medico)
        {
            CheckXmlLoad();

            XmlNode rootNode = document.SelectSingleNode(ROOT);

            XmlNode cadastrarMedico = rootNode.SelectSingleNode(CADASTRAR_MEDICO)
                ?? document.CreateElement(CADASTRAR_MEDICO);
            cadastrarMedico.InnerXml = medico.ToXml();

            if (rootNode.SelectSingleNode(CADASTRAR_MEDICO) == null)
                rootNode.AppendChild(cadastrarMedico);

            document.Save(SAVELOCATION);
        }

        public static void GetCadastrarMedico()
        {
            CheckXmlLoad();

            Medico medico = new Medico();
            medico.ID_Medico = Convert.ToInt32(document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + ID_MEDICO_XPATH).Value);
            medico.CRM = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + CRM_XPATH).Value;
            medico.Nome = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + NOME_XPATH).Value;
            medico.RG = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + RG_XPATH).Value;
            medico.CPF = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + CPF_XPATH).Value;
            medico.Contato = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + CONTATO_XPATH).Value;
            medico.Email = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + EMAIL_XPATH).Value;
            medico.Estado_Civil = document.SelectSingleNode(ROOT_XPATH + CADASTRAR_MEDICO_XPATH + ESTADO_CIVIL_XPATH).Value;
        }

        private static string GetXPath(string screen, string element)
        {
            return ROOT_XPATH + screen + element;
        }
    }
}