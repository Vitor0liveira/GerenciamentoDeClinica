﻿using Biblioteca.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.especialidade
{
    public class NegocioEspecialidade : IEspecialidade
    {
        public void Cadastrar(Especialidade especialidade)
        {
            ClinicaUtils.ValidarCodigo(especialidade.ID_Especialidade);

            if (VerificaExistencia(especialidade) != false)
            {
                throw new Exception("Código de especialidade já cadastrado");
            }

            ClinicaUtils.ValidarVazio(especialidade.Descricao.Trim(), ClinicaUtils.ERRO_ESPECIALIDADE);
            ClinicaUtils.ValidarExceder(especialidade.Descricao.Trim(), 20, ClinicaUtils.ERRO_ESPECIALIDADE);

            new EspecialidadeBD().Cadastrar(especialidade);
        }


        public void Atualizar(Especialidade especialidade)
        {
            ClinicaUtils.ValidarCodigo(especialidade.ID_Especialidade);

            ClinicaUtils.ValidarVazio(especialidade.Descricao.Trim(), ClinicaUtils.ERRO_ESPECIALIDADE);
            ClinicaUtils.ValidarExceder(especialidade.Descricao.Trim(), 20, ClinicaUtils.ERRO_ESPECIALIDADE);

            new EspecialidadeBD().Atualizar(especialidade);
        }


        public void Remover(Especialidade especialidade)
        {
            ClinicaUtils.ValidarCodigo(especialidade.ID_Especialidade);

            new EspecialidadeBD().Remover(especialidade);
        }

        public List<Especialidade> Listar(Especialidade filtro)
        {
            return new EspecialidadeBD().Listar(filtro);
        }

        public bool VerificaExistencia(Especialidade especialidade)
        {
            ClinicaUtils.ValidarCodigo(especialidade.ID_Especialidade);

            return new EspecialidadeBD().VerificaExistencia(especialidade);
        }

    }
}