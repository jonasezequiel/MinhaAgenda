using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreBusiness.Entidades;

namespace CasosDeUso.Interface;

public interface IAdicionarObservacaoUseCase
{
    Task ExecutaAsync(Observacao observacao);
}

public interface IEditarObservacaoUseCase
{
    Task ExecutaAsync(Observacao observacao);
}

public interface IVisualizarObservacoesUseCase
{
    Task<Observacao?> ExecutaAsync(Guid id);
    Task<IEnumerable<Observacao>> ExecutaListAsync(Guid contatoId);
}

public interface IApagarObservacaoUseCase
{
    Task ExecutaAsync(Observacao observacao);
}