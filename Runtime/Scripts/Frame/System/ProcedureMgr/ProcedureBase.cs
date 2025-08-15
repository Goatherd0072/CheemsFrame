using System;

namespace Cheems
{
    public interface IProcedure
    {
    }

    [Serializable]
    public class ProcedureBase : StateBase, IProcedure
    {
    }
}