﻿using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Recursos;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class IniciarServico : Evento
    {
        public IniciarServico()
        {
            Gerenciador<IniciarServico>.nascimento(this);
        }

        protected override void Estrategia()
        {
            if (!GerenciadorDeRecursos<Funcionario>.VerificarDisponibilidade(MotorPosto.FuncionariosNecessarios))
                return;

            var carro = MotorPosto.FilaAtendimento.Remover();

            var funcionariosAlocados = GerenciadorDeRecursos<Funcionario>.Alocar(MotorPosto.FuncionariosNecessarios);

            var evtFinalizar = new FinalizarServico(carro, funcionariosAlocados);
            Agendador.AgendarEm(evtFinalizar, MotorPosto.TempoParaFinalizar);
        }

        protected override void Destruir()
        {
            Gerenciador<IniciarServico>.morte(this);
        }
    }
}
